using api_gateway.Src.DTOs.Subject;
using api_gateway.Src.Helpers;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using SubjectsProto;

namespace api_gateway.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController: ControllerBase
    {
        private readonly SubjectService.SubjectServiceClient _grpcClient;
        public SubjectController(SubjectService.SubjectServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects()
        {
            try
            {

                var grpcResponse = await _grpcClient.GetAllSubjectsAsync(new Empty());
                var httpResponse = grpcResponse.Subjects.Select(subject => new SubjectDto
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    Code = subject.Code,
                    Department = subject.Department,
                    Semester = subject.Semester,
                    Credits = subject.Credits,

                });

                return Ok(httpResponse);
            }
            catch (RpcException ex)
            {
                return StatusCode(GrpcStatusToHttp.MapGrpcStatusToHttp(ex.StatusCode), ex.Status.Detail);
            }
        }

        [HttpGet("prerequisites-map/objects")]
        public async Task<ActionResult<IEnumerable<SubjectRelationshipDto>>> GetPrerequisitesMap()
        {
            try
            {

                var grpcResponse = await _grpcClient.GetPrerequisitesMapObjectsAsync(new Empty());
                var httpResponse = grpcResponse.SubjectRelationships.Select(subjectRelationship => new SubjectRelationshipDto
                {
                    Id = subjectRelationship.Id,
                    SubjectCode = subjectRelationship.SubjectCode,
                    PreSubjectCode = subjectRelationship.PreSubjectCode,
                });
                return Ok(httpResponse);
            }
            catch (RpcException ex)
            {
                return StatusCode(GrpcStatusToHttp.MapGrpcStatusToHttp(ex.StatusCode), ex.Status.Detail);
            }
        }

        [HttpGet("prerequisites-map")]
        public async Task<ActionResult<Dictionary<string, List<string>>>> GetPrerequisitesMapDict()
        {
            try
            {

                var grpcResponse = await _grpcClient.GetPrerequisitesMapAsync(new Empty());
                var prerequisitesDictionary = grpcResponse.Prerequisites.ToDictionary(
                    subjectRelationship => subjectRelationship.Key,
                    subjectRelationship => subjectRelationship.Value
                );

                var httpResponse = new {
                    Prerequisites = prerequisitesDictionary
                };
                return Ok(httpResponse);
            }
            catch (RpcException ex)
            {
                return StatusCode(GrpcStatusToHttp.MapGrpcStatusToHttp(ex.StatusCode), ex.Status.Detail);
            }
        }

        [HttpGet("postrequisites-map")]

        public async Task<ActionResult<Dictionary<string, List<string>>>> GetPosrequisitesMapDict()
        {
            try
            {

                var grpcResponse = await _grpcClient.GetPostrequisitesMapAsync(new Empty());
                var posrequisitesDictionary = grpcResponse.Postrequisites.ToDictionary(
                    subjectRelationship => subjectRelationship.Key,
                    subjectRelationship => subjectRelationship.Value
                );

                var httpResponse = new {
                    Posrequisites = posrequisitesDictionary
                };
                return Ok(httpResponse);
            }
            catch (RpcException ex)
            {
                return StatusCode(GrpcStatusToHttp.MapGrpcStatusToHttp(ex.StatusCode), ex.Status.Detail);
            }
        }

    }
}