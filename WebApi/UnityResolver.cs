using Hivemind.Factories;
using Hivemind.Services;
using Microsoft.Practices.Unity;
using System.Web.Http.Dependencies;
using System;
using System.Collections.Generic;

namespace WebApi
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public UnityResolver()
        {
            container = SetupContainer();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }

        public UnityContainer SetupContainer()
        {
            UnityContainer container = new UnityContainer();

            // factories and managers
            container.RegisterType<IGangerFactory, GangerFactory>();
            container.RegisterType<IGangFactory, GangFactory>();
            container.RegisterType<IInjuryFactory, InjuryFactory>();
            container.RegisterType<ISkillFactory, SkillFactory>();
            container.RegisterType<IWeaponFactory, WeaponFactory>();
            container.RegisterType<ITerritoryFactory, TerritoryFactory>();

            // services
            container.RegisterType<IExperienceService, ExperienceService>();
            container.RegisterType<IIncomeService, IncomeService>();
            container.RegisterType<IInjuryService, InjuryService>();
            container.RegisterType<IGameService, GameService>();

            return container;
        }
    }
}