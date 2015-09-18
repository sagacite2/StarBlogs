using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;

namespace StarBlogs.Blogs.Dtos
{
    public class GetPostInput : IInputDto
    {
        public int Id { get; set; }
    }
    public class GetPostByStarInput : IInputDto, IPagedResultRequest, ISortedResultRequest, ICustomValidate
    {
        public int StarId { get; set; }
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public string Sorting { get; set; }
        public void AddValidationErrors(List<ValidationResult> results)
        {
            var validSortingValues = new[] { "PostTime Desc", "PostTime Asc" };

            if (!Sorting.IsIn(validSortingValues))
            {
                results.Add(new ValidationResult("Sorting is not valid. Valid values: " + string.Join(", ", validSortingValues)));
            }
        }
    }
    public class GetPostByBlogInput : IInputDto, IPagedResultRequest,ISortedResultRequest, ICustomValidate
    {
        public int BlogId { get; set; }
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public string Sorting { get; set; }
        public void AddValidationErrors(List<ValidationResult> results)
        {
            var validSortingValues = new[] { "PostTime Desc", "PostTime Asc" };

            if (!Sorting.IsIn(validSortingValues))
            {
                results.Add(new ValidationResult("Sorting is not valid. Valid values: " + string.Join(", ", validSortingValues)));
            }
        }
    }
    public class GetAllPostInput : IInputDto, IPagedResultRequest, ISortedResultRequest, ICustomValidate
    {
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public string Sorting { get; set; }
        public void AddValidationErrors(List<ValidationResult> results)
        {
            var validSortingValues = new[] { "PostTime Desc", "PostTime Asc" };

            if (!Sorting.IsIn(validSortingValues))
            {
                results.Add(new ValidationResult("Sorting is not valid. Valid values: " + string.Join(", ", validSortingValues)));
            }
        }
    }
}
