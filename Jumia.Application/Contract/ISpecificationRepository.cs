using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Contract
{
    public interface ISpecificationRepository
    {
        Task<IQueryable<Specification>> GetAllAsync();
        Task<Specification> GetOneAsync(int id);
    }
}
