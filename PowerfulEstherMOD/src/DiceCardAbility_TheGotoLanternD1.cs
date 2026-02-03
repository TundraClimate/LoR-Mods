public class DiceCardAbility_TheGotoLanternD1 : DiceCardAbilityBase
{
    public static string Desc = "[的中時] 次の幕に相手の速度ダイスを1つ封印。";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
        if (target != null)
        {
            target.bufListDetail.AddReadyBuf(new BattleUnitBuf_sealTemp());
        }
    }
}
