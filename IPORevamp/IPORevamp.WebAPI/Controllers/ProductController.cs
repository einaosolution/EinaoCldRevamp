using EmailEngine.Base.Repository.EmailRepository;
using IPORevamp.Data.Entity.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data.Entities.Email;
using Microsoft.AspNetCore.Identity;
using IPORevamp.Data.UserManagement.Model;

using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;
using Microsoft.Extensions.Configuration;
using System.Net;
using IPORevamp.Core.Utilities;
using EmailEngine.Base.Entities;
using IPORevamp.Data.SetupViewModel;
using IPORevamp.Data.Entity.Interface.Entities.Product;
using IPORevamp.Repository.Department;
using Newtonsoft.Json;

namespace IPORevamp.WebAPI.Controllers

{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;


        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPORevamp.Repository.Product.IProductRepository _productRepository;

        public ProductController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
        

            IPORevamp.Repository.Product.IProductRepository  productRepository,

            IEmailSender emailsender,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment,
            IAuditTrailManager<AuditTrail> auditTrailManager) : base(
                userManager,
                signInManager,
                roleManager,
                configuration,
                mapper,
                logger,
                auditTrailManager

                )

        {
            _productRepository = productRepository;

        }

        [HttpGet("DeleteProduct")]
        public async Task<IActionResult> DeleteCountry([FromQuery]String ProductId, [FromQuery]String UserId)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(UserId.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Product Exist 
                var record = await _productRepository.GetProductById(Convert.ToInt32(ProductId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the country

                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(ProductId);


                var delete = await _productRepository.DeleteProduct(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted product {record}  product  successfully",
                    Entity = "ProductDelete",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.DeleteRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Product", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllCountries([FromQuery] string RequestById)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var product = await _productRepository.GetProduct();

                if (product != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all countries  successfully",
                        Entity = "GetAllProduct",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip 
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Product Returned Successfully", false, product);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Product", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productview)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(productview.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 

                var record = await _productRepository.GetProductById(productview.id);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the country


                record.Code = productview.Code;
               
                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = productview.CreatedBy.ToString();
                record.IsActive = true;
                record.Name = productview.Name;
                record.Id = productview.id;


                var save = await _productRepository.UpdateProduct(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);


                // get User Information
                user = await _userManager.FindByIdAsync(productview.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Product {record} product  successfully",
                    Entity = "ProductUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Product", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpPost("SaveProduct")]
        public async Task<IActionResult> SaveProduct(ProductViewModel productview)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];


                var user = await _userManager.FindByIdAsync(productview.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 
                var checkCount = await _productRepository.CheckExistingProduct(productview.Code);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict,"Product Exist", false, null);
                }

                // attempt to save
                Product content = new Product();
                content.Code = productview.Code;
                content.Name = productview.Name;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = productview.CreatedBy.ToString();
                content.IsActive = true;
              
                content.IsDeleted = false;


                var save = await _productRepository.SaveProduct(content);
                string json = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(productview.CreatedBy.ToString());


                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.Name}  Product  successfully",
                    Entity = "ProductAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json 
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Country", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }
    }
    }

