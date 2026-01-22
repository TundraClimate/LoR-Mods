public class PassiveAbility_Esther : PassiveAbilityBase
{
    public override void OnRoundStartAfter()
    {
        if (base.owner == null)
        {
            return;
        }

        base.owner.allyCardDetail.ExhaustAllCards();
        this.ApplyPattern();
    }

    public override int SpeedDiceNumAdder()
    {
        BattleUnitBuf grace = base.owner.bufListDetail.GetActivatedBufList().Find(buf => buf is BattleUnitBuf_GraceOfPrescript);

        if (grace == null)
        {
            UnityEngine.Debug.LogWarning("GraceOfPrescript not found");

            return 0;
        }

        if (3 > grace.stack && grace.stack >= 0)
        {
            return 2;
        }
        else if (9 > grace.stack && grace.stack >= 3)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    private void AddCard(int id, int priority = 0)
    {
        this.AddCard(new LorId(PowerfulEstherMOD.packageId, id), priority);
    }

    private void AddCard(LorId id, int priority = 0)
    {
        BattleDiceCardModel card = base.owner.allyCardDetail.AddNewCard(id);

        if (card == null)
        {
            return;
        }

        card.SetCostToZero();
        card.SetPriorityAdder(priority);
    }

    private void ApplyPattern()
    {
        if (base.owner.breakDetail.IsBreakLifeZero())
        {
            return;
        }

        BattleUnitBuf grace = base.owner.bufListDetail.GetActivatedBufList().Find(buf => buf is BattleUnitBuf_GraceOfPrescript);

        if (grace == null)
        {
            UnityEngine.Debug.LogWarning("GraceOfPrescript not found");

            return;
        }

        if (3 > grace.stack && grace.stack >= 0)
        {
            this.UseLv0Pattern();
        }
        else if (6 > grace.stack && grace.stack >= 3)
        {
            if (this._otf_1)
            {
                this._otf_1 = false;
                this._elapsedTurn = 0;
            }

            this.UseLv1Pattern();
        }
        else if (9 > grace.stack && grace.stack >= 6)
        {
            if (this._otf_2)
            {
                this._otf_2 = false;
                this._elapsedTurn = 0;
            }

            this.UseLv2Pattern();
        }
        else
        {
            if (this._otf_3)
            {
                this._otf_3 = false;
                this._elapsedTurn = 0;
            }

            this.UseLv3Pattern();
        }

        this._elapsedTurn++;
    }

    private void UseLv0Pattern()
    {
        switch (this._elapsedTurn % 3)
        {
            case 0:
                this.AddCard(new LorId(605004), 999);
                this.AddCard(new LorId(605006), 99);
                this.AddCard(4, 9);

                if (base.owner.emotionDetail.EmotionLevel > 3)
                {
                    this.AddCard(1, 0);
                }

                break;
            case 1:
                this.AddCard(new LorId(605004), 999);
                this.AddCard(5, 99);
                this.AddCard(new LorId(605006), 9);

                if (base.owner.emotionDetail.EmotionLevel > 3)
                {
                    this.AddCard(1, 0);
                }

                break;
            case 2:
                this.AddCard(new LorId(605006), 999);
                this.AddCard(new LorId(605007), 99);
                this.AddCard(1, 9);

                break;
        }
    }

    private void UseLv1Pattern()
    {
        this.AddCard(13, 999);
    }

    private void UseLv2Pattern()
    {
    }

    private void UseLv3Pattern()
    {
    }

    private int _elapsedTurn = 0;

    private bool _otf_1 = true;

    private bool _otf_2 = true;

    private bool _otf_3 = true;
}
