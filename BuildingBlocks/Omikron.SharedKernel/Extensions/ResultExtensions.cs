using System.Threading.Tasks;
using Omikron.SharedKernel.Api.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Omikron.SharedKernel.Extensions
{
    public static class ResultExtensions
    {
        public static ObjectResult ToActionResult<T>(this ApiResult<T> result)
        {
            return new ObjectResult(result) { StatusCode = (int)result.HttpStatusCode };
        }

        public static ObjectResult ToActionResult(this ApiResult result)
        {
            return new ObjectResult(result) { StatusCode = (int)result.HttpStatusCode };
        }

        public static ObjectResult ToActionResult(this Result result)
        {
            ApiResult apiResult = result;
            return new ObjectResult(result) { StatusCode = (int)apiResult.HttpStatusCode };
        }

        public static ObjectResult ToActionResult(this Infrastructure.Commands.EmptyResult result)
        {
            ApiResult apiResult = result;
            return new ObjectResult(result) { StatusCode = (int)apiResult.HttpStatusCode };
        }

        public static async Task<ObjectResult> ToActionResultAsync(this Task<Result> result)
        {
            ApiResult apiResult = await result;
            return new ObjectResult(apiResult) { StatusCode = (int)apiResult.HttpStatusCode };
        }

        public static async Task<ObjectResult> ToActionResultAsync<T>(this Task<Result<T>> result)
        {
            ApiResult<T> apiResult = await result;
            return new ObjectResult(apiResult) { StatusCode = (int)apiResult.HttpStatusCode };
        }
    }
}