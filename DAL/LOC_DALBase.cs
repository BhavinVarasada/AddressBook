using AddressBook.Areas.LOC_City.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AddressBook.DAL
{
    public class LOC_DALBase : DALHelper
    {
        #region PR_LOC_Country_SelectAll & Filter
        public DataTable PR_LOC_Country_SelectAll(string CountryName = null, string CountryCode = null)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectAll");

                if (CountryName != null || CountryCode!= null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectByCountryNameCode");
                    sqlDB.AddInParameter(dbCMD, "@CountryName", DbType.String, CountryName != null ? CountryName : DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@CountryCode", DbType.String, CountryCode != null ? CountryCode : DBNull.Value);
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

        #region PR_LOC_State_SelectAll & Filter
        public DataTable PR_LOC_State_SelectAll(string countryName = null, string stateName = null, string? stateCode = null)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectAll");

                if (countryName != null || stateName != null || stateCode != null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectByStateNameCode");

                    sqlDB.AddInParameter(dbCMD, "@CountryName", DbType.String, countryName != null ? countryName : DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@StateName", DbType.String, stateName != null ? stateName : DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@StateCode", DbType.String, stateCode != null ? stateCode : DBNull.Value);                   
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

        #region PR_LOC_City_SelectAll & Filter
        public DataTable PR_LOC_City_SelectAll(string countryName = null, string stateName = null, string cityName = null, string cityCode = null)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectAll");
                if (countryName != null || stateName != null || cityName != null || cityCode != null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectByCityNameCode");

                    sqlDB.AddInParameter(dbCMD, "@CountryName", DbType.String, countryName != null ? countryName : DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@StateName", DbType.String, stateName != null ? stateName : DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@CityName", DbType.String, cityName != null ? cityName : DBNull.Value);
                    sqlDB.AddInParameter(dbCMD, "@CityCode", DbType.String, cityCode != null ? cityCode : DBNull.Value);
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

        #region PR_LOC_Country_Insert
        public DataTable PR_LOC_Country_Insert(string CountryName, string CountryCode, DateTime CreationDate, DateTime ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.NVarChar, CountryCode);         
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.Date, DBNull.Value);
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

        #region PR_LOC_State_Insert
        public DataTable PR_LOC_State_Insert(string StateCode, string StateName, int CountryID, DateTime CreationDate, DateTime ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_Insert");

                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.NVarChar, StateCode);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, StateName);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.NVarChar, CountryID);
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

        #region PR_LOC_City_Insert
        public DataTable PR_LOC_City_Insert(LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_Insert");

                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.NVarChar, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.NVarChar, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.NVarChar, modelLOC_City.CityCode);             

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

        #region PR_LOC_Country_UpdateByPK
        public DataTable PR_LOC_Country_UpdateByPK(int CountryID, string CountryName, string CountryCode, DateTime ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.NVarChar, CountryCode);
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

        #region PR_LOC_State_UpdateByPK
        public DataTable PR_LOC_State_UpdateByPK(int StateID, string StateCode, string StateName, int CountryID, DateTime ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_UpdateByPK");

                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.NVarChar, StateCode);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, StateName);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.NVarChar, CountryID);
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

        #region PR_LOC_City_UpdateByPK
        public DataTable PR_LOC_City_UpdateByPK(LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_UpdateByPK");

                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelLOC_City.CityID);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.NVarChar, modelLOC_City.CityCode);
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

        #region PR_LOC_Country_DeleteByPK
        public DataTable PR_LOC_Country_DeleteByPK(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);

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

        #region PR_LOC_State_DeleteByPK
        public DataTable PR_LOC_State_DeleteByPK(int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);

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

        #region PR_LOC_City_DeleteByPK
        public DataTable PR_LOC_City_DeleteByPK(int CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(MyConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);

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
