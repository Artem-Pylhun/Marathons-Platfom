using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.Core.Entities
{
    public class Marathon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int ThemeId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfStart { get; set; }
        public int Duration { get; set; }
        public int NumOfParticipants { get; set; }
        
        [ForeignKey("ThemeId")]
        public virtual Theme Theme { get; set; }
        public virtual ICollection<Exercise> Exercises{ get; set; }
        public virtual ICollection<Progress> Progresses { get; set; }

        public virtual ICollection<UserRoleInMarathon> UserRoleInMarathons { get; set; }

        public virtual ICollection<BadgeMarathon> BadgesMarathon { get; set; }

    }
}
