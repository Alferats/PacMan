using System;

namespace DataLayer.Concrete
{
    public class UnitOfWork : IDisposable
    {
        private readonly EfDbContext _db = new EfDbContext();
        private EfRecordRepository _recordsRepository;
        private bool _disposed;

        public EfRecordRepository Records => _recordsRepository ?? (_recordsRepository = new EfRecordRepository(_db));

        public void Save()
        {
            _db.SaveChanges();
        }
        
        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _db.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
