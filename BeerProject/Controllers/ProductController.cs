using System;
using System.Collections.Generic;
using BeerProject.Utility.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BeerProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly EntityContext _context;

        public ProductController(EntityContext context)
        {
            _context = context;
        }

        #region --- Beers ---
        // GET: api/Beers
        [HttpGet("GetBeer")]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeer()
        {
            return await _context.Beers.ToListAsync();
        }

        // GET: api/Beers/id
        [HttpGet("GetBeer/{id}")]
        public async Task<ActionResult<Beer>> GetBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            return beer;
        }

        // GET: api/Beers/{gtAlcoholByVolume}/{ltAlcoholByVolume}
        [HttpGet("GetBeer/{gtAlcoholByVolume}/{ltAlcoholByVolume}")]
        public List<Beer> GetBeer(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
            var beer = _context.Beers.Where(x => x.PercentageAlcoholByVolume > gtAlcoholByVolume && x.PercentageAlcoholByVolume < ltAlcoholByVolume);
            List<Beer> lstbeers = new List<Beer>();

            foreach (var item in beer)
            {
                lstbeers.Add(new Beer
                {
                    BeerID = item.BeerID,
                    BarID = item.BarID,
                    BreweryID = item.BreweryID,
                    Name = item.Name,
                    PercentageAlcoholByVolume = item.PercentageAlcoholByVolume
                });
            }
            return lstbeers;
        }

        // POST: api/Beers/
        [HttpPost("InsertBeer")]
        public async Task<IActionResult> InsertBeer(Beer beer)
        {
            _context.Beers.Add(beer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_context.Beers.Any(e => e.BeerID == beer.BeerID))
                    return Conflict();
                else
                    throw;
            }
            return CreatedAtAction("GetProduct", new { id = beer.BeerID }, beer);
        }

        // PUT: api/Beers
        [HttpPut("UpdateBeer/{id}")]
        public async Task<ActionResult<Beer>> UpdateBeer(int id, Beer beer)
        {
            if (id != beer.BeerID)
            {
                return BadRequest();
            }
            _context.Entry(beer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Beers.Any(e => e.BeerID == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }
        #endregion

        #region --- Bars ---
        // GET: api/Bars
        [HttpGet("GetBar")]
        public async Task<ActionResult<IEnumerable<Bar>>> GetBar()
        {
            return await _context.Bars.ToListAsync();
        }

        // GET: api/Bars/id
        [HttpGet("GetBar/{id}")]
        public async Task<ActionResult<Bar>> GetBar(int id)
        {
            var bar = await _context.Bars.FindAsync(id);
            if (bar == null)
            {
                return NotFound();
            }
            return bar;
        }

        // POST: api/Bars/
        [HttpPost("InsertBar")]
        public async Task<IActionResult> InsertBar(Bar bar)
        {
            _context.Bars.Add(bar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_context.Bars.Any(e => e.BarID == bar.BarID))
                    return Conflict();
                else
                    throw;
            }
            return CreatedAtAction("GetProduct", new { id = bar.BarID }, bar);
        }

        // PUT: api/Bars
        [HttpPut("UpdateBar/{id}")]
        public async Task<ActionResult<Bar>> UpdateBar(int id, Bar bar)
        {
            if (id != bar.BarID)
            {
                return BadRequest();
            }
            _context.Entry(bar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bars.Any(e => e.BarID == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }
        #endregion

        #region --- Breweries ---
        // GET: api/Breweries
        [HttpGet("GetBrewery")]
        public async Task<ActionResult<IEnumerable<Brewery>>> GetBrewery()
        {
            return await _context.Brewerys.ToListAsync();
        }

        // GET: api/Breweries/id
        [HttpGet("GetBrewery/{id}")]
        public async Task<ActionResult<Brewery>> GetBrewery(int id)
        {
            var brewery = await _context.Brewerys.FindAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }
            return brewery;
        }

        // POST: api/Breweries/
        [HttpPost("InsertBrewery")]
        public async Task<IActionResult> InsertBrewery(Brewery brewery)
        {
            _context.Brewerys.Add(brewery);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_context.Brewerys.Any(e => e.BreweryID == brewery.BreweryID))
                    return Conflict();
                else
                    throw;
            }
            return CreatedAtAction("GetProduct", new { id = brewery.BreweryID }, brewery);
        }

        // PUT: api/Breweries
        [HttpPut("UpdateBrewery/{id}")]
        public async Task<ActionResult<Brewery>> UpdateBrewery(int id, Brewery brewery)
        {
            if (id != brewery.BreweryID)
            {
                return BadRequest();
            }
            _context.Entry(brewery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Brewerys.Any(e => e.BreweryID == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }
        #endregion

        #region --- Breweries Beers ---
        // GET: api/Brewery/{breweryId}/beer 
        [HttpGet("GetBrewerywithAssociatedBeers/{id}")]
        public async Task<ActionResult<object>> GetBrewerywithAssociatedBeers(int id)
        {
            List<string> lstBeers = new List<string>();
            var brewery = await _context.Brewerys.FindAsync(id);

            if (brewery == null)
                return NotFound();
            else
            {
                var beers = _context.Beers.ToList().FindAll(e => e.BreweryID == id);

                foreach (var beer in beers)
                {
                    lstBeers.Add(beer.Name);
                }
            }
            var result = new { breweryname = brewery.Name, beers = lstBeers };
            return result;
        }

        // GET: api/Brewery/beer 
        [HttpGet("GetAllBrewerywithAssociatedBeers")]
        public async Task<ActionResult<object>> GetAllBrewerywithAssociatedBeers()
        {
            List<string> lstBeers = new List<string>();
            List<string> lstBreweries = new List<string>();
            var LstBrewery = await _context.Brewerys.ToListAsync();

            foreach (var item in LstBrewery)
            {
                lstBreweries.Add(item.Name);
                var beers = _context.Beers.ToList().FindAll(e => e.BreweryID == item.BreweryID);

                foreach (var beer in beers)
                {
                    lstBeers.Add(beer.Name);
                }
            }
            var result = new { result = "List of all breweries with associated beers.", Breweries = lstBreweries, beers = lstBeers };
            return result;
        }
        #endregion

        #region --- Bar Beers ---
        // GET: api/Bar/{barId}/beer 
        [HttpGet("GetBarwithAssociatedBeers/{id}")]
        public async Task<ActionResult<object>> GetBarwithAssociatedBeers(int id)
        {
            List<string> lstBeers = new List<string>();
            var bar = await _context.Bars.FindAsync(id);

            if (bar == null)
                return NotFound();
            else
            {
                var beers = _context.Beers.ToList().FindAll(e => e.BreweryID == id);

                foreach (var beer in beers)
                {
                    lstBeers.Add(beer.Name);
                }
            }
            var result = new { barname = bar.Name, beers = lstBeers };
            return result;
        }

        // GET: api/Bar/beer 
        [HttpGet("GetAllBarwithAssociatedBeers")]
        public async Task<ActionResult<object>> GetAllBarwithAssociatedBeers()
        {
            List<string> lstBeers = new List<string>();
            List<string> lstBars = new List<string>();
            var LstBar = await _context.Bars.ToListAsync();

            foreach (var item in LstBar)
            {
                lstBars.Add(item.Name);
                var beers = _context.Beers.ToList().FindAll(e => e.BarID == item.BarID);

                foreach (var beer in beers)
                {
                    lstBeers.Add(beer.Name);
                }
            }
            var result = new { result = "List of all bars with associated beers.", bar = lstBars, beers = lstBeers };
            return result;
        }

        // POST: api/Brewery/Beer
        // - POST /brewery/beer - Insert a single brewery beer link
        // - POST / bar / beer - Insert a single bar beer link
        [HttpPost("InsertBreweryBarBeer")]
        public void InsertBreweryBarBeer(Beer beer)
        {
            try
            {
                _context.Beers.Add(beer);
                _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
