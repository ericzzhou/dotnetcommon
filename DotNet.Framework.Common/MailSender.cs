using System;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace DotNet.Framework.Common
{
    /// <summary>
    /// 邮件发送
    /// </summary>
    public class MailSender
    {
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="tomail">接受地址</param>
        /// <param name="bccmail">抄送地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="files">附件</param>
        public static void Send(string tomail, string bccmail, string subject, string body, params string[] files)
        {
            Send(SmtpConfig.Create().SmtpSetting.Sender, tomail, bccmail, subject, body, true, Encoding.Default, true, files);
        }

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="frommail">接受地址</param>
        /// <param name="tomail">发送地址</param>
        /// <param name="bccmail">抄送地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">正文是否是html形式</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isAuthentication">是否需要认证</param>
        /// <param name="files">附件</param>
        public static void Send(string frommail, string tomail, string bccmail, string subject,
                        string body, bool isBodyHtml, Encoding encoding, bool isAuthentication, params string[] files)
        {
            Send(SmtpConfig.Create().SmtpSetting.Server, SmtpConfig.Create().SmtpSetting.UserName, SmtpConfig.Create().SmtpSetting.Password, frommail,
                tomail, "", bccmail, subject, body, isBodyHtml, encoding, isAuthentication, files);
        }

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="server"></param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="frommail">来自mail地址</param>
        /// <param name="tomail">发往maild地址</param>
        /// <param name="ccmail">抄送mail地址</param>
        /// <param name="bccmail">密件抄送</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">正文是否html</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="isAuthentication">是否需要认证</param>
        /// <param name="files">附件</param>
        public static void Send(string server, string username, string password, string frommail, string tomail, string ccmail, string bccmail, string subject,
                        string body, bool isBodyHtml, Encoding encoding, bool isAuthentication, params string[] files)
        {
            SmtpClient smtpClient = new SmtpClient(server);
            //MailAddress from = new MailAddress("ben@contoso.com", "Ben Miller");
            //MailAddress to = new MailAddress("jane@contoso.com", "Jane Clayton");
            MailMessage message = new MailMessage(frommail, tomail);

            if (bccmail.Length > 1)
            {
                string[] maillist = Common.StringT.StringHelper.GetStrArray(bccmail);
                foreach (string m in maillist)
                {
                    if (m.Trim() != "")
                    {
                        MailAddress bcc = new MailAddress(m.Trim());
                        message.Bcc.Add(bcc);
                    }
                }
            }
            if (ccmail.Length > 1)
            {
                string[] maillist = Common.StringT.StringHelper.GetStrArray(ccmail);
                foreach (string m in maillist)
                {
                    if (m.Trim() != "")
                    {
                        MailAddress cc = new MailAddress(m.Trim());
                        message.CC.Add(cc);
                    }
                }
            }
            message.IsBodyHtml = isBodyHtml;
            message.SubjectEncoding = encoding;
            message.BodyEncoding = encoding;

            message.Subject = subject;
            message.Body = body;

            message.Attachments.Clear();
            if (files != null && files.Length != 0)
            {
                for (int i = 0; i < files.Length; ++i)
                {
                    Attachment attach = new Attachment(files[i]);
                    message.Attachments.Add(attach);
                }
            }

            if (isAuthentication == true)
            {
                smtpClient.Credentials = new NetworkCredential(username, password);
            }
            smtpClient.Send(message);
            message.Attachments.Dispose();

        }
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="recipient">收件人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">正文</param>
        public static void Send(string recipient, string subject, string body)
        {
            Send(SmtpConfig.Create().SmtpSetting.Sender, recipient, "", subject, body, true, Encoding.Default, true, null);
        }
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="Recipient">收件人</param>
        /// <param name="Sender">发送人</param>
        /// <param name="Subject">主题</param>
        /// <param name="Body">正文</param>
        public static void Send(string Recipient, string Sender, string Subject, string Body)
        {
            Send(Sender, Recipient, "", Subject, Body, true, Encoding.UTF8, true, null);
        }

    }
    /// <summary>
    /// 邮件发送服务配置
    /// </summary>
    public class SmtpSetting
    {
        private string _server;
        /// <summary>
        /// smtp服务地址
        /// </summary>
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        private bool _authentication;
        /// <summary>
        /// 是否需要验证
        /// </summary>
        public bool Authentication
        {
            get { return _authentication; }
            set { _authentication = value; }
        }
        private string _username;
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        private string _sender;
        /// <summary>
        /// 发送者
        /// </summary>
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        private string _password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
    /// <summary>
    /// 邮件发送config配置文件 /Config/SmtpSetting.config
    /// </summary>
    public class SmtpConfig
    {
        private static SmtpConfig _smtpConfig;
        private string ConfigFile
        {
            get
            {
                string configPath = ConfigurationManager.AppSettings["SmtpConfigPath"];
                if (string.IsNullOrEmpty(configPath) || configPath.Trim().Length == 0)
                {
                    configPath = HttpContext.Current.Request.MapPath("/Config/SmtpSetting.config");
                }
                else
                {
                    if (!Path.IsPathRooted(configPath))
                        configPath = HttpContext.Current.Request.MapPath(Path.Combine(configPath, "SmtpSetting.config"));
                    else
                        configPath = Path.Combine(configPath, "SmtpSetting.config");
                }
                return configPath;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public SmtpSetting SmtpSetting
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.ConfigFile);
                SmtpSetting smtpSetting = new SmtpSetting();
                smtpSetting.Server = doc.DocumentElement.SelectSingleNode("Server").InnerText;
                smtpSetting.Authentication = Convert.ToBoolean(doc.DocumentElement.SelectSingleNode("Authentication").InnerText);
                smtpSetting.UserName = doc.DocumentElement.SelectSingleNode("User").InnerText;
                smtpSetting.Password = doc.DocumentElement.SelectSingleNode("Password").InnerText;
                smtpSetting.Sender = doc.DocumentElement.SelectSingleNode("Sender").InnerText;

                return smtpSetting;
            }
        }
        private SmtpConfig()
        {

        }
        /// <summary>
        /// if (_smtpConfig == null)
        /// {
        ///     _smtpConfig = new SmtpConfig();
        /// }
        /// return _smtpConfig;
        /// </summary>
        /// <returns></returns>
        public static SmtpConfig Create()
        {
            if (_smtpConfig == null)
            {
                _smtpConfig = new SmtpConfig();
            }
            return _smtpConfig;
        }
    }
}
