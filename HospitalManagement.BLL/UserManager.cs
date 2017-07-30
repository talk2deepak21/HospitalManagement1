using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.DAL;

namespace HospitalManagement.BLL
{
    public class UserManager
    {
        public void AddUser(AspNetUser user)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(false))
            {
                unitOfWork.AspNetUserRepository.Add(user);
            }
        }
        public AspNetUser GetUser(string userName, string password)
        {
            AspNetUser user;
            using (UnitOfWork unitOfWork = new UnitOfWork(true))
            {
                user = unitOfWork.AspNetUserRepository.UserInfo(userName, password);
            }
            return user;
        }
    }
}
