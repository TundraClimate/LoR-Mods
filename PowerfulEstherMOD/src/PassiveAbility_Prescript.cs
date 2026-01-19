public class PassiveAbility_Prescript : PassiveAbilityBase
{
    public override void OnWaveStart()
    {
        if (base.owner != null || !base.owner.bufListDetail.HasBuf<BattleUnitBuf_GraceOfPrescript>())
        {
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_GraceOfPrescript());
        }

        this._isPassedByTarget = false;
        this._isPrescriptPassed = true;
    }

    public override void OnRoundStart()
    {
        if (base.owner == null)
        {
            return;
        }

        BattleUnitBufListDetail bufList = base.owner.bufListDetail;

        if (!bufList.HasBuf<BattleUnitBuf_GraceOfPrescript>())
        {
            bufList.AddBuf(new BattleUnitBuf_GraceOfPrescript());
        }

        BattleUnitBuf_GraceOfPrescript grace = (BattleUnitBuf_GraceOfPrescript)bufList.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_GraceOfPrescript);

        if (this._isPrescriptPassed && this._isPassedByTarget)
        {
            grace.AddStack(3);
        }
        else if (this._isPrescriptPassed)
        {
            grace.AddStack(1);
        }

        this._isPassedByTarget = false;
        this._isPrescriptPassed = true;
    }

    private bool _isPrescriptPassed;

    private bool _isPassedByTarget;
}
