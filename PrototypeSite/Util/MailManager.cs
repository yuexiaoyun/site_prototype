using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using log4net;
using System.Drawing;

namespace Util
{
    public class MailManager
    {
        private readonly SmtpClient smtpClient = MailFactory.CreateInnerSmtpClient();
        private static readonly ILog logger = LogManager.GetLogger(typeof(MailManager));
        private delegate void SendHandler(string subject, string body, string from, List<string> attachmentList, List<string> toList, List<string> ccList, List<string> bccList);

        private List<string> attachNameList;

        public List<string> AttachNameList
        {
            get { return attachNameList; }
            set { attachNameList = value; }
        }

        private List<Image> attachImageList; 
        public List<Image> AttachImageList
        {
            get { return attachImageList; }
            set { attachImageList = value; }
        }

        public void Send(string subject, string body, string from, List<string> attachmentList, List<string> toList, List<string> ccList, List<string> bccList)
        {
            logger.InfoFormat("----------------- Begin send email by SMTPClient at: {0} --------------------------", DateTime.Now);
            logger.InfoFormat("Mail content, subject: {0}, from: {1}, to: {2}.", subject, from, StringUtils.JoinListAsString(",", toList));
            MailMessage mailMessage = BuildMail(subject, body, from, attachmentList, toList, ccList, bccList);

            if (mailMessage == null) return;
            try
            {
                logger.InfoFormat("Send mail message.");
                smtpClient.Send(mailMessage);
            }
            catch (Exception exception)
            {
                logger.Error("Mail Send Exception: ", exception);
            }
            logger.InfoFormat("----------------- End send email by SMTPClient at: {0} --------------------------", DateTime.Now);
        }

        public IAsyncResult BeginSend(string subject, string body, string from, List<string> attachmentList, List<string> toList, List<string> ccList, List<string> bccList)
        {
            SendHandler handler = new SendHandler(Send);
            IAsyncResult asyncResult = null;
            try
            {
                asyncResult = handler.BeginInvoke(subject, body, from, attachmentList, toList, ccList, bccList, EndSend, handler);
            }
            catch (Exception exception)
            {
                logger.Error("Mail Send Exception:", exception);
            }
            return asyncResult;
        }

        private void EndSend(IAsyncResult asyncResult)
        {
            if (asyncResult == null) return;
            SendHandler handler = (SendHandler)(asyncResult.AsyncState); 
            handler.EndInvoke(asyncResult);
        }

        public IAsyncResult BeginSend(string subject, string to, string from, string body)
        {
            List<string> toList = new List<string>();
            toList.Add(to);

            return BeginSend(subject, body, from, null, toList, null, null);
        }

        private MailMessage BuildMail(string subject, string body, string from, List<string> attachmentList, List<string> toList, List<string> ccList, List<string> bccList)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.Subject = subject;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Body = body;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.None;
                mailMessage.From = new MailAddress(from);
                FillAttachmentCollection(mailMessage.Attachments, attachmentList, attachNameList);
                FillAttachmentImageCollection(mailMessage.Attachments, attachImageList);
                FillMailAddressCollection(mailMessage.To, toList);
                FillMailAddressCollection(mailMessage.CC, ccList);
                FillMailAddressCollection(mailMessage.Bcc, bccList);
                return mailMessage;
            }
            catch (Exception ex)
            {
                logger.Error("Build mail failed.", ex);
            }
            return null;
        }

        private static void FillAttachmentCollection(AttachmentCollection attachmentCollection, List<string> attachmentList, List<string> attachNameList)
        {
            if (attachmentList == null || attachmentList.Count <= 0)
                return;
            attachmentList.ForEach(
                delegate(string fileName)
                {
                    Attachment attachment = new Attachment(fileName);
                    attachmentCollection.Add(attachment);
                }
                );
            if (attachNameList != null)
                for (int i = 0; i < attachmentList.Count; i++)
                {
                    if (attachmentCollection.Count > i) attachmentCollection[i].Name = attachNameList[i];
                }
        }

        private static void FillMailAddressCollection(MailAddressCollection mailAddressCollection, List<string> addressList)
        {
            if (addressList == null || addressList.Count <= 0)
                return;
            addressList.ForEach(
                delegate(string address)
                {
                    if (!string.IsNullOrEmpty(address))
                    {
                        MailAddress mailAddress = new MailAddress(address);
                        mailAddressCollection.Add(mailAddress);
                    }
                }
                );
        }

        private static void FillAttachmentImageCollection(AttachmentCollection attachmentCollection, List<Image> imageAttachList)
        {
            if (imageAttachList == null || imageAttachList.Count <= 0)
                return;
            for (int i = 0; i < imageAttachList.Count; i++)
            {
                if (imageAttachList[i] == null)
                    continue;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                imageAttachList[i].Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                ms.Position = i;
                Attachment attachment = new Attachment(ms, "Image"+(i+1).ToString(CultureInfo.InvariantCulture));
                attachment.ContentId = "ImageEmail"+(i+1).ToString(CultureInfo.InvariantCulture);
                attachmentCollection.Add(attachment);
                
            }
        }
    }
}
