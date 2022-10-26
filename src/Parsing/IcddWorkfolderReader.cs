using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using IIB.ICDD.Handling;
using IIB.ICDD.Logging;
using IIB.ICDD.Model;
using IIB.ICDD.Validation;
using Microsoft.VisualBasic;

namespace IIB.ICDD.Parsing
{
    public class IcddWorkfolderReader
    {
        protected string Guid;
        protected string Workfolder;
        protected string Filename;
        protected IcddContainerReaderOptions Options;
        protected IcddValidator Validator;

        public IcddWorkfolderReader(string workfolder, string guid, string filename)
        {
            if (Directory.Exists(workfolder) && Directory.Exists(Path.Combine(workfolder, guid)))
            {
                Workfolder = workfolder;
                Guid = guid;
                Filename = filename;
            }
            else
            {
                throw new IcddException("File not found.", new FileNotFoundException());
            }
        }

        public InformationContainer Read(bool useCache = true)
        {
            if (useCache && WorkfolderCache.TryGet(Guid, out ContainerCache cache))
                return cache.Container;

            InformationContainer container = new(Workfolder, Guid, Filename);
            Validator = new IcddValidator(container);

            if (!IsValid()) throw new IcddException("Container is invalid.");

            WorkfolderCache.Put(Validator.GetValidContainer(), Guid);
            return Validator.GetValidContainer();


        }

        public List<IcddValidationResult> GetValidationResults()
        {
            return Validator?.GetResults();
        }

        public bool IsValid()
        {
            return Validator != null && Validator.IsValid();
        }

    }

    public static class WorkfolderCache
    {
        public static ConcurrentDictionary<string, ContainerCache> Caches = new();

        public static void Put(InformationContainer container, string guid)
        {
            try
            {
                if (TryGet(guid, out ContainerCache _)) return;
                Caches.TryAdd(guid, new ContainerCache(container));
                Logger.Log("Cache angelegt für " + guid, Logger.MsgType.Info, "WorkfolderCache.Put");
            }
            catch (Exception e)
            {
                Logger.Log("Cache nicht angelegt für " + guid + ". Fehler: " + e, Logger.MsgType.Error, "WorkfolderCache.Put");
            }


        }

        public static bool TryGet(string guid, out ContainerCache informationContainer)
        {
            if (Caches.TryGetValue(guid, out ContainerCache cachedContainer) && cachedContainer.IsValid())
            {
                try
                {
                    cachedContainer.Update();
                    informationContainer = cachedContainer;
                    Logger.Log("Cache verlängert für " + guid + " bis " + DateTime.Now.AddMinutes(10).ToShortTimeString(), Logger.MsgType.Info, "WorkfolderCache.Put");
                    return informationContainer != null;

                }
                catch (Exception e)
                {

                    Logger.Log("Cache nicht angelegt für " + guid + ". Fehler: " + e, Logger.MsgType.Error, "WorkfolderCache.Put");
                }

            }

            Caches.Remove(guid, out ContainerCache _);
            informationContainer = null;
            return false;
        }
    }

    public class ContainerCache
    {
        public InformationContainer Container
        {
            get;
            set;
        }

        public DateTime TimeStamp
        {
            get;
            set;
        }

        public ContainerCache(InformationContainer container)
        {
            Container = container;
            TimeStamp = DateTime.Now;
        }

        public void Update()
        {
            TimeStamp = DateTime.Now;
        }

        public bool IsValid()
        {
            return DateTime.Now - TimeStamp < TimeSpan.FromMinutes(10);
        }
    }
}