namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_UseCharge5 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            (base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge)
                .UseStack(5, true);
        }
    }
}
