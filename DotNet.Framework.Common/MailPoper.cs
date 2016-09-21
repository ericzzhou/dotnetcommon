using System;
using System.Collections.Generic;
using System.Xml;
using System.Configuration;
using System.Web;
using System.IO;
using DotNet.Framework.Common.Mail;

namespace DotNet.Framework.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class MailPoper
    {
        /// <summary>
        /// 接收邮件
        /// </summary>
        /// <returns></returns>
        public static List<MailMessageEx> Receive()
        {
            PopSetting ps = PopConfig.Create().PopSetting;
            return Receive(ps.Server, ps.Port, ps.UseSSL, ps.UserName, ps.Password);
        }
        /// <summary>
        /// 接收邮件
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="useSsl"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static List<MailMessageEx> Receive(string hostname, int port, bool useSsl, string username, string password)
        {
            using (Pop3Client client = new Pop3Client(hostname, port, useSsl, username, password))
            {
                client.Trace += new Action<string>(Console.WriteLine);
                client.Authenticate();
                client.Stat();
                List<MailMessageEx> maillist = new List<MailMessageEx>();
                MailMessageEx message = null;
                foreach (Pop3ListItem item in client.List())
                {
                    message = client.RetrMailMessageEx(item);
                    if (message != null)
                    {
                        client.Dele(item);
                        maillist.Add(message);
                    }

                }
                client.Noop();
                client.Rset();
                client.Quit();
                return maillist;
            }
        }

    }

    /// <summary>
    /// 邮件POP设置
    /// </summary>
    public class PopSetting
    {
        private string _server;
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        private int _port;
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private bool _usessl;
        /// <summary>
        /// 是否SSL连接
        /// </summary>
        public bool UseSSL
        {
            get { return _usessl; }
            set { _usessl = value; }
        }
        private string _username;
        /// <summary>
        /// 帐号
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
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
    /// POP配置信息，从WebConfig读取
    /// </summary>
    public class PopConfig
    {
        private static PopConfig _popConfig;
        private string ConfigFile
        {
            get
            {
                string configPath = ConfigurationManager.AppSettings["PopConfigPath"];
                if (string.IsNullOrEmpty(configPath) || configPath.Trim().Length == 0)
                {
                    configPath = HttpContext.Current.Request.MapPath("/Config/PopSetting.config");
                }
                else
                {
                    if (!Path.IsPathRooted(configPath))
                        configPath = HttpContext.Current.Request.MapPath(Path.Combine(configPath, "PopSetting.config"));
                    else
                        configPath = Path.Combine(configPath, "PopSetting.config");
                }
                return configPath;
            }
        }
        /// <summary>
        /// POP设置
        /// </summary>
        public PopSetting PopSetting
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.ConfigFile);
                PopSetting popSetting = new PopSetting();
                popSetting.Server = doc.DocumentElement.SelectSingleNode("Server").InnerText;
                popSetting.Port = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("Port").InnerText);
                popSetting.UseSSL = Convert.ToBoolean(doc.DocumentElement.SelectSingleNode("UseSSL").InnerText);
                popSetting.UserName = doc.DocumentElement.SelectSingleNode("User").InnerText;
                popSetting.Password = doc.DocumentElement.SelectSingleNode("Password").InnerText;


                return popSetting;
            }


        }

        //public static Save()
        //{
        //}

        /// <summary>
        /// 创建Config
        /// </summary>
        /// <returns></returns>
        public static PopConfig Create()
        {
            if (_popConfig == null)
            {
                _popConfig = new PopConfig();
            }
            return _popConfig;
        }



    }
}
