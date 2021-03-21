using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using Experimental.System.Messaging;

namespace FundooNotesRepositoryLayer.MSMQ_Services
{
    public class MSMQService :IMSMQService
    {
        /// <summary>
        /// Created a instance of MessageQueue class
        /// </summary>
        private readonly MessageQueue messageQueue = new MessageQueue();
        string email;
        /// <summary>
        /// Create the Queue if not exists
        /// </summary>
        public MSMQService()
        {
            this.messageQueue.Path = @".\private$\Fundoo";
            if (MessageQueue.Exists(this.messageQueue.Path))
            {
            }
            else
            {
                MessageQueue.Create(this.messageQueue.Path);
            }
        }
        /// <summary>
        /// Add Message into MSMQ
        /// </summary>
        /// <param name="message"></param>

        public void AddToQueue(string email)
        {
            this.email = email;
            //Set Formatter to serialize the object,I’m using XML //serialization
            this.messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            this.messageQueue.ReceiveCompleted += this.ReceiveFromQueue;

            this.messageQueue.Send(email);

            this.messageQueue.BeginReceive();

            this.messageQueue.Close();
        }

        /// <summary>
        /// Method to fetch message from MSMQ.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public void ReceiveFromQueue(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = this.messageQueue.EndReceive(e.AsyncResult);

                string data = msg.Body.ToString();

                // Process the logic be sending the message

                // Restart the asynchronous receive operation.
                using (MailMessage mailMessage = new MailMessage("sandeepgaurdec13@gmail.com", email))
                {
                    mailMessage.Subject = "Forgot Password";
                    mailMessage.Body = "Forgot Password Link";
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("sandeepgaurdec13@gmail.com", "sANDEEP123@");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mailMessage);
                    //return "Success";
                }
                using (StreamWriter file = new StreamWriter(@"G:\Repos\FundooNoteAPI\FundooNotes.txt", true))
                {
                    file.WriteLine(data);
                }

                this.messageQueue.BeginReceive();
            }
            catch (MessageQueueException qexception)
            {
                Console.WriteLine(qexception);
            }
        
    }
}
}
