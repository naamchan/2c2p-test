#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _2c2p_test.Exceptions;
using _2c2p_test.Services;

namespace _2c2p_test.Repository
{
    public abstract class RepositoryBase<TModel>
    {
        protected readonly IServiceProvider serviceProvider;

        public RepositoryBase(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public abstract Task<bool> Save(IEnumerable<TModel> models);

        protected MySQLService GetMySQLService()
        {
            var mysqlService = serviceProvider.GetService(typeof(MySQLService)) as MySQLService;
            if (mysqlService is null)
            {
                throw new CannotFindServiceException("MySQL");
            }
            return mysqlService;
        }
    }
}