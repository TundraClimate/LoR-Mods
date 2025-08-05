
namespace WarpClassFive_Card
{
    public class DiceCardAbility_Gain2Light : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.owner.cardSlotDetail.RecoverPlayPoint(2);
        }

        public static string Desc = "[的中] 光を2回復";
    }
}
