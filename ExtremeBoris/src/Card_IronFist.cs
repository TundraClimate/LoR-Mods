public class DiceCardSelfAbility_ExtremeBoris_IronFist : AdvancedCardBase
{
    public override void OnUseCard()
    {
        base.owner.allyCardDetail.DrawCards(1);
        base.owner.cardSlotDetail.RecoverPlayPoint(1);
    }
}
