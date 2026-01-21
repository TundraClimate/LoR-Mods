using System.Collections.Generic;
using LOR_DiceSystem;

public class PrescriptBuf : BattleUnitBuf
{
    public static PrescriptBuf GetOne(params PrescriptBuf[] prescripts)
    {
        if (prescripts.Length == 0)
        {
            return null;
        }

        if (prescripts.Length == 1)
        {
            return prescripts[0];
        }

        int min = 0;
        int max = prescripts.Length - 1;

        return prescripts[RandomUtil.Range(min, max)];
    }

    public void Init()
    {
        base.stack = 0;
    }

    public static PrescriptBuf Create<T>(T instance)
        where T : PrescriptBuf
    {
        PrescriptBuf prescript = (PrescriptBuf)instance;

        prescript.Init();

        return prescript;
    }

    public bool IsPassed = false;

    public bool IsPassedByTarget = false;

    public override void OnRoundEnd()
    {
        base.Destroy();
    }

    public virtual bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        return true;
    }

    public virtual bool IsSelectable(PassiveAbilityBase self)
    {
        return true;
    }

    public bool SetIndexMarkForAtkDice(BattleDiceCardModel model)
    {
        return !ItemXmlDataList.instance.GetCardItem(model.GetID()).DiceBehaviourList.TrueForAll(dice =>
                dice.Type == BehaviourType.Def || dice.Type == BehaviourType.Standby
            );
    }

    public bool SelectAtkDiceNeeds(PassiveAbilityBase self)
    {
        List<BattleDiceCardModel> hands = self.Owner.allyCardDetail.GetHand();

        bool hasAtkDice = !hands.TrueForAll(hand =>
                ItemXmlDataList.instance.GetCardItem(hand.GetID()).DiceBehaviourList.TrueForAll(dice =>
                    dice.Type == BehaviourType.Def || dice.Type == BehaviourType.Standby
                )
            );

        return hasAtkDice;
    }
}
