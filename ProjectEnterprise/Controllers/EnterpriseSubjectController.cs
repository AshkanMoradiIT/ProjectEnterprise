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
    public class EnterpriseSubjectController : Controller
    {
        public ActionResult GetList()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var enterprises = session.Query<EnterpriseSubject>().ToList();
                return View(enterprises);
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EnterpriseSubject enterprise)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(enterprise);
                transaction.Commit();
            }
            return RedirectToAction("GetList");
        }

        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var enterprise = GetById(id);
                if (enterprise == null)
                {
                    return HttpNotFound();
                }
                return View(enterprise);
            }
        }

        [HttpPost]
        public ActionResult Edit(EnterpriseSubject enterprise)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var existingEnterprise = session.Get<EnterpriseSubject>(enterprise.Id);
                if (existingEnterprise != null)
                {
                    existingEnterprise.Name = enterprise.Name;
                    existingEnterprise.Code = enterprise.Code;
                    existingEnterprise.SubjectType = enterprise.SubjectType;

                    session.Update(existingEnterprise);
                    transaction.Commit();
                }
            }
            return RedirectToAction("GetList");
        }

        public ActionResult Delete(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var enterprise = GetById(id);
                if (enterprise != null)
                {
                    session.Delete(enterprise);
                    transaction.Commit();
                }
            }
            return RedirectToAction("GetList");
        }

        public EnterpriseSubject GetById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var enterprise = session.Get<EnterpriseSubject>(id);
                return enterprise;
            }
        }
    }

}