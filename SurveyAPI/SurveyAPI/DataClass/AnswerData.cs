using Microsoft.Extensions.Configuration;
using SurveyAPI.DataAccess;
using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.DataClass
{
    public class AnswerData
    {
        public readonly IConfiguration _config;
        public AnswerData(IConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// Save answer
        /// </summary>
        /// <param name="model"></param>
        /// <returns>0 if Question or Survey does not exist, 1 if success, 2 if no option available</returns>
        public int SaveAnswer(AnswersDataModel model)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            Dapper.DynamicParameters parameter = new Dapper.DynamicParameters();
            //@SurveyId int,
            parameter.Add("SurveyId", model.SurveyId);
            //@QuestionId int,
            parameter.Add("QuestionId", model.QuestionId);
            //@OptionId int = 0,
            parameter.Add("OptionId", model.OptionId);
            //@Answer nvarchar(MAX) = NULL,
            parameter.Add("Answer", model.Answer);
            //@PersonName nvarchar(200),
            parameter.Add("PersonName", model.Person.Name);
            //@PersonEmail nvarchar(300) 
            parameter.Add("PersonEmail", model.Person.Email);

            parameter.Add("p", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            return sqlDataAccess.SaveDataWithReturn<int>("uspSaveAnswer", parameter, "p", ConnectionStrings.SurveyDatabase.ToString());

        }
    }
}
