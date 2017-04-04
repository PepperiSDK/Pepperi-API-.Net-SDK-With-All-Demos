using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    /// <summary>
    /// metadata for concrete type
    /// </summary>
    /// <remarks>
    /// 1. The concrete type may\may not be subtype (derived)
    /// 1.1 for concrete classes which are not derived, 
    ///         the field SubTypeID in the TypeMetadata is empty
    /// 1.2 for concrete classes which are derived,
    ///         the fields SubTypeID in the TypeMetadata is not empty
    ///         the sub type specific fields are custom fields (TSA)
    ///
    ///2. Samples     
    ///     abstract activity:	                        has header
    ///     concrete activities may be agent actions:   eg, visit
    ///     in the metadata:                            SubTypeId identidies the concrete type. OverrideTypes, identifies the base type.


    ///     abstract transaction:	                    has line and header
    ///     concrete transaction may be:                sales order,    invoice, etc 
    ///     in the metadata:                            SubTypeId identidies the concrete type. OverrideTypes, identifies the base type.
    ///     
    /// 3. in API
    ///     3.1 for flat api of "derived types", the API needs to know the concere type to know which fields to expect.
    ///         Thus, The SubTypeID is sent on the url on BulkUpload and CSV
    ///         for non bulk interactions on insert, API sets default SubType (eg, ActivityTypeID) and it can not be updated
    /// </remarks>
    public class TypeMetadata
    {
        public string Type { get; set; }                    //eg:   transactions
        public string SubTypeID { get; set; }               //eg:     137743
        public string SubTypeName { get; set; }               //eg:   sales order    
        public List<OverwriteType> OverwriteTypes { get; set; }
        public bool ReadOnly { get; set; }
    }

    public class OverwriteType
    {
        public string Type { get; set; }
        public string Field { get; set; }
    }
}



/*
 * [
  {
    "Type": "transactions",
    "SubTypeID": "137743",
    "OverwriteTypes": [
      {
        "Type": "Selective",
        "Field": "ActivityTypeID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "items",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "transaction_lines",
    "SubTypeID": "137743",
    "OverwriteTypes": [
      {
        "Type": "Selective",
        "Field": "TransactionExternalID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "users",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      }
    ],
    "ReadOnly": true
  },
  {
    "Type": "contacts",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Selective",
        "Field": "AccountExternalID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "accounts",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      },
      {
        "Type": "Selective",
        "Field": "TypeDefinitionID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "accounts",
    "SubTypeID": "137746",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      },
      {
        "Type": "Selective",
        "Field": "TypeDefinitionID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "item_dimensions1",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "price_lists",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "item_dimensions2",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "item_prices",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      },
      {
        "Type": "Selective",
        "Field": "PriceListExternalID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "catalogs",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      }
    ],
    "ReadOnly": true
  },
  {
    "Type": "account_catalogs",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      },
      {
        "Type": "Selective",
        "Field": "AccountExternalID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "Property",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "activities",
    "SubTypeID": "137744",
    "OverwriteTypes": [
      {
        "Type": "Selective",
        "Field": "ActivityTypeID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "activities",
    "SubTypeID": "137745",
    "OverwriteTypes": [
      {
        "Type": "Selective",
        "Field": "ActivityTypeID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "account_users",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      },
      {
        "Type": "Selective",
        "Field": "AccountExternalID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "images",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "inventory",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "account_inventory",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      },
      {
        "Type": "Selective",
        "Field": "AccountExternalID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "user_defined_tables",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      },
      {
        "Type": "Selective",
        "Field": "MapDataExternalID"
      }
    ],
    "ReadOnly": false
  },
  {
    "Type": "special_prices",
    "SubTypeID": "",
    "OverwriteTypes": [
      {
        "Type": "Full",
        "Field": ""
      }
    ],
    "ReadOnly": false
  }
]
*/