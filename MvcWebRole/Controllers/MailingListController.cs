using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace MvcWebRole.Controllers
{
    public class MailingListController : Controller
    {
        private readonly IMailingListRepository _mailingListRepository;

        public MailingListController(IMailingListRepository mailingListRepository)
        {
            _mailingListRepository = mailingListRepository;
        }

        //
        // GET: /MailingList/

        public ActionResult Index()
        {
            var lists = _mailingListRepository.ListMailingLists();
            return View(lists);
        }

        //
        // GET: /MailingList/Details/5

        public ActionResult Details(string id)
        {
            var mailingList = _mailingListRepository.GetMailingListByName(id);
            return View(mailingList);
        }

        //
        // GET: /MailingList/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MailingList/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MailingList mailingList)
        {
            if (ModelState.IsValid)
            {
                _mailingListRepository.AddMailingList(mailingList);
                return RedirectToAction("Index");
            }

            return View(mailingList);
        }

        //
        // GET: /MailingList/Edit/5

        public ActionResult Edit(string id)
        {
            var mailingList = _mailingListRepository.GetMailingListByName(id);
            return View(mailingList);
        }

        //
        // POST: /MailingList/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, MailingList editedMailingList)
        {
            if (ModelState.IsValid)
            {
                var mailingList=new MailingList();
                UpdateModel(mailingList);
                _mailingListRepository.UpdateMailingList(id, mailingList);
                return RedirectToAction("Index");
            }

            return View(editedMailingList);
        }

        //
        // GET: /MailingList/Delete/5

        public ActionResult Delete(string id)
        {
            var mailingList = _mailingListRepository.GetMailingListByName(id);
            return View(mailingList);
        }

        //
        // POST: /MailingList/Delete/5

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            _mailingListRepository.DeleteMailingList(id);
            return RedirectToAction("Index");
        }
    }
}
