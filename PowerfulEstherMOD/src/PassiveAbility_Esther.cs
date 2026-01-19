using System.Collections.Generic;

public class PassiveAbility_Esther : PassiveAbilityBase
{
    public override void OnWaveStart()
    {
        this.CleanupDeck();

        this.owner.bufListDetail.AddBuf(new BattleUnitBuf_GraceOfPrescript());
    }

    public override void OnRoundStart()
    {
        this.CleanupHands();

        this.AddCard(1);
        this.AddCard(1);
        this.AddCard(1);
        this.AddCard(1);
        this.AddCard(1);

        BattleUnitBuf_GraceOfPrescript gps = (BattleUnitBuf_GraceOfPrescript)base.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_GraceOfPrescript);

        if (gps == null)
        {
            UnityEngine.Debug.LogError("BattleUnitBuf_GraceOfPrescript is missing");

            return;
        }

        gps.AddStack(1);
    }

    public override int SpeedDiceNumAdder()
    {
        return 4;
    }

    private void CleanupDeck()
    {
        List<BattleDiceCardModel> deck = base.owner.allyCardDetail.GetAllDeck();

        deck.Clear();
    }

    private void CleanupHands()
    {
        List<BattleDiceCardModel> hands = base.owner.allyCardDetail.GetHand();

        hands.Clear();
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
