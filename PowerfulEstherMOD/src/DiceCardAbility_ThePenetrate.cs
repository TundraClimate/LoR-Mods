public class DiceCardAbility_ThePenetrate : DiceCardAbilityBase
{
    public static string Desc = "[的中] 麻痺1を付与";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
        if (target != null)
        {
            target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 1, base.owner);
        }
    }
}
