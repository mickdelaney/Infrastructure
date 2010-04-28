using System;
using Machine.Specifications;
using Moq;

namespace Md.Infrastructure.Specs
{
    public abstract class SpecificationFor<TSubject> where TSubject : class
    {
        private static MockFactory _factory;
        private static AutoMockContainer _container;
        protected static TSubject Subject { get; set; }

        protected SpecificationFor(){}
        protected SpecificationFor(TSubject instance)
        {
            _container.Register(instance);
            Subject = instance;
        }

        Establish context = () =>
        {
            _factory = new MockFactory(MockBehavior.Loose);
            _container = new AutoMockContainer(_factory);
            Subject = _container.Create<TSubject>();
        };

        Cleanup stuff = () =>
        {
            Subject = null;
            _container = null;
        };

        public T Create<T>(Func<IResolve, T> activator) where T : class
        {
            return _container.Create(activator);
        }

        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        } 
    }
}
