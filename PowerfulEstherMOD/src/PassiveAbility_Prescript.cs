using System;
using System.Collections.Generic;

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

        BattleUnitBuf grace = base.owner.bufListDetail.GetActivatedBufList().Find(buf => buf is BattleUnitBuf_GraceOfPrescript);

        if (grace == null)
        {
            grace = new BattleUnitBuf_GraceOfPrescript();

            base.owner.bufListDetail.AddBuf(grace);
        }

        if (false)
        {
            // TODO Only debug

            this.SetPrescript(PrescriptBuf.Create(new BattleUnitBuf_TheTerminateAll()));

            return;
        }

        List<SpecialPrescriptBuf> specialInstances = new List<SpecialPrescriptBuf>()
        {
        };

        SpecialPrescriptBuf findOne = specialInstances.Find(sp => sp.ShouldSendScript());

        if (findOne != null)
        {
            this.SendSpecialPrescript(findOne);

            return;
        }

        if (3 > grace.stack && grace.stack >= 0)
        {
            this.SendPrescript(0);
        }
        else if (6 > grace.stack && grace.stack >= 3)
        {
            this.SendPrescript(1);
        }
        else if (9 > grace.stack && grace.stack >= 6)
        {
            this.SendPrescript(2);
        }
        else
        {
            this.SendPrescript(3);
        }
    }

    public override void OnRoundStartAfter()
    {
        List<BattleDiceCardModel> hands = base.owner.allyCardDetail.GetHand();

        BattleUnitBuf prescript = base.owner.bufListDetail.GetActivatedBufList().Find(buf => buf is PrescriptBuf);

        if (prescript != null)
        {
            this.AddIndexMark(hands, ((PrescriptBuf)prescript).IsIndexMarkNeeds);
        }
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

        BattleUnitBuf_GraceOfPrescript grace = (BattleUnitBuf_GraceOfPrescript)base.owner.bufListDetail.GetActivatedBufList().Find(buf => buf is BattleUnitBuf_GraceOfPrescript);

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
            BattleUnitBuf_Karma karma = (BattleUnitBuf_Karma)base.owner.bufListDetail.GetActivatedBufList().Find(buf => buf is BattleUnitBuf_Karma);

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

    private void AddIndexMark(List<BattleDiceCardModel> cards, Func<BattleDiceCardModel, bool> priority, int limit = 2)
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

        List<BattleDiceCardModel> compatCards = new List<BattleDiceCardModel>();
        List<BattleDiceCardModel> otherCards = new List<BattleDiceCardModel>();

        foreach (BattleDiceCardModel card in cards)
        {
            if (card == null)
            {
                continue;
            }

            if (priority.Invoke(card))
            {
                compatCards.Add(card);
            }
            else
            {
                otherCards.Add(card);
            }
        }

        if (compatCards.Count < limit)
        {
            foreach (BattleDiceCardModel card in compatCards)
            {
                card.AddBuf(new BattleDiceCardBuf_IndexMark());
            }

            this.AddIndexMark(otherCards, _ => true, limit: limit - compatCards.Count);

            return;
        }

        int rangeMin = 0;
        int rangeMax = compatCards.Count - 1;

        int rand1 = RandomUtil.Range(rangeMin, rangeMax);
        int rand2 = RandomUtil.Range(rangeMin, rangeMax);

        if (rand1 == rand2)
        {
            if (rand1 != rangeMax)
            {
                rand2 += 1;
            }
            else
            {
                rand2 -= 1;
            }
        }

        if (limit == 1)
        {
            compatCards[rand1].AddBuf(new BattleDiceCardBuf_IndexMark());

            return;
        }
        else
        {
            compatCards[rand1].AddBuf(new BattleDiceCardBuf_IndexMark());
            compatCards[rand2].AddBuf(new BattleDiceCardBuf_IndexMark());
        }

    }

    private void SendPrescript(int level)
    {
        List<PrescriptBuf> ablePrescripts;

        switch (level)
        {
            case 0:
                ablePrescripts = new List<PrescriptBuf>
                {
                    new BattleUnitBuf_TheHitMarked(),
                    new BattleUnitBuf_TheUseMarked()
                };

                break;
            case 1:
                ablePrescripts = new List<PrescriptBuf>
                {
                    new BattleUnitBuf_TheOneAttack(),
                    new BattleUnitBuf_TheOverWinMatch(),
                    new BattleUnitBuf_TheThree(),
                    new BattleUnitBuf_TheCounter(),
                    new BattleUnitBuf_ThePenetrate(),
                    new BattleUnitBuf_TheSlash(),
                    new BattleUnitBuf_TheBleed(),
                };

                break;
            case 2:
                ablePrescripts = new List<PrescriptBuf>
                {
                    new BattleUnitBuf_TheKillOrDamage(),
                    new BattleUnitBuf_TheBreakOrKill(),
                    new BattleUnitBuf_TheOneSpeedDice(),
                    new BattleUnitBuf_TheWinAndLose(),
                    new BattleUnitBuf_TheLoseMatch(),
                    new BattleUnitBuf_TheFiveBuf(),
                    new BattleUnitBuf_TheWinByHigh(),
                    new BattleUnitBuf_TheNormalResist(),
                    new BattleUnitBuf_TheHealBreakLife(),
                    new BattleUnitBuf_TheRollAllRange(),
                };

                break;
            case 3:
                ablePrescripts = new List<PrescriptBuf>
                {
                    new BattleUnitBuf_TheTerminateAll(),
                };

                break;
            default:
                UnityEngine.Debug.LogError("A level the " + level + " is not valid");

                return;
        }

        if (!base.owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            ablePrescripts.RemoveAll(prescript => !prescript.IsSelectable(this));
        }

        if (ablePrescripts.Count == 0)
        {
            UnityEngine.Debug.LogWarning("Prescript was not sent");

            return;
        }

        this.SetPrescript(PrescriptBuf.Create(PrescriptBuf.GetOne(ablePrescripts.ToArray())));
    }

    private void SendSpecialPrescript(SpecialPrescriptBuf prescriptBuf)
    {
        this.SetPrescript(PrescriptBuf.Create(prescriptBuf));
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

        public override void OnRoundEnd()
        {
            base.Destroy();
        }
    }

    public class DiceCardAbility_Marker : DiceCardAbilityBase
    {
    }
}
