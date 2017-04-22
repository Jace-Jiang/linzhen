using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace XGhms.Helper
{
    class EmailHelper
    {
        /// <summary>
        /// 发送邮件的方法
        /// </summary>
        /// <param name="smtpserver">SMTP服务器地址</param>
        /// <param name="userName">邮箱账号</param>
        /// <param name="pwd">邮箱密码</param>
        /// <param name="nickName">发件人昵称</param>
        /// <param name="strfrom">发件人</param>
        /// <param name="strto">收件人</param>
        /// <param name="subj">主题</param>
        /// <param name="bodys">内容</param>
        public static void sendMail(string smtpserver, string userName, string pwd, string nickName, string strfrom, string strto, string subj, string bodys)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpserver;//指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);//用户名和密码
            MailAddress mailFrom = new MailAddress(strfrom,nickName,Encoding.UTF8);
            MailAddress mailTo = new MailAddress(strto);
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo);
            mailMessage.Subject = subj;//主题
            mailMessage.Body = bodys;//内容
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Normal;//优先级
            smtpClient.Send(mailMessage);
        }
    }
}
