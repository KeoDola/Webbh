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
    public class NhapKhoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NhapKhoe
        public IQueryable<NhapKho> GetNhapKhoes()
        {
            return db.NhapKhoes;
        }

        // GET: api/NhapKhoe/5
        [ResponseType(typeof(NhapKho))]
        public async Task<IHttpActionResult> GetNhapKho(int id)
        {
            NhapKho nhapKho = await db.NhapKhoes.FindAsync(id);
            if (nhapKho == null)
            {
                return NotFound();
            }

            return Ok(nhapKho);
        }

        // PUT: api/NhapKhoe/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNhapKho(int id, NhapKho nhapKho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nhapKho.NhapKhoID)
            {
                return BadRequest();
            }

            db.Entry(nhapKho).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhapKhoExists(id))
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

        // POST: api/NhapKhoe
        [ResponseType(typeof(NhapKho))]
        public async Task<IHttpActionResult> PostNhapKho(NhapKho nhapKho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NhapKhoes.Add(nhapKho);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = nhapKho.NhapKhoID }, nhapKho);
        }

        // DELETE: api/NhapKhoe/5
        [ResponseType(typeof(NhapKho))]
        public async Task<IHttpActionResult> DeleteNhapKho(int id)
        {
            NhapKho nhapKho = await db.NhapKhoes.FindAsync(id);
            if (nhapKho == null)
            {
                return NotFound();
            }

            db.NhapKhoes.Remove(nhapKho);
            await db.SaveChangesAsync();

            return Ok(nhapKho);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NhapKhoExists(int id)
        {
            return db.NhapKhoes.Count(e => e.NhapKhoID == id) > 0;
        }
    }
}