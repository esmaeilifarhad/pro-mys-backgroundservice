using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Mys.DataAccess.Dapper
{
    public class DapperService
    {

        public  IEnumerable<T> ReturnData<T>(string query, object obj = null, string connectionString = "")
        {

            if (connectionString == "")
            {
                connectionString = @"Data Source=.;Initial Catalog=8719_manageyourself;Integrated Security=false;User ID=fe;Password=1234";
            }
            else
            {

            }

            if (obj == null)
            {
                using (IDbConnection DB = new SqlConnection(connectionString))
                {

                    var res = DB.Query<T>(query).ToList();
                    return res;
                }
            }
            else
            {
                using (IDbConnection DB = new SqlConnection(connectionString))
                {

                    var res = DB.Query<T>(query, obj).ToList();
                    return res;
                }
            }

        }
    }
}
