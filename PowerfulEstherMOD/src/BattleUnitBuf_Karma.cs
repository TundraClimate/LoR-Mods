using System;
using LOR_DiceSystem;

public class BattleUnitBuf_Karma : BattleUnitBuf
{
    protected override string keywordId
    {
        get
        {
            return "Karma";
        }
    }

    public BattleUnitBuf_Karma()
    {
        base.stack = 1;
    }

    public BattleUnitBuf_Karma(int stack)
    {
        base.stack = stack;
    }

    public override AtkResist GetResistHP(AtkResist origin, BehaviourDetail detail)
    {
        if (base.stack >= 5)
        {
            AtkResist res = base.GetResistHP(origin, detail);

            res = this.GetResistUpOne(res);

            if (base.stack >= 10)
            {
                res = this.GetResistUpOne(res);
            }

            return res;
        }

        return base.GetResistHP(origin, detail);
    }

    private AtkResist GetResistUpOne(AtkResist res)
    {
        switch (res)
        {
            case AtkResist.None:
                return AtkResist.None;
            case AtkResist.Vulnerable:
                return AtkResist.Weak;
            case AtkResist.Normal:
                return AtkResist.Vulnerable;
            case AtkResist.Endure:
                return AtkResist.Normal;
            case AtkResist.Resist:
                return AtkResist.Endure;
            case AtkResist.Immune:
                return AtkResist.Resist;
            default:
                return AtkResist.None;
        }
    }

    public void AddStack(int stack = 1)
    {
        base.stack = Math.Min(base.stack + stack, 10);
    }
}
