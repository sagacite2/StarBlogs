using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Linq.Extensions;
using StarBlogs.Authorization;
using StarBlogs.Translates.Dtos;
using StarBlogs.Configuration;
using StarBlogs.Users;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace StarBlogs.Translates
{
    public class TranslateAppService : ApplicationService, ITranslateAppService
    {
    }
}
