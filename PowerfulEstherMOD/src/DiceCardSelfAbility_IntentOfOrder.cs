public class DiceCardSelfAbility_IntentOfOrder : DiceCardSelfAbilityBase
{
    public static string Desc = "[使用時] 「唯一」状態なら、光を2回復しページを2枚引く。";

    public override void OnUseCard()
    {
        if (base.owner == null)
        {
            return;
        }

        if (base.owner.allyCardDetail.IsHighlander())
        {
            base.owner.cardSlotDetail.RecoverPlayPointByCard(2);
            base.owner.allyCardDetail.DrawCards(2);
        }
    }
}
