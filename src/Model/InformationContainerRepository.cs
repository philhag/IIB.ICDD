using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IIB.ICDD.Handling;

namespace IIB.ICDD.Model
{
    public class InformationContainerRepository
    {
        public static InformationContainerRepository GetForInformationContainer(InformationContainer container, bool create = true)
        {
            if (LibGit2Sharp.Repository.IsValid(container.PathToContainer))
            {
                return new InformationContainerRepository(container.PathToContainer);
            }

            if (!create) return null;

            var path = Repository.Init(container.PathToContainer);
            return new InformationContainerRepository(path);
        }

        public string CommitHash
        {
            get
            {
                return _repo.Head.Tip.Sha;
            }
        }

        public string BranchName
        {
            get
            {
                return _repo.Head.FriendlyName;
            }
        }

        public string TrackedBranchName
        {
            get
            {
                return _repo.Head.IsTracking ? _repo.Head.TrackedBranch.FriendlyName : String.Empty;
            }
        }

        public bool HasUnpushedCommits
        {
            get
            {
                return _repo.Head.TrackingDetails.AheadBy > 0;
            }
        }

        public bool HasUncommittedChanges
        {
            get
            {
                return _repo.RetrieveStatus().Any(s => s.State != FileStatus.Ignored);
            }
        }

        public IEnumerable<Commit> Log
        {
            get
            {
                return _repo.Head.Commits;
            }
        }

        public Commit Commit(string commitMessage, string name = "icdd", string mail = "icdd-plattform@ruhr-uni-bochum.de")
        {
            if (HasUncommittedChanges)
            {
                StageChanges();
                var signature = new Signature(new Identity(name, mail),
                    new DateTimeOffset(DateTime.Now));
                return _repo.Commit(commitMessage, signature, signature);
            }

            return null;

        }

        public void StageChanges()
        {
            try
            {
                Commands.Stage(_repo, "*");
            }
            catch (Exception ex)
            {
                new IcddException("Exception:RepoActions:StageChanges " + ex.Message, ex);
            }
        }


        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _repo.Dispose();
            }
        }

        private InformationContainerRepository(string path)
        {
            _repo = new Repository(path);
        }

        private bool _disposed;
        private readonly Repository _repo;
    }
}
