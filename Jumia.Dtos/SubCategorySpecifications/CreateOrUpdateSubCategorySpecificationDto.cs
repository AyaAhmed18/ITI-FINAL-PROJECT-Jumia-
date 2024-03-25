using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.SubCategorySpecifications
{
    public class CreateOrUpdateSubCategorySpecificationDto
    {
        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public int specificationId { get; set; }
        public int SpecName { get; set; }
        // [Bind(Prefix = "Entity.SelectedSpecification")]
        public List<string>? SelectedSpecification { get; set; }
        // public string SubCategory { get; set; }
        //public string Specification { get; set; }
    }
}
