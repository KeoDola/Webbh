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
    public class LoSanPhamController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LoSanPham
        public IQueryable<LoSanPham> GetLoSanPhams()
        {
            return db.LoSanPhams;
        }

        // GET: api/LoSanPham/5
        [ResponseType(typeof(LoSanPham))]
        public async Task<IHttpActionResult> GetLoSanPham(int id)
        {
            LoSanPham loSanPham = await db.LoSanPhams.FindAsync(id);
            if (loSanPham == null)
            {
                return NotFound();
            }

            return Ok(loSanPham);
        }

        // PUT: api/LoSanPham/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLoSanPham(int id, LoSanPham loSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loSanPham.LoSanPhamID)
            {
                return BadRequest();
            }

            db.Entry(loSanPham).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoSanPhamExists(id))
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

        // POST: api/LoSanPham
        [ResponseType(typeof(LoSanPham))]
        public async Task<IHttpActionResult> PostLoSanPham(LoSanPham loSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoSanPhams.Add(loSanPham);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoSanPhamExists(loSanPham.LoSanPhamID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = loSanPham.LoSanPhamID }, loSanPham);
        }

        // DELETE: api/LoSanPham/5
        [ResponseType(typeof(LoSanPham))]
        public async Task<IHttpActionResult> DeleteLoSanPham(int id)
        {
            LoSanPham loSanPham = await db.LoSanPhams.FindAsync(id);
            if (loSanPham == null)
            {
                return NotFound();
            }

            db.LoSanPhams.Remove(loSanPham);
            await db.SaveChangesAsync();

            return Ok(loSanPham);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoSanPhamExists(int id)
        {
            return db.LoSanPhams.Count(e => e.LoSanPhamID == id) > 0;
        }
    }
}