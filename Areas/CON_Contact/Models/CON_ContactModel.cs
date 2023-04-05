using System.ComponentModel.DataAnnotations;

namespace AddressBook.Areas.CON_Contact.Models
{
    public class CON_ContactModel
    {
        public int? ContactID { get; set; }

        [Required]
        public string PersonName { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Select City")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "Please Select State")]

        public string CityName { get; set; }
        public int StateID { get; set; }

        [Required(ErrorMessage = "Please Select Country")]

        public string StateName { get; set; }
        public int CountryID { get; set; }

        [Required]

        public string CountryName { get; set; }
        public int Pincode { get; set; }
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
        public string? AlternateContact { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime AnniversaryDate { get; set; }
        public string? Linkedin { get; set; }
        public string? Twitter { get; set; }
        public string? Instagram { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string? TypeOfProfession { get; set; }
        public string? CompanyName { get; set; }
        public string? Designation { get; set; }
        public int? ContactCategory { get; set; }
        public string ContactCategoryName { get; set; }
        public IFormFile File { get; set; }
        public string? PhotoPath { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }

    public class CON_ContactDropDownModel
    {
        public int ContactID { get; set; }
        public string PersonName { get; set; }

    }
}

