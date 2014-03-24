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
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using UTCCatholic.Models;

namespace UTCCatholic.Controllers.API
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using UTCCatholic.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<User>("User");
    builder.EntitySet<IdentityUserClaim>("IdentityUserClaim"); 
    builder.EntitySet<IdentityUserLogin>("IdentityUserLogin"); 
    builder.EntitySet<IdentityUserRole>("IdentityUserRole"); 
    builder.EntitySet<Medical>("Medical"); 
    builder.EntitySet<Misc>("Misc"); 
    builder.EntitySet<Personal>("Personal"); 
    builder.EntitySet<Religion>("Religion"); 
    builder.EntitySet<Retreat>("Retreat"); 
    builder.EntitySet<School>("School"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class UserController : ODataController
    {
        private AppDbContext db = new AppDbContext();

        // GET odata/User
        [Queryable]
        public IQueryable<User> GetUser()
        {
            return db.IdentityUsers;
        }

        // GET odata/User(5)
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] string key)
        {
            return SingleResult.Create(db.IdentityUsers.Where(user => user.Id == key));
        }

        // PUT odata/User(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(user);
        }

        // POST odata/User
        public async Task<IHttpActionResult> Post(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IdentityUsers.Add(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(user);
        }

        // PATCH odata/User(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<User> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await db.IdentityUsers.FindAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            patch.Patch(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(user);
        }

        // DELETE odata/User(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            User user = await db.IdentityUsers.FindAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            db.IdentityUsers.Remove(user);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/User(5)/Claims
        [Queryable]
        public IQueryable<IdentityUserClaim> GetClaims([FromODataUri] string key)
        {
            return db.IdentityUsers.Where(m => m.Id == key).SelectMany(m => m.Claims);
        }

        // GET odata/User(5)/Logins
        [Queryable]
        public IQueryable<IdentityUserLogin> GetLogins([FromODataUri] string key)
        {
            return db.IdentityUsers.Where(m => m.Id == key).SelectMany(m => m.Logins);
        }

        // GET odata/User(5)/Roles
        [Queryable]
        public IQueryable<IdentityUserRole> GetRoles([FromODataUri] string key)
        {
            return db.IdentityUsers.Where(m => m.Id == key).SelectMany(m => m.Roles);
        }

        // GET odata/User(5)/Medical
        [Queryable]
        public SingleResult<Medical> GetMedical([FromODataUri] string key)
        {
            return SingleResult.Create(db.IdentityUsers.Where(m => m.Id == key).Select(m => m.Medical));
        }

        // GET odata/User(5)/Misc
        [Queryable]
        public SingleResult<Misc> GetMisc([FromODataUri] string key)
        {
            return SingleResult.Create(db.IdentityUsers.Where(m => m.Id == key).Select(m => m.Misc));
        }

        // GET odata/User(5)/Personal
        [Queryable]
        public SingleResult<Personal> GetPersonal([FromODataUri] string key)
        {
            return SingleResult.Create(db.IdentityUsers.Where(m => m.Id == key).Select(m => m.Personal));
        }

        // GET odata/User(5)/Religion
        [Queryable]
        public SingleResult<Religion> GetReligion([FromODataUri] string key)
        {
            return SingleResult.Create(db.IdentityUsers.Where(m => m.Id == key).Select(m => m.Religion));
        }

        // GET odata/User(5)/Retreats
        [Queryable]
        public IQueryable<Retreat> GetRetreats([FromODataUri] string key)
        {
            return db.IdentityUsers.Where(m => m.Id == key).SelectMany(m => m.Retreats);
        }

        // GET odata/User(5)/School
        [Queryable]
        public SingleResult<School> GetSchool([FromODataUri] string key)
        {
            return SingleResult.Create(db.IdentityUsers.Where(m => m.Id == key).Select(m => m.School));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string key)
        {
            return db.IdentityUsers.Count(e => e.Id == key) > 0;
        }
    }
}
