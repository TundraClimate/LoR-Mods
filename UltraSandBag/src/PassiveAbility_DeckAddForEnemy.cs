using System.Collections.Generic;

public class PassiveAbility_DeckAddForEnemy : PassiveAbilityBase
{
    public override void OnRoundStart()
    {
        List<BattleUnitModel> enemies = BattleObjectManager.instance.GetAliveList(false);

        foreach (BattleUnitModel enemy in enemies)
        {
            List<BattleDiceCardModel> personalCards = enemy.personalEgoDetail.GetCardAll();
            List<LorId> personalIds = personalCards.ConvertAll((BattleDiceCardModel model) => model.GetID());

            foreach (int numId in this._pageIds)
            {
                LorId loredId = new LorId(UltraSandBagMOD.packageId, numId);

                if (!personalIds.Contains(loredId))
                {
                    enemy.personalEgoDetail.AddCard(loredId);
                }
            }
        }
    }

    private int[] _pageIds
    {
        get
        {
            return new int[]
            {
                1,
                2
            };
        }
    }
}
