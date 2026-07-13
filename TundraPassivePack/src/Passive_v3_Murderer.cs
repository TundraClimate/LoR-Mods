public class PassiveAbility_TundraPassivePack_Murderer : AdvancedPassiveBase
{
    public override int DrawCardAddr => _count / 2;

    public override void OnRoundStart()
    {
        base.owner.cardSlotDetail?.RecoverPlayPoint(_count / 4);
    }

    public override int MaxPlayPointAdder()
    {
        return _count / 4;
    }

    public override bool TeamKill()
    {
        return true;
    }

    public override int SpeedDiceNumAdder()
    {
        return _count;
    }

    public override void OnKill(BattleUnitModel target)
    {
        if (target.faction != base.owner.faction)
        {
            return;
        }

        _count += 1;
    }

    private int _count;
}
