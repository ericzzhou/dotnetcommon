using System;
using DotNet.Framework.Common.PDF;

/********************************************************
 * Class：_default
 * Description：
 * Create Date:2011-11-30 17:29:33
 * Create Year:2011 
 * Update Date:
 * Author：zhouzhaokun
 *******************************************************/
namespace Demo.PDF
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CreatePDF("");
        }
        private void CreatePDF(string code)
        {
            string Path = "~/PDF/";
            string FileName = Session.SessionID + ".pdf";
            string CSID = code;
            string ContactData = "";
            string ContactTime = "";
            string EntrierCode = "";
            string EntryDate = "";
            string EntryTime = "";
            string ContactType = "";
            string ContactWay = "";
            string ContacterCode = "";
            string ContacterName = "";
            string ContacterPhone = "";
            string ReceiverCode = "";
            string ReceiveData = "";
            string ReceiveTime = "";
            string ReceiveProgressCode = "";
            string IsFeedback = "";
            string FeedBackWay = "";
            string FeedbackerCode = "";
            string FeedBackDate = "";
            string FeedBackTime = "";
            string ComplaintContent = "";
            string OfVerificationContent = "";
            string HandleViews = "";
            string HandleResult = "";
            string ReturnVisitContent = "";
            string ReturnVisitDate = "";
            string ReturnVisitTime = "";
            string ReturnVisterCode = "";
            string ReturnVisterWay = "";
            string IsComplete = "";
            string IsArchive = "";
            string ArchiveTime = "";
            string ArchivePeople = "";
            string ArchiveReason = "";
            string EndTime = "";
            string EndPeople = "";
            string EndReason = "";




            PDFCS pdf = new PDFCS(CSID, ContactData, ContactTime, EntrierCode, EntryDate, EntryTime, ContactType, ContactWay, ContacterCode, ContacterName, ContacterPhone, ReceiverCode, ReceiveData, ReceiveTime, ReceiveProgressCode, IsFeedback, FeedBackWay, FeedbackerCode, FeedBackDate, FeedBackTime, ComplaintContent, OfVerificationContent, HandleViews, HandleResult, ReturnVisitContent, ReturnVisitDate, ReturnVisitTime, ReturnVisterCode, ReturnVisterWay, IsComplete, IsArchive, ArchiveTime, ArchivePeople, ArchiveReason, EndTime, EndPeople, EndReason, Path, FileName);
            string SuccessPDFFilePath = pdf.CreatePDF();
            if (SuccessPDFFilePath != "")
            {
                Response.Redirect(SuccessPDFFilePath);
            }

        }
    }
}
