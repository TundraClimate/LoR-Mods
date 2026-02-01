using System;

public class DiceCardSelfAbility_TheWinByHighSelf : DiceCardSelfAbilityBase
{
    public static string Desc = @"このページのコストは変わらない
[マッチ開始時] マッチ相手の使用中のページとこのページのコストの差だけダメージ量とダイス威力が増加。";

    public override bool IsFixedCost()
    {
        return true;
    }

    public override void OnStartParrying()
    {
        if (base.owner != null && base.card != null && base.card.target != null && base.card.target.currentDiceAction != null)
        {
            int selfCost = base.card.card.GetCost();
            int otherCost = base.card.target.currentDiceAction.card.GetCost();

            int plusNum = Math.Abs(selfCost - otherCost);

            base.card.ApplyDiceStatBonus(_ => true, new DiceStatBonus()
            {
                power = plusNum,
                dmg = plusNum,
            });
        }
    }
}
