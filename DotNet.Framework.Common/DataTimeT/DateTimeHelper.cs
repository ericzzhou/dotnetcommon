using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/********************************************************
 * Class：DateTimeHelper
 * Description：
 * Create Date:2011-12-02 10:21:53
 * Create Year:2011 
 * Update Date:
 * Author：zhouzhaokun
 *******************************************************/
namespace DotNet.Framework.Common.DataTimeT
{
    public static class DateTimeHelper
    {
        public enum DateFormat
        {
            /// <summary>
            /// yyyy/MM/dd的格式
            /// </summary>
            yyyyMMdd,
            /// <summary>
            /// yy/MM/dd的格式
            /// </summary>
            yyMMdd,
            /// <summary>
            /// yyyy/MM/dd hh:mm:ss的格式
            /// </summary>
            yyyyMMddhhmmss,
            /// <summary>
            /// yy/MM/dd hh:mm:ss的格式
            /// </summary>
            yyMMddhhmmss,
            /// <summary>
            /// hh:mm:ss 的格式
            /// </summary>
            hhmmss
        }
        /// <summary>
        /// 检测日期是否为空
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>空：True 不为空 False</returns>
        public static bool IsBlank(string value)
        {
            if (StringT.StringHelper.IsBlank(value)) return true;
            DateTime dt = ToDateTime(value);
            return (dt == DateTime.MinValue);
        }

        /// <summary>
        /// DateTime字符串转换成DateTime类型
        /// </summary>
        /// <param name="value">日期字符串</param>
        /// <returns>DateTime结果</returns>
        public static DateTime ToDateTime(object value)
        {
            DateTime outValue = DateTime.MinValue;
            if (ObjectT.ObjectHelper.IsBlank(value)) return outValue;
            outValue = DateTime.TryParse(value.ToString(), out outValue) ? outValue : DateTime.MinValue;
            return outValue;
        }

        /// <summary>
        /// DateTime类型转换成DateTime字符串
        /// </summary>
        /// <param name="value">日期字符串</param>
        /// <param name="DateFormat">日期格式</param>
        /// <returns>DateTime字符串</returns>
        public static string ToDateString(object value, DateFormat dateFormat)
        {
            //设置默认的日期格式
            string format = "yyyy-MM-dd";
            switch (dateFormat)
            {
                case DateFormat.yyyyMMdd:
                    format = "yyyy-MM-dd";
                    break;
                case DateFormat.yyMMdd:
                    format = "yy-MM-dd";
                    break;
                case DateFormat.yyyyMMddhhmmss:
                    format = "yyyy-MM-dd hh:mm:ss";
                    break;
                case DateFormat.yyMMddhhmmss:
                    format = "yy-MM-dd hh:mm:ss";
                    break;
                case DateFormat.hhmmss:
                    format = "hh:mm:ss";
                    break;
                default:
                    break;
            }
            DateTime outValue = DateTime.MinValue;
            if (ObjectT.ObjectHelper.IsBlank(value)) return outValue.ToString(format);
            outValue = DateTime.TryParse(value.ToString(), out outValue) ? outValue : DateTime.MinValue;
            return outValue.ToString(format);
        }

        /// <summary>
        /// 获取当前时间点最近一周前的日期
        /// </summary>
        /// <param name="date">当前时间点</param>
        /// <returns>一周前的日期</returns>
        public static DateTime GetLastWeek(DateTime date)
        {
            return date.AddDays(-6);
        }

        /// <summary>
        /// 获取当前时间点最近一月前的日期
        /// </summary>
        /// <param name="date">当前时间点</param>
        /// <returns>一月前的日期</returns>
        public static DateTime GetLast30Days(DateTime date)
        {
            return date.AddMonths(-1);
        }

        /// <summary>
        /// 获取当前时间点最近一年前的日期
        /// </summary>
        /// <param name="date">当前时间点</param>
        /// <returns>一年前的日期</returns>
        public static DateTime GetLast365Days(DateTime date)
        {
            return date.AddYears(-1);
        }
    }
}
