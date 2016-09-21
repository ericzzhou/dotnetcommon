using System;
using System.Web;
using System.Web.UI;
using System.IO;

namespace DotNet.Framework.Common
{ 
    /// <summary>
    /// 页面基类
    /// </summary>
    public class BasePage : Page
    {
        /// <summary>
        /// 获取页面名称，不包括 .aspx
        /// </summary>
        /// <returns></returns>
        private string GetPageName()
        {
            string[] path = Request.FilePath.Split(new Char[] { '/' });
            string fileName = path[path.Length - 1];
            string FormCode = "";
            string[] strTem = fileName.Split(new char[] { '.' });
            FormCode = strTem[0];
            return FormCode;
        }
        /// <summary>
        /// 页面加载事件，
        /// 页面继承 页面基类后需要 重写 Page_Load 方法，并且使用 base 关键字实现权限的验证
        /// 使用方法：参考 System/CemeteryInfoEdit.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 页面初始化 OnInit 事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        #region 日志处理

        /// <summary>
        /// 出错处理:写日志,导航到公共出错页面(或者展示友好错误信息)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnError(EventArgs e)
        {
            Exception ex = this.Server.GetLastError();
            string error = this.DealException(ex);
            WriteFileLog(error);
            if (ex.InnerException != null)
            {
                error = this.DealException(ex);
                WriteFileLog(error);
            }
            //如果不想让出错之后跳转到出错页面，就注释以下2行
            this.Server.ClearError();
            this.Response.Redirect("~/Error/Error.aspx");
        }

        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteFileLog(string exMsg)
        {
            string path = HttpContext.Current.Request.PhysicalApplicationPath + "LogFile";
            FileStream fs = null;
            StreamWriter m_streamWriter = null;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(DateTime.Now.ToString() + "\n");
                m_streamWriter.WriteLine("-----------------------------------------------------------");
                m_streamWriter.WriteLine("-----------------------------------------------------------");
                m_streamWriter.WriteLine(exMsg);
                m_streamWriter.WriteLine("-----------------------------------------------------------");
                m_streamWriter.WriteLine("-----------------------------------------------------------");
                m_streamWriter.Flush();
            }
            finally
            {
                if (m_streamWriter != null)
                {
                    m_streamWriter.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        /// <summary>
        /// 处理异常，用来将主要异常信息写入文本日志
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string DealException(Exception ex)
        {
            this.Application["StackTrace"] = ex.StackTrace;
            this.Application["MessageError"] = ex.Message;
            this.Application["SourceError"] = ex.Source;
            this.Application["TargetSite"] = ex.TargetSite.ToString();
            string error = string.Format("URl：{0}\n引发异常的方法：{1}\n错误信息：{2}\n错误堆栈：{3}\n",
                this.Request.RawUrl, ex.TargetSite, ex.Message, ex.StackTrace);
            return error;
        }

        #endregion
    }
}
