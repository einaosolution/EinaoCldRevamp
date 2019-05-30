using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Core.Utilities;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.Department;
using IPORevamp.Data.SetupViewModel;
using IPORevamp.Data.UserManagement.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Department")]
    [ApiController]
    public class DepartmentController : BaseController
    {

        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPORevamp.Repository.Department.IDepartmentRepository _departmentRepository;

        public DepartmentController(UserManager<ApplicationUser> userManager,
     RoleManager<ApplicationRole> roleManager,
     SignInManager<ApplicationUser> signInManager,
     IConfiguration configuration,
     IMapper mapper, ILogger<UserController> logger,
     EmailEngine.Base.Repository.EmailRepository.IEmailManager<EmailLog, EmailTemplate> emailManager,

     IPORevamp.Repository.Department.IDepartmentRepository departmentRepository,


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
            _departmentRepository = departmentRepository;

        }


        [HttpGet("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment([FromQuery]String ProductId, [FromQuery]String UserId)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(UserId.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 
                var record = await _departmentRepository.GetDepartmentById(Convert.ToInt32(ProductId));

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the country

                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(ProductId);


                var delete = await _departmentRepository.DeleteDepartment(record);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted department {record}  department  successfully",
                    Entity = "DepartmentDelete",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.DeleteRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Department", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }

        [HttpGet("GetAllDepartment")]
        public async Task<IActionResult> GetAllDepartment([FromQuery] string RequestById)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var department = await _departmentRepository.GetDepartment();

                if (department != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all department  successfully",
                        Entity = "GetAlldepartment",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "department Returned Successfully", false, department);

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


        [HttpPost("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DepartmentViewModel departmentview)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(departmentview.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 

                var record = await _departmentRepository.GetDepartmentById(departmentview.id);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the country


                record.Code = departmentview.Code;

                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = departmentview.CreatedBy.ToString();
                record.IsActive = true;
                record.Name = departmentview.Name;
                record.Id = departmentview.id;


                var save = await _departmentRepository.UpdateDepartment(record);

                // get User Information
                user = await _userManager.FindByIdAsync(departmentview.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Department {record} department successfully",
                    Entity = "DepartmentUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Department", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpPost("SaveDepartment")]
        public async Task<IActionResult> SaveProduct(DepartmentViewModel departmentview)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(departmentview.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 
                var checkCount = await _departmentRepository.CheckExistingDepartment(departmentview.Code);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, "Department  Exist", false, null);
                }

                // attempt to save
               Department content = new Department();
                content.Code = departmentview.Code;
                content.Name = departmentview.Name;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = departmentview.CreatedBy.ToString();
                content.IsActive = true;

                content.IsDeleted = false;


                var save = await _departmentRepository.SaveDepartment(content);

                // get User Information
                user = await _userManager.FindByIdAsync(departmentview.CreatedBy.ToString());


                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.Name}  Department  successfully",
                    Entity = "DepartmentAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Department", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }
    }
}
