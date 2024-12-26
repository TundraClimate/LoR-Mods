
namespace WarpClassFive_Card
{
    public class DiceCardAbility_GainQuick : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Quickness, 2, base.owner);
        }
    }
}
