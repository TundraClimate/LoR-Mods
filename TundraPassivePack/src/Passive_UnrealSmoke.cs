public class PassiveAbility_TundraPassivePack_UnrealSmoke : AdvancedPassiveBase
{
    private Cand RandomCand => (Cand)RandomUtil.Range(0, 3);

    public override void OnRoundStart()
    {
        if (base.owner?.GetBufStack<BattleUnitBuf_smoke>() >= 9)
        {
            _cand = RandomCand;
        }
        else
        {
            _cand = Cand.None;
        }

        if (_cand is Cand.Power)
        {
            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 3, base.owner);
            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Quickness, 5, base.owner);
        }
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
        if (_cand is Cand.Ignore)
        {
            curCard?.ignorePower = true;
        }
    }

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
        if (_cand is Cand.Dice && behavior?.Type is LOR_DiceSystem.BehaviourType.Atk)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus() { min = 4, max = 4 });
        }

        if (_cand is Cand.Evade && behavior?.Detail is LOR_DiceSystem.BehaviourDetail.Evasion)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus() { min = -3, max = 12 });
        }
    }

    private Cand _cand = Cand.None;

    private enum Cand
    {
        Power,
        Dice,
        Evade,
        Ignore,
        None,
    }
}
