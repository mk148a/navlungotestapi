using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace navlungoapi.Models
{
    public class Job
    {
        [Key]
        public string shipper { get; set; }
        public string destinationCountry { get; set; }
        public CronJob cronjob { get; set; }
    }
}
