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
        public const string PAYMENT_NOTIFICATION = "PAY001";
        public const int Individual_Account = 1;

    }


}

