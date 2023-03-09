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
        /// Creator is user Id (who created the survey)
        /// </summary>
        public Guid? Creator { get; set; }

        /// <summary>
        /// Account is the UUID of the account which the survey was opened for
        /// </summary>
        public Guid? Account { get; set; }

        /// <summary>
        /// Template is the survey templates you create in survey builder
        /// </summary>
        public string Template { get; set; }

        public string StatusName { get; set; }
        public Boolean? Hidden { get; set; }

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
