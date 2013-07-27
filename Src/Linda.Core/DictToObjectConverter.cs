using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linda.Core
{
    class DictToObjectConverter
    {
        public void Convert<TConfig>(object source, ref TConfig result) where TConfig : new()
        {
            object o = result;
            Convert(source, ref o);
            result = (TConfig) o;
        }

        public void Convert(object source, ref object result)
        {
            if (!(source is Dictionary<object, object>))
            {
                result = System.Convert.ChangeType(source, result.GetType());
                return;
            }

            var sourceDictionary = source as Dictionary<object, object>;
            var destType = result.GetType();
            var properties = destType.GetProperties();

            foreach (var element in sourceDictionary)
            {
                try
                {
                    var pInfo = properties.Single(p => p.Name == (string)element.Key);

                    var propertyType = pInfo.PropertyType;

                    var tmpresult = propertyType != typeof(string) ? Activator.CreateInstance(propertyType) : string.Empty;

                    Convert(element.Value, ref tmpresult);

                    pInfo.SetValue(result, tmpresult, null);
                }
                catch (InvalidOperationException ex)
                {

                }
            }
        }
    }
}
