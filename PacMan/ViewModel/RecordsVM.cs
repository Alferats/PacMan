using DataLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PacMan.ViewModel
{
    class RecordSort
    {
        public int Number { get; set; }
        public int RecordId { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
    }
    class RecordsVm
    {
        public List<RecordSort> SortRecords { get; } = new List<RecordSort>();

        public RecordsVm(List<Record> recordsDb, Record currentRecord)
        {
            FillRecordsList(recordsDb, currentRecord);
        }

        private void FillRecordsList(List<Record> recordsDb, Record currentRecord)
        {
            var shortSort = SortingRecords(recordsDb, currentRecord);

            FillRecordsListTop10(shortSort);
        }

        private List<RecordSort> SortingRecords(List<Record> recordsDb, Record currentRecord)
        {
            recordsDb.Add(currentRecord);

            var orderRecords = new List<Record>(recordsDb.OrderBy(t => t.Score));
            var sort= new List<RecordSort>();
            for (int i = 0; i < orderRecords.Count; i++)
            {
                sort.Add(new RecordSort{Score = orderRecords[i].Score, Number = i+1, PlayerName = orderRecords[i].PlayerName, RecordId = orderRecords[i].RecordId});
            }
            var shortSort = new List<RecordSort>(sort.OrderBy(t => t.Number));

            return shortSort;
        }

        private void FillRecordsListTop10(List<RecordSort> shortSort)
        {
            var count = shortSort.Count < 10 ? shortSort.Count : 10;

            var current = shortSort.FirstOrDefault(t => t.RecordId == 0);

            bool isCurrentContains = false;
            for (var i = 0; i < count; i++)
            {
                if (shortSort[i] == current) isCurrentContains = true;
                SortRecords.Add(shortSort[i]);
            }

            if (!isCurrentContains)
            {
                SortRecords.Add(current);
            }
        }
    }
}
