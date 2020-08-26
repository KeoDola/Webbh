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
    public class ThongSoSanPhamController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ThongSoSanPham
        public IQueryable<ThongSoSanPham> GetThongSoSanPhams()
        {
            return db.ThongSoSanPhams;
        }

        // GET: api/ThongSoSanPham/5
        [ResponseType(typeof(ThongSoSanPham))]
        public async Task<IHttpActionResult> GetThongSoSanPham(int id)
        {
            ThongSoSanPham thongSoSanPham = await db.ThongSoSanPhams.FindAsync(id);
            if (thongSoSanPham == null)
            {
                return NotFound();
            }

            return Ok(thongSoSanPham);
        }

        // PUT: api/ThongSoSanPham/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutThongSoSanPham(int id, ThongSoSanPham thongSoSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != thongSoSanPham.ThongSoSanPhamID)
            {
                return BadRequest();
            }

            db.Entry(thongSoSanPham).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThongSoSanPhamExists(id))
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

        // POST: api/ThongSoSanPham
        [ResponseType(typeof(ThongSoSanPham))]
        public async Task<IHttpActionResult> PostThongSoSanPham(ThongSoSanPham thongSoSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ThongSoSanPhams.Add(thongSoSanPham);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = thongSoSanPham.ThongSoSanPhamID }, thongSoSanPham);
        }

        // DELETE: api/ThongSoSanPham/5
        [ResponseType(typeof(ThongSoSanPham))]
        public async Task<IHttpActionResult> DeleteThongSoSanPham(int id)
        {
            ThongSoSanPham thongSoSanPham = await db.ThongSoSanPhams.FindAsync(id);
            if (thongSoSanPham == null)
            {
                return NotFound();
            }

            db.ThongSoSanPhams.Remove(thongSoSanPham);
            await db.SaveChangesAsync();

            return Ok(thongSoSanPham);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThongSoSanPhamExists(int id)
        {
            return db.ThongSoSanPhams.Count(e => e.ThongSoSanPhamID == id) > 0;
        }
    }
}