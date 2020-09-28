using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Muzej.DAL;
using Muzej.Model;
using Muzej.Web.Models;

namespace Muzej.Web.Controllers
{
    [Authorize]
    [Route("Proizvodac")]
    public class MakerController : Controller
    {

        private MuzejManagerDbContext _dbContext;

        public MakerController(MuzejManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [AllowAnonymous]
        [Route("Index")]
        public IActionResult Index()
        {
            return View(this._dbContext.Makers
                .Include(p => p.Country)
                .Include(c => c.Cars)
                .ToList());
        }

        [AllowAnonymous]
        [Route("Index")]
        [HttpPost]
        public ActionResult IndexAjax(MakerFilterModel filter)
        {

            var makerList = this._dbContext.Makers
                .Include(p => p.Country)
                .Include(c => c.Cars)
                .ToList();

            makerList = makerList
                .Where(p => string.IsNullOrWhiteSpace(filter.Name) || p.Name.ToLower().Contains(filter.Name.ToLower()))
                .Where(p => string.IsNullOrWhiteSpace(filter.Country) || (p.Country != null && p.Country.Name.ToLower().Contains(filter.Country.ToLower())))
                .ToList();

            var model = makerList.ToList();
            return PartialView("_IndexTable", model);
        }

        [Authorize(Roles = "Admin")]
        [Route("Novi-Proizvodac")]
        public IActionResult Create()
        {
            FillDropdownValuesMaker();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Novi-Proizvodac")]
        public IActionResult Create(Maker model)
        {
            if (ModelState.IsValid)
            {
                this._dbContext.Makers.Add(model);
                this._dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            this.FillDropdownValuesMaker();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        [Route("Uredi-Proizvodaca")]
        [Route("Uredi-Proizvodaca/{id:regex(^[[1-9]]$)}")]
        public IActionResult Edit(int? id = null)
        {
            FillDropdownValuesMaker();
            Maker maker = this._dbContext.Makers.FirstOrDefault(m => m.ID == id);

            if (maker == null)
            {
                return NotFound();
            }

            return View(maker);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Edit")]
        [Route("Uredi-Proizvodaca")]
        [Route("Uredi-Proizvodaca/{id:regex(^[[1-9]]$)}")]
        public async Task<IActionResult> EditMaker(int? id = null)
        {

            var maker = this._dbContext.Makers.FirstOrDefault(c => c.ID == id);
            bool ok = await this.TryUpdateModelAsync(maker);

            if (ok)
            {
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            FillDropdownValuesMaker();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var maker = this._dbContext.Makers.First(c => c.ID == id);

            this._dbContext.Makers.Remove(maker);

            this._dbContext.SaveChanges();

            return View("Index");
        }

        private void FillDropdownValuesMaker()
        {
            var selectItems = new List<SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in this._dbContext.Countries)
            {
                listItem = new SelectListItem(category.Name, category.ID.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleCountries = selectItems;
        }
    }
}