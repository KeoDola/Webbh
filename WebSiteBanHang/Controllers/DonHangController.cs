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
    public class DonHangController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DonHang
        public IQueryable<DonHang> GetDonHangs()
        {
            return db.DonHangs;
        }

        // GET: api/DonHang/5
        [ResponseType(typeof(DonHang))]
        public async Task<IHttpActionResult> GetDonHang(int id)
        {
            DonHang donHang = await db.DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }

            return Ok(donHang);
        }

        // PUT: api/DonHang/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDonHang(int id, DonHang donHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != donHang.DonHangID)
            {
                return BadRequest();
            }

            db.Entry(donHang).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonHangExists(id))
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

        // POST: api/DonHang
        [ResponseType(typeof(DonHang))]
        public async Task<IHttpActionResult> PostDonHang(DonHang donHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DonHangs.Add(donHang);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = donHang.DonHangID }, donHang);
        }

        // DELETE: api/DonHang/5
        [ResponseType(typeof(DonHang))]
        public async Task<IHttpActionResult> DeleteDonHang(int id)
        {
            DonHang donHang = await db.DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }

            db.DonHangs.Remove(donHang);
            await db.SaveChangesAsync();

            return Ok(donHang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DonHangExists(int id)
        {
            return db.DonHangs.Count(e => e.DonHangID == id) > 0;
        }
    }
}