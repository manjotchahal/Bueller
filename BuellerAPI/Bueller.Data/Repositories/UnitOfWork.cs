using Bueller.Data.Models;
using System;
using System.Collections.Generic;

namespace Bueller.Data.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly IDbContext _context;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWork()
        {
            _context = new BuellerContext();
        }

        public UnitOfWork(IDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public Crud<T> Crud<T>() where T : BaseModel
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Crud<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (Crud<T>)_repositories[type];
        }

        public AssignmentRepository AssignmentRepository()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(AssignmentRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(AssignmentRepository);
                var repositoryInstace = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstace);
            }

            return (AssignmentRepository)_repositories[type];
        }

        public BookRepository BookRepository()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(BookRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BookRepository);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (BookRepository)_repositories[type];
        }

        public ClassRepository ClassRepository()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(ClassRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(ClassRepository);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (ClassRepository)_repositories[type];
        }

        public FileRepository FileRepository()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(FileRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(FileRepository);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (FileRepository)_repositories[type];
        }

        public GradeRepository GradeRepository()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(GradeRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GradeRepository);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (GradeRepository)_repositories[type];
        }

        public StudentRepository StudentRepository()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(StudentRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(StudentRepository);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (StudentRepository)_repositories[type];
        }

        public SubjectRepository SubjectRepository()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(SubjectRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(SubjectRepository);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (SubjectRepository)_repositories[type];
        }

        public TeacherRepository TeacherRepository()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(TeacherRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(TeacherRepository);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (TeacherRepository)_repositories[type];
        }
    }
}
