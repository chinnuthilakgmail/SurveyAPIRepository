using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class QuestionsDataModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public QuestionTypeModel QuestionType { get; set; }
    }

    public class QuestionsModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int QuestionNumber { get; set; }
        public QuestionTypeModel QuestionType { get; set; }

        public List<OptionsModel> Options { get; set; }
    }
}
