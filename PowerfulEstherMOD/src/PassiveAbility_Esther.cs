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
            if (base.owner.emotionDetail.EmotionLevel > 3)
            {
                return 2;
            }

            return 3;
        }
        else
        {
            if (base.owner.emotionDetail.EmotionLevel > 3)
            {
                return 3;
            }

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
        BattleUnitBuf prescript = base.owner.bufListDetail.GetActivatedBufList().Find(buf => buf is PrescriptBuf);

        if (prescript is BattleUnitBuf_TheOneAttack)
        {
            this.AddCard(11, 9999);
            this.AddCard(11, 9999);
        }

        if (prescript is BattleUnitBuf_TheOverWinMatch)
        {
            this.AddCard(12, 9999);
            this.AddCard(12, 9999);
        }

        if (prescript is BattleUnitBuf_TheThree)
        {
            this.AddCard(13, 9999);
            this.AddCard(13, 9999);
        }

        if (prescript is BattleUnitBuf_TheCounter)
        {
            this.AddCard(14, 9999);
            this.AddCard(14, 9999);
        }

        if (prescript is BattleUnitBuf_ThePenetrate)
        {
            this.AddCard(15, 9999);
            this.AddCard(15, 9999);
        }

        if (prescript is BattleUnitBuf_TheSlash)
        {
            this.AddCard(16, 9999);
            this.AddCard(16, 9999);
        }

        if (prescript is BattleUnitBuf_TheBleed)
        {
            this.AddCard(17, 9999);
            this.AddCard(17, 9999);
        }

        switch (this._elapsedTurn % 3)
        {
            case 0:
                this.AddCard(new LorId(605004), 99);
                this.AddCard(new LorId(605006), 9);

                break;
            case 1:
                this.AddCard(5, 99);
                this.AddCard(new LorId(605006), 9);

                break;
            case 2:
                this.AddCard(new LorId(605004), 99);
                this.AddCard(2, 9);

                break;
        }

        if (base.owner.emotionDetail.EmotionLevel > 3 && this._elapsedTurn % 3 != 2)
        {
            this.AddCard(2, 0);
        }
    }

    private void UseLv2Pattern()
    {
        this.AddCard(28, 9999);

        switch (this._elapsedTurn % 6)
        {
            case 0:
                this.AddCard(9, 999);
                this.AddCard(8, 99);
                this.AddCard(4, 9);

                break;
            case 1:
                this.AddCard(6, 999);
                this.AddCard(7, 99);
                this.AddCard(4, 9);

                break;
            case 2:
                this.AddCard(9, 999);
                this.AddCard(8, 99);
                this.AddCard(4, 9);

                break;
            case 3:
                this.AddCard(6, 999);
                this.AddCard(7, 99);
                this.AddCard(4, 9);

                break;
            case 4:
                this.AddCard(9, 999);
                this.AddCard(8, 99);
                this.AddCard(4, 9);

                break;
            case 5:
                this.AddCard(6, 999);
                this.AddCard(4, 99);
                this.AddCard(3, 9);

                break;
        }
    }

    private void UseLv3Pattern()
    {
    }

    private int _elapsedTurn = 0;

    private bool _otf_1 = true;

    private bool _otf_2 = true;

    private bool _otf_3 = true;
}
