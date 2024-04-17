using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.DTOS.ViewResultDtos
{
    public class ResultDataForPagination<TEntity>
    {
        public List<TEntity> Entities { get; set; }
        public int? count { get; set; } = 0;
        public int? CurrentPage { get; set; }
        public int? TotalPages { get; set; }
        public int? PageSize { get; set; }
        public ResultDataForPagination()
        {
            Entities = new List<TEntity>();
        }
    }
}
