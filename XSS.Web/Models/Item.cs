using System;
using System.ComponentModel.DataAnnotations;

namespace XSS.Web.Models
{
    public class Item
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "标题")]
        public string Title { get; set; }

        [Display(Name = "内容")]
        public string Content { get; set; }
    }
}