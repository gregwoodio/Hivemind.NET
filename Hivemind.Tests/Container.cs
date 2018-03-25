using Hivemind.Managers;
using Hivemind.Managers.Implementation;
using Hivemind.Providers;
using Hivemind.Services;
using Hivemind.Services.Implementation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests
{
    public class Container
    {
        public static UnityContainer GetContainer()
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
            container.RegisterType<IGangerProvider, GangerProvider>();
            container.RegisterType<IGangProvider, GangProvider>();
            container.RegisterType<IInjuryProvider, InjuryProvider>();
            container.RegisterType<ISkillProvider, SkillProvider>();
            container.RegisterType<ITerritoryProvider, TerritoryProvider>();
            container.RegisterType<IUserProvider, UserProvider>();
            container.RegisterType<IWeaponProvider, WeaponProvider>();
            
            return container;
        }
    }
}
