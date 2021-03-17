using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyAPI.DataClass;
using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SurveyAPI.Authentication;

namespace SurveyAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {

        private readonly ILogger<SurveyController> _logger;
        private readonly IConfiguration _config;

        public SurveyController(ILogger<SurveyController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        #region Survey
        [Authorize(Roles =UserRoles.Admin)]
        [HttpPost]
        [Route("CreateSurvey")]
        public IActionResult CreateSurvey(SurveyDataModel model)
        {
            try
            {
                SurveyData data = new SurveyData(_config);
                int ret = data.CreateSurvey(model);
                return Ok(model.Id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("UpdateSurvey")]
        public IActionResult UpdateSurvey(SurveyDataModel model)
        {
            try
            {
                SurveyData data = new SurveyData(_config);
                data.UpdateSurvey(model);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        [Route("DeleteSurvey")]
        public IActionResult DeleteSurvey(int surveyId)
        {
            try
            {
                SurveyData data = new SurveyData(_config);
                data.DeleteSurvey(surveyId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("CloseSurvey")]
        public IActionResult CloseSurvey(int surveyId)
        {
            try
            {
                SurveyData data = new SurveyData(_config);
                data.CloseSurvey(surveyId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("GetSurvey")]
        [AllowAnonymous]
        public IActionResult GetSurvey(int surveyId)
        {
            try
            {
                SurveyData data = new SurveyData(_config);                 
                return Ok(data.GetSurveyDetails(surveyId));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion


        #region Question
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("CreateQuestion")]
        public IActionResult CreateQuestion(CreateQuestionModel model)
        {
            try
            {
                if (model != null && model.Question != null && model.Question.Id > 0 && model.Question.QuestionType != null)
                {
                    QuestionData data = new QuestionData(_config);

                    int ret = data.CreateQuestion(model); //Create Question
                    if (ret == 1)
                    {
                        if (model.Question.QuestionType.IsOptionsRequired && model.Options != null && model.Options.Count > 0)
                        {
                            foreach (OptionsModel item in model.Options)
                            {
                                OptionsDataModel optionDBModel = new OptionsDataModel()
                                {
                                    Id = item.Id,
                                    OptionText = item.OptionText,
                                    QuestionId = model.Question.Id
                                };
                                data.InsertOptions(optionDBModel); //insert options
                            }
                        }
                    }
                    return Ok(model.Question.Id);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("UpdateQuestion")]
        public IActionResult UpdateQuestion(CreateQuestionModel model)
        {
            try
            {
                if (model != null && model.Question != null && model.Question.Id > 0 && model.Question.QuestionType != null)
                {
                    QuestionData data = new QuestionData(_config);
                    int ret = data.UpdateQuestion(model); //update question
                    if (ret == 1)
                    {
                        data.DeleteOptions(model.Question.Id); //delete all options for question

                        if (model.Question.QuestionType.IsOptionsRequired && model.Options != null && model.Options.Count > 0)
                        {
                            foreach (OptionsModel item in model.Options)
                            {
                                OptionsDataModel optionDBModel = new OptionsDataModel()
                                {
                                    Id = item.Id,
                                    OptionText = item.OptionText,
                                    QuestionId = model.Question.Id
                                };
                                data.InsertOptions(optionDBModel); //insert new options
                            }
                        }
                    }
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        [Route("DeleteQuestion")]
        public IActionResult DeleteQuestion(int questionId)
        {
            try
            {
                QuestionData data = new QuestionData(_config);
                data.DeleteQuestion(questionId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region QuestionType
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("SaveQuestionType")]
        public IActionResult SaveQuestionType(QuestionTypeModel model)
        {
            try
            {
                if (model != null)
                {
                    QuestionData data = new QuestionData(_config);

                    int ret = data.SaveQuestionType(model);

                    return Ok(ret);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("GetAllQuestionTypes")]
        [AllowAnonymous]
        public IActionResult GetAllQuestionTypes()
        {
            try
            {
                 
                    QuestionData data = new QuestionData(_config);
                 
                    return Ok(data.GetAllQuestionType());
                
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Answer
        [HttpPost]
        [Route("SaveAnswer")]
        [AllowAnonymous]
        public IActionResult SaveAnswer(AnswersDataModel model)
        {
            try
            {
                if (model != null)
                {
                    AnswerData data = new AnswerData(_config);

                    int ret = data.SaveAnswer(model);

                    return Ok(ret);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
