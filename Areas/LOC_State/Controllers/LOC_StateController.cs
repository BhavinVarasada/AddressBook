using AddressBook.Areas.LOC_Country.Models;
using AddressBook.Areas.LOC_State.Models;
using AddressBook.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace AddressBook.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
    public class LOC_StateController : Controller
    {
        string MyConnectionString = DALHelper.MyConnectionString;
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }

        #region SelectAll
        public IActionResult Index(LOC_StateModel modelLOC_State)
        {
            DataTable stateSelectAlldt = new DataTable();
            LOC_DAL dalLOC = new LOC_DAL();
            stateSelectAlldt = dalLOC.PR_LOC_State_SelectAll(modelLOC_State.CountryName, modelLOC_State.StateName, modelLOC_State.StateCode);
            
                SqlConnection conn1 = new SqlConnection(MyConnectionString);
                conn1.Open();
                SqlCommand countryDropDown = conn1.CreateCommand();
                countryDropDown = conn1.CreateCommand();
                countryDropDown.CommandType = CommandType.StoredProcedure;
                countryDropDown.CommandText = "PR_LOC_Country_SelectForDropDown";

                DataTable countryDropDowndt = new DataTable();
                SqlDataReader countryDropDownsdr = countryDropDown.ExecuteReader();
                countryDropDowndt.Load(countryDropDownsdr);
                List<LOC_CountryDropDownModel> countrydropdownlist = new List<LOC_CountryDropDownModel>();
                foreach (DataRow dr1 in countryDropDowndt.Rows)
                {
                    LOC_CountryDropDownModel countryvlst = new LOC_CountryDropDownModel();
                    countryvlst.CountryID = (int)dr1["CountryID"];
                    countryvlst.CountryName = (string)dr1["CountryName"];
                    countrydropdownlist.Add(countryvlst);
                }
                ViewBag.CountryList = countrydropdownlist;

                SqlConnection conn2 = new SqlConnection(MyConnectionString);
                conn2.Open();
                SqlCommand stateDropDown = conn2.CreateCommand();
                stateDropDown = conn2.CreateCommand();
                stateDropDown.CommandType = CommandType.StoredProcedure;
                stateDropDown.CommandText = "PR_LOC_State_SelectForDropDown";
                SqlDataReader stateDropDownsdr = stateDropDown.ExecuteReader();
                DataTable stateDropDowndt = new DataTable();
                stateDropDowndt.Load(stateDropDownsdr);
                List<LOC_StateDropDownModel> statedropdownlist = new List<LOC_StateDropDownModel>();
                foreach (DataRow dr1 in stateDropDowndt.Rows)
                {
                    LOC_StateDropDownModel statevlst = new LOC_StateDropDownModel();
                    statevlst.StateID = (int)dr1["StateID"];
                    statevlst.StateName = (string)dr1["StateName"];
                    statedropdownlist.Add(statevlst);
                }
                ViewBag.StateList = statedropdownlist;

            return View("LOC_StateList", stateSelectAlldt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable stateDeletedt = dalLOC.PR_LOC_State_DeleteByPK(StateID);
            return RedirectToAction("Index");
        }

        #endregion

        #region Insert

        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State, string conn, int StateID, DateTime CreationDate, string StateName, string StateCode, int CountryID, DateTime ModificationDate, string PhotoPath)
        {
            LOC_DAL dalLOC = new LOC_DAL();

            if (modelLOC_State.StateID == null)
            {
                DataTable stateInsertdt = dalLOC.PR_LOC_State_Insert(StateCode, StateName, CountryID, CreationDate, ModificationDate);                
            }
            else
            {
                DataTable stateUpdatedt = dalLOC.PR_LOC_State_UpdateByPK(StateID, StateCode, StateName, CountryID, ModificationDate);              
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? StateID)
        {
            #region Country DropDown

            DataTable countrySelectdt = new DataTable();
            SqlConnection conn1 = new SqlConnection(MyConnectionString);
            conn1.Open();

            SqlCommand countryDropDowncmd = conn1.CreateCommand();
            countryDropDowncmd.CommandType = CommandType.StoredProcedure;
            countryDropDowncmd.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader countryDropDownsdr = countryDropDowncmd.ExecuteReader();
            countrySelectdt.Load(countryDropDownsdr);

            List<LOC_CountryDropDownModel> countryDropDownlist = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in countrySelectdt.Rows)
            {
                LOC_CountryDropDownModel countryvlst = new LOC_CountryDropDownModel();
                countryvlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                countryvlst.CountryName = dr["CountryName"].ToString();
                countryDropDownlist.Add(countryvlst);
            }
            ViewBag.CountryList = countryDropDownlist;

            #endregion 

            if (StateID != null)
            {
                SqlConnection conn = new SqlConnection(MyConnectionString);
                conn.Open();

                SqlCommand stateSelectcmd = conn.CreateCommand();
                stateSelectcmd.CommandType = CommandType.StoredProcedure;
                stateSelectcmd.CommandText = "PR_LOC_State_SelectByPK";
                stateSelectcmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                DataTable stateSelectdt = new DataTable();
                SqlDataReader stateSelectsdr = stateSelectcmd.ExecuteReader();
                stateSelectdt.Load(stateSelectsdr);

                LOC_StateModel modelLOC_State = new LOC_StateModel();
                foreach (DataRow dr in stateSelectdt.Rows)
                {
                    modelLOC_State.CountryID = (int)dr["CountryID"];
                    modelLOC_State.StateName = dr["StateName"].ToString();
                    modelLOC_State.StateCode = dr["StateCode"].ToString();
                    modelLOC_State.CreationDate = (DateTime)dr["CreationDate"];
                    modelLOC_State.ModificationDate = (DateTime)dr["ModificationDate"];
                }
                return View("LOC_StateAddEdit", modelLOC_State);
            }
            return View("LOC_StateAddEdit");
        }

        #endregion
    }
}
