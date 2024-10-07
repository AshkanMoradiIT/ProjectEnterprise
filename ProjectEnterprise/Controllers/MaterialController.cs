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
    public class MaterialController : Controller
    {
        public ActionResult Create(Material material)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var enterprise = new EnterpriseSubject
                {
                    Id = material.Id.ToString(),
                    Code = GenerateCode(session),
                    Name = $"{material.Name} ({material.Color})",
                    SubjectType = 3
                };

                session.Save(enterprise);
                session.Save(material);
                transaction.Commit();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Material material)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var existingMaterial = GetById(material.Id);
                if (existingMaterial != null)
                {
                    existingMaterial.Name = material.Name;
                    existingMaterial.Weight = material.Weight;
                    existingMaterial.Color = material.Color;
                    existingMaterial.UnitOfMeasureName = material.UnitOfMeasureName;

                    var enterprise = session.Get<EnterpriseSubject>(material.Id);
                    if (enterprise != null)
                    {
                        enterprise.Name = $"{material.Name} ({material.Color})";
                    }

                    session.Update(existingMaterial);
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
                var material = GetById(id);
                if (material != null)
                {
                    session.Delete(material);
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
                var materials = session.Query<Material>().ToList();
                return View(materials);
            }
        }

        public Material GetById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var material = session.Get<Material>(id);
                return material;
            }
        }

        private string GenerateCode(ISession session)
        {
            var maxCode = session.Query<EnterpriseSubject>().Max(es => (int?)Convert.ToInt32(es.Code)) ?? 0;
            return (maxCode + 1).ToString();
        }
    }

}