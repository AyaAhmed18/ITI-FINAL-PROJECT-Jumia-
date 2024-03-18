using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.SubCategory
{
    public class CreateOrUpdateSubDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string NameAr { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }

        public byte[]? Image { get; set; }

        public int CategoryId { get; set; }
    }
}
