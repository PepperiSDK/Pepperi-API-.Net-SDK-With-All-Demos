using Newtonsoft.Json;
using Pepperi.SDK.Model.Fixed.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.Resources
{
    public class SurveyTemplate
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FromToDateTime ActiveDateRange { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public DateTime? CreationDateTime { get; set; }

        public bool? Hidden { get; set; }
        public bool? Active { get; set; }

        public IEnumerable<SurveyTemplateSection> Sections { get; set; }
    }

    public class SurveyTemplateSection
    {
        public string Title { get; set; }
        public string Key { get; set; }

        public IEnumerable<SurveyTemplateQuestion> Questions { get; set; }
    }

    public class SurveyTemplateQuestion
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public bool? Mandatory { get; set; }
        public IEnumerable<SurveyTemplateQuestion_KeyValue> OptionalValues { get; set; }
        public IDictionary<string, object> AdditionalFields { get; set; }

        [JsonProperty("selectionColumns")]
        public int? SelectionColumns { get; set; }

        public bool? IsShowIf { get; set; }
        public SurveyTemplateQuestion_ShowIf ShowIf { get; set; }

        public string MaxCharacters { get; set; }
        public string True { get; set; }
        public string False { get; set; }
        public bool? AddNoneOption { get; set; }
    }

    public class SurveyTemplateQuestion_KeyValue
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class SurveyTemplateQuestion_ShowIf
    {

        // Tree Logic
        public string ComplexId { get; set; }
        public string Operation { get; set; }
        public SurveyTemplateQuestion_ShowIf LeftNode { get; set; }
        public SurveyTemplateQuestion_ShowIf RightNode { get; set; }


        public string ApiName { get; set; }
        public string ExpressionId { get; set; }
        public string FieldType { get; set; }

        public IEnumerable<string> Values { get; set; }
    }
}
