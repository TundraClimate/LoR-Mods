public class SpecialCardBase : AdvancedCardBase
{
    public override bool OnChooseCard(BattleUnitModel owner)
    {
        OnClick(owner);

        return false;
    }

    public virtual void OnClick(BattleUnitModel owner)
    {
    }
}
