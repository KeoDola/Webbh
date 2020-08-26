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
    public class DanhMucSanPhamController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DanhMucSanPham
        public IQueryable<DanhMucSanPham> GetDanhMucSanPhams()
        {
            return db.DanhMucSanPhams;
        }

        // GET: api/DanhMucSanPham/5
        [ResponseType(typeof(DanhMucSanPham))]
        public async Task<IHttpActionResult> GetDanhMucSanPham(int id)
        {
            DanhMucSanPham danhMucSanPham = await db.DanhMucSanPhams.FindAsync(id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }

            return Ok(danhMucSanPham);
        }

        // PUT: api/DanhMucSanPham/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDanhMucSanPham(int id, DanhMucSanPham danhMucSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != danhMucSanPham.DanhMucSanPhamID)
            {
                return BadRequest();
            }

            db.Entry(danhMucSanPham).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhMucSanPhamExists(id))
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

        // POST: api/DanhMucSanPham
        [ResponseType(typeof(DanhMucSanPham))]
        public async Task<IHttpActionResult> PostDanhMucSanPham(DanhMucSanPham danhMucSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DanhMucSanPhams.Add(danhMucSanPham);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = danhMucSanPham.DanhMucSanPhamID }, danhMucSanPham);
        }

        // DELETE: api/DanhMucSanPham/5
        [ResponseType(typeof(DanhMucSanPham))]
        public async Task<IHttpActionResult> DeleteDanhMucSanPham(int id)
        {
            DanhMucSanPham danhMucSanPham = await db.DanhMucSanPhams.FindAsync(id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }

            db.DanhMucSanPhams.Remove(danhMucSanPham);
            await db.SaveChangesAsync();

            return Ok(danhMucSanPham);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DanhMucSanPhamExists(int id)
        {
            return db.DanhMucSanPhams.Count(e => e.DanhMucSanPhamID == id) > 0;
        }
    }
}