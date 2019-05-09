using EmailEngine.Base.Entities;
using EmailEngine.Base.Entities.Models;
using EmailEngine.Repository.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailEngine.Repository.EmailRepository
{
    public interface IBilling<TBill, TPayment, TUser, TUserKey> where TBill:VatBill<TUser, TUserKey, TPayment> where TUser: IdentityUser<TUserKey> where TUserKey: IEquatable<TUserKey> where TPayment:VatPaymentLog<TUser, TUserKey>
    {

        Task<TBill> GenerateBillLog(TBill billInfo, string billPrefix = "IPO");
        Task<string> GeneratePayment(int billId, TPayment payment, string prefix = "IPOPY");
        Task<PaymentDetails> GetPaymentReceiptAsync(string transactionRef);        
    }
}
