using NHibernate;
using NHibernate.Linq;
using ProjectEnterprise.Models.Entities;
using ProjectEnterprise.Models.NHibernate.Mapping;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProjectEnterprise.Controllers
{
    public class PersonSubjectController : Controller
    {
        public ActionResult GetList()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var people = session.Query<PersonSubject>().ToList();
                return View(people);
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PersonSubject person)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var enterprise = new EnterpriseSubject
                {
                    Id = person.Id.ToString(),
                    Code = GenerateCode(session),
                    Name = $"{person.FirstName} {person.LastName}",
                    SubjectType = 1
                };

                session.Save(enterprise);
                session.Save(person);
                transaction.Commit();
            }
            return RedirectToAction("GetList");
        }


        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var person = GetById(id);
                if (person == null)
                {
                    return HttpNotFound();
                }
                return View(person);
            }
        }
        [HttpPost]
        public ActionResult Edit(PersonSubject person)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var existingPerson = GetById(person.Id);
                if (existingPerson != null)
                {
                    existingPerson.FirstName = person.FirstName;
                    existingPerson.LastName = person.LastName;
                    existingPerson.PhoneNumber = person.PhoneNumber;

                    var enterprise = session.Get<EnterpriseSubject>(person.Id);
                    if (enterprise != null)
                    {
                        enterprise.Name = $"{person.FirstName} {person.LastName}";
                    }

                    session.Update(existingPerson);
                    session.Update(enterprise);
                    transaction.Commit();
                }
            }
            return RedirectToAction("GetList");
        }

        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var person = GetById(id);
                if (person != null)
                {
                    session.Delete(person);
                    var enterprise = session.Get<EnterpriseSubject>(id);
                    if (enterprise != null)
                    {
                        session.Delete(enterprise);
                    }
                    transaction.Commit();
                }
            }
            return RedirectToAction("GetList");
        }


        public PersonSubject GetById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var person = session.Get<PersonSubject>(id);
                return person;
            }
        }

        private string GenerateCode(ISession session)
        {
            var maxCode = session.Query<EnterpriseSubject>().Max(es => (int?)Convert.ToInt32(es.Code)) ?? 0;
            return (maxCode + 1).ToString();
        }
    }

}