using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.WebAPI.Models;

namespace IPORevamp.WebAPI.Filters
{    
    public class ValidateModelAttribute : ActionFilterAttribute
    {        

        public override void OnActionExecuting(ActionExecutingContext context)
        {            
            if (!context.ModelState.IsValid)
            {                
                var errors = context.ModelState.Where(a => a.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).ToList();
                context.Result = new BadRequestObjectResult(new ApiResponse {
                    Content = errors,
                    Error = true,
                    Message = errors.FirstOrDefault().ErrorMessage,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                });                
            }
        }
    }

}
