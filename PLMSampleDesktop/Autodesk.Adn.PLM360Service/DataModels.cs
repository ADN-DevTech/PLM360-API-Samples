////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved 
// Written by Daniel Du 2013 - ADN/Developer Technical Services
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Autodesk.Adn.PLM360API
{
    /// <summary>
    /// Different transfer objects that are serialized to/from json
    /// </summary>

    public class UnknownEntity
    {

        public long? id { set; get; }

        public string body { set; get; }
    }

    public class Credentials
    {

        public string password { set; get; }

        public string userId { set; get; }

        public string customerId { set; get; }
    }

    public class OxygenCredentials
    {

        public string customerId { set; get; }

        public string validation { set; get; }
    }


    public class Session
    {

        public string userId { set; get; }

        public string sessionId { set; get; }

        public string customerToken { set; get; }
    }


    public class Error
    {

        public long? code { set; get; }

        public int? status { set; get; }

        public string body { set; get; }
    }


    public class Workspace
    {

        public long id { set; get; }

        public string url { set; get; }

        public string displayName { set; get; }

        public string systemName { set; get; }

        public string description { set; get; }

        public string workspaceTypeId { set; get; }
    }

    public class Item
    {

        [Category("Item"), Description("Item Id"), ReadOnly(true), DisplayName("Id")]
        public long id { set; get; }

        [Category("Item"), Description("If this item has been versioned, the root will refer to the original"), ReadOnly(true), DisplayName("Root Id")]
        public long rootId { set; get; }

        [Category("Item"), Description("The workspace Id of the item"), ReadOnly(true), DisplayName("Workspace Id")]
        public long workspaceId { set; get; }

        [Category("Item"), Description("Item version"), ReadOnly(true), DisplayName("Version Number")]
        public long version { set; get; }

        [Category("Item"), Description("URL to naviate to this item"), ReadOnly(true), DisplayName("Rest Url")]
        public string url { set; get; }

        [Category("Item"), Description("Revision name"), ReadOnly(true), DisplayName("Revision Name")]
        public string revision { set; get; }

        [Category("Item"), Description("Item identification at visual level"), DisplayName("Item Descriptor"), ReadOnly(true)]
        public string itemDescriptor { set; get; }

        [Category("Item"), Description("Indicates if this item is marked deleted"), ReadOnly(true), DisplayName("Is Deleted?")]
        public bool? deleted { set; get; }

        [Category("Item Fields"), Description("Item fields")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Dictionary<string, string> fields { set; get; }

        [Category("Item Picklist Fields"), Description("Item picklist fields")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Dictionary<string, List<PicklistValue>> picklistFields { set; get; }
    }

    public class ItemDetail : Item
    {
        public bool isWorkingVersion { set; get; }

        public bool isLatestVersion { set; get; }

    }





    public class File
    {

        public long id { set; get; }

        public long itemId { set; get; }

        public long workspaceId { set; get; }

        public string url { set; get; }

        public string fileName { set; get; }

        public string title { set; get; }

        public string description { set; get; }

        public string status { set; get; }

        public string version { set; get; }

        public long size { set; get; }
    }


    public class Page
    {

        public long? index { set; get; }

        public long size { set; get; }

        public string nextUrl { set; get; }

        public string prevUrl { set; get; }
    }


    public class PagedCollection<T>
    {

        public Page page { set; get; }

        public List<T> elements { set; get; }
    }

    public class FileUploadRequest
    {
        public string fileName { set; get; }

        public string title { set; get; }

        public string description { set; get; }

    }

    public class PicklistValue
    {

        public long? id { set; get; }

        public string displayName { set; get; }

        public string itemUrl { set; get; }
    }

    public class Picklist
    {
        public string id { get; set; }
        public string url { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public PicklistType type { get; set; }
        public int? workspaceId { get; set; }
        public bool showDeletedItems { get; set; }
        public string picklistValuesUrl { get; set; }
    }

    public class FieldType
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public DataType dataType { get; set; }
    }

    public class FieldDefinition
    {
        string id { get; set; }	//The unique identifier for the object.
        string displayName { get; set; }	//The display name.
        string description { get; set; }	//The description of the field definition.
        FieldTypeId fieldTypeId { get; set; }	//The field type.
        string unitOfMeasure { get; set; }	//The unit of measure string. This value may be null depending on the field type.
        int displayLength { get; set; }	//The number of characters to display. This value may be null depending on the field type.
        int fieldLength { get; set; }	//The maxium number of characters allowed in a field value. This value may be null depending on the field type.
        FieldDefinitionEditable editable { get; set; }	//The edit behavior.
        FieldDefinitionVisible visible { get; set; }	//The visibility behavior.
        string defaultValue { get; set; }	//The default value.
        string pivotFieldDefinitionId { get; set; }	//The field definition to use as a pivot for a derived field. The field definition must be a linked pick list in the same workspace.
        string sourceFieldDefinitionId { get; set; }	//The field to use as a source for a derived field. The field is defined in the workspace referenced by the pivot field definition.
        string picklistId { get; set; }	//The ID of the pick list. This value is only used for pick list field types.
        string picklistFieldDefinitionId { get; set; }	//The field that provides the value for the item in the pick list. This value is only used for linked pick list field types.
        FieldValidation[] validations { get; set; }	//The set of validations on the field. A validation is a constraint on a field value. Data cannot be set on a field if it violates a constraint.

    }

    public enum FieldTypeId
    {
        AUTO_NUMBER,    //An integer that increments for every new object.
        BOM_UNITS_PICK_LIST,	//BOM unit-of-measure picklist.
        BOOLEAN,    //A true or false value. This type is listed as "checkbox" in the UI.
        CSV,	//Comma Separated values.
        DATE,    //A calendar Date. Does not contain time information.
        DECIMAL,    //A numeric value with decimals.
        DERIVED,    //A picklist derived from the items in another workspace.
        EMAIL,    //An email address.
        FLASH,    //A Flash control.
        IMAGE,    //A picture.
        INTEGER,	//For legacy purposes only.
        LONG,    //A whole number.
        MONEY,	//For legacy purposes only.
        MONEY_EXTENDED,    //A currency value.
        PARAGRAPH,    //A string of characters inteded to be displayed in paragraph form.
        PARAGRAPH_NO_LINE_BREAK,    //A string of characters inteded to be displayed in paragraph form wihtout line breaks.
        PICK_LIST,    //A set of values to pick from.
        PICK_LIST_FIRST_VALUE_DEFAULT,    //A pick list where the first value in the list is the default value.
        PICK_LIST_LAST_SAVED_LABEL,    //A pick list that retains "last saved" information.
        PICK_LIST_LINKED,    //A pick list containing values from another workspace.
        PICK_LIST_LINKED_FILTERED,    //A pick list containing filtered values from another workspace.
        PICK_LIST_LINKED_FIRST_VALUE_DEFAULT,    //A linked pick list where the first value in the list is the default value.
        PICK_LIST_LINKED_LATEST_VERSION,    //A pick list showing the latest version of items from another workspace.
        PICK_LIST_LINKED_MULTI_SELECT,    //A pick list allowing multiple selections from items in another workspace.
        PICK_LIST_LINKED_RADIO,    //A picklist allowing a single selection from items in another workspace.
        PICK_LIST_LINKED_SEARCH_FILTER,    //A picklist containing values from another workspace with a search filter.
        PICK_LIST_MULTI_SELECT,    //A pick list allowing multiple selections.
        PICK_LIST_RADIO,    //A picklist allowing only a single selection.
        PICK_LIST_SEARCH_FILTER,    //A pick list with a search filter.
        SINGLE_LINE_TEXT,    //A string of characters intended to be displayed as a single line.
        URL,	//The type of value used to handle URLs. It is a string of characters.
        UNSUPPORTED	//The type is not supported by the API.
    }

    public enum DataType
    {
        INTEGER,    //A whole number.
        DECIMAL,    //A numeric value with a fixed decimal point. Note: This is not the same as a floating point number.
        STRING,    //A block of text. In most cases, null is not the same as an empty string.
        DATE,    //A calendar day. The format should always be "YYYY-MM-DD". There is no DateTime data type.
        BLOB,    //A block of binary data.
        BOOLEAN,    //A true or false value. The only accepted values are "true", "false" and null.
        PICK_LIST,    //A value from a fixed set.
        UNSUPPORTED	//The type is not supported by the API.
    }

    public enum FieldDefinitionEditable
    {
        ALWAYS,	//Field is always editable.
        NEVER,	//Field is never editable.
        CREATE_ONLY,	//Field is only editable during the creation of the object.
        UNSUPPORTED	//The edit type is not supported by the API.
    }

    public enum FieldDefinitionVisible
    {
        ALWAYS,	//Field is always be visible.
        EDIT_ONLY,	//Field is visible only in edit mode.
        UNSUPPORTED	//The visibility type is not supported by the API.
    }

    public class FieldValidation
    {
        int id { get; set; }	//	The unique indentifier for the object.
        FieldValidationType type { get; set; }	//		The type of validtor.
        Dictionary<string, string> settings { get; set; }	//		The validator settings, which are dependant on the validator type. The map key is the setting ID. The map value is the value on the validation

    }

    public enum FieldValidationType
    {
        BOTH_OR_NONE,	//Validates that either both or none of the fields are entered.
        BYTE,	//Validates that a field can be converted to a Byte.
        CONDITIONALLY_REQUIRED,	//Validates that the field is entered when another field contains a specific value.
        CSV_COUNT,	//Validates the number of entries in the CSV.
        CSV_IDENTICAL_PREFIX,	//Validates that each designator in a comma separated list begins with the same characters. Eg. R1, R12, R13
        CSV_MASK,	//Validates each substring in a CSV String with a mask.
        CSV_UNIQUE_IN_BOM,	//Validates that each CSV in BOM tab are unique.
        DATE,	//Validates that the value conforms to a valid date format. Date values set through the UI allow a variety of formats. Date values set through the API must be the "YYYY-MM-DD" format.
        DATE_LESS_THAN,	//Validates if the date entered in one field is less than the date entered in another field.
        DATE_LESS_THAN_OR_EQUAL_TO,	//Validates if the date entered in one field is less than or equal to the date entered in another field.
        DATE_GREATER_THAN,	//Validates if the date entered in one field is greater than the date entered in another field.
        DATE_GREATER_THAN_OR_EUQAL_TO,	//Validates if the date entered in one field is greater than or equal to the date entered in another field.
        DECIMAL_RANGE,	//Validates that a decimal field is within a specified range.
        DECIMAL_WITH_PRECISION,	//Validates a positive or negative decimal number with a specified precision.
        EMAIL,	//Validates that the string is the format of an email address.
        IDENTICAL,	//Validates that a field is identical to another specified field.
        INTEGER,	//Validates a positive or negative integer number.
        INTEGER_RANGE,	//Validates that the integer is within the valid range.
        LONG,	//Validates a positive or negative long integer number.
        MASK,	//Validates according to a regular expression.
        MIN_LENGTH,	//Validates that input data isn't less than a specified minimum length.
        MAX_LENGTH,	//Validates that input data doesn't exceed a specified maximum length.
        MONEY,	//Validates that the field is a valid currency amount.
        MONEY_EXTENDED,	//Validates that the field is a valid currency amount.
        ONE_OF_THREE_NOT_MORE,	//Validates that the only one of three fields has a value.
        ONE_OR_OTHER_NOT_BOTH,	//Validates that the only one of two fields has a value.
        PERCENTAGE_RANGE_WITHIN,	//Validates that a field entered is within the allowed percentage.
        PERCENTAGE_WITHIN,	//Validates that a field entered is within the allowed percentage range of the other numeric field.
        REQUIRED,	//Validates that the value for a given field is entered.
        SELECTION_REQUIRED,	//Validates that the value for a given picklist field is entered.
        SELECTION_VALID_OR_NONE,	//Validates that selection for an editable dropdown is from available options or null.
        UNIQUE,	//Checks that this field is unique among all non-deleted records of this workspace.
        UNIQUE_COMBO,	//Validates that the combination of 2 field values is unique within a workspace.
        UNIQUE_COMBO_NOT_KEYWORD,	//Validates that a combination of fields is unique in a workspace unless one of the fields is a keyword.
        UNIQUE_WITH_WILDCARD,	//Validates that a field is unique within a workspace. Can specify a number of wildcard characters both before and after the value that will be ignored from uniqueness check.
        UNSUPPORTED	//This validation type is not supported by the API.
    }

    public enum PicklistType
    {
        FIXED,
        WORKSPACE
    }

}
