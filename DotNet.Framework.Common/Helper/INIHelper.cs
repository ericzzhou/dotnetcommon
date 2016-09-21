using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DotNet.Framework.Common.Helper
{
    public class INIHelper
    {
        //读取INI文件的路径
        private string _fileName = System.AppDomain.CurrentDomain.BaseDirectory + "\\Config.ini";
        //默认的应用程序名
        private string _defaultAppName;

        /// <summary>
        /// 路径
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        #region -------------------注册WindowAPI方法-------------------

        //读取Ini文件
        [DllImport("kernel32")]
        private static extern string GetPrivateProfileInt(
           string lpApplicationName,
           string lpKeyName,
           int nDefault,
           string lpFileName);

        [DllImport("kernel32")]
        private static extern bool GetPrivateProfileString(
          string lpApplicationName,
          string lpKeyName,
          string lpDefault,
          StringBuilder lpReturnedString,
          int nSize,
          string lpFileName);

        //写入Ini文件
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(
          string lpApplicationName,
          string lpKeyName,
          string lpValue,
          string lpFileName);

        [DllImport("kernel32")]
        private static extern bool GetPrivateProfileSection(
          string lpAppName,
          StringBuilder lpReturnedString,
          int nSize,
          string lpFileName);

        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileSection(
          string lpAppName,
          string lpString,
          string lpFileName);

        #endregion -------------------注册WindowAPI方法-------------------

        /// <summary>
        /// 构造函数
        /// </summary>
        public INIHelper()
        {
            _defaultAppName = "LbrPath";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defaultAppName">默认的应用程序名</param>
        public INIHelper(string defaultAppName)
        {
            _defaultAppName = defaultAppName;
        }


        #region -------------------读写操作-------------------

        /// <summary>
        /// 写入Ini文件
        /// </summary>
        /// <param name="keyName">节点名</param>
        /// <param name="value">节点值</param>
        /// <returns>是否写入成功</returns>
        public bool Write(string keyName, object value)
        {
            return Write(_defaultAppName, keyName, value);
        }


        /// <summary>
        /// 写入Ini文件
        /// </summary>
        /// <param name="appName">应用程序名</param>
        /// <param name="keyName">节点名</param>
        /// <param name="value">节点值</param>
        /// <returns>是否写入成功</returns>
        public bool Write(string appName, string keyName, object value)
        {
            return WritePrivateProfileString(appName, keyName, value.ToString(), _fileName);
        }


        /// <summary>
        /// 读取Ini文件String类型节点值
        /// </summary>
        /// <param name="keyName">节点名</param>
        /// <returns>节点值</returns>
        public string ReadString(string keyName)
        {
            return ReadString(_defaultAppName, keyName);
        }


        /// <summary>
        /// 读取Ini文件String类型节点值
        /// </summary>
        /// <param name="appName">应用程序名</param>
        /// <param name="keyName">节点名</param>
        /// <returns>节点值</returns>
        public string ReadString(string appName, string keyName)
        {
            StringBuilder reString = new StringBuilder(100);
            if (GetPrivateProfileString(appName, keyName, "", reString, 100, _fileName))
            {
                return reString.ToString();
            }

            return "";
        }


        /// <summary>
        /// 读取Ini文件Int类型节点值
        /// </summary>
        /// <param name="keyName">节点名</param>
        /// <returns>节点值</returns>
        public int ReadInt(string keyName)
        {
            return ReadInt(_defaultAppName, keyName);
        }


        /// <summary>
        /// 读取Ini文件Int类型节点值
        /// </summary>
        /// <param name="appName">应用程序名</param>
        /// <param name="keyName">节点名</param>
        /// <returns>节点值</returns>
        public int ReadInt(string appName, string keyName)
        {
            string value = ReadString(appName, keyName);
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }

            return int.Parse(value);
        }

        #endregion -------------------读写操作-------------------
    }
}
