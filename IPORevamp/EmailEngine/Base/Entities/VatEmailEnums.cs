using System;
using System.Collections.Generic;
using System.Text;

namespace EmailEngine.Base.Entities
{
    public enum IPOEmailTemplateType
    {
        AccountCreation_ = 1,
        PasswordReset,
        AccountCreation,
        AttendeeRegistration,
        ResendCOnfirmationLink
    }

    public enum IPOEmailStatus
    {
        Fresh = 1,
        Sent,
        Failed
    }

   

    public enum IPOCategory
    {
       Individual = 1 ,
       Agent = 2
    }

    public enum IPOPATENTTYPE
    {
        CONVENTIONAL = 1,
        NONCONVENTIONAL = 2
    }

    public enum IPORoles
    {
        SuperAdministrator = 1,
        Administrator =2,
        CorporateAgent_Trade_Mark  =3,
        Search_Officer_Trade_Mark = 5,
        Search_Officer_Patent = 22,
        Examination_Officer_Trade_Mark = 6,
        Publication_Officer_Trade_Mark =7,
        Certificate_Officer_Trade_Mark =9,
        Appeal_Officer_Trade_Mark = 10,
        Opposition_Officer_Trade_Mark =17,
        Recordals_Officers_Trade_Mark = 12  ,
        Individual =13,
        Patent_Examiner =21,
        Registrar =8


    }

    public class DEPARTMENT
    {
        public const string Trademark = "3";
        public const string Patent = "1";

    }

        public class DATASTATUS
    {
        
        public const string Certificate = "Certificate";
        public const string Opposition = "Opposition";
        public const string Search = "Search";
        public const string Examiner = "Examiner";
       
        public const string Publication = "Publication";
        public const string Kiv = "Kiv";
        public const string ReconductSearch = "Reconduct-Search";
        public const string ApplicantKiv = "ApplicantKiv";
        public const string Recordal = "Recordal";

        




    }


    public class STATUS
    {
        public const string Fresh = "Fresh";
        public const string Paid = "Paid";
        public const string Pending = "Pending";
        public const string Batch = "Batch";
        public const string Kiv = "Kiv";
        public const string Counter = "Counter";
        public const string Submitted = "Submitted";
        public const string ReceiveAppeal = "ReceiveAppeal";
        public const string DelegateAppeal = "DelegateAppeal";
        public const string ReconductSearch = "ReconductSearch";
        
        public const string AutoMoveComment = "Auto Move To Certificate By Admin";

        public const string ApplicantKiv = "ApplicantKiv";
        public const string Refused = "Refused";
        public const string Judgement = "Judgement";
        public const string Applicant = "Applicant";
        public const string Registra = "Registra";
        public const string Appeal = "Appeal";
        public const string Approved = "Approved";
        public const string Merger = "Merger";
        public const string Renewal = "Renewal";
        public const string RecordalRenewalComment = "Recordal Renewal";

        


























    }

        public class IPOCONSTANT
    {
        public const string ACTIVATIONCODE = "ACTIVATIONEMAIL";
        public const string Individual_Account_Verification = "IV001";
        public const string Corporate_Account_Verification = "AV002";
        public const string Account_Creation = "AC003";
        public const string CHANGE_PASSWORD_FIRST_LOGIN_NOTIFICATION = "AC004";
        public const string FORGOT_PASSWORD_EMAIL_TEMPLATE = "AC005";
        public const string Admin_User = "AC006";
        public const string Send_Registra_Mail = "AC007";
        public const string PublicationDue_Mail = "ACP007";
        public const string CertificatePayment = "ACP008";
        public const string NotifyUserOfOpposition = "ACP009";

        

        public const string Preliminary_Search = "PRELIM001";
        public const string Invoice = "RECPT001";
        public const string Receipt = "RECPT002";
        public const string Acceptance = "ACP001";
        public const string PatentAcceptance = "ACP013";
        public const string Refusal = "ACP002";
        public const string NoticeOfOpposition = "ACP003";
        public const string RegistrartoAppealUnit = "ACP004";
        public const string ApplicationSentToOpposition = "ACP005";
        public const string AppealReply = "ACP006";
        public const string SentToKiv = "ACP010";

        public const string SendPatentExaminerEmail = "ACP011";
        public const string ApplicationAccepted = "ACP012";


        public const string PublicationMaxDay = "Pub";
        public const string PublicationCount = "PubCount";

        

        public const string PAYMENT_NOTIFICATION = "PAY001";

        public const int Individual_Account = 1;

    }


}

