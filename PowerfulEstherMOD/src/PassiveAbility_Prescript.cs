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

        if (base.owner.breakDetail.IsBreakLifeZero())
        {
            return;
        }

        BattleUnitBuf grace = base.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_GraceOfPrescript);

        if (grace == null)
        {
            grace = new BattleUnitBuf_GraceOfPrescript();

            base.owner.bufListDetail.AddBuf(grace);
        }

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

    public override void OnRoundStartAfter()
    {
        List<BattleDiceCardModel> hands = base.owner.allyCardDetail.GetHand();

        this.AddIndexMarks(hands);
    }

    public override void OnRoundEnd()
    {
        if (base.owner == null)
        {
            return;
        }

        if (base.owner.breakDetail.IsBreakLifeZero())
        {
            return;
        }

        BattleUnitBuf_GraceOfPrescript grace = (BattleUnitBuf_GraceOfPrescript)base.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_GraceOfPrescript);

        if (grace == null)
        {
            UnityEngine.Debug.LogWarning("GraceOfPrescript cannot obtain");

            return;
        }

        if (this._prescript.IsPassedByTarget)
        {
            grace.AddStack(3);
        }
        else if (this._prescript.IsPassed)
        {
            grace.AddStack(1);
        }
        else if (grace.stack != 9)
        {
            BattleUnitBuf_Karma karma = (BattleUnitBuf_Karma)base.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_Karma);

            if (karma == null)
            {
                base.owner.bufListDetail.AddBuf(new BattleUnitBuf_Karma());
            }
            else
            {
                karma.AddStack(1);
            }
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

        bool hasPrioritizeCard = !cards.TrueForAll((BattleDiceCardModel card) => !this._prioritizeCards.Contains(card.GetID()));

        if (hasPrioritizeCard)
        {
            this.AddIndexMarksBySpecialPrescript(cards);

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

    private void AddIndexMarksBySpecialPrescript(List<BattleDiceCardModel> cards)
    {
        int limit = 2;

        foreach (BattleDiceCardModel card in cards)
        {
            if (this._prioritizeCards.Contains(card.GetID()) && limit > 0)
            {
                limit--;

                card.AddBuf(new BattleDiceCardBuf_IndexMark());
            }
        }

        if (cards.Count - limit <= 0)
        {
            return;
        }

        for (int i = 0; limit != 0; i++)
        {
            if (this._prioritizeCards.Contains(cards[i].GetID()))
            {
                continue;
            }

            cards[i].AddBuf(new BattleDiceCardBuf_IndexMark());

            limit--;
        }
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
            this.SetPrescript(PrescriptBuf.Create(PrescriptBuf.GetOne(new BattleUnitBuf_TheHitMarked(), new BattleUnitBuf_TheUseMarked())));
        }
        else
        {
            this.SetPrescript(PrescriptBuf.Create(new BattleUnitBuf_TheUseMarked()));
        }
    }

    private void SendLv1Prescript()
    {
        this.SetPrescript(PrescriptBuf.Create(PrescriptBuf.GetOne(new BattleUnitBuf_TheOneAttack(), new BattleUnitBuf_TheOverWin())));
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

    private List<LorId> _prioritizeCards = new List<LorId>()
    {
        new LorId(PowerfulEstherMOD.packageId, 11),
    };

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
