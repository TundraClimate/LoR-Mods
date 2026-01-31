using System.Collections.Generic;
using LOR_DiceSystem;

public class DiceCardSelfAbility_TheWinAndLoseSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[戦闘開始時] このページがターゲットしている相手に、反撃ダイス2つ(回避、(1~1)と防御、(6~6))を追加する。";

    public override void OnStartBattle()
    {
        BattleUnitModel target = base.card.target;
        BattleKeepedCardDataInUnitModel keepCard = target.cardSlotDetail.keepCard;

        if (target == null)
        {
            return;
        }

        if (keepCard == null)
        {
            keepCard = new BattleKeepedCardDataInUnitModel(base.owner);
        }

        List<DiceBehaviour> dbehs = base.card.GetDiceBehaviourXmlList();

        if (dbehs.Count == 0)
        {
            return;
        }

        DiceBehaviour evadeDice = dbehs[0].Copy();
        DiceBehaviour guardDice = dbehs[0].Copy();

        evadeDice.Type = BehaviourType.Standby;
        evadeDice.Detail = BehaviourDetail.Evasion;
        evadeDice.Min = 1;
        evadeDice.Dice = 1;
        evadeDice.Script = "";
        evadeDice.MotionDetail = MotionDetail.E;

        guardDice.Type = BehaviourType.Standby;
        guardDice.Detail = BehaviourDetail.Guard;
        guardDice.Min = 6;
        guardDice.Dice = 6;
        guardDice.Script = "";
        guardDice.MotionDetail = MotionDetail.G;

        keepCard.AddDice(new BattleDiceBehavior()
        {
            behaviourInCard = evadeDice,
        });
        keepCard.AddDice(new BattleDiceBehavior()
        {
            behaviourInCard = guardDice,
        });
    }
}
