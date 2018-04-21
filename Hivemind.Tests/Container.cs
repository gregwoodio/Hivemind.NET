using Hivemind.Managers;
using Hivemind.Managers.Implementation;
using Hivemind.Providers;
using Hivemind.Services;
using Hivemind.Services.Implementation;
using Hivemind.Utilities;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests
{
    public class Container
    {
        public static UnityContainer GetContainer()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
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
            container.RegisterType<IGangerProvider, GangerProvider>(new InjectionConstructor(connectionString));
            container.RegisterType<IGangProvider, GangProvider>(new InjectionConstructor(connectionString));
            container.RegisterType<IInjuryProvider, InjuryProvider>(new InjectionConstructor(connectionString));
            container.RegisterType<ISkillProvider, SkillProvider>(new InjectionConstructor(connectionString));
            container.RegisterType<ITerritoryProvider, TerritoryProvider>(new InjectionConstructor(connectionString));
            container.RegisterType<IUserProvider, UserProvider>(new InjectionConstructor(connectionString));
            container.RegisterType<IWeaponProvider, WeaponProvider>(new InjectionConstructor(connectionString));

            // utilities
            container.RegisterType<IDiceRoller, DiceRoller>();

            return container;
        }
    }
}
