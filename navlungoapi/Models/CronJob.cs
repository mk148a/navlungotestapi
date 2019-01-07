using System.ComponentModel.DataAnnotations;

namespace navlungoapi.Models
{
    public class CronJob
    {
        [Key]
        public string mailadress { get; set; }
        public string schedule { get; set; }
             
    }
   

}
