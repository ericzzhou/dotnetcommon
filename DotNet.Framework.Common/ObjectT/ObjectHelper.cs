using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/********************************************************
 * Class：ObjectHelper
 * Description：
 * Create Date:2011-12-02 10:26:32
 * Create Year:2011 
 * Update Date:
 * Author：zhouzhaokun
 *******************************************************/
namespace DotNet.Framework.Common.ObjectT
{
    /// <summary>
    /// 对object类型数据进行操作
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// 检测对象是否为空
        /// </summary>
        /// <param name="value">对象</param>
        /// <returns>空：True 不为空 False</returns>
        public static bool IsBlank(object value)
        {
            return (value == null || StringT.StringHelper.IsBlank(value.ToString()));
        }
    }
}
