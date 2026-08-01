public class PassiveAbility_TundraPassivePack_FusionRedMist : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        StageController.Instance.TryAddNewLibrarian(new LorId(50022), base.owner.index);

        BattleObjectManager.instance.InitUI();

        void FindSelf()
        {
            var self = owner.faction.AliveUnits.Find(unit => unit.index == owner.index && !unit.passiveDetail.HasPassive<PassiveAbility_TundraPassivePack_FusionRedMist>());

            if (self is not null)
            {
                self.OnWaveStart();

                BattleTickAction.OnTick -= FindSelf;
            }
        }

        BattleTickAction.OnTick += FindSelf;
    }
}
