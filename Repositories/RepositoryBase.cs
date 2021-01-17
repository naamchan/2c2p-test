#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2c2p_test.Repository
{
    public abstract class RepositoryBase<TModel>
    {
        protected readonly IServiceProvider serviceProvider;

        public RepositoryBase(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public abstract Task<bool> Save();
    }
}