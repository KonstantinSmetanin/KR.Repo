using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KR.Core.Models;

namespace KR.Core.Interfaces
{
    public interface IWorkers
    {
        public IEnumerable<Worker> Workers { get; }
    }
}
