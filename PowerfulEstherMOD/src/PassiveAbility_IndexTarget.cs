using System.Collections.Generic;

public class PassiveAbility_IndexTarget : PassiveAbilityBase
{
    public override void OnRoundStart()
    {
        List<BattleUnitModel> targets = BattleObjectManager.instance.GetAliveList_random(base.owner.faction == Faction.Player ? Faction.Enemy : Faction.Player, 1);

        targets.RemoveAll(unit => !unit.IsTargetable(base.owner));

        if (targets.Count == 0)
        {
            return;
        }

        BattleUnitModel target = targets[0];

        PrescriptBuf prescript = (PrescriptBuf)base.owner.bufListDetail.GetActivatedBufList().Find(buf => buf is PrescriptBuf);

        if (prescript != null)
        {
            target = prescript.FixedIndexTarget(targets, target);
        }

        target.bufListDetail.AddBuf(new BattleUnitBuf_TargetOfPrescript());
    }
}
