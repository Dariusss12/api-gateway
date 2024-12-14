using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Src.DTOs.User
{
    public class UpdateUserProgressDto
    {
        public List<string> AddSubjects { get; set; } = [];

        public List<string> DeleteSubjects { get; set; } = [];
    }
}