using FluentValidation;
using Locations.Api.Responses;
using Locations.Application.Common.Exceptions;
using Locations.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException e)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;
                List<Error> erros = e.Errors.Select(x => new Error { Type = x.PropertyName, Description = x.ErrorMessage }).ToList();
                ErrorResponse errorResponse = new ErrorResponse("Validation Failed", 400, erros);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
            catch(LocationException e)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = e.Status;
                ErrorResponse errorResponse = new ErrorResponse(e.Message, e.Status, e.Errors);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
            catch (Exception e)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                ErrorResponse errorResponse = new ErrorResponse("An Error has occurred in server. Please try again!", 500);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}
