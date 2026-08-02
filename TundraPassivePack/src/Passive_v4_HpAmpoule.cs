using HarmonyExtension;

public class PassiveAbility_TundraPassivePack_HpAmpoule : AdvancedPassiveBase
{
    public override bool IsDieInsteadOfExtinction => !_activated;

    public override void OnWaveStart()
    {
        _activated = false;
    }

    public override void OnExtinct()
    {
        if (StageController.Instance.IsLogState())
        {
            owner.AddRencounterEvent(RencounterEvent.PrintEffect, () =>
            {
                typeof(BattleUnitModel).Method("set_hp")
                    .Invoke(base.owner, [base.owner.UnitData.unitData.bookItem.DeadLine]);
                base.owner.view.unitBottomStatUI.EnableCanvas(false);
                BattleManagerUI.Instance.ui_unitListInfoSummary.UpdateCharacterProfile(base.owner, owner.faction, owner.hp, owner.breakDetail.breakGauge, null);
                StageController.Instance.GetAllCards().RemoveAll(card => card.owner == base.owner);
            });
        }
        else
        {
            typeof(BattleUnitModel).Method("set_hp")
                .Invoke(base.owner, [base.owner.UnitData.unitData.bookItem.DeadLine]);
            base.owner.view.unitBottomStatUI.EnableCanvas(false);
            BattleManagerUI.Instance.ui_unitListInfoSummary.UpdateCharacterProfile(base.owner, owner.faction, owner.hp, owner.breakDetail.breakGauge, null);
            StageController.Instance.GetAllCards().RemoveAll(card => card.owner == base.owner);
        }
    }

    public override void OnPreRoundEnd()
    {
        if (base.owner.IsExtinction() && !_activated)
        {
            base.owner.Extinct(false);
            base.owner.RecoverHP(base.owner.MaxHp + base.owner.UnitData.unitData.bookItem.DeadLine);
            if (base.owner.IsBreakLifeZero())
            {
                base.owner.breakDetail.ResetBreakDefault();
                base.owner.breakDetail.RecoverBreakLife(1);
                base.owner.breakDetail.ResetGauge();
                base.owner.breakDetail.nextTurnBreak = false;
            }
            base.owner.view.unitBottomStatUI.EnableCanvas(true);
            BattleManagerUI.Instance.ui_unitListInfoSummary.UpdateCharacterProfile(base.owner, owner.faction, owner.hp, owner.breakDetail.breakGauge, null);

            Util.LoadPrefab("Battle/DiceAttackEffects/New/FX/Mon/Claw/FX_Mon_Claw_Recovery")?.Let(go =>
            {
                go.transform.position = base.owner.view.characterRotationCenter.position;
                go.AddComponent<AutoDestruct>().time = 3f;
            });

            _activated = true;
        }
    }

    private bool _activated;
}
