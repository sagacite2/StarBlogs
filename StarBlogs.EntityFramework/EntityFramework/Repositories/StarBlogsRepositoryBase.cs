using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace StarBlogs.EntityFramework.Repositories
{
    public abstract class StarBlogsRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<StarBlogsDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected StarBlogsRepositoryBase(IDbContextProvider<StarBlogsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class StarBlogsRepositoryBase<TEntity> : StarBlogsRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected StarBlogsRepositoryBase(IDbContextProvider<StarBlogsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
