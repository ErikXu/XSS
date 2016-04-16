using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using XSS.Web.Models;

namespace XSS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\data.txt");
        private List<Item> _items = new List<Item>();

        public HomeController()
        {
            var data = System.IO.File.ReadAllText(_dataPath);

            if (!string.IsNullOrWhiteSpace(data))
            {
                _items = JsonConvert.DeserializeObject<List<Item>>(data);
            }
        }

        public ActionResult Index()
        {
            return View(_items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                _items.Add(item);

                var data = JsonConvert.SerializeObject(_items);

                System.IO.File.WriteAllText(_dataPath, data);

                return RedirectToAction("Index");
            }

            return View(item);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = _items.SingleOrDefault(n => n.Id == id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                var itemInDb = _items.Single(n => n.Id == item.Id);
                itemInDb.Title = item.Title;
                itemInDb.Content = item.Content;

                var data = JsonConvert.SerializeObject(_items);

                System.IO.File.WriteAllText(_dataPath, data);

                return Json(true);
            }

            throw new Exception("输入有误!");
        }
    }
}