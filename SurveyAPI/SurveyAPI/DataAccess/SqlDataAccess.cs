using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SurveyAPI.DataAccess
{
    public enum ConnectionStrings
    {
        SurveyDatabase,
        AspNetDatabase
    }
    public class SqlDataAccess
    {
        public readonly IConfiguration config;
        public SqlDataAccess(IConfiguration configuration)
        {
            config = configuration;
        }
        public string GetSqlConnectionString(string name)
        {
            return config.GetConnectionString(name);
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetSqlConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetSqlConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        
        public T SaveDataWithReturn<T>(string storedProcedure, DynamicParameters parameters, string retParameterName,   string connectionStringName)
        {
            string connectionString = GetSqlConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<T>(retParameterName);
            }
        }
        //public T SaveDataWithReturnAndOut<T,U>(string storedProcedure, DynamicParameters parameters, string retParameterName, string connectionStringName, string outParameterName,out U outValue)
        //{
        //    string connectionString = GetSqlConnectionString(connectionStringName);

        //    using (IDbConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Query<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        //        outValue = parameters.Get<U>(outParameterName);
        //        return parameters.Get<T>(retParameterName);
        //    }
        //}

        public T SaveDataWithReturnAndOut<T, U>(string storedProcedure, DynamicParameters parameters, string retParameterName , string connectionStringName, string outParameterName, out U outValue  )
        {
            string connectionString = GetSqlConnectionString(connectionStringName);
            outValue = default(U);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                if (outParameterName != null && !string.IsNullOrEmpty( outParameterName))
                { 
                    outValue = parameters.Get<U>(outParameterName); 
                }
                return parameters.Get<T>(retParameterName);

            }
        }
    }
}
