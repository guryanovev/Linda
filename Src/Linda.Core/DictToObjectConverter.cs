using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linda.Core
{
    using System.Collections;

    class DictToObjectConverter
    {
        public void Convert<TConfig>(object source, ref TConfig result) where TConfig : new()
        {
            object o = result;
            Convert(source, ref o, typeof(TConfig));
            result = (TConfig)o;
        }

        public void Convert(object source, ref object result, Type sourceType)
        {
            if (source is IList<object>)
            {
                var arrType = sourceType.GetElementType();

                var list = source as IList<object>;

                var listType = sourceType;

                //var constructedListType = listType.MakeGenericType(arrType);

                //var instance = (IList)Activator.CreateInstance(constructedListType);

                for (var i = 0; i < list.Count(); ++i)
                {
                    var tmpresult = new object();

                    Convert(list[i], ref tmpresult, arrType);



                    list[i] = (tmpresult);
                }

                //result = ArrayList.Adapter(instance).ToArray(arrType); //.CreateInstance(arrType, list.Count);

                result = list;

                return;
            }

            if (!(source is Dictionary<object, object>))
            {
                result = System.Convert.ChangeType(source, sourceType);
                return;
            }

            var sourceDictionary = source as Dictionary<object, object>;
            var destType = sourceType;
            var properties = destType.GetProperties();

            foreach (var element in sourceDictionary)
            {
                try
                {
                    var pInfo = properties.Single(p => p.Name == (string)element.Key);

                    var propertyType = pInfo.PropertyType;

                    
                    //var tmpresult = propertyType != typeof(string) ? Activator.CreateInstance(propertyType) : string.Empty;

                    //var tmpresult = new byte[];

                    object tmpresult;

                    try
                    {
                        tmpresult = Activator.CreateInstance(propertyType);
                    }
                    catch (Exception)
                    {
                        
                        tmpresult = new object();
                    }


                    Convert(element.Value, ref tmpresult, propertyType);

                    pInfo.SetValue(result, tmpresult, null);
                }
                catch (InvalidOperationException ex)
                {

                }
            }
        }
    }
}
