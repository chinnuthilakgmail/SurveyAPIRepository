using Microsoft.Extensions.Configuration;
using SurveyAPI.DataAccess;
using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.DataClass
{
    public class QuestionData
    {
        public readonly IConfiguration _config;
        public QuestionData(IConfiguration configuration)
        {
            _config = configuration;
        }
        /// <summary>
        /// Create question
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int CreateQuestion(CreateQuestionModel model)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            Dapper.DynamicParameters parameter = new Dapper.DynamicParameters();
            parameter.Add("Question", model.Question.Question);
            parameter.Add("TypeId", model.Question.QuestionType.Id);
            parameter.Add("SurveyId", model.SurveyId);
            parameter.Add("QuestionNumber", model.QuestionNumber);
            parameter.Add("p", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            parameter.Add("QuestionId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            int outValue;
            int retValue = sqlDataAccess.SaveDataWithReturnAndOut<int,int>("uspCreateQuestion", parameter, "p", ConnectionStrings.SurveyDatabase.ToString(), "QuestionId", out outValue);
            model.Question.Id = outValue;
            return retValue;
        }
        /// <summary>
        /// SaveQuestionType
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1 if inserted, 2 if type exists</returns>
        public int SaveQuestionType(QuestionTypeModel model)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            Dapper.DynamicParameters parameter = new Dapper.DynamicParameters();
            parameter.Add("QuestionType", model.QuestionType);
            parameter.Add("IsOptionsRequired", model.IsOptionsRequired);
            parameter.Add("p", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

            return sqlDataAccess.SaveDataWithReturn<int>("uspSaveQuestionType", parameter, "p", ConnectionStrings.SurveyDatabase.ToString());
        }
        /// <summary>
        /// get all question types
        /// </summary>
        /// <returns></returns>
        public List<QuestionTypeModel> GetAllQuestionType()
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            return sqlDataAccess.LoadData<QuestionTypeModel, dynamic>("uspGetAllQuestionTypes", null, ConnectionStrings.SurveyDatabase.ToString());

        }
        /// <summary>
        /// update question
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateQuestion(CreateQuestionModel model)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            Dapper.DynamicParameters parameter = new Dapper.DynamicParameters();
            parameter.Add("QuestionId", model.Question.Id);
            parameter.Add("Question", model.Question.Question);
            parameter.Add("TypeId", model.Question.QuestionType.Id);
            parameter.Add("SurveyId", model.SurveyId);
            parameter.Add("QuestionNumber", model.QuestionNumber);
            parameter.Add("p", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

            return sqlDataAccess.SaveDataWithReturn<int>("uspUpdateQuestion", parameter, "p", ConnectionStrings.SurveyDatabase.ToString());
        }
        /// <summary>
        /// delete question
        /// </summary>
        /// <param name="questionId"></param>
        public void DeleteQuestion(int questionId)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            var parameter = new { QuestionId = questionId };
            sqlDataAccess.SaveData("uspDeleteQuestion", parameter, ConnectionStrings.SurveyDatabase.ToString());
        }
        /// <summary>
        /// Save option for question
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1 if option inserted, 0 if not inserted</returns>
        public int InsertOptions(OptionsDataModel model)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);

            Dapper.DynamicParameters parameter = new Dapper.DynamicParameters();
            parameter.Add("OptionText", model.OptionText);
            parameter.Add("QuestionId", model.QuestionId);
            parameter.Add("p", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            return sqlDataAccess.SaveDataWithReturn<int>("uspSaveOption", parameter, "p", ConnectionStrings.SurveyDatabase.ToString());

        }
        /// <summary>
        /// delete options in question
        /// </summary>
        /// <param name="questionId"></param>
        public void DeleteOptions(int questionId)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            var parameter = new { QuestionId = questionId };
            sqlDataAccess.SaveData("uspDeleteOptions", parameter, ConnectionStrings.SurveyDatabase.ToString());
        }
       
    }
}
