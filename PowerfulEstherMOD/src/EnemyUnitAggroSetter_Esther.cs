using System.Collections.Generic;

public class EnemyUnitAggroSetter_Esther : EnemyUnitAggroSetter
{
    public override void OnRoundStart(List<BattleUnitModel> playerUnits)
    {
        if (StageController.Instance.RoundTurn == 1)
        {
            EnemyUnitAggroSetter_Esther._prev = null;
        }

        foreach (BattleUnitModel player in playerUnits)
        {
            if (player.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                player.aggroDetail.AddRoundScore(9999);
                EnemyUnitAggroSetter_Esther._prev = player.id;
            }
            else if (player.id == EnemyUnitAggroSetter_Esther._prev)
            {
                player.aggroDetail.AddRoundScore(-9999);
            }
        }
    }

    private static int? _prev = null;
}
