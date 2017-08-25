using Hivemind.Factories;
using Hivemind.Services;
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
            container.RegisterType<IPostGameService, PostGameService>();
            container.RegisterType<IPreGameService, PreGameService>();

            return container;
        }
    }
}
