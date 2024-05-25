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
    public class Progress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MarathonId { get; set; }
        public int Percent { get; set; }
        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("MarathonId")]
        public virtual Marathon Marathon { get; set; }

        
    }
}
