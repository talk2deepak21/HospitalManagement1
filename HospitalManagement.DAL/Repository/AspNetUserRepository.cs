using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.DAL.Repository
{
    public class AspNetUserRepository: RepositoryBase<AspNetUser>
    {
        public AspNetUserRepository(HospitalManagementMasterEntities objContext)
            : base(objContext)
        {

        }
        public List<AspNetRole> GetRoles()
        {
            return objDataContext.AspNetRoles.OrderBy(x => x.Name).ToList();
        }
        public List<AspNetUser> GetUsersByUserName(List<string> userName)
        {
            return objDataContext.AspNetUsers.Where(u => userName.Contains(u.UserName)).ToList();
        }
        //public IEnumerable<Guid> GetActiveUsers(IEnumerable<Guid> userIds, Guid userId)
        //{
        //    return objDataContext
        //}
        public void AddUser(AspNetUser user)
        {
            objDataContext.AspNetUsers.Add(user);
        }
        public AspNetUser GetUser(string userId)
        {
            return objDataContext.AspNetUsers.FirstOrDefault(x => x.Id == userId);
        }
        public AspNetUser UserInfo(string userName, string Password)
        {
            return objDataContext.AspNetUsers.Where(x => x.UserName == userName && x.PasswordHash == Password).FirstOrDefault();
        }
    }
}
