public class DiceCardAbility_ExtremeBoris_BorisDecide : EqualDice
{
    public override string[] Keywords => ["ExtremeBoris_EqualDice"];

    public override void OnLoseParrying()
    {
        base.owner.bufListDetail.RemoveBufAll(KeywordBuf.DmgUp);
    }
}
