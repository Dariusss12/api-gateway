using api_gateway.Src.DTOs.Career;
using api_gateway.Src.Helpers;
using CareersProto;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace api_gateway.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CareerController : ControllerBase
    {
        private readonly CareerService.CareerServiceClient _grpcClient;

        public CareerController(CareerService.CareerServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareerDto>>> GetCareers()
        {
            try
            {

                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var metadata = new Metadata
                {
                    { "Authorization", $"Bearer {token}" }
                };


                var grpcResponse = await _grpcClient.GetAllCareersAsync(new Empty(), metadata);

                var httpResponse = grpcResponse.Careers.Select(career => new CareerDto
                {
                    Id = career.Id,
                    Name = career.Name
                });

                return Ok(httpResponse);
            }
            catch (RpcException ex)
            {
                return StatusCode(GrpcStatusToHttp.MapGrpcStatusToHttp(ex.StatusCode), ex.Status.Detail);
            }
        }

    }
}