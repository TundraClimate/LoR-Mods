using System.Collections.Generic;
using LOR_DiceSystem;

public class PassiveAbility_Prescript : PassiveAbilityBase
{
    public override void OnWaveStart()
    {
        if (base.owner != null || !base.owner.bufListDetail.HasBuf<BattleUnitBuf_GraceOfPrescript>())
        {
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_GraceOfPrescript());
        }
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

        if (this._prescript != null)
        {
            if (this._prescript.IsPassedByTarget)
            {
                grace.AddStack(3);
            }
            else if (this._prescript.IsPassed)
            {
                grace.AddStack(1);
            }

        }

        List<BattleDiceCardModel> hands = base.owner.allyCardDetail.GetHand();

        this.AddIndexMarks(hands);

        if (3 > grace.stack && grace.stack >= 0)
        {
            this.SendLv0Prescript();
        }
        else if (6 > grace.stack && grace.stack >= 3)
        {
            this.SendLv1Prescript();
        }
        else if (9 > grace.stack && grace.stack >= 6)
        {
            this.SendLv2Prescript();
        }
        else
        {
            this.SendLv3Prescript();
        }
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

    private void SetPrescript(PrescriptBuf prescript)
    {
        this._prescript = prescript;

        if (base.owner == null)
        {
            return;
        }

        base.owner.bufListDetail.AddBuf(prescript);
    }

    private void SendLv0Prescript()
    {
        List<BattleDiceCardModel> hands = base.owner.allyCardDetail.GetHand();

        bool hasAtkDice = !hands.TrueForAll((BattleDiceCardModel hand) =>
                ItemXmlDataList.instance.GetCardItem(hand.GetID()).DiceBehaviourList.TrueForAll((DiceBehaviour dice) =>
                    dice.Type == BehaviourType.Def || dice.Type == BehaviourType.Standby
                )
            );

        if (hands.Count != 0 || hasAtkDice)
        {
            this.SetPrescript(PrescriptBuf.GetOne(new BattleUnitBuf_TheHitMarked(), new BattleUnitBuf_TheUseMarked()));
        }
        else
        {
            this.SetPrescript(new BattleUnitBuf_TheUseMarked());
        }
    }

    private void SendLv1Prescript()
    {
        this.SetPrescript(PrescriptBuf.Create(new BattleUnitBuf_TheTerminateAll()));
    }

    private void SendLv2Prescript()
    {
        this.SetPrescript(PrescriptBuf.Create(new BattleUnitBuf_TheTerminateAll()));
    }

    private void SendLv3Prescript()
    {
        this.SetPrescript(PrescriptBuf.Create(new BattleUnitBuf_TheTerminateAll()));
    }

    private PrescriptBuf _prescript;

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
