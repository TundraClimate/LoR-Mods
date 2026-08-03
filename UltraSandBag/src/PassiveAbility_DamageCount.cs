public class PassiveAbility_UltraSandBagMOD_DamageCount : AdvancedPassiveBase
{
    public override void OnRoundStart()
    {
        if (base.owner == null)
        {
            return;
        }

        base.owner.bufListDetail.AddBuf(new BattleUnitBuf_DamageCount(this._count));

        this._count = 0;
    }

    public override void OnStartBattle()
    {
        base.owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_DamageCount));
    }

    public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
    {
        this._count += dmg;

        var ds = base.owner.GetBufAndInitIfNull<BattleUnitBuf_DamageShow>(() => new(0));

        ds.Add(dmg);

        Hermes.Say($"Damage taken by: {dmg}");

        return base.BeforeTakeDamage(attacker, dmg);
    }

    public override void OnRoundEnd()
    {
        Hermes.Say($"Damage total taken: {_count}");
    }

    private int _count = 0;

    public class BattleUnitBuf_DamageCount : BattleUnitBuf
    {
        protected override string keywordId => "UltraSandBagMOD_DamageCount";

        public BattleUnitBuf_DamageCount(int stack)
        {
            base.stack = stack;
        }

        public override void OnRoundEnd()
        {
            base.Destroy();
        }
    }

    public class BattleUnitBuf_DamageShow : BattleUnitBuf
    {
        protected override string keywordId => "UltraSandBagMOD_DamageShow";

        public BattleUnitBuf_DamageShow(int stack)
        {
            base.stack = stack;
        }

        public void Add(int val)
        {
            this.stack += val;
        }

        public override void OnRoundEnd()
        {
            base.Destroy();
        }
    }
}
