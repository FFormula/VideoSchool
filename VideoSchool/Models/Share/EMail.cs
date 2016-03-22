using System;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace VideoSchool.Models.Share
{
    public class EMail
    {
        Shared shared;

        string smtpHost;
        string smtpPort;
        string smtpUser;
        string smtpPass;
        string mailFrom;
        string mailName;
        string mailReplyTo;
        string mailCc;

        public EMail(Shared shared)
        {
            this.shared = shared;
            this.smtpHost = shared.config.smtpHost;
            this.smtpPort = shared.config.smtpPort;
            this.smtpUser = shared.config.smtpUser;
            this.smtpPass = shared.config.smtpPass;

            this.mailFrom = shared.config.mailFrom;
            this.mailName = shared.config.mailName;
            this.mailReplyTo = shared.config.mailReplyTo;
            this.mailCc   = shared.config.mailCc;
            CheckConfig();
        }

        public void CheckConfig ()
        {
            if ((smtpHost ?? "") == "" ||
                (smtpPort ?? "") == "" ||
                (smtpUser ?? "") == "" ||
                (smtpPass ?? "") == "")
                throw new Exception("SMTP not configured");
            if ((mailFrom ?? "") == "" ||
                (mailName ?? "") == "")
                throw new Exception("Mail not configured");
        }
        
        public void Send (string to, string subject, string message)
        {
            try
            {
                if (to.IndexOf("@") == -1)
                {
                    shared.error.MarkUserError("Invalid Email address");
                    return;
                }
                CheckConfig();
                SmtpClient smtpClient = new SmtpClient(smtpHost, Convert.ToInt16(smtpPort));

                smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);
                //smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();

                //Setting From , To and CC
                mail.From = new MailAddress(mailFrom, mailName);
                mail.To.Add(new MailAddress(to));
                if (mailReplyTo != "")
                    mail.ReplyToList.Add(new MailAddress(mailReplyTo));
                if (mailCc != "")
                    mail.CC.Add(new MailAddress(mailCc));

                mail.Subject = subject;
                mail.Body = message;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                mail.HeadersEncoding = Encoding.UTF8;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                shared.error.MarkSystemError(ex);
            }
        }

    }
}