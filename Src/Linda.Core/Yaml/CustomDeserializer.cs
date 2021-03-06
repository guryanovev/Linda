﻿namespace Linda.Core.Yaml
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using YamlDotNet.Core;
    using YamlDotNet.Core.Events;
    using YamlDotNet.RepresentationModel.Serialization;
    using YamlDotNet.RepresentationModel.Serialization.NamingConventions;
    using YamlDotNet.RepresentationModel.Serialization.NodeDeserializers;
    using YamlDotNet.RepresentationModel.Serialization.NodeTypeResolvers;

    public sealed class CustomDeserializer : IYamlDeserializer
    {
        private static readonly Dictionary<string, Type> PredefinedTagMappings = new Dictionary<string, Type>
                                                                                     {
                                                                                         {
                                                                                             "tag:yaml.org,2002:map", typeof(Dictionary<object, object>) 
                                                                                         }, 
                                                                                         {
                                                                                             "tag:yaml.org,2002:bool", typeof(bool)
                                                                                         }, 
                                                                                         {
                                                                                             "tag:yaml.org,2002:float", typeof(double)
                                                                                         }, 
                                                                                         {
                                                                                             "tag:yaml.org,2002:int", typeof(int)
                                                                                         }, 
                                                                                         {
                                                                                             "tag:yaml.org,2002:str", typeof(string)
                                                                                         }, 
                                                                                         {
                                                                                             "tag:yaml.org,2002:timestamp", typeof(DateTime)
                                                                                         }, 
                                                                                     };

        private readonly Dictionary<string, Type> _tagMappings;
        private readonly List<IYamlTypeConverter> _converters;
        private readonly TypeDescriptorProxy _typeDescriptor = new TypeDescriptorProxy();
        private readonly IValueDeserializer _valueDeserializer;

        public CustomDeserializer(IObjectFactory objectFactory = null, INamingConvention namingConvention = null)
        {
            objectFactory = objectFactory ?? new DefaultObjectFactory();
            namingConvention = namingConvention ?? new NullNamingConvention();

            this._typeDescriptor.TypeDescriptor =
                new YamlAttributesTypeDescriptor(
                    new NamingConventionTypeDescriptor(
                        new ReadableAndWritablePropertiesTypeDescriptor(),
                        namingConvention));

            this._converters = new List<IYamlTypeConverter>();
            this.NodeDeserializers = new List<INodeDeserializer>
                                    {
                                        new TypeConverterNodeDeserializer(this._converters),
                                        new NullNodeDeserializer(),
                                        new ScalarNodeDeserializer(),
                                        new ArrayNodeDeserializer(),
                                        new GenericDictionaryNodeDeserializer(objectFactory),
                                        new NonGenericDictionaryNodeDeserializer(objectFactory),
                                        new GenericCollectionNodeDeserializer(objectFactory),
                                        new NonGenericListNodeDeserializer(objectFactory),
                                        new EnumerableNodeDeserializer(),
                                        new CustomObjectNodeDeserializer(
                                            objectFactory, this._typeDescriptor)
                                    };

            this._tagMappings = new Dictionary<string, Type>(PredefinedTagMappings);
            this.TypeResolvers = new List<INodeTypeResolver>
                                {
                                    new TagNodeTypeResolver(this._tagMappings),
                                    new TypeNameInTagNodeTypeResolver(),
                                    new DefaultContainersNodeTypeResolver()
                                };

            this._valueDeserializer =
                new AliasValueDeserializer(
                    new NodeValueDeserializer(
                        this.NodeDeserializers, this.TypeResolvers));
        }

        public IList<INodeDeserializer> NodeDeserializers { get; private set; }

        public IList<INodeTypeResolver> TypeResolvers { get; private set; }

        public void RegisterTagMapping(string tag, Type type)
        {
            this._tagMappings.Add(tag, type);
        }

        public void RegisterTypeConverter(IYamlTypeConverter typeConverter)
        {
            this._converters.Add(typeConverter);
        }

        public T Deserialize<T>(string content) where T : new()
        {
            var result = (T)Deserialize(new EventReader(new Parser(new StringReader(content))), typeof(T));
            return Equals(result, null) ? new T() : result;
        }

        public object Deserialize(Type type, string content)
        {
            var result = Deserialize(new EventReader(new Parser(new StringReader(content))), type);
            return Equals(result, null) ? new object() : result;
        }

        /// <summary>
        /// Deserializes an object of the specified type.
        /// </summary>
        /// <param name="reader">The <see cref="EventReader" /> where to deserialize the object.</param>
        /// <param name="type">The static type of the object to deserialize.</param>
        /// <returns>Returns the deserialized object.</returns>
        public object Deserialize(EventReader reader, Type type)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var hasStreamStart = reader.Allow<StreamStart>() != null;

            var hasDocumentStart = reader.Allow<DocumentStart>() != null;

            object result = null;
            if (!reader.Accept<DocumentEnd>() && !reader.Accept<StreamEnd>())
            {
                using (var state = new SerializerState())
                {
                    result = this._valueDeserializer.DeserializeValue(reader, type, state, this._valueDeserializer);
                }
            }

            if (hasDocumentStart)
            {
                reader.Expect<DocumentEnd>();
            }

            if (hasStreamStart)
            {
                reader.Expect<StreamEnd>();
            }

            return result;
        }

        private class TypeDescriptorProxy : ITypeDescriptor
        {
            public ITypeDescriptor TypeDescriptor;

            public IEnumerable<IPropertyDescriptor> GetProperties(Type type)
            {
                return this.TypeDescriptor.GetProperties(type);
            }

            public IPropertyDescriptor GetProperty(Type type, string name)
            {
                return this.TypeDescriptor.GetProperty(type, name);
            }
        }
    }
}