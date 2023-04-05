using AddressBook.Areas.LOC_City.Models;
using AddressBook.Areas.LOC_Country.Models;
using AddressBook.Areas.LOC_State.Models;
using AddressBook.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]
    public class LOC_CityController : Controller
    {
        string MyConnectionString = DALHelper.MyConnectionString;
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }

        #region SelectAll
        public IActionResult Index(LOC_CityModel modelLOC_City)
        {
            DataTable citySelectAlldt = new DataTable();
            LOC_DALBase dalLOC = new LOC_DALBase();
            citySelectAlldt = dalLOC.PR_LOC_City_SelectAll(modelLOC_City.CountryName, modelLOC_City.StateName, modelLOC_City.CityName, modelLOC_City.CityCode);
            
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
                foreach (DataRow dr in countryDropDowndt.Rows)
                {
                    LOC_CountryDropDownModel countryvlst = new LOC_CountryDropDownModel();
                    countryvlst.CountryID = (int)dr["CountryID"];
                    countryvlst.CountryName = (string)dr["CountryName"];
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
                SqlCommand cityDropDown = conn3.CreateCommand();
                cityDropDown = conn3.CreateCommand();
                cityDropDown.CommandType = CommandType.StoredProcedure;
                cityDropDown.CommandText = "PR_LOC_City_SelectForDropDown";
                SqlDataReader cityDropDownsdr = cityDropDown.ExecuteReader();
                DataTable cityDropDowndt = new DataTable();
                cityDropDowndt.Load(cityDropDownsdr);
                List<LOC_CityDropDownModel> citydropdownlist = new List<LOC_CityDropDownModel>();
                foreach (DataRow dr in cityDropDowndt.Rows)
                {
                    LOC_CityDropDownModel cityvlst = new LOC_CityDropDownModel();
                    cityvlst.CityID = (int)dr["CityID"];
                    cityvlst.CityName = (string)dr["CityName"];
                    citydropdownlist.Add(cityvlst);
                }
                ViewBag.CityList = citydropdownlist;
                conn3.Close();
            return View("LOC_CityList", citySelectAlldt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CityID)
        {         
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable cityDeletedt = dalLOC.PR_LOC_City_DeleteByPK(CityID);
            return RedirectToAction("Index");
        }
        #endregion

        #region Insert and Update
        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            LOC_DAL dalLOC = new LOC_DAL();

            if (modelLOC_City.CityID == null)
            {
                DataTable cityInsertdt = dalLOC.PR_LOC_City_Insert(modelLOC_City);             
            }
            else
            {
                DataTable cityUpdatedt = dalLOC.PR_LOC_City_UpdateByPK(modelLOC_City);             
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? CityID)
        {

            #region Country Dropdown

            DataTable countrySelectdt = new DataTable();
            SqlConnection conn1 = new SqlConnection(MyConnectionString);
            conn1.Open();

            SqlCommand countryDropDowncmd = conn1.CreateCommand();
            countryDropDowncmd.CommandType = CommandType.StoredProcedure;
            countryDropDowncmd.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader countryDropDownsdr = countryDropDowncmd.ExecuteReader();
            countrySelectdt.Load(countryDropDownsdr);
            conn1.Close();

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

            #region State Dropdown

            List<LOC_StateDropDownModel> stateDropDownlist = new List<LOC_StateDropDownModel>();
            ViewBag.StateList = stateDropDownlist;

            #endregion

            #region Select By PK

            if (CityID != null)
            {
                SqlConnection conn = new SqlConnection(MyConnectionString);
                conn.Open();
                SqlCommand citySelectcmd = conn.CreateCommand();
                citySelectcmd.CommandType = CommandType.StoredProcedure;
                citySelectcmd.CommandText = "PR_LOC_City_SelectByPK";
                citySelectcmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                DataTable citySelectdt = new DataTable();
                SqlDataReader citySelectsdr = citySelectcmd.ExecuteReader();
                citySelectdt.Load(citySelectsdr);

                LOC_CityModel modelLOC_City = new LOC_CityModel();
                foreach (DataRow dr in citySelectdt.Rows)
                {
                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_City.CityName = dr["CityName"].ToString();
                    modelLOC_City.CityCode = dr["CityCode"].ToString();
                    modelLOC_City.CreationDate = (DateTime)dr["CreationDate"];
                    modelLOC_City.ModificationDate = (DateTime)dr["ModificationDate"];
                }

                DataTable dtState = new DataTable();
                SqlConnection connState = new SqlConnection(MyConnectionString);
                connState.Open();

                SqlCommand cmdState = connState.CreateCommand();
                cmdState.CommandType = CommandType.StoredProcedure;
                cmdState.CommandText = "PR_LOC_State_SelectDropDownByCountryID";
                cmdState.Parameters.AddWithValue("@CountryID", modelLOC_City.CountryID);
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
                cmdCity.Parameters.AddWithValue("@StateID", modelLOC_City.StateID);
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

                return View("LOC_CityAddEdit", modelLOC_City);
            }
            #endregion

            return View("LOC_CityAddEdit");
        }
        #endregion

        #region DropDown By CountryID
        public IActionResult DropDownByCountry(int CountryID)
        {
            DataTable dropDownByCountrydt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionString);
            conn.Open();

            SqlCommand dropDownByCountrycmd = conn.CreateCommand();
            dropDownByCountrycmd.CommandType = CommandType.StoredProcedure;
            dropDownByCountrycmd.CommandText = "PR_LOC_State_SelectDropDownByCountryID";
            dropDownByCountrycmd.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader dropDownByCountrysdr = dropDownByCountrycmd.ExecuteReader();
            dropDownByCountrydt.Load(dropDownByCountrysdr);

            List<LOC_StateDropDownModel> stateDropDownlist = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr in dropDownByCountrydt.Rows)
            {
                LOC_StateDropDownModel statevlist = new LOC_StateDropDownModel();
                statevlist.StateID = Convert.ToInt32(dr["StateID"]);
                statevlist.StateName = dr["StateName"].ToString();
                stateDropDownlist.Add(statevlist);
            }
            var vModel = stateDropDownlist;
            return Json(vModel);
        }
        #endregion

        #region DropDown By StateID
        public IActionResult DropDownByState(int StateID)
        {          
            DataTable dropDownByStatedt = new DataTable();
            SqlConnection conn = new SqlConnection(MyConnectionString);
            conn.Open();

            SqlCommand dropDownByStatecmd = conn.CreateCommand();
            dropDownByStatecmd.CommandType = CommandType.StoredProcedure;
            dropDownByStatecmd.CommandText = "PR_LOC_City_SelectDropDownByStateID";
            dropDownByStatecmd.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader dropDownByStatesdr = dropDownByStatecmd.ExecuteReader();
            dropDownByStatedt.Load(dropDownByStatesdr);
            conn.Close();

            List<LOC_CityDropDownModel> cityDropDownlist = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr in dropDownByStatedt.Rows)
            {
                LOC_CityDropDownModel cityvlist = new LOC_CityDropDownModel();
                cityvlist.CityID = Convert.ToInt32(dr["CityID"]);
                cityvlist.CityName = dr["CityName"].ToString();
                cityDropDownlist.Add(cityvlist);
            }
            var vModel = cityDropDownlist;
            return Json(vModel);
        }
        #endregion
    }
}

