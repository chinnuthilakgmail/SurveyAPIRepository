using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class OptionsModel
    {
        public int Id { get; set; } 
        public string OptionText { get; set; }
    }
    public class OptionsDataModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
    }
}
