public class DiceCardAbility_TheHealBreakLifeD3 : DiceCardAbilityBase
{
    public static string Desc = @"<color=green>連結ダイス-断片-a</color>
このダイスは連結効果以外で消滅しない。";

    public override bool IsImmuneDestory
    {
        get
        {
            return true;
        }
    }

    public override void OnLoseParrying()
    {
        this.AfterParrying();
    }

    public override void OnDrawParrying()
    {
        this.AfterParrying();
    }

    public void AfterParrying()
    {
        if (base.behavior == null)
        {
            return;
        }

        BattleKeepedCardDataInUnitModel keepCard = base.owner.cardSlotDetail.keepCard;

        if (keepCard == null)
        {
            keepCard = new BattleKeepedCardDataInUnitModel(base.owner);
        }

        keepCard.AddDiceFront(base.behavior);
    }
}
