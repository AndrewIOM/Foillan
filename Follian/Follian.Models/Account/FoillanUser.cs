using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Foillan.Models.Occurrence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Foillan.Models.Account
{
    public class FoillanUser : IdentityUser
    {
        public virtual IEnumerable<Sighting> Sightings { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<FoillanUser> manager, 
            string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}