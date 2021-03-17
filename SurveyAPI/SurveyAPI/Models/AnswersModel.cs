using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
     
    public class AnswersDataModel
    {
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public PersonModel Person { get; set; }
        public int OptionId { get; set; }
        public string Answer { get; set; }
    }
}
