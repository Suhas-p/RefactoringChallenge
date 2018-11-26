using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.ComponentModel;

namespace WinFormApp
{ 
    public class DapperDBAccess
    {
        public DapperDBAccess() { }

        public void QueryDB(string sql, BindingList<SystemUserModel> users, object param = null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DapperDemoDB"].ConnectionString;
            
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                var records = cnn.Query<SystemUserModel>(sql, param, commandType: CommandType.StoredProcedure).ToList();

                users.Clear();
                records.ForEach(x => users.Add(x));
            }            
        }   
        
        public void AddRecord(object record)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DapperDemoDB"].ConnectionString;

            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Execute("dbo.spSystemUser_Create", record, commandType: CommandType.StoredProcedure);               
            }

        }    
    }
};