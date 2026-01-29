using System;

public class DiceCardAbility_TheBleedConsume : DiceCardAbilityBase
{
    public static string Desc = "[マッチ敗北] 自分の出血が2に減少";

    public override void OnLoseParrying()
    {
        if (base.owner != null)
        {
            BattleUnitBuf_bleeding buf = (BattleUnitBuf_bleeding)base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Bleeding);

            if (buf != null)
            {
                buf.stack = Math.Min(buf.stack, 2);
            }
        }
    }
}
