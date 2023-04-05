using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AddressBook.Areas.LOC_State.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }

        [Required]
        [DisplayName("State Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "please Enter Minimum 3 letters")]
        public string StateName { get; set; }
        public string? StateCode { get; set; }

        [Required(ErrorMessage = "Please Select Country")]
        public int? CountryID { get; set; }
        public string CountryName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

    }

    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }
        public string StateName { get; set; }

    }
}
