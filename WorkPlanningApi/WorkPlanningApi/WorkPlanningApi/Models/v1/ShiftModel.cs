using System;
using System.ComponentModel.DataAnnotations;

namespace WorkPlanningApi.Models.v1
{
    public class ShiftModel
    {
        [Required] public Guid WorkerGuid { get; set; }

        [Required] public string WorkerFullName { get; set; }

        [Required] public DateTime StartDate { get; set; }

        [Required] public DateTime EndDate { get; set; }

    }
}
