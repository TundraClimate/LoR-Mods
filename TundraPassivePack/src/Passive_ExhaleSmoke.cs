public class PassiveAbility_TundraPassivePack_ExhaleSmoke : AdvancedPassiveBase
{
    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
        behavior?.card?.target?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Smoke, 1, base.owner);
    }
}
