public class PassiveAbility_TundraPassivePack_Discharge : AdvancedPassiveBase
{
    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
        base.owner?.TakeDamage(3, DamageType.Passive, base.owner);

        base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 1, base.owner);
    }

    public override void OnStartParrying(BattlePlayingCardDataInUnitModel card)
    {
        card.target?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 1, base.owner);
        card.target?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Burn, 3, base.owner);
        card.target?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Paralysis, 1, base.owner);
    }
}
