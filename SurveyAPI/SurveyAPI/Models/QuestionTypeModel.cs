using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class QuestionTypeModel
    {
        public int Id { get; set; }
        public string QuestionType { get; set; }
        public bool IsOptionsRequired { get; set; }
    }
}
