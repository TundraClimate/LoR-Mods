public class DiceCardSelfAbility_UltraSandBagMOD_LightFill : SpecialCardBase
{
    public override void OnClick(BattleUnitModel owner)
    {
        if (owner is null || owner.PlayPoint >= owner.MaxPlayPoint)
        {
            return;
        }

        owner.cardSlotDetail.RecoverPlayPointByCard(owner.MaxPlayPoint - owner.PlayPoint);
    }
}
