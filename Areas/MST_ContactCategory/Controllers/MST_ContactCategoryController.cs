using AddressBook.Areas.MST_ContactCategory.Models;
using AddressBook.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Areas.MST_ContactCategory.Controllers
{
    [Area("MST_ContactCategory")]
    [Route("MST_ContactCategory/[controller]/[action]")]
    public class MST_ContactCategoryController : Controller
    {
        string MyConnectionString = DALHelper.MyConnectionString;
        private IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Back()
        {
            return Index();
        }

        #region SelectAll
        public IActionResult Index()
        { 
            CON_DAL dalLOC = new CON_DAL();
            DataTable contactCategorydt = dalLOC.PR_MST_ContactCategory_SelectAll();
            return View("MST_ContactCategoryList", contactCategorydt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(string conn, int ContactCategoryID)
        {
            CON_DAL dalCON = new CON_DAL();
            DataTable contactCategoryDeletedt = dalCON.PR_MST_ContactCategory_DeleteByPK(ContactCategoryID);
            return RedirectToAction("Index");
        }

        #endregion

        #region Insert
        [HttpPost]
        public IActionResult Save(MST_ContactCategoryModel modelMST_ContactCategory, int ContactCategoryID, DateTime CreationDate, DateTime ModificationDate, string ContactCategoryName, string PhotoPath)
        {
            CON_DAL dalLOC = new CON_DAL();

            if (modelMST_ContactCategory.ContactCategoryID == null)
            {
                DataTable contactCategoryInsertdt = dalLOC.PR_MST_ContactCategory_Insert(CreationDate, ModificationDate, ContactCategoryName);              
            }
            else
            {
                DataTable contactCategoryUpdatedt = dalLOC.PR_MST_ContactCategory_UpdateByPK(ContactCategoryID, ModificationDate, ContactCategoryName);             
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int ContactCategoryID)
        {
            if (ContactCategoryID != null)
            {
                SqlConnection conn = new SqlConnection(MyConnectionString);
                conn.Open();
                SqlCommand ContactCategorycmd = conn.CreateCommand();
                ContactCategorycmd.CommandType = CommandType.StoredProcedure;
                ContactCategorycmd.CommandText = "PR_MST_ContactCategory_SelectByPK";
                ContactCategorycmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable ContactCategorySelectdt = new DataTable();
                SqlDataReader ContactCategorysdr = ContactCategorycmd.ExecuteReader();
                ContactCategorySelectdt.Load(ContactCategorysdr);

                MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();
                foreach (DataRow dr in ContactCategorySelectdt.Rows)
                {
                    modelMST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelMST_ContactCategory.ContactCategoryName = dr["ContactCategoryName"].ToString();
                    modelMST_ContactCategory.CreationDate = (DateTime)dr["CreationDate"];
                    modelMST_ContactCategory.ModificationDate = (DateTime)dr["ModificationDate"];
                }
                return View("MSt_ContactCategoryAddEdit", modelMST_ContactCategory);
            }
            return View("MST_ContactCategoryAddEdit");
        }
        #endregion
    }
}
