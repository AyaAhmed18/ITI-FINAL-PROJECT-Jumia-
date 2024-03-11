using Jumia.Application.Contract;
using Jumia.Context;
using Jumia.Dtos.User;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Infrastructure
{
    public class UserRepository: Repository<UserIdentity, int>, IUserRepository
    {
        private readonly JumiaContext _jumiaContext;

        public UserRepository(JumiaContext jumiaContext) : base(jumiaContext)
        {

            _jumiaContext = jumiaContext;
        }

    }
}
