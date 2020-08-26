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
    public class PhanLoaiSanPhamController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PhanLoaiSanPham
        public IQueryable<PhanLoaiSanPham> GetPhanLoaiSanPhams()
        {
            return db.PhanLoaiSanPhams;
        }

        // GET: api/PhanLoaiSanPham/5
        [ResponseType(typeof(PhanLoaiSanPham))]
        public async Task<IHttpActionResult> GetPhanLoaiSanPham(int id)
        {
            PhanLoaiSanPham phanLoaiSanPham = await db.PhanLoaiSanPhams.FindAsync(id);
            if (phanLoaiSanPham == null)
            {
                return NotFound();
            }

            return Ok(phanLoaiSanPham);
        }

        // PUT: api/PhanLoaiSanPham/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPhanLoaiSanPham(int id, PhanLoaiSanPham phanLoaiSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phanLoaiSanPham.PhanLoaiSanPhamID)
            {
                return BadRequest();
            }

            db.Entry(phanLoaiSanPham).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhanLoaiSanPhamExists(id))
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

        // POST: api/PhanLoaiSanPham
        [ResponseType(typeof(PhanLoaiSanPham))]
        public async Task<IHttpActionResult> PostPhanLoaiSanPham(PhanLoaiSanPham phanLoaiSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhanLoaiSanPhams.Add(phanLoaiSanPham);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = phanLoaiSanPham.PhanLoaiSanPhamID }, phanLoaiSanPham);
        }

        // DELETE: api/PhanLoaiSanPham/5
        [ResponseType(typeof(PhanLoaiSanPham))]
        public async Task<IHttpActionResult> DeletePhanLoaiSanPham(int id)
        {
            PhanLoaiSanPham phanLoaiSanPham = await db.PhanLoaiSanPhams.FindAsync(id);
            if (phanLoaiSanPham == null)
            {
                return NotFound();
            }

            db.PhanLoaiSanPhams.Remove(phanLoaiSanPham);
            await db.SaveChangesAsync();

            return Ok(phanLoaiSanPham);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhanLoaiSanPhamExists(int id)
        {
            return db.PhanLoaiSanPhams.Count(e => e.PhanLoaiSanPhamID == id) > 0;
        }
    }
}