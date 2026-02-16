using System;

public class AmmoBuf : BattleUnitBuf
{
    public AmmoBuf(string keywordId, BattleUnitBuf effect)
    {
        this._keywordId = keywordId;
        this._bulletBuf = effect;
    }

    public static void ApplyOrAdd(BattleUnitModel unit, string keywordId, BattleUnitBuf effect, int stack = 1)
    {
        if (unit == null || stack == 0)
        {
            return;
        }

        AmmoBuf ammo = (AmmoBuf)unit.bufListDetail.GetActivatedBufList().Find(buf => buf is AmmoBuf && ((AmmoBuf)buf)._bulletBuf.GetType() == effect.GetType());

        if (ammo == null)
        {
            AmmoBuf ammoBuf = new AmmoBuf(keywordId, effect);

            ammoBuf.stack = stack;

            unit.bufListDetail.AddBuf(ammoBuf);
        }
        else
        {
            ammo.stack += stack;
        }
    }

    protected override string keywordId
    {
        get
        {
            return this._keywordId;
        }
    }

    public void UseStack(int stack)
    {
        int prev = this.stack;

        this.stack = Math.Max(this.stack - stack, 0);

        if (base._owner == null)
        {
            return;
        }

        int used = prev - this.stack;

        for (int i = 0; used > i; i++)
        {
            base._owner.bufListDetail.AddBuf(this._bulletBuf);
            base._owner.passiveDetail.OnExhaustBullet();
        }
    }

    public override void OnRoundEnd()
    {
        if (this.stack <= 0)
        {
            base.Destroy();
        }
    }

    private string _keywordId;

    private BattleUnitBuf _bulletBuf;
}
