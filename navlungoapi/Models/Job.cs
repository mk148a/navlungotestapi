using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace navlungoapi.Models
{
            public class CronDescApiHelper
            {
        public string description { get; set; }
}
public class Job
    {
        [Key]
        public string shipper { get; set; }
        public string destinationCountry { get; set; }
        public string mailadress { get; set; }
        public string schedule { get; set; }
        public string crondesc { get; set; }
        public async Task<string> getcrondesc(string schedule)
        {
            //https://cronexpressiondescriptor.azurewebsites.net/api/descriptor/?expression=0+0+5+*+*&locale=tr-tr

            string result = "";
            using (var client = new WebClient())
            {
                char[] cronjobparts = schedule.ToCharArray();
                result = await client.DownloadStringTaskAsync(new Uri("https://cronexpressiondescriptor.azurewebsites.net/api/descriptor/?expression=" + cronjobparts[0] + "+" + cronjobparts[1] + "+" + cronjobparts[2] + "+" + cronjobparts[3] + "+" + cronjobparts[4] + "&locale=tr-tr"));
            }
            return result;
        }


        public Job(string shipper, string destinationCountry, string mailadress, string schedule)
        {
            this.shipper = shipper;
            this.destinationCountry = destinationCountry;
            this.mailadress = mailadress;
            this.schedule = schedule;

            this.crondesc = JsonConvert.DeserializeObject<CronDescApiHelper>(getcrondesc(schedule).Result).description;
        }
    }
    
}
