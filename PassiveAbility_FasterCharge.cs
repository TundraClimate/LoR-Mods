namespace WarpClassFive_Passive
{
    public class PassiveAbility_FasterCharge : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 10, this.owner);
            this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Paralysis, 6, this.owner);
        }
    }
}
