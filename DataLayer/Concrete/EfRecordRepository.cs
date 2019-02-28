using System.Collections.Generic;
using System.Data.Entity;
using DataLayer.Abstract;
using DataLayer.Entities;

namespace DataLayer.Concrete
{
    public class EfRecordRepository : IRepository<Record>
    {
        private readonly EfDbContext _db;

        public EfRecordRepository(EfDbContext context)
        {
            _db = context;
        }

        public IEnumerable<Record> GetAll()
        {
            return _db.Records;
        }

        public Record Get(int id)
        {
            return _db.Records.Find(id);
        }

        public void Create(Record record)
        {
            _db.Records.Add(record);
        }

        public void Update(Record record)
        {
            _db.Entry(record).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var record = _db.Records.Find(id);
            if (record != null)
                _db.Records.Remove(record);
        }
    }
}
