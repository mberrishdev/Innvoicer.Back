﻿using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Innvoicer.Application.Exceptions;
using ApplicationException = System.ApplicationException;

namespace Innvoicer.Api.Infrastructure;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = error switch
            {
                ObjectNotFoundException => (int)HttpStatusCode.NotFound,
                NotValidException => (int)HttpStatusCode.BadRequest,
                ObjectAlreadyExistException => (int)HttpStatusCode.Conflict,
                CommandValidationException => (int)HttpStatusCode.BadRequest,
                InvalidCredentialsException => (int)HttpStatusCode.BadRequest,
                ApplicationException => (int)HttpStatusCode.BadRequest,
                Domain.Exceptions.CommandValidationException => (int)HttpStatusCode.BadRequest,
                Domain.Exceptions.DomainException => (int)HttpStatusCode.BadRequest,
                TaskCanceledException => (int)HttpStatusCode.GatewayTimeout,
                not null => (int)HttpStatusCode.InternalServerError,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(
                new
                {
                    error?.Message,
                    InEx = error?.InnerException?.Message ?? "", 
                    error?.StackTrace
                },
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }
            );
            // if (response.StatusCode == 500)
            //     result = JsonSerializer.Serialize(new { error.Message, error.StackTrace.Mess });

            await response.WriteAsync(result);
        }
    }
}