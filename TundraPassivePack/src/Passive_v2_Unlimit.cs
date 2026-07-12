public class PassiveAbility_TundraPassivePack_Unlimit : AdvancedPassiveBase
{
    public override void OnRoundStart()
    {
        if (_charge >= 50)
        {
            base.owner?.RecoverHP(10);
        }

        if (_charge >= 100)
        {
            var charge = base.owner?.bufListDetail?.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge;

            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 10, base.owner);

            charge?.UseStack(5, false);
        }

        if (_charge >= 300)
        {
            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Quickness, 5, base.owner);
        }
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
        if (_charge >= 150)
        {
            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, base.owner);
        }

        if (_charge >= 200)
        {
            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, base.owner);
        }

        curCard.ApplyDiceAbility(_ => true, new DiceCardAbility_TundraPassivePack_Unlimit(this));
    }

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
        if (_charge >= 400)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus() { min = 5, max = 5 });
        }
    }

    public override void OnChangeBufStack(BattleUnitBuf changed, int last)
    {
        if (last > changed.stack && changed is BattleUnitBuf_warpCharge)
        {
            _charge += last - changed.stack;
        }
    }

    private int _charge;

    public class DiceCardAbility_TundraPassivePack_Unlimit(PassiveAbility_TundraPassivePack_Unlimit passive) : AdvancedDiceBase
    {
        public override ParryingResult GetParryingResult(ParryingResult origin)
        {
            if (_passive._charge >= 500 && origin is ParryingResult.Lose)
            {
                return ParryingResult.Draw;
            }

            return base.GetParryingResult(origin);
        }

        private PassiveAbility_TundraPassivePack_Unlimit _passive = passive;
    }
}
