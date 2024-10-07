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
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }
    }

}
