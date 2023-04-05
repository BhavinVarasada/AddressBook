using AddressBook.Areas.LOC_Country.Models;
using AddressBook.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace AddressBook.Areas.LOC_Country.Controllers
{
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        string MyConnectionString = DALHelper.MyConnectionString;
        private IConfiguration Configuration;     
        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }

        #region SelectAll
        public IActionResult Index(LOC_CountryModel modelLOC_Country)
        {          
            DataTable countrySelectAlldt = new DataTable();
            LOC_DAL dalLOC = new LOC_DAL();
            countrySelectAlldt = dalLOC.PR_LOC_Country_SelectAll(modelLOC_Country.CountryName, modelLOC_Country.CountryCode);
        
            SqlConnection conn1 = new SqlConnection(MyConnectionString);
            conn1.Open();
            SqlCommand countryDropDown = conn1.CreateCommand();
            countryDropDown = conn1.CreateCommand();
            countryDropDown.CommandType = CommandType.StoredProcedure;
            countryDropDown.CommandText = "PR_LOC_Country_SelectForDropDown";
            DataTable countryDropDowndt = new DataTable();
            SqlDataReader countrydropdownsdr = countryDropDown.ExecuteReader();
            countryDropDowndt.Load(countrydropdownsdr);
            List<LOC_CountryDropDownModel> countrydropdownlist = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr1 in countryDropDowndt.Rows)
            {
                LOC_CountryDropDownModel countryvlst = new LOC_CountryDropDownModel();
                countryvlst.CountryID = (int)dr1["CountryID"];
                countryvlst.CountryName = (string)dr1["CountryName"];
                countrydropdownlist.Add(countryvlst);
            }
            ViewBag.CountryList = countrydropdownlist;

            conn1.Close();
            
            return View("LOC_CountryList", countrySelectAlldt);
        }

        #endregion

        #region Delete
        public IActionResult Delete(int CountryID)
        {     
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable countryDeletedt = dalLOC.PR_LOC_Country_DeleteByPK(CountryID);
            return RedirectToAction("Index");
        }
        #endregion

        #region Insert

        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country, int CountryID, DateTime CreationDate, string CountryName, string CountryCode, DateTime ModificationDate)
        {         
            LOC_DAL dalLOC = new LOC_DAL();

            if (modelLOC_Country.CountryID == null)
            {
                DataTable countryInsertdt = dalLOC.PR_LOC_Country_Insert(CountryName, CountryCode, CreationDate, ModificationDate);            
            }
            else
            {
                DataTable countryUpdatedt = dalLOC.PR_LOC_Country_UpdateByPK(CountryID, CountryName, CountryCode, ModificationDate);              
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? CountryID)
        {
            if (CountryID != null)
            {
                SqlConnection conn = new SqlConnection(MyConnectionString);
                conn.Open();
                SqlCommand countrySelectcmd = conn.CreateCommand();
                countrySelectcmd.CommandType = CommandType.StoredProcedure;
                countrySelectcmd.CommandText = "PR_LOC_Country_SelectByPK";
                countrySelectcmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                DataTable countrySelectdt = new DataTable();
                SqlDataReader countrySelectsdr = countrySelectcmd.ExecuteReader();
                countrySelectdt.Load(countrySelectsdr);

                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
                foreach (DataRow dr in countrySelectdt.Rows)
                {
                    modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_Country.CountryName = dr["CountryName"].ToString();
                    modelLOC_Country.CountryCode = dr["CountryCode"].ToString();
                    modelLOC_Country.CreationDate = (DateTime)dr["CreationDate"];
                    modelLOC_Country.ModificationDate = (DateTime)dr["ModificationDate"];
                }
                return View("LOC_CountryAddEdit", modelLOC_Country);
            }
            return View("LOC_CountryAddEdit");
        }
        #endregion
    }
}
