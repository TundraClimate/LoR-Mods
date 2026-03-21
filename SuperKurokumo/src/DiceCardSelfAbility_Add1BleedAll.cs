public class DiceCardSelfAbility_Add1BleedAll : AdvancedCardBase
{
    public static string Desc = "[戦闘開始時] 全ての味方のページによる出血付与量+1";

    public override void OnStartBattle()
    {
        var alives = BattleObjectManager.instance.GetAliveList(base.owner.faction);

        alives.ForEach(unit => unit?.GetBufAndInitIfNull<AddBleed1>(() => new AddBleed1()));
    }

    private class AddBleed1 : AdvancedUnitBuf
    {
        public override int OnGiveKeywordBufByCard(BattleUnitBuf cardBuf, int stack, BattleUnitModel target)
        {
            if (cardBuf.bufType == KeywordBuf.Bleeding)
            {
                return 1;
            }
            return 0;
        }

        public override void OnRoundEnd()
        {
            base.Destroy();
        }
    }
}
