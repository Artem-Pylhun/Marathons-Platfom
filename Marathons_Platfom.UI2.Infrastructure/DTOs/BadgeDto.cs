using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marathons_Platfom.UI2.Infrastructure.DTOs
{
    public class BadgeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Claimed { get; set; }
        public string Photo { get; set; }
    }
    public class BadgeCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Photo { get; set; }
        [NotMapped]
        public string BadgeImage { get; set; }
    }
    public class BadgeUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string? BadgeImage { get; set; }
    }
}
