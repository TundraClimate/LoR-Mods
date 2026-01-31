using System.Collections.Generic;
using LOR_DiceSystem;

public class DiceCardSelfAbility_TheLoseMatchSelf : DiceCardSelfAbilityBase
{
    public static string Desc = @"このページは的中時に混乱ダメージを18与える。
[戦闘開始時] 指令対象ではない全ての敵に反撃ダイス(回避、(3~3))を追加する。";

    public override void OnStartBattle()
    {
        List<DiceBehaviour> dbehs = base.card.GetDiceBehaviourXmlList();

        if (dbehs.Count == 0)
        {
            return;
        }

        DiceBehaviour evadeDice = dbehs[0].Copy();

        evadeDice.Type = BehaviourType.Standby;
        evadeDice.Detail = BehaviourDetail.Evasion;
        evadeDice.Min = 3;
        evadeDice.Dice = 3;
        evadeDice.Script = "";
        evadeDice.MotionDetail = MotionDetail.E;

        if (base.owner == null)
        {
            return;
        }

        List<BattleUnitModel> targets = BattleObjectManager.instance.GetAliveList(base.owner.faction == Faction.Player ? Faction.Enemy : Faction.Player).FindAll(unit => unit != base.card.target);

        foreach (BattleUnitModel target in targets)
        {
            BattleKeepedCardDataInUnitModel keepCard = target.cardSlotDetail.keepCard;

            if (target == null)
            {
                return;
            }

            if (keepCard == null)
            {
                keepCard = new BattleKeepedCardDataInUnitModel(base.owner);
            }

            keepCard.AddBehaviour(base.card.card, new BattleDiceBehavior()
            {
                behaviourInCard = evadeDice,
            });
        }
    }

    public override void OnSucceedAttack()
    {
        if (base.owner.currentDiceAction.target != null)
        {
            base.owner.currentDiceAction.target.TakeBreakDamage(18, DamageType.Card_Ability, base.owner);
        }
    }
}
