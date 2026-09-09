public class DiceCardAbility_ExtremeBoris_BorisKill : UnbreakableDice
{
    public override string[] Keywords => ["ExtremeBoris_UnbreakableDice"];

    private Remainder count = new(2);

    public override void AfterAction()
    {
        if (count.Remains && !base.owner.IsBreakLifeZero())
        {
            ActivateBonusAttackDice();
            count.Lose();
        }
        else
        {
            BehaviourAction_borisAction.movable = true;
        }
    }
}
