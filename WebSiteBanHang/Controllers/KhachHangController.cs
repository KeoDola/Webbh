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
    public class KhachHangController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/KhachHang
        public IQueryable<KhachHang> GetKhachHangs()
        {
            return db.KhachHangs;
        }

        // GET: api/KhachHang/5
        [ResponseType(typeof(KhachHang))]
        public async Task<IHttpActionResult> GetKhachHang(int id)
        {
            KhachHang khachHang = await db.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return Ok(khachHang);
        }

        // PUT: api/KhachHang/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutKhachHang(int id, KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != khachHang.KhachHangID)
            {
                return BadRequest();
            }

            db.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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

        // POST: api/KhachHang
        [ResponseType(typeof(KhachHang))]
        public async Task<IHttpActionResult> PostKhachHang(KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KhachHangs.Add(khachHang);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = khachHang.KhachHangID }, khachHang);
        }

        // DELETE: api/KhachHang/5
        [ResponseType(typeof(KhachHang))]
        public async Task<IHttpActionResult> DeleteKhachHang(int id)
        {
            KhachHang khachHang = await db.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            db.KhachHangs.Remove(khachHang);
            await db.SaveChangesAsync();

            return Ok(khachHang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KhachHangExists(int id)
        {
            return db.KhachHangs.Count(e => e.KhachHangID == id) > 0;
        }
    }
}