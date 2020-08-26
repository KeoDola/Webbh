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
    public class SanPhamDonHangController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SanPhamDonHang
        public IQueryable<SanPhamDonHang> GetSanPhamDonHangs()
        {
            return db.SanPhamDonHangs;
        }

        // GET: api/SanPhamDonHang/5
        [ResponseType(typeof(SanPhamDonHang))]
        public async Task<IHttpActionResult> GetSanPhamDonHang(int id)
        {
            SanPhamDonHang sanPhamDonHang = await db.SanPhamDonHangs.FindAsync(id);
            if (sanPhamDonHang == null)
            {
                return NotFound();
            }

            return Ok(sanPhamDonHang);
        }

        // PUT: api/SanPhamDonHang/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSanPhamDonHang(int id, SanPhamDonHang sanPhamDonHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sanPhamDonHang.SanPhamDonHangID)
            {
                return BadRequest();
            }

            db.Entry(sanPhamDonHang).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanPhamDonHangExists(id))
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

        // POST: api/SanPhamDonHang
        [ResponseType(typeof(SanPhamDonHang))]
        public async Task<IHttpActionResult> PostSanPhamDonHang(SanPhamDonHang sanPhamDonHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SanPhamDonHangs.Add(sanPhamDonHang);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SanPhamDonHangExists(sanPhamDonHang.SanPhamDonHangID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sanPhamDonHang.SanPhamDonHangID }, sanPhamDonHang);
        }

        // DELETE: api/SanPhamDonHang/5
        [ResponseType(typeof(SanPhamDonHang))]
        public async Task<IHttpActionResult> DeleteSanPhamDonHang(int id)
        {
            SanPhamDonHang sanPhamDonHang = await db.SanPhamDonHangs.FindAsync(id);
            if (sanPhamDonHang == null)
            {
                return NotFound();
            }

            db.SanPhamDonHangs.Remove(sanPhamDonHang);
            await db.SaveChangesAsync();

            return Ok(sanPhamDonHang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SanPhamDonHangExists(int id)
        {
            return db.SanPhamDonHangs.Count(e => e.SanPhamDonHangID == id) > 0;
        }
    }
}