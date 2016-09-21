/********************************************************
 * Class：BindDropDownList
 * Description：
 * Create Date:2011-12-02 09:46:50
 * Create Year:2011 
 * Update Date:
 * Author：zhouzhaokun
 *******************************************************/
using System.Web.UI.WebControls;
using System;
using System.Data;
namespace DotNet.Framework.Common.WebControl
{
    /// <summary>
    /// 对 下拉 列表进行绑定
    /// </summary>
    public static class DropDownList
    {
        /// <summary>
        /// 绑定下拉列表（数据显示控件或者源数据集不存在都会引发异常）
        /// </summary>
        /// <param name="ddl">数据显示控件</param>
        /// <param name="ds">数据集</param>
        /// <param name="DataTextField">文本数据源</param>
        /// <param name="DataValueField">值数据源</param>
        /// <param name="hasTopItem">bool,是否有默认第一项</param>
        /// <param name="topItemText">第一项文本</param>
        /// <param name="topItemValue">第一项值</param>
        public static void Bind(System.Web.UI.WebControls.DropDownList ddl, DataSet ds, string DataTextField, string DataValueField, bool hasTopItem, string topItemText, string topItemValue)
        {
            if (ddl != null)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddl.ClearSelection();
                    ddl.Items.Clear();
                    ddl.DataTextField = DataTextField;
                    ddl.DataValueField = DataValueField;
                    ddl.DataSource = ds.Tables[0].DefaultView;
                    ddl.DataBind();
                    if (hasTopItem)
                    {
                        ListItem lit = new ListItem(topItemText, topItemValue);
                        ddl.Items.Insert(0, lit);
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
