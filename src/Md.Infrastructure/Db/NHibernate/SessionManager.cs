using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Md.Infrastructure.Audit;
using NHibernate;
using NHibernate.Event;
using Configuration=NHibernate.Cfg.Configuration;

namespace Md.Infrastructure.Data.NHibernate
{
    public class SessionManager
    {
        private readonly List<Assembly> _fluentMappingAssemblies = new List<Assembly>();
        private Configuration configuration;
        public string ConnectionStringOverride;
        private ISessionFactory _sessionFactory;

        public SessionManager(){}
        public SessionManager(Type fluentMappingType)
        {
            _fluentMappingAssemblies.Add(fluentMappingType.Assembly);
        }
        public SessionManager(Assembly fluentMappingAssembly)
        {
            _fluentMappingAssemblies.Add(fluentMappingAssembly);
        }
        public SessionManager(IEnumerable<Assembly> fluentMappingAssemblies)
        {
            _fluentMappingAssemblies.AddRange(fluentMappingAssemblies);
        }

        public Configuration Configuration
        {
            get
            {
                if (_sessionFactory == null)
                    _sessionFactory = BuildFactory();

                return configuration;
            }
        }
        public ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    _sessionFactory = BuildFactory();

                return _sessionFactory;
            }
        }
        public ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }
        
        private ISessionFactory BuildFactory()
        {
            if(_fluentMappingAssemblies == null)
                return BuildFactory(new List<Assembly>());

            return BuildFactory(_fluentMappingAssemblies);
        }

        private ISessionFactory BuildFactory(List<Assembly> fluentMappingAssemblies)
        {
            try
            {
                var connectionString = ConnectionStringOverride ?? ConfigurationManager.AppSettings["ELEVATECONNECTION"];

                Debug.WriteLine("Configuring session factory to connect to " + connectionString);

                configuration = Fluently.Configure()
                    .Database
                    (
                        MsSqlConfiguration.MsSql2005
                                          .ConnectionString(c => c.Is(connectionString))
                                          .CurrentSessionContext(NHibernateConstants.NHibernateWebSessonContext)
                    
                    )
                    .Mappings(m => fluentMappingAssemblies.ForEach(a => m.FluentMappings.AddFromAssembly(a)))
                    .ExposeConfiguration(c => c.SetProperty("generate_statistics", "true"))
                    .ExposeConfiguration(c => c.SetProperty("proxyfactory.factory_class", "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle"))
                    .ExposeConfiguration(c => c.SetProperty("adonet.batch_size", "1"))
                    .BuildConfiguration();

                configuration.SetListener(ListenerType.PreUpdate, new AuditEventListener(LocalContext.Current));
                configuration.SetListener(ListenerType.PreInsert, new AuditEventListener(LocalContext.Current));

                _sessionFactory = configuration.BuildSessionFactory();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return _sessionFactory;
        }
    }
}