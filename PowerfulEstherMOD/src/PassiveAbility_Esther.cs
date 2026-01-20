public class PassiveAbility_Esther : PassiveAbilityBase
{
    public override void OnWaveStart()
    {
        base.owner.allyCardDetail.GetAllDeck().Clear();
    }

    public override void OnRoundStart()
    {
        base.owner.allyCardDetail.GetHand().Clear();

        this.AddCard(1);
    }

    public override int SpeedDiceNumAdder()
    {
        return 4;
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
}
