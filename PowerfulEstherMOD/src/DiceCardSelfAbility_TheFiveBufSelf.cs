using System.Collections.Generic;
using LOR_DiceSystem;

public class DiceCardSelfAbility_TheFiveBufSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[一方攻撃開始時] このページのダイスは的中時にマッチ勝利時に付与する出血の値だけ束縛、麻痺、武装解除を付与する。";

    public override void OnStartOneSideAction()
    {
        if (base.owner.currentDiceAction != null)
        {
            List<BattleDiceBehavior> dices = base.owner.currentDiceAction.GetDiceBehaviorList().FindAll(dice => dice.Type != BehaviourType.Standby);

            dices[0].AddAbility(new DiceCardAbility_TheFiveBufAnotherD1());
        }
    }

    private class DiceCardAbility_TheFiveBufAnotherD1 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            if (base.behavior.card.target != null)
            {
                base.behavior.card.target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 3, base.owner);
                base.behavior.card.target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 3, base.owner);
                base.behavior.card.target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Disarm, 3, base.owner);
            }
        }
    }
}
