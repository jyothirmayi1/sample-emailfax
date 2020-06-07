using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Linq;
using Grpc.Core;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)

        {
            //Detailed Method
            try
            {
                // read from config file 
                // SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings.Get("smtpserver"), Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("smtpport")));
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings.Get("smtpusername"), System.Configuration.ConfigurationManager.AppSettings.Get("smtppassword"));
                smtp.EnableSsl = true;
                MailAddress mailfrom = new MailAddress(System.Configuration.ConfigurationManager.AppSettings.Get("smtpusername"));

                // read from config file 
                //  string[] filePaths = Directory.GetFiles(@"C:\\Test");
                string[] filePaths = Directory.GetFiles(System.Configuration.ConfigurationManager.AppSettings.Get("Filepath"));
                //string filePaths = "@C:\\Test";

                var files = filePaths.Where(x => Path.GetExtension(x).Contains(".FAX"));
                // string[] directories = files.Split(Path.DirectorySeparatorChar);
                // string[] directories = files.split("//");
                //directories.


                foreach (var file in files)
                {
                    //IEnumerable Faxnumber = files.SkipLast(4);
                   // int startindex = 12;
                    int endIndex = file.Length;
                   // string Faxnumber = file.Substring(startindex, endIndex);
                    string Faxnumber = file.Substring(8);

                    //string faxnumber = file.Split(".fax"); // to fax number - 1234 


                    if (Faxnumber is  null)
                    {
                        Console.WriteLine("Faxnumber not found");

                    }

		            else
                    { 
                        MailAddress mailto = new MailAddress(Faxnumber + "@fax2mail.com");
                        System.Net.Mail.MailMessage newmsg = new System.Net.Mail.MailMessage(mailfrom, mailto);

                        newmsg.Subject = "please find attached document";//subjectline(Faxnumber); //"please find attached document"
                        newmsg.Body = "Test"; //Bodytext("Faxnumber");
                        var attachment = new Attachment(file);
                        newmsg.Attachments.Add(attachment);

                        smtp.Send(newmsg);
                    }


                }

                smtp.Dispose();
                //mailfrom.Dispose();
                // mailto.Dispose();
                //newmsg.Dispose();



            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }
        public string subjectline(string Faxnumber)
{
            string sbtext;
            sbtext = Faxnumber + "test subject";
            return sbtext; 

}

    public string Bodytext( string Faxnumber)
{
    string btext;
            btext = Faxnumber + "test body";

      return btext;

}
//public Attachment attachementfile(Attachment file) 
//{
           // var attachmentfile = new Attachment(file);

           // string  attachmentfile = new Attachment(file);
    //return attachmentfile;

//}


    }
}