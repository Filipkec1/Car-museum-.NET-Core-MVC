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
    public class CarController : Controller
    {

        private MuzejManagerDbContext _dbContext;

        public CarController(MuzejManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(this._dbContext.Cars
                .Include(p => p.Maker)
                .Include(p => p.Owner)
                .ToList());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult IndexAjax(CarFilterModel filter)
        {

            var carList = this._dbContext.Cars
                .Include(p => p.Maker)
                .Include(p => p.Owner)
                .ToList();

            carList = carList
                .Where(p => string.IsNullOrWhiteSpace(filter.Name) || p.ModelName.ToLower().Contains(filter.Name.ToLower()))
                .Where(p => string.IsNullOrWhiteSpace(filter.Color) || p.Color.ToLower().Contains(filter.Color.ToLower()))
                .Where(p => string.IsNullOrWhiteSpace(filter.Maker) || (p.Maker != null && p.Maker.Name.ToLower().Contains(filter.Maker.ToLower())))
                .Where(p => string.IsNullOrWhiteSpace(filter.Year) || p.ManufactureDate.Year.ToString().ToLower().Contains(filter.Year.ToLower()))
                .Where(p => string.IsNullOrWhiteSpace(filter.Owner) || (p.Owner != null && p.Owner.FullName.ToLower().Contains(filter.Owner.ToLower())))
                .ToList();

            var model = carList.ToList();
            return PartialView("_IndexTable", model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {

            FillDropdownValuesCar();
            FillDropdownValuesMotor();
            FillDropdownValuesOwner();

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Car model)
        {
            if (ModelState.IsValid)
            {
                this._dbContext.Cars.Add(model);
                this._dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            FillDropdownValuesCar();
            FillDropdownValuesMotor();
            FillDropdownValuesOwner();

            return View();
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        public IActionResult Edit(int id)
        {
            var car = this._dbContext.Cars.FirstOrDefault(m => m.ID == id);
            FillDropdownValuesCar();
            FillDropdownValuesMotor();
            FillDropdownValuesOwner();

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditCar(int id)
        {

            var car = this._dbContext.Cars.FirstOrDefault(c => c.ID == id);
            bool ok = await this.TryUpdateModelAsync(car);

            if (ok)
            {
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            FillDropdownValuesCar();
            FillDropdownValuesMotor();
            FillDropdownValuesOwner();
            return View();
        }

        private void FillDropdownValuesCar()
        {
            var selectItems = new List<SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in this._dbContext.Makers)
            {
                listItem = new SelectListItem(category.Name, category.ID.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleMakers = selectItems;
        }

        private void FillDropdownValuesMotor()
        {
            var selectItems = new List<SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in this._dbContext.Motors)
            {
                listItem = new SelectListItem(category.Name, category.ID.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleMotors = selectItems;
        }

        private void FillDropdownValuesOwner()
        {
            var selectItems = new List<SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in this._dbContext.Owners)
            {
                listItem = new SelectListItem(category.FullName, category.ID.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleOwner = selectItems;
        }
    }
}