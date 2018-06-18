using System.ComponentModel.DataAnnotations;

namespace MySampleApplication.Models.Request
{
    public class BlogAddRequest
    {
        public int Id { get; set; }
        [Required,MaxLength(128)]
        public string Title { get; set; }
        [Required]
        public string BlogContent { get; set; }
        [Required,MaxLength(4000)]
        public string ImageUrl { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}
