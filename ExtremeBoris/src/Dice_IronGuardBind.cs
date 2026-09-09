public class DiceCardAbility_ExtremeBoris_IronGuardBind : EqualDice
{
    public override string[] Keywords => ["ExtremeBoris_EqualDice"];

    public override void OnSucceedAttack(BattleUnitModel target)
    {
        target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, base.owner);
    }
}
