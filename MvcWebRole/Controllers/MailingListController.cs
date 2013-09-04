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

        ////
        //// GET: /MailingList/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /MailingList/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MailingList/Create

        [HttpPost]
        public ActionResult Create(MailingList mailingList)
        {
            if (ModelState.IsValid)
            {
                _mailingListRepository.AddMailingList(mailingList);
                return RedirectToAction("Index");
            }

            return View(mailingList);
        }

        ////
        //// GET: /MailingList/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /MailingList/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /MailingList/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /MailingList/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
