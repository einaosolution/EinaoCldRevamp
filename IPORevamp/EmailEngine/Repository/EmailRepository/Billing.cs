using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EmailEngine.Base.Entities;
using EmailEngine.Base.Entities.Models;
using EmailEngine.Repository.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IPORevamp.Core.Utilities;

namespace EmailEngine.Repository.EmailRepository
{
    public class Billing<TBill, TPayment, TUser, TUserKey> : IBilling<TBill, TPayment, TUser, TUserKey> where TBill:VatBill<TUser,TUserKey,TPayment> where TPayment: VatPaymentLog<TUser, TUserKey> where TUser:IdentityUser<TUserKey> where TUserKey: IEquatable<TUserKey>
    {

        private readonly IGenericRepository<TBill> _repository;
        private readonly IGenericRepository<TPayment> _paymentRepository;        

        public Billing(IGenericRepository<TBill> repository, IGenericRepository<TPayment> paymentRepository)
        {
            _repository = repository;
            _paymentRepository = paymentRepository;
        }
        
        public async Task<TBill> GenerateBillLog(TBill billInfo, string billPrefix = "IPOBL")
        {
            billInfo.BillRefenceNo = GenerateBillRefNo(billPrefix);
            if (!string.IsNullOrEmpty(billInfo.BillRefenceNo.Trim()))
            {
                await _repository.InsertOrUpdateAsync(billInfo);
                await _repository.SaveChangesAsync();

                return billInfo;
            }

            throw new Exception("Bill reference number can not be emty, whitespace or null");            
        }

        public async Task<string> UpdateBillLog(TBill billInfo)
        {
            if (!string.IsNullOrEmpty(billInfo.BillRefenceNo.Trim()))
            {
                await _repository.UpdateAsync(billInfo);
                await _repository.SaveChangesAsync();

                return billInfo.BillRefenceNo;
            }

            throw new Exception("Bill reference number can not be emty, whitespace or null");
        }

        private string GenerateBillRefNo(string prefix)
        {
            var suffix = Utilities.GenerateRandomString(7);
            var randomString = prefix + suffix;
            
            while (_repository.GetAllList(x => x.BillRefenceNo == randomString).Any())
            {
                suffix = Utilities.GenerateRandomString();
                randomString = prefix + suffix;
            }

            return randomString;
        }                

        private string GeneratePaymentRefNo(string prefix)
        {
            var suffix = Utilities.GenerateRandomString(7);
            var randomString = prefix + suffix;
            while (_paymentRepository.GetAllList(x => x.PaymentReferenceNo == randomString).Any())
            {
                suffix = Utilities.GenerateRandomString();
                randomString = prefix + suffix;
            }

            return randomString;
        }

        public async Task<string> GeneratePayment(int billId, TPayment payment, string prefix = "IPOPY")
        {
            payment.PaymentReferenceNo = GeneratePaymentRefNo(prefix);
            if(!string.IsNullOrEmpty(payment.PaymentReferenceNo))
            {
                var bill = _repository.GetById(billId);
                if (bill != null)
                {
                    bill.PaymentDetails.Add(payment);
                    await _repository.SaveChangesAsync();

                    return payment.PaymentReferenceNo;
                }
            }

            throw new Exception("Payment reference could not be generated");
        }

        public async Task<PaymentDetails> GetPaymentReceiptAsync(string transactionRef)
        {
            return await _paymentRepository.GetAll()
                   .Include(x => x.User)
                   .Where(x => x.PaymentReferenceNo == transactionRef)
                   .Select(x => new PaymentDetails
                   {
                       Amount = x.Amount,
                       //FullName = x.User.First,
                       Email = x.User.Email,
                       Phone = x.User.PhoneNumber,
                       TechFee = x.TechFee,
                       Total = x.Amount + x.TechFee,
                       TransactionRef = x.PaymentReferenceNo,
                       //billRefenceNo = x. //VtbBillLog.BillRefenceNo,
                       //BillStatus = x.VtbBillLog.BillStatus
                   }).FirstOrDefaultAsync();
        }

        
    }
}
