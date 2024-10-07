using NHibernate;
using NHibernate.Cfg;
using ProjectEnterprise.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectEnterprise.Models.NHibernate.Mapping
{
    public class NHibernateHelper
    {
        private static ISessionFactory sessionFactory;

        public static void Initialize()
        {
            sessionFactory = new Configuration()
                .Configure()
                .AddAssembly("ProjectEnterprise")
                //.AddAssembly(typeof(NHibernateHelper).Assembly)
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Mapping\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            var AllConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Mapping\Mapping.hbm.xml");
            configuration.AddFile(AllConfigurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }

}
