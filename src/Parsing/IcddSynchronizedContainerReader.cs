using System.Collections.Generic;
using System.Threading;
using IIB.ICDD.Handling;
using IIB.ICDD.Model;

namespace IIB.ICDD.Parsing
{
    public class IcddSynchronizedContainerReader : IcddContainerReader
    {
        private ReaderWriterLockSlim _readWriteLock = new();
        private static List<string> Locked = new();

        public IcddSynchronizedContainerReader(string filepath, IcddContainerReaderOptions options = null) : 
            base(filepath, options)
        {

            if (Locked.Contains(filepath)){
                throw new IcddException("Container is locked by another reader.");
            }
            else
            {
                Locked.Add(filepath);
            }
        }

        public new InformationContainer Read()
        {
            _readWriteLock.TryEnterReadLock(1000);
            return base.Read();

        }
        public void Close()
        {
            _readWriteLock.ExitReadLock();
            Locked.Remove(File);
        }
    }
}
