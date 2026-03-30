using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Configuration;
using System.Data;

namespace TimeTable_api
{
    public static class DatabaseFactory
    {

        #region PrivateMembers 

        private static IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors 

        static DatabaseFactory()
        {
            _dbFactory = new OrmLiteConnectionFactory(
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                MySqlDialect.Provider);
        }

        #endregion

        #region PublicMethods 

        /// <summary>
        /// return connection
        /// </summary>
        /// <returns></returns>
        public static IDbConnection OpenDbConnection()
        {
            return _dbFactory.OpenDbConnection();
        }

        #endregion

    }

}