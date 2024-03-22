using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class Specification:BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        
        public virtual ICollection<SubCategorySpecification> SubCategory { get; set; }
    }
}
