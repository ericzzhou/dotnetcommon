using System;

/********************************************************
 * Class：ValidateImg
 * Description：
 * Create Date:2011-11-30 14:36:40
 * Create Year:2011 
 * Update Date:
 * Author：zhouzhaokun
 *******************************************************/
namespace Demo
{
    public partial class ValidateImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Equals(Session["xk_validate_code"]))
            {
                DotNet.Framework.Common.MessageBox.Show(this, "验证码输入正确");
            }
            else
            {
                DotNet.Framework.Common.MessageBox.Show(this, "输入错误");
            }
        }
    }
}
