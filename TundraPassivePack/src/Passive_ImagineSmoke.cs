public class PassiveAbility_TundraPassivePack_ImagineSmoke : AdvancedPassiveBase
{
    private int RandNum => RandomUtil.Range(1, 3);

    public override void OnRoundStartFirst()
    {
        foreach (var unit in BattleObjectManager.instance.GetAliveList())
        {
            var num = RandNum;

            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Smoke, num, base.owner);

            if (num == 3)
            {
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Paralysis, 1, base.owner);
            }
        }
    }
}
