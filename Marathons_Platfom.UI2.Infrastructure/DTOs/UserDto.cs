using System.ComponentModel.DataAnnotations;

namespace Marathons_Platfom.UI2.Infrastructure.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Age { get; set; }
    }
    public class UserUpdateDto { }
    public class ShowUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
    }
    public class UserLogDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
