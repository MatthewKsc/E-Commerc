using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork {

        private readonly StoreContext context;
        private Hashtable repositories;

        public UnitOfWork(StoreContext context) {
            this.context = context;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity {
            if (repositories == null) repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type)) {
                var respsitoryType = typeof(GenericRepository<>);
                var repostioryInstance = Activator.CreateInstance(respsitoryType.MakeGenericType(typeof(TEntity)), context);

                repositories.Add(type, repostioryInstance);
            }

            return (IGenericRepository<TEntity>) repositories[type];

        }
        public async Task<int> Complete() {
            return await context.SaveChangesAsync();
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
