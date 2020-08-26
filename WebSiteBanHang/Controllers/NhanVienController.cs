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
    public class NhanVienController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NhanVien
        public IQueryable<NhanVien> GetNhanViens()
        {
            return db.NhanViens;
        }

        // GET: api/NhanVien/5
        [ResponseType(typeof(NhanVien))]
        public async Task<IHttpActionResult> GetNhanVien(int id)
        {
            NhanVien nhanVien = await db.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return Ok(nhanVien);
        }

        // PUT: api/NhanVien/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNhanVien(int id, NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nhanVien.NhanVienID)
            {
                return BadRequest();
            }

            db.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
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

        // POST: api/NhanVien
        [ResponseType(typeof(NhanVien))]
        public async Task<IHttpActionResult> PostNhanVien(NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NhanViens.Add(nhanVien);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = nhanVien.NhanVienID }, nhanVien);
        }

        // DELETE: api/NhanVien/5
        [ResponseType(typeof(NhanVien))]
        public async Task<IHttpActionResult> DeleteNhanVien(int id)
        {
            NhanVien nhanVien = await db.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            db.NhanViens.Remove(nhanVien);
            await db.SaveChangesAsync();

            return Ok(nhanVien);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NhanVienExists(int id)
        {
            return db.NhanViens.Count(e => e.NhanVienID == id) > 0;
        }
    }
}