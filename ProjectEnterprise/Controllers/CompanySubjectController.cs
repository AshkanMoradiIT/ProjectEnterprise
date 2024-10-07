using NHibernate;
using NHibernate.Linq;
using ProjectEnterprise.Models.Entities;
using ProjectEnterprise.Models.NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEnterprise.Controllers
{
    public class CompanySubjectController : Controller
    {
        public ActionResult Create(CompanySubject company)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var enterprise = new EnterpriseSubject
                {
                    Id = company.Id.ToString(),
                    Code = GenerateCode(session),
                    Name = $"{company.Name} ({company.LatinName})",
                    SubjectType = 2 
                };

                session.Save(enterprise);
                session.Save(company);
                transaction.Commit();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(CompanySubject company)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var existingCompany = session.Get<CompanySubject>(company.Id);
                if (existingCompany != null)
                {
                    existingCompany.Name = company.Name;
                    existingCompany.LatinName = company.LatinName;
                    existingCompany.Address = company.Address;

                    var enterprise = session.Get<EnterpriseSubject>(company.Id);
                    if (enterprise != null)
                    {
                        enterprise.Name = $"{company.Name} ({company.LatinName})";
                    }

                    session.Update(existingCompany);
                    session.Update(enterprise);
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var company = GetById(id);
                if (company != null)
                {
                    session.Delete(company);
                    var enterprise = session.Get<EnterpriseSubject>(id);
                    if (enterprise != null)
                    {
                        session.Delete(enterprise);
                    }
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var companies = session.Query<CompanySubject>().ToList();
                return View(companies);
            }
        }

        public CompanySubject GetById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var company = session.Get<CompanySubject>(id);
                return company;
            }
        }

        private string GenerateCode(ISession session)
        {
            var maxCode = session.Query<EnterpriseSubject>().Max(es => (int?)Convert.ToInt32(es.Code)) ?? 0;
            return (maxCode + 1).ToString();
        }
    }

}