
namespace WarpClassFive_Card
{
    public class DiceCardAbility_Gain2Light : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.owner.cardSlotDetail.RecoverPlayPoint(2);
        }
    }
}
