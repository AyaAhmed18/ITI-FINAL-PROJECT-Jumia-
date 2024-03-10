using Jumia.Application.Contract;
using Jumia.Context;
using Jumia.Dtos.User;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Infrastructure
{
    public class UserRoleRepository : Repository<UserRole, int>, IUserRoleRepository
    {
        private readonly JumiaContext _jumiaContext;

        public UserRoleRepository(JumiaContext jumiaContext) : base(jumiaContext)
        {
            _jumiaContext = jumiaContext;
        }

        public  Task<IQueryable<RoleUser>> GetUsername()
        {
            var result = from UserRole in _jumiaContext.UserRoles
                         join UserIdentity in _jumiaContext.Users   on UserRole.UserId equals UserIdentity.Id
                         join roleIdentity in _jumiaContext.Roles on UserRole.RoleId equals roleIdentity.Id
                         select new RoleUser
                         {
                             UserName = UserIdentity.UserName,
                             Email = UserIdentity.Email,
                             Id = UserIdentity.Id,
                             PhoneNumber = UserIdentity.PhoneNumber,
                             RoleName = roleIdentity.Name,
                         
                         };
            return Task.FromResult(result);
        }
    }
}
