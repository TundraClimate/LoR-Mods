using System.Collections.Generic;

public class EnemyUnitTargetSetter_Esther : EnemyUnitTargetSetter
{
    public override BattleUnitModel SelectTargetUnit(List<BattleUnitModel> candidates)
    {
        return base.GetHighestAggro(candidates);
    }
}
