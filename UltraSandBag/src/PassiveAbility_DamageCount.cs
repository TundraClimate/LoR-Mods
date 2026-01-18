public class PassiveAbility_DamageCount : PassiveAbilityBase
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
        BattleUnitBuf b = base.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_DamageCount);

        if (b != null)
        {
            base.owner.bufListDetail.RemoveBuf(b);
        }
    }

    public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
    {
        this._count += dmg;

        BattleUnitBuf_DamageShow ds =
            (BattleUnitBuf_DamageShow)base.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_DamageShow);

        if (ds == null)
        {
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_DamageShow(dmg));
        }
        else
        {
            ds.Add(dmg);
        }

        UnityEngine.Debug.Log("Damage taken by: " + dmg);

        return base.BeforeTakeDamage(attacker, dmg);
    }

    public override void OnRoundEnd()
    {
        UnityEngine.Debug.Log("Damage total taken: " + this._count);
    }

    private int _count = 0;

    public class BattleUnitBuf_DamageCount : BattleUnitBuf
    {
        protected override string keywordId
        {
            get
            {
                return "DamageCount";
            }
        }

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
        protected override string keywordId
        {
            get
            {
                return "DamageShow";
            }
        }

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
