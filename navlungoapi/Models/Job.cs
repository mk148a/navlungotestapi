using NCrontab;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
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
    public string status { get; set; }
    public async Task<string> getcrondesc(string schedule)
    {
        //https://cronexpressiondescriptor.azurewebsites.net/api/descriptor/?expression=0+0+5+*+*&locale=tr-tr

        string result = "";
        using (var client = new WebClient())
        {
            string[] cronjobparts = schedule.Split(" ");
            result = await client.DownloadStringTaskAsync(new Uri("https://cronexpressiondescriptor.azurewebsites.net/api/descriptor/?expression=" + cronjobparts[0] + "+" + cronjobparts[1] + "+" + cronjobparts[2] + "+" + cronjobparts[3] + "+" + cronjobparts[4] + "&locale=tr-tr"));
        }
        return result;
    }
    public string getstatus()
    {
               
        System.Timers.Timer t = new System.Timers.Timer();      
        var schedule_ = CrontabSchedule.Parse(schedule);
        DateTime _nextRun = schedule_.GetNextOccurrence(DateTime.Now);
        DateTime __nextRun = schedule_.GetNextOccurrence(_nextRun);            
        TimeSpan span = __nextRun.Subtract(_nextRun);         
        t.Interval = span.TotalMilliseconds;
        t.Elapsed += T_Elapsed;
        t.Start();           
             
        return status;
    }

    private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString());
            string subject = shipper + " test mail job";
            new SmtpClient
            {
                Host = "mail.secretrithm.com",
                Port = 26,
                EnableSsl = false,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("orders@secretrithm.com", "12345")
            }.Send(new MailMessage { From = new MailAddress("orders@secretrithm.com", "SecretRithm"), To = { mailadress }, Subject = subject, Body = "test message", IsBodyHtml = true, BodyEncoding = Encoding.UTF8 });
            status = "success";
        }
        catch(Exception ee)
        {
            System.Diagnostics.Debug.WriteLine(ee.Message);
            status = "error";
        }
    }

    public Job(string shipper, string destinationCountry, string mailadress, string schedule)
    {
        this.shipper = shipper;
        this.destinationCountry = destinationCountry;
        this.mailadress = mailadress;
        this.schedule = schedule;
        this.status = getstatus();
        this.crondesc = JsonConvert.DeserializeObject<CronDescApiHelper>(getcrondesc(schedule).Result).description;
    }
}
    
}
