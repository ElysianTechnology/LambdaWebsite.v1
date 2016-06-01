using System.Data.SqlClient;
using MachSecure.SVM.OverviewService.Properties;
using MachSecure.SEMS.BusinessObjects;

namespace MachSecure.SVM.OverviewService.Nancy
{
    public class ConnectionProvider
    {
        public static SqlConnectionStringBuilder ConnectionString(string dbName)
        {
            return new SqlConnectionStringBuilder
            {
            };

        }
    }
}
