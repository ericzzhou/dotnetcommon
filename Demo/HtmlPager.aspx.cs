using System;

/********************************************************
 * Class：HtmlPager
 * Description：
 * Create Date:2011-11-30 14:44:37
 * Create Year:2011 
 * Update Date:
 * Author：zhouzhaokun
 *******************************************************/
namespace Demo
{
    public partial class HtmlPager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int currentPage = DotNet.Framework.Common.QueryString.QId("page");
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            Literal1.Text = DotNet.Framework.Common.HtmlPager.GetHtmlPager(100, currentPage, "p", "action");
            Literal2.Text = DotNet.Framework.Common.HtmlPager.GetPager(100, currentPage);
            
        }
    }
}
