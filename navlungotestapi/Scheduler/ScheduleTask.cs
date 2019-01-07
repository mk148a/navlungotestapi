using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace navlungotestapi.Scheduler
{
    public class ScheduleTask : ScheduledProcessor
    {

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }
        //0 0 2 * *
        protected override string Schedule => "* * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            string subject = "navlungo testmail";
            new SmtpClient
            {
                Host = "mail.secretrithm.com",
                Port = 26,
                EnableSsl = false,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("orders@secretrithm.com", "12345")
            }.Send(new MailMessage { From = new MailAddress("orders@secretrithm.com", "SecretRithm"), To = { "mk148a@hotmail.com" }, Subject = subject, Body = Environment.NewLine + "deneme", IsBodyHtml = true, BodyEncoding = Encoding.UTF8 });

            return Task.CompletedTask;
        }
    }
}
