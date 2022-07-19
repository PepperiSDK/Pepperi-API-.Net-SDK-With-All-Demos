using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.MetaData
{
    public class Field_MetaData
    {
        public string FieldID { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool? IsUserDefinedField { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool? Hidden { get; set; }
        public Owner Owner { get; set; }
        public string CSVMappedColumnName { get; set; }
        public Field_UIType UIType { get; set; }
        public Field_UserDefinedTableSource UserDefinedTableSource { get; set; }

        public Feild_CalculatedRuleEngine CalculatedRuleEngine { get; set; }

        public Field_TypeSpecificFields TypeSpecificFields { get; set; }
    }


    public class Field_UserDefinedTableSource
    {
        public string TableID { get; set; }
        public string MainKey { get; set; }
        public string SecondayKey { get; set; }
        public long? Index { get; set; }
        public bool? IsUpdatable { get; set; }
        public Field_CalculatedOn CalculatedOn { get; set; }
    }

    public class Feild_CalculatedRuleEngine
    {
        #region Constructor 
        public Feild_CalculatedRuleEngine()
        {
            this.ParticipatingFields = new string[] { };
        }

        #endregion

        public string JSFormula { get; set; }

        public string[] ParticipatingFields { get; set; }

        public Field_CalculatedOn CalculatedOn { get; set; }
    }

    public class Field_TypeSpecificFields
    {
        #region Constructor

        public Field_TypeSpecificFields()
        {
            this.PicklistValues = new string[] { };
        }

        #endregion

        public string[] PicklistValues { get; set; }
        public long? StringLength { get; set; }
        public long? DecimalScale { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CheckBoxTrueValue { get; set; }
        public string CheckBoxFalseValue { get; set; }
        public bool? IsImageDeviceUploadable { get; set; }
        public long? ImageMegaPixels { get; set; }

        public long? ImageQualityPercentage { get; set; }

        public string ProgramReference { get; set; }
        public string TransactionLinesSumFieldID { get; set; }
        public string ReferenceToResourceType { get; set; }
        public Field_TypeSpecificFields_ReferenceTo ReferenceTo { get; set; }

        public string PickListResourceType { get; set; }

        public string PickListDataView { get; set; }

        public string ReadOnlyDisplayValue { get; set; }

    }




    public class Field_TypeSpecificFields_ReferenceTo
    {
        public string ExternalID { get; set; }

        public Guid? UUID { get; set; }
    }

    public class Field_CalculatedOn
    {
        public long? ID { get; set; }        //default 0 

        public string Name { get; set; }
    }



    public class Field_UIType
    {
        public long? ID { get; set; }
        public string Name { get; set; }
    }
}

/*
 * {
    
    
      
     
  },
*/
