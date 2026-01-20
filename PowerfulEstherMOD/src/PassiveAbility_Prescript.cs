using System.Collections.Generic;

public class PassiveAbility_Prescript : PassiveAbilityBase
{
    public override void OnWaveStart()
    {
        if (base.owner != null || !base.owner.bufListDetail.HasBuf<BattleUnitBuf_GraceOfPrescript>())
        {
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_GraceOfPrescript());
        }

        this._isPassedByTarget = false;
        this._isPrescriptPassed = true;
    }

    public override void OnRoundStart()
    {
        if (base.owner == null)
        {
            return;
        }

        BattleUnitBufListDetail bufList = base.owner.bufListDetail;

        if (!bufList.HasBuf<BattleUnitBuf_GraceOfPrescript>())
        {
            bufList.AddBuf(new BattleUnitBuf_GraceOfPrescript());
        }

        BattleUnitBuf_GraceOfPrescript grace = (BattleUnitBuf_GraceOfPrescript)bufList.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_GraceOfPrescript);

        if (this._isPrescriptPassed && this._isPassedByTarget)
        {
            grace.AddStack(3);
        }
        else if (this._isPrescriptPassed)
        {
            grace.AddStack(1);
        }

        this._isPassedByTarget = false;
        this._isPrescriptPassed = true;

        List<BattleDiceCardModel> hands = base.owner.allyCardDetail.GetHand();

        this.AddIndexMarks(hands);
    }

    private void AddIndexMarks(List<BattleDiceCardModel> cards)
    {
        if (cards.Count == 0)
        {
            return;
        }

        if (cards.Count == 1)
        {
            cards[0].AddBuf(new BattleDiceCardBuf_IndexMark());

            return;
        }

        int rangeMin = 0;
        int rangeMax = cards.Count - 1;

        int rand1 = RandomUtil.Range(rangeMin, rangeMax);
        int rand2 = RandomUtil.Range(rangeMin, rangeMax);

        if (rand1 == rand2)
        {
            if (rand1 == 0)
            {
                rand2 = 1;
            }
            else
            {
                rand2 = 0;
            }
        }

        cards[rand1].AddBuf(new BattleDiceCardBuf_IndexMark());
        cards[rand2].AddBuf(new BattleDiceCardBuf_IndexMark());
    }

    private bool _isPrescriptPassed;

    private bool _isPassedByTarget;

    public class BattleDiceCardBuf_IndexMark : BattleDiceCardBuf
    {
        protected override string keywordId
        {
            get
            {
                return "IndexMark";
            }
        }

        protected override string keywordIconId
        {
            get
            {
                return this.keywordId;
            }
        }
    }
}
