using DeviceOfHermes;

public class PassiveAbility_TundraPassivePack_SupplySmoke : AdvancedPassiveBase
{
    public override void OnRoundEnd_before()
    {
        if (base.owner?.TryGetBuf<BattleUnitBuf_smoke>(out var smoke) == true)
        {
            smoke.stack += 1;
        }
        else
        {
            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Smoke, 1, base.owner);
        }
    }
}
