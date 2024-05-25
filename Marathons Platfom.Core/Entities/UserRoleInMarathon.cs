using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Marathons_Platfom.Core.Entities
{
    public class UserRoleInMarathon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

        public int MarathonId { get; set; }

        public virtual Marathon Marathon { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
