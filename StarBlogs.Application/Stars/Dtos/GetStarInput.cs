using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;

namespace StarBlogs.Stars.Dtos
{
    public class GetDeleteBlockStarInput : IInputDto
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
    }
    public class GetStarsInput : IInputDto, IPagedResultRequest, ISortedResultRequest, ICustomValidate
    {
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }

        public string Sorting { get; set; }

        public void AddValidationErrors(List<ValidationResult> results)
        {
            var validSortingValues = new[] { "CreationTime Desc", "Name Asc", "LastUpdateTime Desc" };

            if (!Sorting.IsIn(validSortingValues))
            {
                results.Add(new ValidationResult("Sorting is not valid. Valid values: " + string.Join(", ", validSortingValues)));
            }
        }
    }
}
