using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.Models.DataAccessLayer.Concrete;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Foillan.WebService
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<ITaxonService, TaxonService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IEntity<int>, Entity<int>>();
            container.RegisterType<IFoillanContext, FoillanContext>();
            container.RegisterType<IRepository<Taxon>, TaxonRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}