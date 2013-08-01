namespace Linda.Core.Yaml
{
    using System;
    using System.Reflection;

    using YamlDotNet.Core;
    using YamlDotNet.Core.Events;
    using YamlDotNet.RepresentationModel.Serialization;

    public sealed class CustomObjectNodeDeserializer : INodeDeserializer
    {
        private readonly IObjectFactory _objectFactory;
        private readonly ITypeDescriptor _typeDescriptor;

        public CustomObjectNodeDeserializer(IObjectFactory objectFactory, ITypeDescriptor typeDescriptor)
        {
            this._objectFactory = objectFactory;
            this._typeDescriptor = typeDescriptor;
        }

        bool INodeDeserializer.Deserialize(EventReader reader, Type expectedType, Func<EventReader, Type, object> nestedObjectDeserializer, out object value)
        {
            var mapping = reader.Allow<MappingStart>();
            if (mapping == null)
            {
                value = null;
                return false;
            }

            value = this._objectFactory.Create(expectedType);
            while (!reader.Accept<MappingEnd>())
            {
                var propertyName = reader.Expect<Scalar>();
                PropertyInfo property;

                try
                {
                    property = this._typeDescriptor.GetProperty(expectedType, propertyName.Value).Property;
                }
                catch (Exception ex)
                {
                    reader.Skip();
                    continue;
                }
                
                var propertyValue = nestedObjectDeserializer(reader, property.PropertyType);
                var propertyValuePromise = propertyValue as IValuePromise;
                if (propertyValuePromise == null)
                {
                    var convertedValue = TypeConverter.ChangeType(propertyValue, property.PropertyType);
                    property.SetValue(value, convertedValue, null);
                }
                else
                {
                    var valueRef = value;
                    propertyValuePromise.ValueAvailable += v =>
                    {
                        var convertedValue = TypeConverter.ChangeType(v, property.PropertyType);
                        property.SetValue(valueRef, convertedValue, null);
                    };
                }
            }

            reader.Expect<MappingEnd>();
            return true;
        }
    }
}
