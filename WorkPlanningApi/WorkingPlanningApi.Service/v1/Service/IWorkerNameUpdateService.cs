using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingPlanningApi.Service.v1.Models;

namespace WorkingPlanningApi.Service.v1.Service
{
  public  interface IWorkerNameUpdateService
    {
        void UpdateWorkerNameInShifts(UpdateWorkerFullNameModel updateWorkerFullNameModel);
    }
}
