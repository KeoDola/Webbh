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
    public class DiaChiNhanHangController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DiaChiNhanHang
        public IQueryable<DiaChiNhanHang> GetDiaChiNhanHangs()
        {
            return db.DiaChiNhanHangs;
        }

        // GET: api/DiaChiNhanHang/5
        [ResponseType(typeof(DiaChiNhanHang))]
        public async Task<IHttpActionResult> GetDiaChiNhanHang(int id)
        {
            DiaChiNhanHang diaChiNhanHang = await db.DiaChiNhanHangs.FindAsync(id);
            if (diaChiNhanHang == null)
            {
                return NotFound();
            }

            return Ok(diaChiNhanHang);
        }

        // PUT: api/DiaChiNhanHang/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDiaChiNhanHang(int id, DiaChiNhanHang diaChiNhanHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diaChiNhanHang.DiaChiNhanHangID)
            {
                return BadRequest();
            }

            db.Entry(diaChiNhanHang).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaChiNhanHangExists(id))
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

        // POST: api/DiaChiNhanHang
        [ResponseType(typeof(DiaChiNhanHang))]
        public async Task<IHttpActionResult> PostDiaChiNhanHang(DiaChiNhanHang diaChiNhanHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DiaChiNhanHangs.Add(diaChiNhanHang);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = diaChiNhanHang.DiaChiNhanHangID }, diaChiNhanHang);
        }

        // DELETE: api/DiaChiNhanHang/5
        [ResponseType(typeof(DiaChiNhanHang))]
        public async Task<IHttpActionResult> DeleteDiaChiNhanHang(int id)
        {
            DiaChiNhanHang diaChiNhanHang = await db.DiaChiNhanHangs.FindAsync(id);
            if (diaChiNhanHang == null)
            {
                return NotFound();
            }

            db.DiaChiNhanHangs.Remove(diaChiNhanHang);
            await db.SaveChangesAsync();

            return Ok(diaChiNhanHang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiaChiNhanHangExists(int id)
        {
            return db.DiaChiNhanHangs.Count(e => e.DiaChiNhanHangID == id) > 0;
        }
    }
}