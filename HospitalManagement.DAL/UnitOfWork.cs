using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.DAL.Repository;

namespace HospitalManagement.DAL
{
    public class UnitOfWork : IDisposable
    {
        private HospitalManagementMasterEntities objContext;

        private HospitalManagementClientEntities objClientContext;

        private AspNetUserRepository aspNetUserRepository;

        public AspNetUserRepository AspNetUserRepository
        {
            get
            {
                if(this.aspNetUserRepository == null)
                {
                    this.aspNetUserRepository = new AspNetUserRepository(this.objContext);
                }
                return this.aspNetUserRepository;
            }
        }

        private bool clientDB;
        private bool dispose = false;

        public UnitOfWork(bool clientDB, bool lazyLoading = false)
        {
            this.clientDB = clientDB;
            if(clientDB)
            {
                this.objClientContext = new HospitalManagementClientEntities(this.ConnectionString());
                if(lazyLoading)
                {
                    this.objClientContext.Configuration.ProxyCreationEnabled = false;
                    this.objClientContext.Configuration.LazyLoadingEnabled = true;
                }
            }
            else
            {
                this.objContext = new HospitalManagementMasterEntities();
            }
        }

        /// <summary>
        /// Method is used to save database
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if(this.objContext != null)
            {
                return this.objContext.SaveChanges();
            }
            else
            {
                return this.objClientContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                this.objContext.Dispose();
                this.objClientContext.Dispose();
            }
        }

        public string ConnectionString()
        {
            string databaseName = string.Empty;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HospitalManagementClientEntities"].ConnectionString;

            EntityConnectionStringBuilder ecb = new EntityConnectionStringBuilder(connectionString);
            System.Data.SqlClient.SqlConnectionStringBuilder scsb = new System.Data.SqlClient.SqlConnectionStringBuilder(ecb.ProviderConnectionString);

            scsb.InitialCatalog = "HospitalManagement2";
            ecb.ProviderConnectionString = scsb.ConnectionString;
            return ecb.ConnectionString;
        }
    }
}
