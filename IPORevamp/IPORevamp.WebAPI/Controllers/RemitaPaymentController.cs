
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data.UserManagement.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EmailEngine.Base.Repository.EmailRepository;
using IPORevamp.Data.Entities.Email;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;

using IPORevamp.Data;
using IPORevamp.Data.TempModel;
using IPORevamp.Repository.Interface;

using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entities.Setting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Authorization;

using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data.SetupViewModel;
using IPORevamp.Data.Entities.Country;
using IPORevamp.Repository.Country;
using IPORevamp.Core.Utilities;
using Newtonsoft.Json;
using IPORevamp.Repository.RemittaPayment;
using IPORevamp.Data.Entity.Interface.Entities.RemitaPayment;
using IPORevamp.Repository.Fee;
using System.Net.Http;
using IPORevamp.Repository.RemitaAccountSplit;
using IPORevamp.Repository.Email;
using IPORevamp.Repository.RemitaLineItem;
using System.Text;
using IPORevamp.WebAPI.Utilities;
using System.Globalization;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/RemitaPayment")]
    [ApiController]
    public class RemitaPaymentController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;


        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRemitaPaymentRepository _remitaPaymentRepository;
        private readonly IFeeListRepository _feeRepository;
        private readonly IRemitaAccountSplitRepository _remitaSplitRepository;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;
        private readonly IRemitaLineItemRepository _RemitaLineItemRepository;

        public RemitaPaymentController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,

            IRemitaPaymentRepository remitaPaymentRepository,
            IFeeListRepository feeRepository,

              IEmailTemplateRepository EmailTemplateRepository,
              IRemitaLineItemRepository RemitaLineItemRepository,

            IEmailSender emailsender,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment,
            IAuditTrailManager<AuditTrail> auditTrailManager, IRemitaAccountSplitRepository remitaSplitRepository




            ) : base(
                userManager,
                signInManager,
                roleManager,
                configuration,
                mapper,
                logger,
                auditTrailManager

                )
        {
            _emailManager = emailManager;

            _emailsender = emailsender;
            _httpContextAccessor = httpContextAccessor;
            _remitaPaymentRepository = remitaPaymentRepository;
            _feeRepository = feeRepository;
            _remitaSplitRepository = remitaSplitRepository;
            _EmailTemplateRepository = EmailTemplateRepository;
            _RemitaLineItemRepository = RemitaLineItemRepository;
        }



        //[Authorize]
        /// <summary>
        /// This method will get country and related States
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>


        private string SHA512(string hash_string)
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            string hashed = BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
            return hashed;
        }







        // Generate Remita Payment RRR

        [HttpPost("InitiateRemitaPayment")]
        public async Task<IActionResult> InitiatePayment(RemitaPaymentModel remitaPaymentModelPost)
        {
            try
            {


                var user = _userManager.Users.FirstOrDefault(x => x.Id == remitaPaymentModelPost.UserId);

                if (user != null)
                {

                    if (user.FirstName == null && user.LastName == null)
                    {

                        return PrepareResponse(HttpStatusCode.InternalServerError, "Customer's name is required, please update your profile and try again");
                    }

                    if (user.Email == null)
                    {
                        return PrepareResponse(HttpStatusCode.InternalServerError, "Customer's email is required, please update your profile and try again");
                    }

                    string fullname = user.FirstName + "  " + user.LastName;
                    var rrr = await GenerateRemitaRRRCode(remitaPaymentModelPost.FeeIds, fullname, user.Email, user.PhoneNumber);

                    if (rrr != null && rrr.statuscode != "x")
                    {



                        return PrepareResponse(HttpStatusCode.OK, "RRR has been generated successfully", false, rrr);
                    }

                    return PrepareResponse(HttpStatusCode.InternalServerError, "Error occured while generating RRR", true, rrr);

                }

                return PrepareResponse(HttpStatusCode.NotFound, "User can not be found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Save InitiateRemitaPayment", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        //  Make Payment 


        private async Task<RemitaPaymentResponseModel> GenerateRemitaRRRCode(int[] feeIds, string payerName, string payerEmail, string payerPhone)
        {
            var feeItems = await _feeRepository.GetFeeListById(feeIds);


            decimal totalTechnologyFee = 0;
            decimal totalMerchantFee = 0;
            RemitaPaymentResponseModel errorresult = new RemitaPaymentResponseModel();

            if (feeItems.Count == 0)
            {
                errorresult.status = "No Fee Setup was found for the selected payment";
                errorresult.statuscode = "x";
                return errorresult;
            }

            var remitaPaymentwithoutRRR = new RemitaPaymentPayLoad();

            if (feeItems != null)
            {
                var orderId = "IPONMW" + DateTime.Now.Ticks;

                // loop through all the fee list items

                foreach (var item in feeItems)
                {

                    // insert each of the amount for the above filing
                    await _remitaPaymentRepository.SaveRemitaPayment(new RemitaPayment
                    {
                        Amount = item.init_amt,
                        TechFee = item.TechnologyFee,
                        OrderId = orderId,
                        PayerEmail = payerEmail,
                        PayerName = payerName,
                        PayerPhone = payerPhone,
                        TotalAmount = (item.init_amt + item.TechnologyFee).ToString(),
                        FeeItemName = item.ItemName,
                        FeeId = item.Id,
                        TransactionInitiatedDate = DateTime.Now


                    });

                    // get all the total cost to be pass to gateway later
                    totalTechnologyFee = totalTechnologyFee + item.TechnologyFee;
                    totalMerchantFee = totalMerchantFee + item.init_amt;

                }

                var remitaPayements = await _remitaPaymentRepository.FetchByOrderId(orderId);

                if (remitaPayements != null)
                {
                    remitaPaymentwithoutRRR = new RemitaPaymentPayLoad
                    {


                        amount = remitaPayements.Sum(x => Convert.ToDecimal(x.TotalAmount)),

                        orderId = orderId,
                        payerEmail = payerEmail,
                        payerPhone = payerPhone,
                        payerName = payerName,
                        description = "Payment For Service on IPO Nigeria",


                        serviceTypeId = _configuration.GetValue<string>("ServiceTypeId")
                    };


                    List<RemitaLineItem> lineItems = new List<RemitaLineItem>();
                    List<RemitaLineItem> lineItemsHolder = new List<RemitaLineItem>();

                    var splitAccount = await _remitaSplitRepository.GetRemitaAccountSplits();

                    int RowNumber = 0;
                    foreach (var item in splitAccount)
                    {
                        RowNumber = RowNumber + 1;
                        RemitaLineItem k = new RemitaLineItem();

                        k.lineItemsId = RowNumber.ToString();
                        k.bankCode = item.BeneficiaryBank;
                        k.beneficiaryAccount = item.BeneficiaryAccount;


                        if (item.DeductFee == "1")
                        {
                            k.beneficiaryAmount = totalMerchantFee.ToString();
                        }
                        else
                        {
                            k.beneficiaryAmount = totalTechnologyFee.ToString();
                        }

                        k.beneficiaryName = item.BeneficiaryName;
                        k.orderId = orderId;
                        k.deductFeeFrom = item.DeductFee;

                        lineItems.Add(k);
                    };

                    // insert the line items 


                    foreach (var item in lineItems)
                    {

                        LineItem line = new LineItem();
                        line.BankCode = item.bankCode;
                        line.BeneficiaryAccount = item.beneficiaryAccount;
                        line.BeneficiaryAmount = item.beneficiaryAmount;
                        line.BeneficiaryName = item.beneficiaryName;
                        line.CreatedBy = payerName;
                        line.DateCreated = DateTime.Now;
                        line.DeductFeeFrom = item.deductFeeFrom;
                        line.IsActive = true;
                        line.OrderId = item.orderId;

                        await _RemitaLineItemRepository.SaveLineItem(line);

                    }

                    remitaPaymentwithoutRRR.lineItems = lineItems;

                    var merchantID = _configuration.GetValue<string>("merchantID");
                    var apiKey = _configuration.GetValue<string>("apiKey");
                    var hash = SHA512(merchantID + remitaPaymentwithoutRRR.serviceTypeId + remitaPaymentwithoutRRR.orderId + remitaPaymentwithoutRRR.amount + apiKey);
                    var respModel = new RemitaPaymentResponseModel();

                    WebClient webClient = new WebClient
                    {
                        UseDefaultCredentials = true,
                        Headers =
                        {
                            ["Authorization"] = $"remitaConsumerKey={merchantID},remitaConsumerToken={hash}",

                        }
                    };

                    try
                    {
                        var ttry = JsonConvert.SerializeObject(remitaPaymentwithoutRRR);
                        var URL = _configuration.GetValue<string>("RemitaRRURL");

                        webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                        var remitaResponse = webClient.UploadString(new Uri(URL), "POST", ttry);

                        string jsondata = "";
                        jsondata = remitaResponse.Replace("jsonp (", "");
                        jsondata = jsondata.Replace(")", "");
                        jsondata = jsondata.Replace("jsonp", "");


                        SplitResponseVO remitaResponseSerialized = JsonConvert.DeserializeObject<SplitResponseVO>(jsondata);

                        if (remitaResponseSerialized.statuscode == "025" && !string.IsNullOrEmpty(remitaResponseSerialized.RRR))
                        {

                            RemitaPaymentResponseModel remitaPaymentResponseModel = new RemitaPaymentResponseModel();


                            //Update Remita payment with RRR code _remitaPaymentRepository.UpdatePAy
                            remitaPaymentResponseModel.Hash = SHA512(merchantID + remitaResponseSerialized.RRR + apiKey);
                            remitaPaymentResponseModel.MerchantId = merchantID;
                            remitaPaymentResponseModel.RRR = remitaResponseSerialized.RRR;
                            remitaPaymentResponseModel.status = remitaResponseSerialized.status;
                            remitaPaymentResponseModel.statuscode = remitaResponseSerialized.statuscode;

                            /// update the remita table with the response 

                            var updateInfo = await _remitaPaymentRepository.FetchByOrderId(orderId);

                            /// update all the payments request by order number
                            foreach (var itemResult in updateInfo)
                            {
                                itemResult.RRR = remitaResponseSerialized.RRR;
                                itemResult.RemitaPostPayLoad = ttry;
                                itemResult.RemitaResponsePayLoad = remitaResponse;

                                // update all the requests
                                await _remitaPaymentRepository.UpdateRemitaPayment(itemResult);

                            }

                            SendPaymentNotification(remitaResponseSerialized.RRR);

                            return remitaPaymentResponseModel;
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occured connecting to RRR", "");
                        return null;
                    }


                }

            }

            errorresult.status = "No Fee Setup was found for the selected payment";
            errorresult.statuscode = "x";
            return errorresult;
        }



        [HttpPost("RemitaTransactionRequeryPayment")]
        public async Task<IActionResult> VerifyPayment(RequeryRemitaModel requeryModel)
        {
            var paymentDetails = await _remitaPaymentRepository.FetchByRRRCode(requeryModel.RRR);


            if (paymentDetails != null)
            {
                var singleRecord = paymentDetails.FirstOrDefault();
                if (!string.IsNullOrEmpty(singleRecord.RRR) && singleRecord.RRR == requeryModel.RRR)
                {
                    var merchantID = _configuration.GetValue<string>("merchantID");
                    var apiKey = _configuration.GetValue<string>("apiKey");

                    var hash = SHA512(singleRecord.OrderId + apiKey + merchantID);

                    WebClient webClient = new WebClient
                    {
                        UseDefaultCredentials = true,
                        Headers =
                        {
                            ["Authorization"] = $"remitaConsumerKey={merchantID},remitaConsumerToken={hash}"
                        }
                    };

                    try
                    {


                        var remitaResponseStr = (_configuration.GetValue<string>("RemitaPaymentVerificationURL") + "/" + merchantID + "/" + singleRecord.OrderId + "/" + hash + "/" + "orderstatus.reg");

                        // var URL = _configuration.GetValue<string>("RemitaRRURL");

                        webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                        var remitaResponsek = webClient.UploadString(new Uri(remitaResponseStr), "POST");

                        string jsondata = "";
                        jsondata = remitaResponsek.Replace("jsonp (", "");
                        jsondata = jsondata.Replace(")", "");
                        jsondata = jsondata.Replace("jsonp", "");


                        RemitaQueryResponseModel remitaResponseSerialized = JsonConvert.DeserializeObject<RemitaQueryResponseModel>(jsondata);

                        // Update the table with the return results

                        var updateInfo = await _remitaPaymentRepository.FetchByOrderId(singleRecord.OrderId);


                        /// update all the payments request by order number
                        foreach (var itemResult in updateInfo)
                        {
                            itemResult.Statuscode = remitaResponseSerialized.status;
                            itemResult.Description = remitaResponseSerialized.message;
                            itemResult.RemitaPostVerifyPayLoad = remitaResponseStr;
                            itemResult.RemitaResponseVerifyPayLoad = jsondata;
                            itemResult.Status = remitaResponseSerialized.status;

                            itemResult.PaymentDate = DateTime.Now;

                            if ((remitaResponseSerialized.status == "01" || remitaResponseSerialized.status == "00") && remitaResponseSerialized.message == WebApiMessage.ApprovedPaymentStatus)
                            {
                                itemResult.TransactionCompletedDate = DateTime.Now;
                                itemResult.PaymentStatus = WebApiMessage.SuccessfullyPayment;
                            }

                            // update all the requests
                            await _remitaPaymentRepository.UpdateRemitaPayment(itemResult);

                        }



                        if (remitaResponseSerialized.status == "01" && remitaResponseSerialized.message == WebApiMessage.ApprovedPaymentStatusRemita)
                        {
                            return PrepareResponse(HttpStatusCode.OK, "Payment has been completed successfully", false, remitaResponseSerialized);
                        }
                        else if (remitaResponseSerialized.status == "021" && remitaResponseSerialized.message == WebApiMessage.TransactionPending)
                        {
                            return PrepareResponse(HttpStatusCode.Processing, "Payment has been completed but pending approval, please check back later ", true, remitaResponseSerialized);
                        }
                        else
                        {
                            return PrepareResponse(HttpStatusCode.InternalServerError, remitaResponseSerialized.message);
                        }

                    }
                    catch (Exception ex)
                    {
                        return PrepareResponse(HttpStatusCode.InternalServerError, "Error occured while connecting to Remita");
                    }

                }

            }

            return PrepareResponse(HttpStatusCode.NotFound, "Invalid Payment details");

        }

        [HttpPost("SendPaymentNotification")]
        public async Task SendPaymentNotification(string rrr)
        {
            try
            {

                GenerateBarCodeEngine GenerateBarCode = new GenerateBarCodeEngine();
              
               
                EmailTemplate emailTemplate;
                emailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.PAYMENT_NOTIFICATION);



                var getPaymentDetails = await _remitaPaymentRepository.FetchByRRRCode(rrr);

                var singleRecord = getPaymentDetails.FirstOrDefault();



               
                string mailContent = emailTemplate.EmailBody;
                mailContent = mailContent.Replace("{order}", singleRecord.OrderId);
                mailContent = mailContent.Replace("{rrr}", rrr.ToString());
                mailContent = mailContent.Replace("{channel}", singleRecord.Channel);
                mailContent = mailContent.Replace("{paymentDate}", singleRecord.TransactionCompletedDate.ToString());
                mailContent = mailContent.Replace("{name}", singleRecord.PayerName);
                mailContent = mailContent.Replace("{email}", singleRecord.PayerEmail.ToString());
                mailContent = mailContent.Replace("{phonenumber}", singleRecord.PayerPhone.ToString());

                if (singleRecord.PaymentStatus != WebApiMessage.SuccessfullyPayment)
                {
                    mailContent = mailContent.Replace("{paymentstatus}", "Awaiting  Paid");
                }
                else
                {
                    mailContent = mailContent.Replace("{paymentstatus}", "Approved Payment");
                }

                // get order details 
                var orderdetails = await _RemitaLineItemRepository.GetAllTransactionLineItems(singleRecord.OrderId);

                //  string Message = "<tr> <td>{paydate}</td>    <td>{orderId}</td>    <td>{paymentfor}</td>	 <td>{amount}</td>    <td>{techfee}</td>    <td>{pertotal}</td>  </tr>";
                StringBuilder sb = new StringBuilder();
                string msg = ""; 
                decimal getTotalAmount = 0;
                foreach (var item in getPaymentDetails)
                {
                    //string Message = "<tr> <td>{paydate}</td>    <td>{orderId}</td>    <td>{paymentfor}</td>	 <td>{amount}</td>    <td>{techfee}</td>    <td>{pertotal}</td>  </tr>";


                    decimal totalAmt = +Convert.ToDecimal(item.TechFee) + Convert.ToDecimal(item.Amount);
                    getTotalAmount = getTotalAmount + totalAmt;
                    string amt = item.Amount.ToString();
                    var paymentList = String.Format("<tr> <td>{0}</td>    <td>{1}</td>    <td>{2}</td>	 <td>{3}</td>    <td>{4}</td>    <td>{5}</td>  </tr> ", item.PaymentDate, singleRecord.OrderId, item.FeeItemName, IPORevamp.Core.Utilities.Utilities.FormatAmount(item.Amount.ToString()), IPORevamp.Core.Utilities.Utilities.FormatAmount(item.TechFee.ToString()), IPORevamp.Core.Utilities.Utilities.FormatAmount(item.TotalAmount.ToString()));
                    msg = msg + paymentList;

                    sb.Append(paymentList);
                     
                }

                mailContent = mailContent.Replace("{record}", msg.ToString());
                mailContent = mailContent.Replace("{total}", IPORevamp.Core.Utilities.Utilities.FormatAmount(getTotalAmount.ToString()));
                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                string valueInBarCode = rrr + "/" + singleRecord.PayerName + "/" + singleRecord.PayerEmail + "/" + singleRecord.PayerPhone + "/" + getTotalAmount;
                var generateBarCode = GenerateBarCode.GenerateBarCode(valueInBarCode);

                mailContent = mailContent.Replace("{barcode}", generateBarCode);


                EmailLog emaillog = new EmailLog();
                emaillog.MailBody = mailContent;
                emaillog.Status = IPOEmailStatus.Fresh;
                emaillog.Subject = emailTemplate.EmailSubject;
                emaillog.DateCreated = DateTime.Now;
                emaillog.Receiver = singleRecord.PayerEmail;
                emaillog.Sender = emailTemplate.EmailSender;
                emaillog.SendImmediately = true;
                await _emailsender.SendEmailAsync("bolajiworld@gmail.com", emailTemplate.EmailSubject, mailContent);



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while building payment notification", "");

            }
        }
    }
}