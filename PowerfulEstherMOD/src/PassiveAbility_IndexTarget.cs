using System.Collections.Generic;

public class PassiveAbility_IndexTarget : PassiveAbilityBase
{
    public override void OnRoundStart()
    {
        List<BattleUnitModel> targets = BattleObjectManager.instance.GetAliveList_random(Faction.Player, 1);

        if (targets.Count == 0)
        {
            return;
        }

        BattleUnitModel target = targets[0];

        target.bufListDetail.AddBuf(new BattleUnitBuf_TargetOfPrescript());
    }
}
