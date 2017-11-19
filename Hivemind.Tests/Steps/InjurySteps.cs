using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Managers;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Hivemind.Tests.Steps
{
    [Binding]
    public class InjurySteps
    {
        public Ganger gangerBefore;
        public Ganger ganger;
        public Injury injury;
        private IInjuryManager _injuryFactory;

        public InjurySteps()
        {
            var container = Container.GetContainer();
            _injuryFactory = container.Resolve<IInjuryManager>();
        }

        [Given(@"a ganger with stats as follows:")]
        public void GivenAGangerWithStatsAsFollows(Table table)
        {
            ganger = table.CreateInstance<Ganger>();
            gangerBefore = new Ganger(ganger);
        }

        [When(@"the ganger gets the injury ""(.*)""")]
        public void WhenTheGangerGetsTheInjury(string injuryName)
        {
            injury = _injuryFactory.GetInjury((int)GetEnumValue(injuryName));

            injury.InjuryEffect(ganger);
        }

        [Then(@"the ganger should not be active anymore")]
        public void ThenTheGangerShouldNotBeActiveAnymore()
        {
            Assert.IsFalse(ganger.Active);
        }

        [Then(@"the ganger should get at least one more injury\.")]
        public void ThenTheGangerShouldGetAtLeastOneMoreInjury_()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the ganger should have their toughness reduced by (.*)\.")]
        public void ThenTheGangerShouldHaveTheirToughnessReducedBy_(int reduction)
        {
            Assert.AreEqual(ganger.Toughness, gangerBefore.Toughness - reduction);
        }

        [Then(@"the ganger should have their movement reduced by (.*)\.")]
        public void ThenTheGangerShouldHaveTheirMovementReducedBy_(int reduction)
        {
            Assert.AreEqual(ganger.Move, gangerBefore.Move - reduction);
        }

        [Then(@"the ganger should have their strength reduced by (.*)\.")]
        public void ThenTheGangerShouldHaveTheirStrengthReducedBy_(int reduction)
        {
            Assert.AreEqual(ganger.Strength, gangerBefore.Strength - reduction);
        }

        [Then(@"the ganger should not have a statistics change\.")]
        public void ThenTheGangerShouldNotHaveAStatisticsChange_()
        {
            Assert.AreEqual(ganger.Move, gangerBefore.Move);
            Assert.AreEqual(ganger.WeaponSkill, gangerBefore.WeaponSkill);
            Assert.AreEqual(ganger.BallisticSkill, gangerBefore.BallisticSkill);
            Assert.AreEqual(ganger.Strength, gangerBefore.Strength);
            Assert.AreEqual(ganger.Toughness, gangerBefore.Toughness);
            Assert.AreEqual(ganger.Attack, gangerBefore.Attack);
            Assert.AreEqual(ganger.Initiative, gangerBefore.Initiative);
            Assert.AreEqual(ganger.Wounds, gangerBefore.Wounds);
            Assert.AreEqual(ganger.Leadership, gangerBefore.Leadership);
        }

        [Then(@"the ganger should have their ballistic skill redeced by (.*)")]
        public void ThenTheGangerShouldHaveTheirBallisticSkillRedecedBy(int reduction)
        {
            Assert.AreEqual(ganger.BallisticSkill, gangerBefore.BallisticSkill - reduction);
        }

        [Then(@"the ganger should be marked as having only one eye\.")]
        public void ThenTheGangerShouldBeMarkedAsHavingOnlyOneEye_()
        {
            Assert.IsTrue(ganger.IsOneEyed);
        }

        [Then(@"the ganger should have their leadership reduced by (.*)\.")]
        public void ThenTheGangerShouldHaveTheirLeadershipReducedBy_(int reduction)
        {
            Assert.AreEqual(ganger.Leadership, gangerBefore.Leadership - reduction);
        }

        [Then(@"the ganger loses some fingers on either hand\.")]
        public void ThenTheGangerLosesSomeFingersOnEitherHand_()
        {
            if (ganger.RightHandFingers < 5 || ganger.LeftHandFingers < 5)
            {
                Assert.IsTrue(true);
            } else
            {
                Assert.IsTrue(false);
            }
        }

        [Then(@"the ganger should have their weapon skill reduced by (.*)\.")]
        public void ThenTheGangerShouldHaveTheirWeaponSkillReducedBy_(int reduction)
        {
            Assert.AreEqual(ganger.WeaponSkill, gangerBefore.WeaponSkill - reduction);
        }

        [Then(@"the ganger should be one armed\.")]
        public void ThenTheGangerShouldBeOneArmed_()
        {
            Assert.IsTrue(ganger.IsOneHanded);
        }

        [Then(@"the ganger should be marked as partially deafened\.")]
        public void ThenTheGangerShouldBeMarkedAsPartiallyDeafened_()
        {
            Assert.IsTrue(ganger.IsDeafened);
        }

        [Then(@"the ganger should have their initiative reduced by (.*)\.")]
        public void ThenTheGangerShouldHaveTheirInitiativeReducedBy_(int reduction)
        {
            Assert.AreEqual(ganger.Initiative, gangerBefore.Initiative - reduction);
        }

        [Then(@"the ganger should have a head wound\.")]
        public void ThenTheGangerShouldHaveAHeadWound_()
        {
            Assert.IsTrue(ganger.HasHeadWound);
        }

        [Then(@"the ganger should have an old battle wound\.")]
        public void ThenTheGangerShouldHaveAnOldBattleWound_()
        {
            Assert.IsTrue(ganger.HasOldBattleWound);
        }

        [Then(@"the ganger should have bitter enmity\.")]
        public void ThenTheGangerShouldHaveBitterEnmity_()
        {
            Assert.IsTrue(ganger.HasBitterEnmity);
        }

        [Then(@"the ganger should be marked as a captive\.")]
        public void ThenTheGangerShouldBeMarkedAsACaptive_()
        {
            Assert.IsTrue(ganger.IsCaptured);
        }

        [Then(@"the ganger should have horrible scars\.")]
        public void ThenTheGangerShouldHaveHorribleScars_()
        {
            Assert.IsTrue(ganger.HasHorribleScars);
        }

        [Then(@"the ganger's leadership should increase by (.*)\.")]
        public void ThenTheGangerSLeadershipShouldIncreaseBy_(int increase)
        {
            Assert.AreEqual(ganger.Leadership, gangerBefore.Leadership + increase);
        }

        [Then(@"the ganger should be marked as having impressive scars\.")]
        public void ThenTheGangerShouldBeMarkedAsHavingImpressiveScars_()
        {
            Assert.IsTrue(ganger.HasImpressiveScars);
        }

        [Then(@"the ganger should have an experience increase\.")]
        public void ThenTheGangerShouldHaveAnExperienceIncrease_()
        {
            Assert.IsTrue(ganger.Experience > gangerBefore.Experience);
        }

        private InjuryEnum GetEnumValue(string injuryName)
        {
            InjuryEnum value;
            Enum.TryParse(injuryName.ToUpperInvariant(), out value);
            if (value == 0)
            {
                HivemindException.NoSuchInjuryException(injuryName);
            }
            return value;
        }
    }
}
