using api_gateway.Src.DTOs.Career;
using api_gateway.Src.DTOs.User;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using UsersServiceProto;

namespace api_gateway.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
         private readonly UserService.UserServiceClient _grpcClient;

        public UserController(UserService.UserServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetUserById()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var metadata = new Metadata
                {
                    { "Authorization", $"Bearer {token}" }
                };

                var grpcResponse = await _grpcClient.GetUserByIdAsync(new Empty(), metadata);

                Console.WriteLine(grpcResponse);

                var httpResponse = new UserDto
                {
                    Id = grpcResponse.Id,
                    Name = grpcResponse.Name,
                    FirstLastName = grpcResponse.FirstLastName,
                    SecondLastName = grpcResponse.SecondLastName,
                    Rut = grpcResponse.Rut,
                    Email = grpcResponse.Email,
                    Career = new CareerDto
                    {
                        Id = grpcResponse.Career.Id,
                        Name = grpcResponse.Career.Name
                    }
                };

                return Ok(httpResponse);
            }
            catch (RpcException ex)
            {
                return StatusCode(MapGrpcStatusToHttp(ex.StatusCode), ex.Status.Detail);
            }
        }

        [HttpPatch("update-profile")]
        public async Task<ActionResult<UserDto>> UpdateUserProfile([FromBody] EditUserDto editUserDto)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var metadata = new Metadata
                {
                    { "Authorization", $"Bearer {token}" }
                };

                var grpcRequest = new EditUserRequest
                {
                    Name = editUserDto.Name,
                    FirstLastName = editUserDto.FirstLastName,
                    SecondLastName = editUserDto.SecondLastName
                };

                var grpcResponse = await _grpcClient.EditUserAsync(grpcRequest, metadata);

                var httpResponse = new UserDto
                {                    
                    Id = grpcResponse.Id,
                    Name = grpcResponse.Name,
                    FirstLastName = grpcResponse.FirstLastName,
                    SecondLastName = grpcResponse.SecondLastName,
                    Rut = grpcResponse.Rut,
                    Email = grpcResponse.Email,
                    Career = new CareerDto
                    {
                        Id = grpcResponse.Career.Id,
                        Name = grpcResponse.Career.Name
                    }
                };

                return Ok(httpResponse);
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex);
                return StatusCode(MapGrpcStatusToHttp(ex.StatusCode), ex.Status.Detail);
            }
        }

        [HttpGet("my-progress")]
        public async Task<ActionResult<IEnumerable<UserProgressDto>>> GetUserProgress()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var metadata = new Metadata
                {
                    { "Authorization", $"Bearer {token}" }
                };

                var grpcResponse = await _grpcClient.GetProgressByUserAsync(new Empty(), metadata);

                var httpResponse = grpcResponse.Progress.Select(progress => new UserProgressDto
                {
                    Id = progress.Id,
                    SubjectCode = progress.SubjectCode
                });

                return Ok(httpResponse);
            }
            catch (RpcException ex)
            {
                return StatusCode(MapGrpcStatusToHttp(ex.StatusCode), ex.Status.Detail);
            }
        }

        [HttpPatch("my-progress")]
        public async Task<ActionResult> UpdateUserProgress([FromBody] UpdateUserProgressDto updateUserProgressDto)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var metadata = new Metadata
                {
                    { "Authorization", $"Bearer {token}" }
                };

                var grpcRequest = new SetUserProgressRequest
                {
                    AddSubjects = { updateUserProgressDto.AddSubjects },
                    DeleteSubjects = { updateUserProgressDto.DeleteSubjects }
                };

                var grpcResponse = await _grpcClient.SetUserProgressAsync(grpcRequest, metadata);

                return NoContent();
            }
            catch (RpcException ex)
            {
                return StatusCode(MapGrpcStatusToHttp(ex.StatusCode), ex.Status.Detail);
            }
        }

        private static int MapGrpcStatusToHttp(StatusCode grpcStatusCode)
        {
            return grpcStatusCode switch
            {
                Grpc.Core.StatusCode.OK => 200,
                Grpc.Core.StatusCode.InvalidArgument => 400,
                Grpc.Core.StatusCode.NotFound => 404,
                Grpc.Core.StatusCode.PermissionDenied => 403,
                Grpc.Core.StatusCode.Unauthenticated => 401,
                Grpc.Core.StatusCode.Internal => 500,
                _ => 500
            };
        }

    }
}