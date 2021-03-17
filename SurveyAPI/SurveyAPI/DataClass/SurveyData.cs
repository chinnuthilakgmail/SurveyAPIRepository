using SurveyAPI.DataAccess;
using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SurveyAPI.DataClass
{
    public class SurveyData
    {
        public readonly IConfiguration _config;
        public SurveyData(IConfiguration configuration)
        {
            _config = configuration;
        }
        /// <summary>
        /// Create Survey
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int CreateSurvey(SurveyDataModel model)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
             //parameters
            Dapper.DynamicParameters parameter = new Dapper.DynamicParameters();
            parameter.Add("Title", model.Title);
            parameter.Add("Description", model.Description);
            parameter.Add("StartDate", model.StartDate);
            parameter.Add("EndDate", model.EndDate);
            parameter.Add("IsOpen", model.IsOpen);
            parameter.Add("p", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

            parameter.Add("SurveyId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            int outValue;
            int retValue = sqlDataAccess.SaveDataWithReturnAndOut<int, int>("uspCreateSurvey", parameter, "p", ConnectionStrings.SurveyDatabase.ToString(), "SurveyId", out outValue);
            model.Id = outValue;
            return retValue;
        }
        /// <summary>
        /// Update Survey Details
        /// </summary>
        /// <param name="model"></param>
        public void UpdateSurvey(SurveyDataModel model)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            sqlDataAccess.SaveData<SurveyDataModel>("uspUpdateSurvey", model, ConnectionStrings.SurveyDatabase.ToString());
        }
        /// <summary>
        /// Delete survey
        /// </summary>
        /// <param name="surveyId"></param>
        public void DeleteSurvey(int surveyId)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            var parameter = new { Id = surveyId };
            sqlDataAccess.SaveData("uspDeleteSurvey", parameter, ConnectionStrings.SurveyDatabase.ToString());
        }
        /// <summary>
        /// Close Survey
        /// </summary>
        /// <param name="surveyId"></param>
        public void CloseSurvey(int surveyId)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            var parameter = new { Id = surveyId };
            sqlDataAccess.SaveData("uspCloseSurvey", parameter, ConnectionStrings.SurveyDatabase.ToString());
        }
        /// <summary>
        /// Get survey details, all questions and options
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public SurveyModel GetSurveyDetails(int surveyId)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);
            var parameter = new  { SurveyId = surveyId };
            List<SurveyBindingModel> surveyBindingModels = sqlDataAccess.LoadData<SurveyBindingModel, dynamic>("uspGetSurveyDetails", parameter, ConnectionStrings.SurveyDatabase.ToString());

            return GetModelFromDBModel(surveyBindingModels);
        }
        /// <summary>
        /// Method for converting data model to proper model structure
        /// </summary>
        /// <param name="surveyBindingModels"></param>
        /// <returns></returns>
        private SurveyModel GetModelFromDBModel(List<SurveyBindingModel> surveyBindingModels)
        {
            SurveyModel model = new SurveyModel();
            //survey details
            if (surveyBindingModels.Count > 0)
            {
                model.Id = surveyBindingModels[0].SurveyId;
                model.Title = surveyBindingModels[0].SurveyTitle;
                model.Description = surveyBindingModels[0].SurveyDescription;
                model.StartDate = surveyBindingModels[0].SurveyStartDate;
                model.EndDate = surveyBindingModels[0].SurveyEndDate;
                model.IsOpen = surveyBindingModels[0].IsOpen;
                model.Questions = new List<QuestionsModel>();
            }
            foreach (SurveyBindingModel survey in surveyBindingModels)
            {
                QuestionsModel questionsModel = new QuestionsModel();
                //if question exists
                if (model.Questions.Any(x => x.Id == survey.QuestionId))
                {
                    questionsModel = model.Questions.Where(x => x.Id == survey.QuestionId).First();
                    //add option to question
                    questionsModel.Options.Add(new OptionsModel()
                    {
                        Id = survey.OptionId,
                        OptionText = survey.OptionText
                    });
                }
                else
                {
                    //question details
                    questionsModel.Id = survey.QuestionId;
                    questionsModel.Question = survey.Question;
                    questionsModel.QuestionNumber = survey.QuestionNumber;
                    questionsModel.QuestionType = new QuestionTypeModel()
                    {
                        Id = survey.QuestionTypeId,
                        QuestionType = survey.QuestionType,
                        IsOptionsRequired = survey.IsOptionsRequired
                    };
                    //option details
                    questionsModel.Options = new List<OptionsModel>();
                    questionsModel.Options.Add(new OptionsModel()
                    {
                        Id = survey.OptionId,
                        OptionText = survey.OptionText
                    });
                    model.Questions.Add(questionsModel);
                }
                
            }

            return model;
        }


    }
}
