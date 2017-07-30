using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.DAL
{
    public partial class HospitalManagementClientEntities : DbContext
    {
        public HospitalManagementClientEntities(string dbID)
            : base(dbID)
        {
        }
    }
}
