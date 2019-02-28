using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Importer
    {
        [ImportMany(typeof(IMovingAlgorithm))]
        public IEnumerable<IMovingAlgorithm> ImportedMembers { get; set; }
    }
}
