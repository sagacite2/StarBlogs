using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Runtime.Validation;

namespace StarBlogs.Translates.Dtos
{
    public class GetTranslationByIdInput : IInputDto
    {
        public int Id { get; set; }
    }
    public class GetAllTranslationsInput : IInputDto, IPagedResultRequest, ISortedResultRequest, ICustomValidate
    {
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public string Sorting { get; set; }
        public void AddValidationErrors(List<ValidationResult> results)
        {
            var validSortingValues = new[] { "CreationTime Desc" };

            if (!Sorting.IsIn(validSortingValues))
            {
                results.Add(new ValidationResult("Sorting is not valid. Valid values: " + string.Join(", ", validSortingValues)));
            }
        }
    }
    public class GetTranslationsByPostInput : IInputDto, IPagedResultRequest, ISortedResultRequest
    {
        public int PostId { get; set; }
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public string Sorting { get; set; }
    }
}
