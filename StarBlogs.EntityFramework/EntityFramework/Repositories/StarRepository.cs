using Abp.EntityFramework;
using StarBlogs.Helpers;
using StarBlogs.Stars;
using StarBlogs.Stars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.EntityFramework.Repositories
{
    public class StarRepository : StarBlogsRepositoryBase<Star>, IStarRepository
    {
        public StarRepository(IDbContextProvider<StarBlogsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<Star> GetByGender(Gender gender)
        {
            return GetAll().Where(star => star.Gender == gender).ToList();
        }
    }
}
