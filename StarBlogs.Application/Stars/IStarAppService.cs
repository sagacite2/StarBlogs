using Abp.Application.Services;
using Abp.Application.Services.Dto;
using StarBlogs.Stars.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBlogs.Stars
{
    public interface IStarAppService : IApplicationService
    {
        PagedResultOutput<StarWithBlogsDto> GetStars(GetStarsInput input);
        StarWithBlogsDto GetStar(GetDeleteBlockStarInput input);
        void UpdateStar(CreateUpdateStarInput input);
        void CreateStar(CreateUpdateStarInput input);
        void BlockStar(GetDeleteBlockStarInput input);
        void DeleteStar(GetDeleteBlockStarInput input);
        List<StarTagSettingDto> GetStarTagSettings();
        void UpdateStarTag(UpdateStarTagInput input);
        void CreateTagSetting(CreateTagSettingInput input);
        void UpdateTagSetting(UpdateTagSettingInput input);
    }
}
