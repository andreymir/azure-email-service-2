using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MailingList
    {
        [Required]
        [RegularExpression(@"[\w]+", ErrorMessage = @"Only alphanumeric characters and underscore (_) are allowed.")]
        [Display(Name = "List Name")]
        public string ListName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "'From' Email Address")]
        public string FromEmailAddress { get; set; }

        [MaxLength(5000, ErrorMessage = "Description value is too long.")]
        public string Description { get; set; }
    }
}
