using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkPlanningApi.Domain.Entities
{
    public class Shift
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid WorkerGuid { get; set; }
        public string WorkerFullName { get; set; }
    }
}
