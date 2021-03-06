using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace codeFirst.Models
{
	public class Country
	{
        [Column("CountryId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CountryId { get; set; }

        [Column("CountryName")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column("Population")]
        [Required]
        public Int64 Population { get; set; }

        public List<Developer> Developers { get; set; }
    }
}
