public class BattleUnitBuf_TargetOfPrescript : BattleUnitBuf
{
    protected override string keywordId
    {
        get
        {
            return "TargetOfPrescript";
        }
    }

    public BattleUnitBuf_TargetOfPrescript()
    {
        base.stack = 0;
    }

    public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
    {
        if (atkDice.card.owner.bufListDetail.HasBuf<PrescriptBuf>())
        {
            atkDice.ApplyDiceStatBonus(new DiceStatBonus()
            {
                dmg = 1,
            });
        }

        base.OnTakeDamageByAttack(atkDice, dmg);
    }

    public override void OnRoundEnd()
    {
        base.Destroy();
    }
}
