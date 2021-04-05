using System;
using System.ComponentModel.DataAnnotations;

namespace WorkerApi.Models.v1
{
    public class CreateWorkerModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? Age { get; set; }
    }
}
