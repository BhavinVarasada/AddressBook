using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AddressBook.Areas.LOC_Country.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }

        [Required]
        [DisplayName("Country Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "please Enter Minimum 3 letters")]
        public string CountryName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string CountryCode { get; set; }

        [FileExtensions(Extensions = ".png,.jpg", ErrorMessage = "Please select an Image file.")]
        public string? PhotoPath { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

    }

    public class LOC_CountryDropDownModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

    }


}
