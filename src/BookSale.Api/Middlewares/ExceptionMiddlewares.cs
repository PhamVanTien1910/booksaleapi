﻿using BookSale.Application.Exceptions;
using BookSale.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using FluentValidation;

namespace BookSale.Api.Middlewares
{
    public class ExceptionMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddlewares(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundExceptions ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";
                var response = new { error_code = ex.ErrorCode, message = "The resource does not exist", details = new string[] { ex.Message } };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (CustomExceptions ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";
                var response = new { error_code = ex.ErrorCode, message = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (FluentValidation.ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = new
                {
                    error_code = ErrorCodeEnums.InvalidSyntax,
                    message = "Invalid syntax",
                    details = ex.Errors.Select(e => e.ErrorMessage)
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                if (_env.IsDevelopment())
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(
                    new
                    {
                          error_code = ErrorCodeEnums.ServerError,
                          message = "An unexpected error occurred on the server",
                          detail = ex.InnerException?.Message
                      }));
                }
                else await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    error_code = ErrorCodeEnums.ServerError,
                    message = "An unexpected error occurred on the server"
                }));
            }
           
        }
    }
}
