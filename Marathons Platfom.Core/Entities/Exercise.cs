using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.Core.Entities
{
    public class Exercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int DayNum { get; set; }
        public int MarathonId { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        [ForeignKey("MarathonId")]
        public virtual Marathon Marathon { get; set; }

        public virtual ICollection<UserExerciseStatus> UsersExerciseStatuses { get; set; }
    }
}
