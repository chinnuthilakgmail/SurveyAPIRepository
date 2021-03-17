using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
   

    public class CreateQuestionModel
    {
        public int SurveyId { get;  set; }
        public int QuestionNumber { get; set; }
        public QuestionsDataModel Question { get; set; }
        
        public List<OptionsModel> Options { get; set; }
    }
    
}
