using Microsoft.AspNetCore.Mvc;
using Muzej.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Muzej.Model;

namespace Muzej.Web.Controllers
{
    [Route("/api/maker")]
    [ApiController]
    public class MakerApiController : Controller
    {
        private MuzejManagerDbContext _dbContext;

        public MakerApiController(MuzejManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Get()
        {
            var makers = this._dbContext.Makers
                .Select(c => new MakerDTO()
                {

                    ID = c.ID,
                    Name = c.Name,
                    NumCars = c.Cars.Count(),
                    Country = (new CountryDTO
                    {
                        ID = c.Country.ID,
                        Name = c.Country.Name,
                        NumMakers = c.Country.Makers.Count()
                    })
                })
                .ToList();

            return Ok(makers);
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var maker = this._dbContext.Makers
                .Where(c => c.ID == id)
                .Select(c => new MakerDTO()
                {

                    ID = c.ID,
                    Name = c.Name,
                    NumCars = c.Cars.Count(),
                    Country = (new CountryDTO
                    {
                        ID = c.Country.ID,
                        Name = c.Country.Name,
                        NumMakers = c.Country.Makers.Count()
                    })
                })
                .FirstOrDefault();

            if (maker == null)
            {
                return NotFound();
            }

            return Ok(maker);
        }

        [Route("pretraga/{name?}")]
        public IActionResult Get(string name)
        {
            var maker = this._dbContext.Makers
               .Where(p => (p.Name).ToLower().Contains(name.ToLower()))
                .Select(c => new MakerDTO()
                {

                    ID = c.ID,
                    Name = c.Name,
                    NumCars = c.Cars.Count(),
                    Country = (new CountryDTO
                    {
                        ID = c.Country.ID,
                        Name = c.Country.Name,
                        NumMakers = c.Country.Makers.Count()
                    })
                })
                .FirstOrDefault();

            if (maker == null)
            {
                return NotFound();
            }

            return Ok(maker);
        }


        [HttpPost]
        public IActionResult Post([FromBody] MakerDTO maker)
        {
            if (string.IsNullOrWhiteSpace(maker.Name))
            {
                return BadRequest();
            }

            Maker newMaker;

            newMaker = new Maker()
            {
                Name = maker.Name,
                CountryID = maker.Country.ID
            };

            this._dbContext.Makers.Add(newMaker);
            this._dbContext.SaveChanges();

            return Get(newMaker.ID);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] MakerDTO maker)
        {
            var makerDB = this._dbContext.Makers.First(c => c.ID == id);

            if (!(string.IsNullOrEmpty(maker.Name)))
            {
                makerDB.Name = maker.Name;
            }

            if (maker.Country != null)
            {
                makerDB.CountryID = maker.Country.ID;
            }

            this._dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var makerDB = this._dbContext.Makers.First(c => c.ID == id);

            this._dbContext.Makers.Remove(makerDB);

            this._dbContext.SaveChanges();

            return Ok();
        }
    }

    public class MakerDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumCars { get; set; }
        public CountryDTO Country { get; set; }
    }

    public class CountryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumMakers { get; set; }
    }
}
