using Abp.Domain.Repositories;
using StarBlogs.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Stars
{
    public interface IStarRepository : IRepository<Star>
    {
        List<Star> GetByGender(Gender Gender);
    }
}
