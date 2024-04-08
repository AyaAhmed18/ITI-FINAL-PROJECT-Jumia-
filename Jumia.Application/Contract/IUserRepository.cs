using Jumia.Dtos.User;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Contract
{
    public interface IUserRepository : IRepository<UserIdentity, int>
    {
   
    }
}
