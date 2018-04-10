using Hivemind.Managers;
using Hivemind.Services;
using Microsoft.Practices.Unity;
using System.Web.Http.Dependencies;
using System;
using System.Collections.Generic;
using Hivemind.Services.Implementation;
using Hivemind.Managers.Implementation;
using Hivemind.Providers;
using System.Configuration;

namespace WebApi
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;
        private string _connectionString;

        public UnityResolver(IUnityContainer container)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            this.container = container;
        }

        public UnityResolver()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
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

            // managers
            container.RegisterType<IGangerManager, GangerManager>();
            container.RegisterType<IGangManager, GangManager>();
            container.RegisterType<IInjuryManager, InjuryManager>();
            container.RegisterType<ISkillManager, SkillManager>();
            container.RegisterType<IWeaponManager, WeaponManager>();
            container.RegisterType<ITerritoryManager, TerritoryManager>();
            container.RegisterType<IUserManager, UserManager>();

            // services
            container.RegisterType<IExperienceService, ExperienceService>();
            container.RegisterType<IIncomeService, IncomeService>();
            container.RegisterType<IInjuryService, InjuryService>();
            container.RegisterType<IGameService, GameService>();

            // providers
            container.RegisterType<IGangerProvider, GangerProvider>(new InjectionConstructor(_connectionString));
            container.RegisterType<IGangProvider, GangProvider>(new InjectionConstructor(_connectionString));
            container.RegisterType<IInjuryProvider, InjuryProvider>(new InjectionConstructor(_connectionString));
            container.RegisterType<ISkillProvider, SkillProvider>(new InjectionConstructor(_connectionString));
            container.RegisterType<ITerritoryProvider, TerritoryProvider>(new InjectionConstructor(_connectionString));
            container.RegisterType<IUserProvider, UserProvider>(new InjectionConstructor(_connectionString));
            container.RegisterType<IWeaponProvider, WeaponProvider>(new InjectionConstructor(_connectionString));

            return container;
        }
    }
}