using System.Collections.Generic;

public class BattleUnitBuf_TheLoseMatch : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheLoseMatch";
        }
    }

    public override void OnSuccessAttack(BattleDiceBehavior behavior)
    {
        if (!this._usedCards.Contains(behavior.card))
        {
            this._usedCards.Add(behavior.card);

            if (behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this._totalHitTarget++;
            }
        }

        int count = behavior.owner.view.speedDiceSetterUI.SpeedDicesCount;
        int usedSpeedDices = 0;

        for (int i = 0; count > i; i++)
        {
            if (behavior.owner.view.speedDiceSetterUI.GetSpeedDiceByIndex(i) != null)
            {
                usedSpeedDices++;
            }
        }

        if (usedSpeedDices == this._usedCards.Count)
        {
            this.IsPassed = true;

            if (this._totalHitTarget >= 4)
            {
                this.IsPassedByTarget = true;
            }
        }
    }

    public override void OnLoseParrying(BattleDiceBehavior behavior)
    {
        if (behavior.card.slotOrder != 0)
        {
            return;
        }

        this._isLosed = true;

        if (this._isLosed)
        {
            this.IsPassed = true;

            if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>() || behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = true;
            }
        }
    }

    private bool _isLosed = false;

    private int _totalHitTarget = 0;

    private List<BattlePlayingCardDataInUnitModel> _usedCards = new List<BattlePlayingCardDataInUnitModel>();
}
