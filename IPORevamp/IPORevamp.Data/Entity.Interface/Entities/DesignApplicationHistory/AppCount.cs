using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory
{
   public class AppCount
    {
        public int trademarkcount { get; set; }
        public int patentcount { get; set; }
        public int designcount { get; set; }
        public int trademarkmigrationcount { get; set; }
        public int patentmigrationcount { get; set; }
        public int designmigrationcount { get; set; }
    }



    public class DashBoardCount
    {
        public int newapplicationcount { get; set; }
        public int treatedApplicationcount { get; set; }
        public int ResearchApplicationcount { get; set; }
        public int TrademarkExaminerFreshcount { get; set; }
        public int TrademarkExaminerTreatedcount { get; set; }
        public int TrademarkPublicationFreshcount { get; set; }
        public int TrademarkPublicationPublishcount { get; set; }
        public int TrademarkUnPublishcount { get; set; }
        public int TrademarkPaidCertificatecount { get; set; }
        public int TrademarkIssuedCertificatecount { get; set; }
        public int TrademarkRenewedCertificatecount { get; set; }
        public int TrademarkOppositionFreshCount { get; set; }
        public int TrademarkOppositionJudgementCount { get; set; }


    }


    public class DashBoardPatentCount
    {
        public int newapplicationcount { get; set; }
        public int treatedApplicationcount { get; set; }
        public int ResearchApplicationcount { get; set; }
        public int PatentExaminerFreshcount { get; set; }
        public int PatentExaminerTreatedcount { get; set; }
        public int PatentPaidCertificatecount { get; set; }
        public int PatentIssuedCertificatecount { get; set; }
        public int TrademarkAppealcount { get; set; }
        public int PatentAppealcount { get; set; }
        public int DesignAppealcount { get; set; }
        public int Usercount { get; set; }



    }


    public class DashBoardDesignCount
    {
        public int newapplicationcount { get; set; }
        public int treatedApplicationcount { get; set; }
        public int ResearchApplicationcount { get; set; }
        public int DesignExaminerFreshcount { get; set; }
        public int DesignExaminerTreatedcount { get; set; }
        public int DesignPaidCertificatecount { get; set; }
        public int DesignIssuedCertificatecount { get; set; }

        public int DesignOppositionFreshCount { get; set; }
        public int DesignOppositionJudgementCount { get; set; }
        public int DesignRenewedCertificatecount { get; set; }

        public int DesignPublicationFreshcount { get; set; }
        public int DesignPublicationPublishcount { get; set; }
        public int DesignUnPublishcount { get; set; }



    }
}
