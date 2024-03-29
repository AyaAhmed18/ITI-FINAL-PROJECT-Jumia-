using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.Brand
{
    public class GetAllBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public byte[]? LogoURL { get; set; }
        public string? Website { get; set; }
    }
}
