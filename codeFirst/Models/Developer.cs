using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace codeFirst.Models
{
	public class Developer
	{
        [Column("DeveloperId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int DeveloperId { get; set; }

        [Column("DeveloperName")]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Column("Skills")]
        [Required]
        [StringLength(500)]
        public string Skills { get; set; }

        public int CountryID { get; set; }

        [ForeignKey("CountryID")]
        public Country Country { get; set; }
    }
}
