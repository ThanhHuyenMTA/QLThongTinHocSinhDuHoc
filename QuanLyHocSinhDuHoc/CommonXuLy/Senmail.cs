using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace QuanLyHocSinhDuHoc.CommonXuLy
{
    public class Senmail
    {
        //GET : Gửi mail
        public void SendEmail(string address,string message)
        {
            string formmail = "duhocnhatbangotojapan95@gmail.com";
            string password = "duhocnhatbanGotojapan9595";
            MailMessage mail = new MailMessage();
            mail.To.Add(address);
            mail.From = new MailAddress(formmail);
            //mail.Subject = subject;
            mail.Subject = "GOTOJAPAN.VN";
            string Body = message;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            //ĐỊA CHỈ SMTP Server
            smtp.Host = "smtp.gmail.com";
            //Cổng SMTP
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(formmail, password); // Enter seders User name and password  
            //SMTP yêu cầu mã hóa dữ liệu theo SSL _bảo mật
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}