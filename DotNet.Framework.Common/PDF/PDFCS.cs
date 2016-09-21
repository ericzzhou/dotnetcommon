/********************************************************
 * Class：PDFCS
 * Description：
 * Create Date:2011-11-30 17:28:39
 * Create Year:2011 
 * Update Date:
 * Author：zhouzhaokun
 *******************************************************/
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace DotNet.Framework.Common.PDF
{
    public class PDFCS
    {
        PDFCommon pdfCommon = new PDFCommon();

        #region 属性
        public string CSID { get; set; }
        public string ContactData { get; set; }
        public string ContactTime { get; set; }
        /// <summary>
        /// 填表人
        /// </summary>
        public string EntrierCode { set; get; }
        public string EntryDate { get; set; }
        public string ContactType { get; set; }
        public string ContactWay { get; set; }
        public string ContacterName { get; set; }
        public string ContacterPhone { get; set; }
        /// <summary>
        /// 受理人
        /// </summary>
        public string ReceiverCode { get; set; }
        public string ReceiveData { get; set; }
        public string ReceiveTime { get; set; }
        public string ReceiveProgressCode { get; set; }
        public string IsFeedback { get; set; }
        public string FeedBackWay { get; set; }
        public string FeedbackerCode { get; set; }
        public string FeedBackDate { get; set; }
        public string FeedBackTime { get; set; }
        public string ComplaintContent { get; set; }
        public string OfVerificationContent { get; set; }
        public string HandleViews { get; set; }
        public string HandleResult { get; set; }
        public string ReturnVisitContent { get; set; }
        public string ReturnVisitDate { get; set; }
        public string ReturnVisitTime { get; set; }
        public string ReturnVisterCode { get; set; }
        public string ReturnVisterWay { get; set; }
        public string IsComplete { get; set; }
        public string IsArchive { get; set; }
        public string ArchiveTime { get; set; }
        public string ArchivePeople { get; set; }
        public string ArchiveReason { get; set; }
        public string EndTime { get; set; }
        public string EndPeople { get; set; }
        public string EndReason { get; set; }

        public string EntryTime { get; set; }
        public string ContacterCode { get; set; }

        public string Path { get; set; }
        public string FileName { get; set; }
        #endregion


        #region 构造函数
        public PDFCS(

    string CSID,
    string ContactData,
    string ContactTime,
    string EntrierCode,
    string EntryDate,
    string EntryTime,
    string ContactType,
    string ContactWay,
    string ContacterCode,
    string ContacterName,
    string ContacterPhone,
    string ReceiverCode,
    string ReceiveData,
    string ReceiveTime,
    string ReceiveProgressCode,
    string IsFeedback,
    string FeedBackWay,
    string FeedbackerCode,
    string FeedBackDate,
    string FeedBackTime,
    string ComplaintContent,
    string OfVerificationContent,
    string HandleViews,
    string HandleResult,
    string ReturnVisitContent,
    string ReturnVisitDate,
    string ReturnVisitTime,
    string ReturnVisterCode,
    string ReturnVisterWay,
    string IsComplete,
    string IsArchive,
    string ArchiveTime,
    string ArchivePeople,
    string ArchiveReason,
    string EndTime,
    string EndPeople,
    string EndReason,
              string Path, string FileName
            )
        {

            this.CSID = CSID;
            this.ContactData = ContactData;
            this.ContactTime = ContactTime;
            this.EntrierCode = EntrierCode;
            this.EntryDate = EntryDate;
            this.EntryTime = EntryTime;
            this.ContactType = ContactType;
            this.ContactWay = ContactWay;
            this.ContacterCode = ContacterCode;
            this.ContacterName = ContacterName;
            this.ContacterPhone = ContacterPhone;
            this.ReceiverCode = ReceiverCode;
            this.ReceiveData = ReceiveData;
            this.ReceiveTime = ReceiveTime;
            this.ReceiveProgressCode = ReceiveProgressCode.Replace("<br/>", "\n");
            this.IsFeedback = IsFeedback;
            this.FeedBackWay = FeedBackWay;
            this.FeedbackerCode = FeedbackerCode;
            this.FeedBackDate = FeedBackDate;
            this.FeedBackTime = FeedBackTime;
            this.ComplaintContent = ComplaintContent;
            this.OfVerificationContent = OfVerificationContent;
            this.HandleViews = HandleViews;
            this.HandleResult = HandleResult;
            this.ReturnVisitContent = ReturnVisitContent;
            this.ReturnVisitDate = ReturnVisitDate;
            this.ReturnVisitTime = ReturnVisitTime;
            this.ReturnVisterCode = ReturnVisterCode;
            this.ReturnVisterWay = ReturnVisterWay;
            this.IsComplete = IsComplete;
            this.IsArchive = IsArchive;
            this.ArchiveTime = ArchiveTime;
            this.ArchivePeople = ArchivePeople;
            this.ArchiveReason = ArchiveReason;
            this.EndTime = EndTime;
            this.EndPeople = EndPeople;
            this.EndReason = EndReason;
            this.Path = Path;
            this.FileName = FileName;
        }
        #endregion

        /// <summary>
        /// 生成PDF文件
        /// </summary>
        /// <returns>返回PDF文件全路径</returns>
        public string CreatePDF()
        {
            ///生成的PDF文件路径 
            ///"PDF/001.pdf";
            string Tpath = this.Path + this.FileName;

            try
            {
                ///声明一个 操作pdf的对象  参数：纸张大小:A5,50则是上下左右边距   下一句表示横排显示
                using (Document document = new Document(PageSize.A4, 50, 50, 50, 50))
                {
                    ///创建PDF，如果已经存在同名文件，则覆盖
                    using (PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath(Tpath), FileMode.Create)))
                    {
                        //打开创建好的PDF文件
                        document.Open();
                        #region 创建图片的方法 (注释掉的)
                        ///创建图像
                        //iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(Server.MapPath("img.jpg"));
                        ///添加图像
                        //document.Add(jpeg);
                        #endregion
                        //添加页眉
                        AddHeader(document);
                        //追加核心数据
                        AppendData(document);
                        //追加页脚
                        AppendBottom(document);
                        ///关闭文档
                        document.Close();
                        //Response.Redirect(FilePath);
                    }
                }
            }
            catch (DocumentException de)
            {
                Tpath = "";
            }
            return Tpath;
        }

        #region 追加页脚信息
        /// <summary>
        /// 追加页脚信息
        /// </summary>
        /// <param name="document"></param>
        private void AppendBottom(Document document)
        {
            //PdfPTable tableBottom = new PdfPTable(4);

            //pdfCommon.AddCellBottom("接待厅班组长审核：", 0, tableBottom);
            //pdfCommon.AddCellBottom("", 0, tableBottom);
            //pdfCommon.AddCellBottom("持证人签字：", 0, tableBottom);
            //pdfCommon.AddCellBottom("", 0, tableBottom);
            //document.Add(tableBottom);
        }
        #endregion


        #region 把PdfPTable数据追加到 Document 对象
        /// <summary>
        /// 把PdfPTable数据追加到 Document 对象
        /// </summary>
        /// <param name="document"></param>
        private void AppendData(Document document)
        {
            //创建 Table
            PdfPTable table = new PdfPTable(4);
            //设置 PdfPTable 的基础样式
            pdfCommon.SetPTableStyle(table, new float[] { 0.4f, 0.6f, 0.4f, 0.6f }, 95);
            //填充数据
            AddTableData(table);
            //添加Table
            document.Add(table);
        }
        #endregion

        /// <summary>
        /// 添加 页眉 到 Document 对象中
        /// </summary>
        /// <param name="document"></param>
        private void AddHeader(Document document)
        {
            //标题居中
            Paragraph paragraph = new Paragraph("客户投诉建议记录表", pdfCommon.FontTitle);
            paragraph.Alignment = Element.ALIGN_CENTER;

            document.Add(paragraph);

            paragraph =
            new Paragraph(
            "\n\n     记录编号：" + this.CSID + "\n\n", new iTextSharp.text.Font(pdfCommon.BaseFont, 9));
            paragraph.Alignment = Element.ALIGN_LEFT;
            document.Add(paragraph);
        }

        /// <summary>
        /// 往PdfPTable填充数据
        /// </summary>
        /// <param name="table"></param>
        private void AddTableData(PdfPTable table)
        {
            //第一行
            pdfCommon.AddCell("联络日期", 0, table, true, true);
            pdfCommon.AddCell(this.ContactData, 0, table, false, false);
            pdfCommon.AddCell("联络时间", 0, table, true, true);
            pdfCommon.AddCell(this.ContactTime, 0, table, false, false);
            //第二行
            pdfCommon.AddCell("填 表 人", 0, table, true, true);
            pdfCommon.AddCell(this.EntrierCode, 0, table, false, false);
            pdfCommon.AddCell("填表日期", 0, table, true, true);
            pdfCommon.AddCell(this.EntryDate, 0, table, false, false);
            //第三行
            pdfCommon.AddCell("联络类型", 0, table, true, true);
            pdfCommon.AddCell(this.ContactType, 0, table, false, false);
            pdfCommon.AddCell("联络途径", 0, table, true, true);
            pdfCommon.AddCell(this.ContactWay, 0, table, false, false);

            pdfCommon.AddCell("联 络 人", 0, table, true, true);
            pdfCommon.AddCell(this.ContacterName, 0, table, false, false);
            pdfCommon.AddCell("联系方式", 0, table, true, true);
            pdfCommon.AddCell(this.ContacterPhone, 0, table, false, false);

            pdfCommon.AddCell("受 理 人", 0, table, true, true);
            pdfCommon.AddCell(this.ReceiverCode, 0, table, false, false);
            pdfCommon.AddCell("受理日期", 0, table, true, true);
            pdfCommon.AddCell(this.ReceiveData, 0, table, false, false);

            pdfCommon.AddCell("受理进度", 0, table, true, true);
            pdfCommon.AddCell(this.ReceiveProgressCode, 3, table, false, false);

            pdfCommon.AddCell("是否反馈", 0, table, true, true);
            pdfCommon.AddCell(this.IsFeedback, 0, table, false, false);
            pdfCommon.AddCell("反馈方式", 0, table, true, true);
            pdfCommon.AddCell(this.FeedBackWay, 0, table, false, false);

            pdfCommon.AddCell("反 馈 人", 0, table, true, true);
            pdfCommon.AddCell(this.FeedbackerCode, 0, table, false, false);
            pdfCommon.AddCell("反馈时间", 0, table, true, true);
            pdfCommon.AddCell(this.FeedBackDate, 0, table, false, false);

            pdfCommon.AddCell("投诉内容", 0, table, true, true);
            pdfCommon.AddCell(this.ComplaintContent, 3, table, false, false);

            pdfCommon.AddCell("情况核实", 0, table, true, true);
            pdfCommon.AddCell(this.OfVerificationContent, 3, table, false, false);

            pdfCommon.AddCell("处理意见", 0, table, true, true);
            pdfCommon.AddCell(this.HandleViews, 3, table, false, false);

            pdfCommon.AddCell("处理结果", 0, table, true, true);
            pdfCommon.AddCell(this.HandleResult, 3, table, false, false);

            pdfCommon.AddCell("客户回访", 0, table, true, true);
            pdfCommon.AddCell(this.ReturnVisitContent, 3, table, false, false);

        }


    }
}
