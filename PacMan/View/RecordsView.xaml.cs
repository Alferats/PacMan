using DataLayer.Entities;
using System.Collections.Generic;
using PacMan.ViewModel;

namespace PacMan.View
{
    /// <summary>
    /// Interaction logic for RecordsView.xaml
    /// </summary>
    public partial class RecordsView
    {
        public RecordsView(List<Record> recordsDb, Record currentRecord)
        {
            DataContext = new RecordsVm(recordsDb, currentRecord);
            InitializeComponent();
        }
    }
}
