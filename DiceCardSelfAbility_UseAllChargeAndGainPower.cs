namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_UseAllChargeAndGainPower : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            BattleUnitBuf_warpCharge activatedBuf = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge;
            int stack = activatedBuf.stack;
            int buff = stack / 2;
            activatedBuf.UseStack(stack, true);
            base.card.ApplyDiceStatBonus(DiceMatch.AllAttackDice, new DiceStatBonus { power = buff });
        }
    }
}
