namespace WarpClassFive_Passive
{
    public class PassiveAbility_JumpForce : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            BattleUnitBuf activatedBuf = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge);
            if (activatedBuf != null && activatedBuf.stack >= 11)
            {
                this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Quickness, 3, this.owner);
            }
        }
    }
}
