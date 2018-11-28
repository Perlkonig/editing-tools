using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EditTools
{
    public class Search
    {
        public string Find { get; set; }
        public string Replace { get; set; }
        public bool Wildcards { get; set; }
        public bool CaseSensitive { get; set; }
        public bool ReplaceAll { get; set; }
    }

    [TypeConverter(typeof(SearchesConverter))]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public class Searches
    {
        public Dictionary<string, Search> Dict {get; set;}
        public int Count { get { return this.Dict.Count; } }

        public Searches()
        {
            Dict = new Dictionary<string, Search>();
        }
    }

    public class SearchesConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                Dictionary<string,Search> lst = JsonConvert.DeserializeObject<Dictionary<string,Search>>(value.ToString());
                Searches searches = new Searches();
                searches.Dict = lst;
                return searches;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string val = JsonConvert.SerializeObject(((Searches)value).Dict);
                Debug.WriteLine("Serializer says: " + val);
                return val;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class Comment
    {
        public string Text { get; set; }
    }

    [TypeConverter(typeof(BoilerplateConverter))]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public class Boilerplate
    {
        public Dictionary<string, Comment> Dict { get; set; }
        public int Count { get { return this.Dict.Count; } }

        public Boilerplate()
        {
            Dict = new Dictionary<string, Comment>();
        }
    }

    public class BoilerplateConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                Dictionary<string, Comment> lst = JsonConvert.DeserializeObject<Dictionary<string, Comment>>(value.ToString());
                Boilerplate bp = new Boilerplate();
                bp.Dict = lst;
                return bp;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string val = JsonConvert.SerializeObject(((Boilerplate)value).Dict);
                Debug.WriteLine("Serializer says: " + val);
                return val;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
