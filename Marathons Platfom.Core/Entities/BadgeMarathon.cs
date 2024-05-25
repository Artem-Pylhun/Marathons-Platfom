using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.Core.Entities
{
    public class BadgeMarathon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public int BadgeId { get; set; }
        public virtual Badge Badge { get; set; }

        public int MarathonId { get; set; }
        public virtual Marathon Marathon { get; set; }
    }
}
