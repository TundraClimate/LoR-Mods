using System.Collections.Generic;
using LOR_DiceSystem;

public class PrescriptBuf : BattleUnitBuf
{
    public bool IsPassed
    {
        get
        {
            return _isPassed;
        }
        set
        {
            if (!_isPassed && value)
            {
                UnityEngine.Debug.Log("Prescript was passed");
            }
            else if (_isPassed && !value)
            {
                UnityEngine.Debug.Log("Prescript was failed");
            }

            _isPassed = value;
        }
    }

    public bool IsPassedByTarget
    {
        get
        {
            return _isPassedByTarget;
        }
        set
        {
            if (!_isPassedByTarget && value)
            {
                UnityEngine.Debug.Log("Prescript was passed by target");
            }
            else if (_isPassedByTarget && !value)
            {
                UnityEngine.Debug.Log("Prescript was failed by target");
            }

            _isPassedByTarget = value;
        }
    }

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

    private bool _isPassed = false;

    private bool _isPassedByTarget = false;

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

    public virtual BattleUnitModel FixedIndexTarget(List<BattleUnitModel> candidates, BattleUnitModel origin)
    {
        return origin;
    }

    public bool SetIndexMarkForAtkDice(BattleDiceCardModel model)
    {
        return ItemXmlDataList.instance.GetCardItem(model.GetID()).DiceBehaviourList.Exists(dice =>
                dice.Type == BehaviourType.Atk
            );
    }

    public bool SelectAtkDiceNeeds(PassiveAbilityBase self)
    {
        List<BattleDiceCardModel> hands = self.Owner.allyCardDetail.GetHand();

        bool hasAtkDice = hands.Exists(hand =>
                ItemXmlDataList.instance.GetCardItem(hand.GetID()).DiceBehaviourList.Exists(dice =>
                    dice.Type == BehaviourType.Atk
                )
            );

        return hasAtkDice;
    }
}
