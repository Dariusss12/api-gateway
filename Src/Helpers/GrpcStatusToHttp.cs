using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace api_gateway.Src.Helpers
{
    public class GrpcStatusToHttp
    {
        public static int MapGrpcStatusToHttp(StatusCode grpcStatusCode)
        {
            return grpcStatusCode switch
            {
                StatusCode.OK => 200,
                StatusCode.InvalidArgument => 400,
                StatusCode.NotFound => 404,
                StatusCode.PermissionDenied => 403,
                StatusCode.Unauthenticated => 401,
                StatusCode.Internal => 500,
                _ => 500
            };
        }
    }
}