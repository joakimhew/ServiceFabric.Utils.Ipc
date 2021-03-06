﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ServiceFabric.Utils.Http.Extensions;

namespace ServiceFabric.Utils.Http
{
    public class ApiHttpActionResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _requestMessage;
        private readonly HttpStatusCode _code;
        private readonly object _message;
        private readonly object _info;

        public ApiHttpActionResult(HttpRequestMessage request, HttpStatusCode statusCode,
            object message, object additionalInfo = null)
        {
            _requestMessage = request;
            _code = statusCode;
            _message = message;
            _info = additionalInfo;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var responseMessageResult = new ResponseMessageResult(
                _requestMessage.CreateApiResponse(_code, _message, _info));

            var response = await responseMessageResult.ExecuteAsync(cancellationToken);

            return response;
        }
    }
}
