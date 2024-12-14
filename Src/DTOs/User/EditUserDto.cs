using System.ComponentModel.DataAnnotations;

namespace api_gateway.Src.DTOs.User
{
    public class EditUserDto
    {

        public string  Name { get; set; } = string.Empty;


        public string FirstLastName { get; set; } = string.Empty;


        public string SecondLastName { get; set; } = string.Empty;
    }
}