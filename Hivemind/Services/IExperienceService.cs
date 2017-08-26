using Hivemind.Contracts;
using Hivemind.Entities;
using Hivemind.Enums;

namespace Hivemind.Services
{
    public interface IExperienceService
    {
        GangLevelUpReport ProcessExperience(BattleReport battleReport);
        int GetUnderdogBonus(int gangRating, int opponentGangRating, bool hasWon);
        int GetWoundingHitBonus(int woundingHits);
        int GetLeaderBonus(Ganger ganger, GameType gameType, bool hasWon, bool isAttacker);
        int GetNumberOfAdvanceRolls(Ganger ganger, int experience);
        int GetObjectivesBonus(int objectives, GameType gameType);
        int GetWinningBonus(bool hasWon, GameType gameType);
        int GetSurvivalBonus();
    }
}
