using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class SurveyDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpen { get; set; }
    }
    public class SurveyModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsOpen { get; set; }

        public List<QuestionsModel> Questions { get; set; }
    }
    public class SurveyBindingModel
    {
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyDescription { get; set; }
        public string SurveyStartDate { get; set; }
        public string SurveyEndDate { get; set; }
        public bool IsOpen { get; set; }
        public int QuestionNumber { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionType { get; set; }
        public bool IsOptionsRequired { get; set; }
        public int OptionId { get; set; }
        public string OptionText { get; set; }
    }
}
