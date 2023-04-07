using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.Resources
{
    public class Survey
    {
        public string Key { get; set; }

        /// <summary>
        /// Creator is user Id (UUID) (who created the survey)
        /// </summary>
        public Guid? Creator { get; set; }
        [JsonProperty("Creator.ExternalID")]
        public string Creator_ExternalID { get; set; }
        [JsonProperty("Creator.Email")]
        public string Creator_Email { get; set; }
        [JsonProperty("Creator.FirstName")]
        public string Creator_FirstName { get; set; }
        [JsonProperty("Creator.LastName")]
        public string Creator_LastName { get; set; }

        /// <summary>
        /// Account is the UUID of the account which the survey was opened for
        /// </summary>
        public Guid? Account { get; set; }
        [JsonProperty("Account.ExternalID")]
        public string Account_ExternalID { get; set; }
        [JsonProperty("Account.Name")]
        public string Account_Name { get; set; }

        /// <summary>
        /// Template is the survey templates you create in survey builder
        /// </summary>
        public string Template { get; set; }
        [JsonProperty("Template.Name")]
        public string Template_Name { get; set; }
        [JsonProperty("Template.Active")]
        public string Template_Active { get; set; }
        [JsonProperty("Template.Description")]
        public string Template_Description { get; set; }
        [JsonProperty("Template.Sections")]
        public IEnumerable<SurveyTemplateSection> Template_Sections { get; set; }

        public string StatusName { get; set; }
        public Boolean? Hidden { get; set; }

        [JsonProperty("erpReadStatus")]
        public bool? ErpReadStatus { get; set; }

        public DateTime? ModificationDateTime { get; set; }
        public DateTime? CreationDateTime { get; set; }

        public IEnumerable<SurveyAnswer> Answers { get; set; }
    }

    public class SurveyAnswer
    {
        public object Answer { get; set; }

        /// <summary>
        ///  Key of the question in the survey template
        /// </summary>
        public string Key { get; set; }
    }
}
