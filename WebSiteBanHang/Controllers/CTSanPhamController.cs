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
    public class CTSanPhamController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CTSanPham
        public IQueryable<CTSanPham> GetCTSanPhams()
        {
            return db.CTSanPhams;
        }

        // GET: api/CTSanPham/5
        [ResponseType(typeof(CTSanPham))]
        public async Task<IHttpActionResult> GetCTSanPham(int id)
        {
            CTSanPham cTSanPham = await db.CTSanPhams.FindAsync(id);
            if (cTSanPham == null)
            {
                return NotFound();
            }

            return Ok(cTSanPham);
        }

        // PUT: api/CTSanPham/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCTSanPham(int id, CTSanPham cTSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cTSanPham.CTSanPhamID)
            {
                return BadRequest();
            }

            db.Entry(cTSanPham).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTSanPhamExists(id))
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

        // POST: api/CTSanPham
        [ResponseType(typeof(CTSanPham))]
        public async Task<IHttpActionResult> PostCTSanPham(CTSanPham cTSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CTSanPhams.Add(cTSanPham);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cTSanPham.CTSanPhamID }, cTSanPham);
        }

        // DELETE: api/CTSanPham/5
        [ResponseType(typeof(CTSanPham))]
        public async Task<IHttpActionResult> DeleteCTSanPham(int id)
        {
            CTSanPham cTSanPham = await db.CTSanPhams.FindAsync(id);
            if (cTSanPham == null)
            {
                return NotFound();
            }

            db.CTSanPhams.Remove(cTSanPham);
            await db.SaveChangesAsync();

            return Ok(cTSanPham);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CTSanPhamExists(int id)
        {
            return db.CTSanPhams.Count(e => e.CTSanPhamID == id) > 0;
        }
    }
}