public class DiceCardAbility_GiveBleed2 : AdvancedDiceBase
{
    public static string Desc = "[的中] 出血を2付与";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
        target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Bleeding, 2, base.owner);
    }
}
