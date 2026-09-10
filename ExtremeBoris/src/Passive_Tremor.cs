using LOR_DiceSystem;

public class PassiveAbility_ExtremeBoris_Tremor : AdvancedPassiveBase
{
    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
        curCard.ApplyDiceAbility(DiceMatch.LastDice, new TB());
    }

    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
        if (behavior.card?.target is BattleUnitModel target && behavior.Detail is BehaviourDetail.Hit)
        {
            target.bufListDetail.AddKeywordBufThisRoundByEtc(LimKeywordBuf.Tremor, 5, base.owner);
        }
    }

    class TB : AdvancedDiceBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (behavior.Detail is BehaviourDetail.Hit)
            {
                target.bufListDetail.AddKeywordBufThisRoundByEtc(LimKeywordBuf.TremorBurst, 1, base.owner);
                target.bufListDetail.AddKeywordBufThisRoundByEtc(LimKeywordBuf.ConsumeTremor, 3, base.owner);
            }
        }
    }
}
