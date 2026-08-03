using LOR_DiceSystem;

public class PassiveAbility_UltraSandBagMOD_UnitBuf : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        base.owner.GetBufAndInitIfNull<BattleUnitBuf_Resist>(() => new());
        base.owner.GetBufAndInitIfNull<BattleUnitBuf_Break>(() => new());
    }

    class BattleUnitBuf_Resist : AdvancedUnitBuf
    {
        protected override string keywordId => "UltraSandBagMOD_Resist";

        public override void OnClick(ClickType ty)
        {
            if (ty is ClickType.Left)
            {
                UpResist();
            }
            else if (ty is ClickType.Right)
            {
                DownResist();
            }

            BattleObjectManager.instance.InitUI();
        }

        public override AtkResist GetResistHP(AtkResist origin, BehaviourDetail detail)
        {
            return res;
        }

        void UpResist()
        {
            if (res is AtkResist.Immune)
            {
                return;
            }

            res = (AtkResist)((int)res + 1);

            Util.LoadPrefab("Battle/DiceAttackEffects/New/FX/Mon/Claw/FX_Mon_Claw_MistRed")?.Let(go =>
            {
                go.transform.position = base._owner.view.characterRotationCenter.position;
                go.AddComponent<AutoDestruct>().time = 3f;
            });
        }

        void DownResist()
        {
            if (res is AtkResist.Weak)
            {
                return;
            }

            res = (AtkResist)((int)res - 1);

            Util.LoadPrefab("Battle/DiceAttackEffects/New/FX/Mon/Claw/FX_Mon_Claw_MistBlue")?.Let(go =>
            {
                go.transform.position = base._owner.view.characterRotationCenter.position;
                go.AddComponent<AutoDestruct>().time = 3f;
            });
        }

        private AtkResist res = AtkResist.Normal;
    }

    class BattleUnitBuf_Break : AdvancedUnitBuf
    {
        protected override string keywordId => "UltraSandBagMOD_Break";

        public override void OnClick(ClickType ty)
        {
            base._owner.breakDetail.nextTurnBreak = true;
        }
    }
}
