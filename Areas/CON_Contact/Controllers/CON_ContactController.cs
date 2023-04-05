using AddressBook.Areas.CON_Contact.Models;
using AddressBook.Areas.LOC_City.Models;
using AddressBook.Areas.LOC_Country.Models;
using AddressBook.Areas.LOC_State.Models;
using AddressBook.Areas.MST_ContactCategory.Models;
using AddressBook.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Areas.CON_Contact.Controllers
{
    [Area("CON_Contact")]
    [Route("CON_Contact/[controller]/[action]")]
    public class CON_ContactController : Controller
    {
        string MyConnectionString = DALHelper.MyConnectionString;
        private IConfiguration Configuration;
        public CON_ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }

        #region SelectAll
        public IActionResult Index(CON_ContactModel modelCON_Contact)
        {
            
            DataTable contactSelectAlldt = new DataTable();
            CON_DAL dalCON = new CON_DAL();
            contactSelectAlldt = dalCON.PR_CON_Contact_SelectAll(modelCON_Contact.PersonName, modelCON_Contact.ContactCategoryName , modelCON_Contact.CountryName, modelCON_Contact.CityName ,modelCON_Contact.StateName);

                SqlConnection conn1 = new SqlConnection(MyConnectionString);
                conn1.Open();
                SqlCommand countryDropDowncmd = conn1.CreateCommand();
                countryDropDowncmd = conn1.CreateCommand();
                countryDropDowncmd.CommandType = CommandType.StoredProcedure;
                countryDropDowncmd.CommandText = "PR_LOC_Country_SelectForDropDown";
                DataTable ountryDropDowndt = new DataTable();
                SqlDataReader countryDropDownsdr = countryDropDowncmd.ExecuteReader();
                ountryDropDowndt.Load(countryDropDownsdr);
                List<LOC_CountryDropDownModel> countrydropdownlist = new List<LOC_CountryDropDownModel>();
                foreach (DataRow dr in ountryDropDowndt.Rows)
                {
                    LOC_CountryDropDownModel countryvlst = new LOC_CountryDropDownModel();
                    countryvlst.CountryID = (int)dr["CountryID"];
                    countryvlst.CountryName = (string)dr["CountryName"];
                    countrydropdownlist.Add(countryvlst);
                }
                ViewBag.CountryList = countrydropdownlist;


                SqlConnection conn2 = new SqlConnection(MyConnectionString);
                conn2.Open();
                SqlCommand stateDropDowncmd = conn2.CreateCommand();
                stateDropDowncmd = conn2.CreateCommand();
                stateDropDowncmd.CommandType = CommandType.StoredProcedure;
                stateDropDowncmd.CommandText = "PR_LOC_State_SelectForDropDown";
                SqlDataReader stateDropDownsdr = stateDropDowncmd.ExecuteReader();
                DataTable stateDropDowndt = new DataTable();
                stateDropDowndt.Load(stateDropDownsdr);
                List<LOC_StateDropDownModel> statedropdownlist = new List<LOC_StateDropDownModel>();
                foreach (DataRow dr in stateDropDowndt.Rows)
                {
                    LOC_StateDropDownModel statevlst = new LOC_StateDropDownModel();
                    statevlst.StateID = (int)dr["StateID"];
                    statevlst.StateName = (string)dr["StateName"];
                    statedropdownlist.Add(statevlst);
                }
                ViewBag.StateList = statedropdownlist;


                SqlConnection conn3 = new SqlConnection(MyConnectionString);
                conn3.Open();
                SqlCommand cityDropDowncmd = conn3.CreateCommand();
                cityDropDowncmd = conn3.CreateCommand();
                cityDropDowncmd.CommandType = CommandType.StoredProcedure;
                cityDropDowncmd.CommandText = "PR_LOC_City_SelectForDropDown";
                SqlDataReader cityDropDownsdr = cityDropDowncmd.ExecuteReader();
                DataTable cityDropDowndt = new DataTable();
                cityDropDowndt.Load(cityDropDownsdr);
                List<LOC_CityDropDownModel> citydropdownlist = new List<LOC_CityDropDownModel>();
                foreach (DataRow dr2 in cityDropDowndt.Rows)
                {
                    LOC_CityDropDownModel cityvlst = new LOC_CityDropDownModel();
                    cityvlst.CityID = (int)dr2["CityID"];
                    cityvlst.CityName = (string)dr2["CityName"];
                    citydropdownlist.Add(cityvlst);
                }
                ViewBag.CityList = citydropdownlist;

                SqlConnection conn4 = new SqlConnection(MyConnectionString);
                conn4.Open();
                SqlCommand contactDropDowncmd = conn4.CreateCommand();
                contactDropDowncmd = conn4.CreateCommand();
                contactDropDowncmd.CommandType = CommandType.StoredProcedure;
                contactDropDowncmd.CommandText = "PR_CON_Contact_SelectForDropDown";
                SqlDataReader contactDropDownsdr = contactDropDowncmd.ExecuteReader();
                DataTable contactDropDowndt = new DataTable();
                contactDropDowndt.Load(contactDropDownsdr);
                List<CON_ContactDropDownModel> contactDropDownlist = new List<CON_ContactDropDownModel>();
                foreach (DataRow dr in contactDropDowndt.Rows)
                {
                    CON_ContactDropDownModel contactvlst = new CON_ContactDropDownModel();
                    contactvlst.ContactID = (int)dr["ContactID"];
                    contactvlst.PersonName = (string)dr["PersonName"];
                    contactDropDownlist.Add(contactvlst);
                }
                ViewBag.ContactList = contactDropDownlist;
                conn4.Close();

            return View("CON_ContactList", contactSelectAlldt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {      
            CON_DAL dalCON = new CON_DAL();
            DataTable contactDeletedt = dalCON.PR_CON_Contact_DeleteByPK(ContactID);
            return RedirectToAction("Index");
        }
        #endregion

        #region Insert and Update
        [HttpPost]
        public IActionResult Save(CON_ContactModel modelCON_Contact)
        {
            if (modelCON_Contact.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileNameWithPath = Path.Combine(path, modelCON_Contact.File.FileName);             
                modelCON_Contact.PhotoPath = FilePath.Replace("wwwroot\\", "/") + "/" + modelCON_Contact.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelCON_Contact.File.CopyTo(stream);
                }
            }

        
            CON_DAL dalLOC = new CON_DAL();

            if (modelCON_Contact.ContactID == null)
            {
                DataTable contactInsertdt = dalLOC.PR_CON_Contact_Insert(modelCON_Contact);               
            }
            else
            {
                DataTable contactUpdatedt = dalLOC.PR_CON_Contact_UpdateByPK(modelCON_Contact);               
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public object Add(int ContactID)
        {
            #region Country Dropdown
          
            DataTable countryDropDowndt = new DataTable();
            SqlConnection conn1 = new SqlConnection(MyConnectionString);
            conn1.Open();

            SqlCommand countryDropDowncmd = conn1.CreateCommand();
            countryDropDowncmd.CommandType = CommandType.StoredProcedure;
            countryDropDowncmd.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader countryDropDownsdr = countryDropDowncmd.ExecuteReader();
            countryDropDowndt.Load(countryDropDownsdr);
            conn1.Close();

            List<LOC_CountryDropDownModel> countryDropDownlist = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in countryDropDowndt.Rows)
            {
                LOC_CountryDropDownModel countryDropDownvlst = new LOC_CountryDropDownModel();
                countryDropDownvlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                countryDropDownvlst.CountryName = dr["CountryName"].ToString();
                countryDropDownlist.Add(countryDropDownvlst);
            }
            ViewBag.CountryList = countryDropDownlist;

            #endregion

            #region State Dropdown

            List<LOC_StateDropDownModel> stateDropDownlist = new List<LOC_StateDropDownModel>();
            ViewBag.StateList = stateDropDownlist;

            #endregion

            #region City DropDown

            List<LOC_CityDropDownModel> cityDropDownlist = new List<LOC_CityDropDownModel>();
            ViewBag.CityList = cityDropDownlist;

            #endregion

            #region Contact Category DropDown
        
            DataTable contactCategorydt = new DataTable();
            SqlConnection conn4 = new SqlConnection(MyConnectionString);
            conn4.Open();
            SqlCommand contactCategorycmd = conn4.CreateCommand();
            contactCategorycmd.CommandType = CommandType.StoredProcedure;
            contactCategorycmd.CommandText = "PR_MST_ContactCategory_SelectForDropDown";
            SqlDataReader contactCategorysdr = contactCategorycmd.ExecuteReader();
            contactCategorydt.Load(contactCategorysdr);
            conn4.Close();

            List<MST_ContactCategoryDropDownModel> contactCategoryDropDownlist = new List<MST_ContactCategoryDropDownModel>();
            foreach (DataRow dr in contactCategorydt.Rows)
            {
                MST_ContactCategoryDropDownModel contactCategoryvlst = new MST_ContactCategoryDropDownModel();
                contactCategoryvlst.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                contactCategoryvlst.ContactCategoryName = dr["ContactCategoryName"].ToString();
                contactCategoryDropDownlist.Add(contactCategoryvlst);
            }
            ViewBag.ContactCategoryList = contactCategoryDropDownlist;

            #endregion

            #region Select By PK
            if (ContactID != null)
            {              
                SqlConnection conn = new SqlConnection(MyConnectionString);
                conn.Open();
                SqlCommand ContactSelectcmd = conn.CreateCommand();
                ContactSelectcmd.CommandType = CommandType.StoredProcedure;
                ContactSelectcmd.CommandText = "PR_CON_Contact_SelectByPK";
                ContactSelectcmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;
                DataTable contactSelectdt = new DataTable();
                SqlDataReader contactSelectsdr = ContactSelectcmd.ExecuteReader();
                contactSelectdt.Load(contactSelectsdr);

                CON_ContactModel modelCON_Contact = new CON_ContactModel();
                foreach (DataRow dr in contactSelectdt.Rows)
                {
                    modelCON_Contact.ContactID = Convert.ToInt32(dr["ContactID"]);
                    modelCON_Contact.PersonName = dr["PersonName"].ToString();
                    modelCON_Contact.Address = dr["Address"].ToString();
                    modelCON_Contact.CityID = (int)dr["CityID"];
                    modelCON_Contact.StateID = (int)dr["StateID"];
                    modelCON_Contact.CountryID = (int)dr["CountryID"];
                    modelCON_Contact.Pincode = (int)dr["Pincode"];
                    modelCON_Contact.Email = dr["Email"].ToString();
                    modelCON_Contact.MobileNumber = dr["MobileNumber"].ToString();
                    modelCON_Contact.AlternateContact = dr["AlternateContact"].ToString();
                    modelCON_Contact.BirthDate = (DateTime)dr["BirthDate"];
                    modelCON_Contact.AnniversaryDate = (DateTime)dr["AnniversaryDate"];
                    modelCON_Contact.Linkedin = dr["Linkedin"].ToString();
                    modelCON_Contact.Twitter = dr["Twitter"].ToString();
                    modelCON_Contact.Instagram = dr["Instagram"].ToString();
                    modelCON_Contact.Gender = dr["Gender"].ToString();
                    modelCON_Contact.TypeOfProfession = dr["TypeOfProfession"].ToString();
                    modelCON_Contact.CompanyName = dr["CompanyName"].ToString();
                    modelCON_Contact.Designation = dr["Designation"].ToString();
                    modelCON_Contact.ContactCategory = (int)dr["ContactCategory"];
                    modelCON_Contact.PhotoPath = dr["PhotoPath"].ToString();
                    modelCON_Contact.CreationDate = (DateTime)dr["CreationDate"];
                    modelCON_Contact.ModificationDate = (DateTime)dr["ModificationDate"];
                }

               
                DataTable dtState = new DataTable();
                SqlConnection connState = new SqlConnection(MyConnectionString);
                connState.Open();

                SqlCommand cmdState = connState.CreateCommand();
                cmdState.CommandType = CommandType.StoredProcedure;
                cmdState.CommandText = "PR_LOC_State_SelectDropDownByCountryID";
                cmdState.Parameters.AddWithValue("@CountryID", modelCON_Contact.CountryID);
                SqlDataReader sdrState = cmdState.ExecuteReader();
                dtState.Load(sdrState);

                List<LOC_StateDropDownModel> vListState = new List<LOC_StateDropDownModel>();
                foreach (DataRow drState in dtState.Rows)
                {
                    LOC_StateDropDownModel vStateList = new LOC_StateDropDownModel();
                    vStateList.StateID = Convert.ToInt32(drState["StateID"]);
                    vStateList.StateName = drState["StateName"].ToString();
                    vListState.Add(vStateList);
                }
                ViewBag.StateList = vListState;

               
                DataTable dtCity = new DataTable();
                SqlConnection connCity = new SqlConnection(MyConnectionString);
                connCity.Open();

                SqlCommand cmdCity = connState.CreateCommand();
                cmdCity.CommandType = CommandType.StoredProcedure;
                cmdCity.CommandText = "PR_LOC_City_SelectDropDownByStateID";
                cmdCity.Parameters.AddWithValue("@StateID", modelCON_Contact.StateID);
                SqlDataReader sdrCity = cmdCity.ExecuteReader();
                dtCity.Load(sdrCity);
                connCity.Close();


                List<LOC_CityDropDownModel> vListCity = new List<LOC_CityDropDownModel>();
                foreach (DataRow drCity in dtCity.Rows)
                {
                    LOC_CityDropDownModel vCityList = new LOC_CityDropDownModel();
                    vCityList.CityID = Convert.ToInt32(drCity["CityID"]);
                    vCityList.CityName = drCity["CityName"].ToString();
                    vListCity.Add(vCityList);
                }
                ViewBag.CityList = vListCity;

                return View("CON_ContactAddEdit", modelCON_Contact);
            }
            #endregion

            return View("CON_ContactAddEdit");
        }

        #endregion

        #region DropDown By CountryID
        public IActionResult DropDownByCountry(int CountryID)
        {
            DataTable dropdownByCountrydt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionString);
            conn.Open();

            SqlCommand countrycmd = conn.CreateCommand();
            countrycmd.CommandType = CommandType.StoredProcedure;
            countrycmd.CommandText = "PR_LOC_State_SelectDropDownByCountryID";
            countrycmd.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader dropdownByCountrysdr = countrycmd.ExecuteReader();
            dropdownByCountrydt.Load(dropdownByCountrysdr);

            List<LOC_StateDropDownModel> StateDropDownlist = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr in dropdownByCountrydt.Rows)
            {
                LOC_StateDropDownModel Statevlst = new LOC_StateDropDownModel();
                Statevlst.StateID = Convert.ToInt32(dr["StateID"]);
                Statevlst.StateName = dr["StateName"].ToString();
                StateDropDownlist.Add(Statevlst);
            }
            var vModel = StateDropDownlist;
            return Json(vModel);
        }
        #endregion

        #region DropDown By StateID
        public IActionResult DropDownByState(int StateID)
        {
            DataTable dropdownByStatedt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionString);
            conn.Open();

            SqlCommand statecmd = conn.CreateCommand();
            statecmd.CommandType = CommandType.StoredProcedure;
            statecmd.CommandText = "PR_LOC_City_SelectDropDownByStateID";
            statecmd.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader statesdr = statecmd.ExecuteReader();
            dropdownByStatedt.Load(statesdr);
            conn.Close();

            List<LOC_CityDropDownModel> citylist = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr in dropdownByStatedt.Rows)
            {
                LOC_CityDropDownModel cityvlst = new LOC_CityDropDownModel();
                cityvlst.CityID = Convert.ToInt32(dr["CityID"]);
                cityvlst.CityName = dr["CityName"].ToString();
                citylist.Add(cityvlst);
            }
            var vModel = citylist;
            return Json(vModel);
        }
        #endregion
    }
}
