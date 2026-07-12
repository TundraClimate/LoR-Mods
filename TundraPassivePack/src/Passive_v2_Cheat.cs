public class PassiveAbility_TundraPassivePack_Cheat : AdvancedPassiveBase
{
    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
        curCard.ApplyDiceAbility(_ => true, new DiceCardAbility_TundraPassivePack_Cheat());
    }

    public class DiceCardAbility_TundraPassivePack_Cheat : AdvancedDiceBase
    {
        public override ParryingResult GetParryingResult(ParryingResult origin)
        {
            if (base.owner.GetBuf<BattleUnitBuf_warpCharge>() is BattleUnitBuf_warpCharge charge && charge.stack >= 6)
            {
                charge.UseStack(6, false);

                return ParryingResult.Win;
            }

            return base.GetParryingResult(origin);
        }
    }
}
