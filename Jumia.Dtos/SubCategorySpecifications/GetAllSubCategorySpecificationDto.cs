using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.SubCategorySpecifications
{
    public class GetAllSubCategorySpecificationDto
    {
        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public int specificationId { get; set; }
        public string SubCategoryName { get; set; }
        public string SpecificationName { get; set;}
    }
}
