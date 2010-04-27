using System;
using Castle.Windsor;
using NHibernate.Bytecode;
using NHibernate.ByteCode.Castle;
using NHibernate.Properties;
using NHibernate.Type;

namespace Md.Infrastructure.Data.NHibernate
{
    public class EnhancedBytecodeProvider : IBytecodeProvider, IInjectableCollectionTypeFactoryClass,
                                            IInjectableProxyFactoryFactory
    {
        private readonly IWindsorContainer container;
        private readonly IObjectsFactory _objectsFactory;

        private ICollectionTypeFactory _collectionTypeFactory;
        private IProxyFactoryFactory _proxyFactoryFactory;
        private Type _proxyFactoryFactoryType = typeof(ProxyFactoryFactory);
        private Type _colletionTypeFactoryType = typeof(DefaultCollectionTypeFactory);

        public EnhancedBytecodeProvider(IWindsorContainer container)
        {
            this.container = container;
            _objectsFactory = new WindsorNHibernateObjectsFactory(container);
        }

        #region IBytecodeProvider Members

        public IReflectionOptimizer GetReflectionOptimizer(Type clazz, IGetter[] getters, ISetter[] setters)
        {
            return new WindsorLightweightReflectionOptimizer(container, clazz, getters, setters);
        }

        public IProxyFactoryFactory ProxyFactoryFactory
        {
            get
            {
                if (_proxyFactoryFactory == null)
                {
                    if (container.Kernel.HasComponent(_proxyFactoryFactoryType))
                        _proxyFactoryFactory = (IProxyFactoryFactory)container.Resolve(_proxyFactoryFactoryType);
                    else
                        _proxyFactoryFactory = (IProxyFactoryFactory)Activator.CreateInstance(_proxyFactoryFactoryType);
                }
                return _proxyFactoryFactory;
            }
        }

        public IObjectsFactory ObjectsFactory
        {
            get { return _objectsFactory; }
        }

        public ICollectionTypeFactory CollectionTypeFactory
        {
            get
            {
                if (_collectionTypeFactory == null)
                {
                    if (container.Kernel.HasComponent(_colletionTypeFactoryType))
                        _collectionTypeFactory = (ICollectionTypeFactory)container.Resolve(_colletionTypeFactoryType);
                    else
                        _collectionTypeFactory = (ICollectionTypeFactory)Activator.CreateInstance(_colletionTypeFactoryType);
                }
                return _collectionTypeFactory;
            }
        }

        #endregion

        #region IInjectableCollectionTypeFactoryClass Members

        public void SetCollectionTypeFactoryClass(string typeAssemblyQualifiedName)
        {
            SetCollectionTypeFactoryClass(Type.GetType(typeAssemblyQualifiedName, true));
        }

        public void SetCollectionTypeFactoryClass(Type type)
        {
            _colletionTypeFactoryType = type;
        }

        #endregion

        #region IInjectableProxyFactoryFactory Members

        public void SetProxyFactoryFactory(string typeName)
        {
            _proxyFactoryFactoryType = Type.GetType(typeName, true);
        }

        #endregion


    }
}
