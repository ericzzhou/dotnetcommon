/********************************************************
 * Class：PDFCommon
 * Description：
 * Create Date:2011-11-30 17:27:30
 * Create Year:2011 
 * Update Date:
 * Author：zhouzhaokun
 *******************************************************/
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Drawing;
namespace DotNet.Framework.Common.PDF
{
    public class PDFCommon
    {
        public PDFCommon()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        #region 单例 创建PDF文档中的字体 BaseFont
        /// <summary>
        /// 创建PDF文档中的字体
        /// </summary>
        private BaseFont _BaseFont = null;
        /// <summary>
        /// 单例 创建PDF文档中的字体
        /// </summary>
        public BaseFont BaseFont
        {
            get
            {
                if (_BaseFont == null)
                {
                    _BaseFont = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\SIMFANG.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                }
                return _BaseFont;
            }
        }
        #endregion

        #region 字体属性
        private iTextSharp.text.Font _fontBodyBlod = null;
        private iTextSharp.text.Font _fontBodyNormal = null;
        #endregion

        #region 单例  基于 BaseFont 创建内容字体，10号大小、粗体、黑色
        /// <summary>
        /// 单例  基于 BaseFont 创建内容字体，10号大小、粗体、黑色
        /// </summary>
        public iTextSharp.text.Font FontBodyBlod
        {
            get
            {
                if (_fontBodyBlod == null)
                {
                    _fontBodyBlod = new iTextSharp.text.Font(BaseFont, 10, iTextSharp.text.Font.BOLD, new BaseColor(Color.Black));
                }
                return _fontBodyBlod;
            }
        }
        #endregion

        #region 单例 基于 BaseFont 创建内容字体，10号大小、默认、黑色
        /// <summary>
        /// 单例 基于 BaseFont 创建内容字体，10号大小、默认、黑色
        /// </summary>
        public iTextSharp.text.Font FontBodyNormal
        {
            get
            {
                if (_fontBodyNormal == null)
                {
                    _fontBodyNormal = new iTextSharp.text.Font(BaseFont, 10, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black));
                }
                return _fontBodyNormal;
            }
        }
        #endregion

        #region 单例 通知单标题文字字体样式(14号字体、默认系统样式、黑色)
        /// <summary>
        /// 通知单标题文字字体样式(14号字体、默认系统样式、黑色)
        /// </summary>
        private iTextSharp.text.Font _FontTitle = null;
        /// <summary>
        /// 单例 通知单标题文字字体样式(14号字体、默认系统样式、黑色)
        /// </summary>
        public iTextSharp.text.Font FontTitle
        {
            get
            {
                if (_FontTitle == null)
                {
                    _FontTitle = new iTextSharp.text.Font(BaseFont, 14, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black));
                }
                return _FontTitle;
            }
        }
        #endregion

        #region 添加单元格数据
        /// <summary>
        /// 添加单元格数据
        /// </summary>
        /// <param name="Text">数据内容</param>
        /// <param name="Colspan">跨列数</param>
        /// <param name="table"></param>
        /// <param name="center">是否居中</param>
        /// <param name="blod">是否粗体</param>
        public void AddCell(string Text, int Colspan, PdfPTable table, bool center, bool blod)
        {
            PdfPCell PCell = new PdfPCell();
            //顶部距离
            PCell.PaddingTop = 10f;
            //底部举例
            PCell.PaddingBottom = 12f;
            if (Colspan != 0)
            {
                PCell.Colspan = Colspan;
            }
            iTextSharp.text.Font f = blod == true ? FontBodyBlod : FontBodyNormal;
            Paragraph pgr = new Paragraph(Text, f);
            if (center)
                pgr.Alignment = Element.ALIGN_CENTER;

            PCell.AddElement(pgr);
            table.AddCell(PCell);

        }
        #endregion

        #region 添加报表页脚
        /// <summary>
        /// 添加报表页脚
        /// </summary>
        /// <param name="Text">内容</param>
        /// <param name="Colspan">跨列数</param>
        /// <param name="table">Table</param>
        /// <param name="center">是否居中</param>
        public void AddCellBottom(string Text, int Colspan, PdfPTable table)
        {
            PdfPCell PCell = new PdfPCell();
            PCell.Border = 0;
            PCell.PaddingTop = 10f;
            if (Colspan != 0)
            {
                PCell.Colspan = Colspan;
            }
            PCell.Phrase = new Phrase(Text, FontBodyNormal);
            table.AddCell(PCell);
        }
        #endregion

        #region 设置 PdfPTable 的基础样式
        /// <summary>
        /// 设置 PdfPTable 的基础样式
        /// </summary>
        /// <param name="table"></param>
        /// <param name="floatArr">设置单元格的宽度，对应你的列数</param>
        /// <param name="WidthPercentage">表格占页面的百分比 </param>
        public void SetPTableStyle(PdfPTable table, float[] floatArr, float WidthPercentage)
        {
            //对齐方式   
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.SetWidths(floatArr);
            table.WidthPercentage = WidthPercentage;
            //table.TotalWidth = 500f;
            //table.LockedWidth = true;
        }
        #endregion
    }
}
