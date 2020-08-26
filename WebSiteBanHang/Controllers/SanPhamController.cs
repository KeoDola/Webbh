using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class SanPhamController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SanPham
        public IQueryable<SanPham> GetSanPhams()
        {
            return db.SanPhams;
        }

        // GET: api/SanPham/5
        [ResponseType(typeof(SanPham))]
        public async Task<IHttpActionResult> GetSanPham(int id)
        {
            SanPham sanPham = await db.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return Ok(sanPham);
        }

        // PUT: api/SanPham/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSanPham(int id, SanPham sanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sanPham.SanPhamID)
            {
                return BadRequest();
            }

            db.Entry(sanPham).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanPhamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SanPham
        [ResponseType(typeof(SanPham))]
        public async Task<IHttpActionResult> PostSanPham(SanPham sanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SanPhams.Add(sanPham);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sanPham.SanPhamID }, sanPham);
        }

        // DELETE: api/SanPham/5
        [ResponseType(typeof(SanPham))]
        public async Task<IHttpActionResult> DeleteSanPham(int id)
        {
            SanPham sanPham = await db.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            db.SanPhams.Remove(sanPham);
            await db.SaveChangesAsync();

            return Ok(sanPham);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SanPhamExists(int id)
        {
            return db.SanPhams.Count(e => e.SanPhamID == id) > 0;
        }
    }
}