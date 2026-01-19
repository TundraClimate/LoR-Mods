using System.Collections.Generic;

public class EnemyUnitTargetSetter_Esther : EnemyUnitTargetSetter
{
    public override BattleUnitModel SelectTargetUnit(List<BattleUnitModel> candidates)
    {
        foreach (BattleUnitModel candidate in candidates)
        {
            if (candidate.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                return candidate;
            }
        }

        return base.SelectTargetUnit(candidates);
    }
}
