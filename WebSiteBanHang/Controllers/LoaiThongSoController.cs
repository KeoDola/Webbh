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
    public class LoaiThongSoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LoaiThongSo
        public IQueryable<LoaiThongSo> GetLoaiThongSoes()
        {
            return db.LoaiThongSoes;
        }

        // GET: api/LoaiThongSo/5
        [ResponseType(typeof(LoaiThongSo))]
        public async Task<IHttpActionResult> GetLoaiThongSo(int id)
        {
            LoaiThongSo loaiThongSo = await db.LoaiThongSoes.FindAsync(id);
            if (loaiThongSo == null)
            {
                return NotFound();
            }

            return Ok(loaiThongSo);
        }

        // PUT: api/LoaiThongSo/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLoaiThongSo(int id, LoaiThongSo loaiThongSo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loaiThongSo.LoaiThongSoID)
            {
                return BadRequest();
            }

            db.Entry(loaiThongSo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiThongSoExists(id))
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

        // POST: api/LoaiThongSo
        [ResponseType(typeof(LoaiThongSo))]
        public async Task<IHttpActionResult> PostLoaiThongSo(LoaiThongSo loaiThongSo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoaiThongSoes.Add(loaiThongSo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoaiThongSoExists(loaiThongSo.LoaiThongSoID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = loaiThongSo.LoaiThongSoID }, loaiThongSo);
        }

        // DELETE: api/LoaiThongSo/5
        [ResponseType(typeof(LoaiThongSo))]
        public async Task<IHttpActionResult> DeleteLoaiThongSo(int id)
        {
            LoaiThongSo loaiThongSo = await db.LoaiThongSoes.FindAsync(id);
            if (loaiThongSo == null)
            {
                return NotFound();
            }

            db.LoaiThongSoes.Remove(loaiThongSo);
            await db.SaveChangesAsync();

            return Ok(loaiThongSo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoaiThongSoExists(int id)
        {
            return db.LoaiThongSoes.Count(e => e.LoaiThongSoID == id) > 0;
        }
    }
}