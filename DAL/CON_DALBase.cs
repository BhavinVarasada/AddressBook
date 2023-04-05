using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using AddressBook.Areas.CON_Contact.Models;
using System.Security.Cryptography.X509Certificates;

namespace AddressBook.DAL
{
    public class CON_DALBase : DALHelper
    {
        #region PR_MST_ContactCategory_SelectAll
        public DataTable PR_MST_ContactCategory_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region PR_CON_Contact_SelectAll & Filter
        public DataTable PR_CON_Contact_SelectAll(string PersonName = null, string ContactCategoryName = null, string CountryName = null, string CityName = null, string StateName = null)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_SelectAll");
                if (PersonName != null || ContactCategoryName != null || CountryName != null || StateName != null || CityName != null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_SelectByContactNameCode");

                    sqlDB.AddInParameter(dbCMD, "@CountryName", DbType.String, CountryName != null ? CountryName : DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@StateName", DbType.String, StateName != null ? StateName : DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@CityName", DbType.String, CityName != null ? CityName : DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@PersonName", DbType.String, PersonName != null ? PersonName : DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@ContactCategoryName", DbType.String, ContactCategoryName != null ? ContactCategoryName : DBNull.Value);
                }

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region PR_MST_ContactCategory_Insert

        public DataTable PR_MST_ContactCategory_Insert(DateTime CreationDate, DateTime ModificationDate, string ContactCategoryName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_Insert");

                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.NVarChar, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.NVarChar, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.NVarChar,ContactCategoryName);
                
                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region PR_CON_Contact_Insert
        public DataTable PR_CON_Contact_Insert(CON_ContactModel modelCON_Contact)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_Insert");

                sqlDB.AddInParameter(dbCMD, "PersonName", SqlDbType.NVarChar, modelCON_Contact.PersonName);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.NVarChar, modelCON_Contact.Address);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelCON_Contact.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelCON_Contact.StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelCON_Contact.CityID);
                sqlDB.AddInParameter(dbCMD, "Pincode", SqlDbType.Int, modelCON_Contact.Pincode);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.NVarChar, modelCON_Contact.Email);
                sqlDB.AddInParameter(dbCMD, "MobileNumber", SqlDbType.NVarChar, modelCON_Contact.MobileNumber);
                sqlDB.AddInParameter(dbCMD, "AlternateContact", SqlDbType.NVarChar, modelCON_Contact.AlternateContact);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.Date, modelCON_Contact.BirthDate);
                sqlDB.AddInParameter(dbCMD, "AnniversaryDate", SqlDbType.Date, modelCON_Contact.AnniversaryDate);
                sqlDB.AddInParameter(dbCMD, "Linkedin", SqlDbType.NVarChar, modelCON_Contact.Linkedin);
                sqlDB.AddInParameter(dbCMD, "Twitter", SqlDbType.NVarChar, modelCON_Contact.Twitter);
                sqlDB.AddInParameter(dbCMD, "Instagram", SqlDbType.NVarChar, modelCON_Contact.Instagram);
                sqlDB.AddInParameter(dbCMD, "Gender", SqlDbType.NVarChar, modelCON_Contact.Gender);
                sqlDB.AddInParameter(dbCMD, "TypeOfProfession", SqlDbType.NVarChar, modelCON_Contact.TypeOfProfession);
                sqlDB.AddInParameter(dbCMD, "CompanyName", SqlDbType.NVarChar, modelCON_Contact.CompanyName);
                sqlDB.AddInParameter(dbCMD, "Designation", SqlDbType.NVarChar, modelCON_Contact.Designation);
                sqlDB.AddInParameter(dbCMD, "ContactCategory", SqlDbType.NVarChar, modelCON_Contact.ContactCategory);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelCON_Contact.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.Date, DBNull.Value);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region PR_MST_ContactCategory_UpdateByPK
        public DataTable PR_MST_ContactCategory_UpdateByPK(int ContactCategoryID, DateTime ModificationDate, string ContactCategoryName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_UpdateByPK");

                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.NVarChar,ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.NVarChar, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.NVarChar, ContactCategoryName);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region PR_CON_Contact_UpdateByPK
        public DataTable PR_CON_Contact_UpdateByPK(CON_ContactModel modelCON_Contact)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_UpdateByPK");

                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, modelCON_Contact.ContactID);
                sqlDB.AddInParameter(dbCMD, "PersonName", SqlDbType.NVarChar, modelCON_Contact.PersonName);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.NVarChar, modelCON_Contact.Address);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelCON_Contact.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelCON_Contact.StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelCON_Contact.CityID);
                sqlDB.AddInParameter(dbCMD, "Pincode", SqlDbType.Int, modelCON_Contact.Pincode);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.NVarChar, modelCON_Contact.Email);
                sqlDB.AddInParameter(dbCMD, "MobileNumber", SqlDbType.NVarChar, modelCON_Contact.MobileNumber);
                sqlDB.AddInParameter(dbCMD, "AlternateContact", SqlDbType.NVarChar, modelCON_Contact.AlternateContact);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.Date, modelCON_Contact.BirthDate);
                sqlDB.AddInParameter(dbCMD, "AnniversaryDate", SqlDbType.Date, modelCON_Contact.AnniversaryDate);
                sqlDB.AddInParameter(dbCMD, "Linkedin", SqlDbType.NVarChar, modelCON_Contact.Linkedin);
                sqlDB.AddInParameter(dbCMD, "Twitter", SqlDbType.NVarChar, modelCON_Contact.Twitter);
                sqlDB.AddInParameter(dbCMD, "Instagram", SqlDbType.NVarChar, modelCON_Contact.Instagram);
                sqlDB.AddInParameter(dbCMD, "Gender", SqlDbType.NVarChar, modelCON_Contact.Gender);
                sqlDB.AddInParameter(dbCMD, "TypeOfProfession", SqlDbType.NVarChar, modelCON_Contact.TypeOfProfession);
                sqlDB.AddInParameter(dbCMD, "CompanyName", SqlDbType.NVarChar, modelCON_Contact.CompanyName);
                sqlDB.AddInParameter(dbCMD, "Designation", SqlDbType.NVarChar, modelCON_Contact.Designation);
                sqlDB.AddInParameter(dbCMD, "ContactCategory", SqlDbType.NVarChar, modelCON_Contact.ContactCategory);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelCON_Contact.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, DBNull.Value);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region PR_MST_ContactCategory_DeleteByPK

        public DataTable PR_MST_ContactCategory_DeleteByPK(int ContactCategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, ContactCategoryID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region PR_CON_Contact_DeleteByPK

        public DataTable PR_CON_Contact_DeleteByPK(int ContactID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, ContactID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
