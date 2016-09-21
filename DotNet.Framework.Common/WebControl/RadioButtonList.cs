using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
/********************************************************
 * Class：BindRadioButtonList
 * Description：
 * Create Date:2011-12-02 10:00:56
 * Create Year:2011 
 * Update Date:
 * Author：zhouzhaokun
 *******************************************************/
namespace DotNet.Framework.Common.WebControl
{
    /// <summary>
    /// 对RadioButtonList的一些操作
    /// </summary>
    public static class RadioButtonList
    {
        /// <summary>
        /// 绑定下拉列表（数据显示控件或者源数据集不存在都会引发异常）
        /// </summary>
        /// <param name="rbtnl">数据显示控件</param>
        /// <param name="ds">数据集</param>
        /// <param name="DataTextField">文本数据源</param>
        /// <param name="DataValueField">值数据源</param>
        /// <param name="hasTopItem">bool,是否有默认第一项</param>
        /// <param name="topItemText">第一项文本</param>
        /// <param name="topItemValue">第一项值</param>
        public static void Bind(System.Web.UI.WebControls.RadioButtonList rbtnl, DataSet ds, string DataTextField, string DataValueField, bool hasTopItem, string topItemText, string topItemValue)
        {
            if (rbtnl != null)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rbtnl.ClearSelection();
                    rbtnl.Items.Clear();
                    rbtnl.DataTextField = DataTextField;
                    rbtnl.DataValueField = DataValueField;
                    rbtnl.DataSource = ds.Tables[0].DefaultView;
                    rbtnl.DataBind();
                    if (hasTopItem)
                    {
                        ListItem lit = new ListItem(topItemText, topItemValue);
                        rbtnl.Items.Insert(0, lit);
                    }
                }
                else
                {
                    throw new Exception("源数据集不存在");
                }

            }
            else
            {
                throw new Exception("数据控件不存在");
            }
        }
    }
}
