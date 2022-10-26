using System.Collections.Generic;
using System.IO;
using System.Threading;
using IIB.ICDD.Handling;
using IIB.ICDD.Model;

namespace IIB.ICDD.Parsing
{
    public class IcddSynchronizedWorkfolderReader : IcddWorkfolderReader
    {
        private ReaderWriterLockSlim _readWriteLock = new();
        private static List<string> Locked = new();

        public IcddSynchronizedWorkfolderReader(string workfolder, string guid, string filename) : base(workfolder, guid, filename)
        {
            var filepath = Path.Combine(workfolder, guid);
            if (Locked.Contains(filepath))
            {
                throw new IcddException("Container is locked by another reader.");
            }
            Locked.Add(filepath);
        }
        public InformationContainer Read()
        {
            _readWriteLock.TryEnterReadLock(1000);
            return base.Read();

        }
        public void Close()
        {
            var filepath = Path.Combine(Workfolder, Guid);
            _readWriteLock.ExitReadLock();
            Locked.Remove(filepath);
        }
    }
}