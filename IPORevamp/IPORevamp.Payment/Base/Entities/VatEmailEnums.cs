using System;
using System.Collections.Generic;
using System.Text;

namespace EmailEngine.Base.Entities
{
    public enum IPOEmailTemplateType
    {
        AccountCreation = 1,
        PasswordReset,
        QuoteRequestAutoResponse,
        AttendeeRegistration,
        ResendCOnfirmationLink
    }

    public enum IPOEmailStatus
    {
        Fresh = 1,
        Sent,
        Failed
    }
}

