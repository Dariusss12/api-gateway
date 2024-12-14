using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Src.DTOs.User
{
    public class UserProgressDto
    {
        public int Id { get; set; }
        public string SubjectCode { get; set; } = null!;
    }
}