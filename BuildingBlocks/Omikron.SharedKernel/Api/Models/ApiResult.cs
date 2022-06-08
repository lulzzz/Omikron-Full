using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Omikron.SharedKernel.Infrastructure.Json;
using Omikron.SharedKernel.Infrastructure.Logging;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.SharedKernel.Api.Models
{
    public class ApiResult
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CorrelationId { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PaginationInfo PaginationInfo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[] Errors { get; set; }

        [JsonIgnore] public HttpStatusCode HttpStatusCode { get; set; }

        [JsonIgnore]
        public bool IsSuccess => HttpStatusCode >= (HttpStatusCode)200 && HttpStatusCode <= (HttpStatusCode)299;

        public static ApiResult BadRequest(params string[] messages)
        {
            return new ApiResult
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Errors = messages
            };
        }

        public static ApiResult BadRequest(IdentityResult result)
        {
            return new ApiResult
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Errors = result.Errors.Select(x => x.Description).ToArray()
            };
        }

        public static ApiResult BadRequest(Result result)
        {
            return new ApiResult
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Errors = new[] { result.Error }
            };
        }

        public static ApiResult NotFound(params string[] messages)
        {
            return new ApiResult
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Errors = messages
            };
        }

        public static ApiResult NoContent()
        {
            return new ApiResult
            {
                HttpStatusCode = HttpStatusCode.NoContent
            };
        }

        public static ApiResult Success()
        {
            return new ApiResult
            {
                HttpStatusCode = HttpStatusCode.OK
            };
        }

        public static ApiResult FromHttpResponse(HttpResponseMessage responseMessage)
        {
            var payload = responseMessage.Content.ReadAsStringAsync().Result;
            var result = !string.IsNullOrWhiteSpace(payload)
                ? JsonSerializer.Deserialize<ApiResult>(payload, DefaultJsonSerializerOptions.DefaultSerializerOptions)
                : new ApiResult();

            result.HttpStatusCode = responseMessage.StatusCode;

            if (responseMessage.Headers.TryGetValues(LoggerConstants.CorrelationIdHeaderKey, out var id))
            {
                result.CorrelationId = id.FirstOrDefault();
            }
            return result;
        }

        public static implicit operator ApiResult(Result result)
        {
            return result.IsSuccess ? Success() : BadRequest(result);
        }

        public static implicit operator ApiResult(EmptyResult result)
        {
            return Success();
        }
    }

    public class ApiResult<TResult> : ApiResult
    {
        public ApiResult()
        {
        }

        public ApiResult(TResult records)
        {
            Records = records;
            HttpStatusCode = HttpStatusCode.OK;
        }

        public TResult Records { get; set; }

        public ApiResult<TResult> WithMeta<TEntity>(PaginationQuery<TEntity> meta, long total)
        {
            PaginationInfo = new PaginationInfo { PageSize = meta.PageSize, Page = meta.Page, Total = total };
            return this;
        }

        public ApiResult<TResult> WithData(TResult result)
        {
            Records = result;
            return this;
        }

        public new static ApiResult<TResult> Success()
        {
            return new ApiResult<TResult>
            {
                HttpStatusCode = HttpStatusCode.OK
            };
        }

        public new static ApiResult<TResult> NotFound(params string[] messages)
        {
            return new ApiResult<TResult>
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Errors = messages
            };
        }

        public new static ApiResult<TResult> BadRequest(params string[] messages)
        {
            return new ApiResult<TResult>
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Errors = messages
            };
        }

        public new static ApiResult<TResult> BadRequest(IdentityResult result)
        {
            return new ApiResult<TResult>
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Errors = result.Errors.Select(x => x.Description).ToArray()
            };
        }

        public static implicit operator ApiResult<TResult>(Result<TResult> result)
        {
            return result.IsSuccess ? Success().WithData(result.Value) : BadRequest(result.Error);
        }
    }
}