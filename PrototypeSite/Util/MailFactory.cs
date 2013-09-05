using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Web.Configuration;

namespace Util
{
    public static class MailFactory
    {
        public static SmtpClient CreateInnerSmtpClient()
        {
            SmtpClient smtpClient = new SmtpClient();

            //host
            smtpClient.Host = "smtp.gmail.com";
            if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["SMTP_Host"]))
                smtpClient.Host = WebConfigurationManager.AppSettings["SMTP_Host"];

            //port
            smtpClient.Port = 25;
            if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["SMTP_Port"]))
                smtpClient.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["SMTP_Port"]);

            //user
            string userName = string.Empty;
            if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["SMTP_UserName"]))
                userName = WebConfigurationManager.AppSettings["SMTP_UserName"];

            //password
            string password = string.Empty;
            if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["SMTP_Password"]))
                password = WebConfigurationManager.AppSettings["SMTP_Password"];

            //credentials
            smtpClient.Credentials = new NetworkCredential(userName, password);

            //enable ssl
            smtpClient.EnableSsl = false;
            if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["SMTP_SSL"]))
                smtpClient.EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["SMTP_SSL"]);

            return smtpClient;
        }
    }
}
