using Jumia.Model.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class Specification: LocalizableEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string NameAr { get; set; }
        public virtual ICollection<SubCategorySpecification> SubCategory { get; set; }
    }
}
