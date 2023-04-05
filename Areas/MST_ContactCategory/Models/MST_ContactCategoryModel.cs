using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AddressBook.Areas.MST_ContactCategory.Models
{
    public class MST_ContactCategoryModel
    {
        public int? ContactCategoryID { get; set; }

        [Required]
        [DisplayName("Contact Category Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "please Enter Minimum 3 letters")]
        public string ContactCategoryName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

    }

    public class MST_ContactCategoryDropDownModel
    {
        public int ContactCategoryID { get; set; }
        public string ContactCategoryName { get; set; }

    }
}

