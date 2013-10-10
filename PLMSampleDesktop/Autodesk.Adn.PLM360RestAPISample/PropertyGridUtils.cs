
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Adn.PLM360API;

namespace Autodesk.Adn.PLM360RestAPISample
{
    class ItemPropertyGrid : ICustomTypeDescriptor
    {
        Item item;

        public ItemPropertyGrid(Item item)
        {
            this.item = item;
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return item;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection defaultPropertyDescriptors = TypeDescriptor.GetProperties(item);
            PropertyDescriptorCollection propertyDescriptors = new PropertyDescriptorCollection(null);
            //first copy defaults
            foreach (PropertyDescriptor pd in defaultPropertyDescriptors)
            {
                Type type = pd.PropertyType;
                if (typeof(IDictionary).IsAssignableFrom(type))
                {
                    object value = pd.GetValue(item);
                    IDictionary dict = (IDictionary)value;
                    foreach (DictionaryEntry e in dict)
                    {
                        DictionaryPropertyDescriptor dictProp = new DictionaryPropertyDescriptor(pd.Attributes, dict, e.Key);
                        propertyDescriptors.Add(dictProp);
                    }
                }
                else
                {
                    propertyDescriptors.Add(pd);
                }
            }
            return propertyDescriptors;
        }

    }


    class DictionaryPropertyDescriptor : PropertyDescriptor
    {
        IDictionary dict;
        object key;
        AttributeCollection attributes;

        public override string Category
        {
            get
            {
                string value = base.Category;
                CategoryAttribute a = (CategoryAttribute)FindAttribute(typeof(CategoryAttribute));
                if (a != null) value = a.Category;
                return value;
            }
        }

        public override string Description
        {
            get
            {
                string value = base.Description;
                DescriptionAttribute a = (DescriptionAttribute)FindAttribute(typeof(DescriptionAttribute));
                if (a != null) value = a.Description;
                return value;
            }
        }


        public override string DisplayName
        {
            get
            {
                string value = base.DisplayName;
                DisplayNameAttribute a = (DisplayNameAttribute)FindAttribute(typeof(DisplayNameAttribute));
                if (a != null) value = a.DisplayName;
                return value;
            }
        }


        private Attribute FindAttribute(Type type)
        {
            Attribute ret = null;
            foreach (Attribute a in attributes)
            {
                if (a != null && type.IsAssignableFrom(a.GetType()))
                {
                    ret = a;
                    break;
                }
            }
            return ret;
        }


        public DictionaryPropertyDescriptor(AttributeCollection attributes, IDictionary dict, object key)
            : base(key.ToString(), null)
        {
            this.attributes = attributes;
            this.dict = dict;
            this.key = key;

        }


        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return null; }
        }

        public override object GetValue(object component)
        {
            return dict[key];
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get
            {
                if (dict[key] != null) return dict[key].GetType();
                else return typeof(object);
            }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            dict[key] = value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }


    //class PicklistFieldsConverter : ExpandableObjectConverter
    //{
    //    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    //    {
    //        if (destinationType == typeof(List<PicklistValue>))
    //        {
    //            return true;
    //        }
    //        return base.CanConvertTo(context, destinationType);
    //    }

    //    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
    //    {
    //        if (destinationType == typeof(List<PicklistValue>) && value is List<PicklistValue>)
    //        {
    //            List<PicklistValue> pklst = (List<PicklistValue>)value;

    //            StringBuilder sb = new StringBuilder();
    //            foreach (var item in pklst)
    //            {
    //                sb.Append(item.GetType().ToString() + ":" + item.displayName + ",");
    //            }

    //            return sb.ToString();
    //        }
    //        return base.ConvertTo(context, culture, value, destinationType);
    //    }

    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    //    {
    //        return base.CanConvertFrom(context, sourceType);
    //    }
    //    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    //    {
    //        return base.ConvertFrom(context, culture, value);
    //    }
    //}
}
