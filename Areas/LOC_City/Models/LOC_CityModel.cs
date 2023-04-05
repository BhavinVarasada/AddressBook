using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AddressBook.Areas.LOC_City.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }

        [Required]
        [DisplayName("City Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "please Enter Minimum 3 letters")]
        public string CityName { get; set; }
        public string? CityCode { get; set; }

        [Required(ErrorMessage = "Please Select Country")]
        public int? CountryID { get; set; }
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Please Select State")]
        public int? StateID { get; set; }
        public string StateName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

    }

    public class LOC_CityDropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

    }
}
