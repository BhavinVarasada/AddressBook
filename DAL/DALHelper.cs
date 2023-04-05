namespace AddressBook.DAL
{
    public class DALHelper
    {
        #region database connection string
        public static string MyConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConnectionString");
        #endregion
    }
}
