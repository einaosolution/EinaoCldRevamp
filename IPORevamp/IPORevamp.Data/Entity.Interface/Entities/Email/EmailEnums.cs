using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities.Email
{
    public enum EmailTemplateType
    {
        Account = 1
    }

    public enum EmailStatus
    {
        Fresh = 1,
        Sent,
        Failed
    }
}

