public class PassiveAbility_Esther : PassiveAbilityBase
{
    public override void OnWaveStart()
    {
        base.owner.allyCardDetail.GetAllDeck().Clear();
    }

    public override void OnRoundStart()
    {
        base.owner.allyCardDetail.GetHand().Clear();
    }

    public override void OnRoundStartAfter()
    {
        BattleUnitBuf grace = base.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_GraceOfPrescript);

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
            this.UseLv1Pattern();
        }
        else if (9 > grace.stack && grace.stack >= 6)
        {
            this.UseLv2Pattern();
        }
        else
        {
            this.UseLv3Pattern();
        }
    }

    public override int SpeedDiceNumAdder()
    {
        BattleUnitBuf grace = base.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_GraceOfPrescript);

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
        BattleDiceCardModel card = base.owner.allyCardDetail.AddTempCard(id);

        if (card == null)
        {
            return;
        }

        card.SetCostToZero();
        card.SetPriorityAdder(priority);
    }

    private void UseLv0Pattern()
    {
    }

    private void UseLv1Pattern()
    {
    }

    private void UseLv2Pattern()
    {
    }

    private void UseLv3Pattern()
    {
    }
}
