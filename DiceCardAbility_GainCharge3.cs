
namespace WarpClassFive_Card
{
    public class DiceCardAbility_GainCharge3 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 3, base.owner);
        }
    }
}
