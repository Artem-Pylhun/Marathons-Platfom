using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Marathons_Platfom.Core.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public int Age { get; set; }
        public virtual ICollection<Progress> Progresses { get; set; }
        public virtual ICollection<User_Badge> User_Badges { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRoleInMarathon> UsersRoleInMarathon { get; set; }
        public virtual ICollection<UserExerciseStatus> UserExercisesStatuses { get; set; }
    }
}
