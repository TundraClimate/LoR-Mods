public class PassiveAbility_TundraPassivePack_FusionRedMist : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        StageController.Instance.TryAddNewLibrarian(new LorId(50022), base.owner.index);

        BattleObjectManager.instance.InitUI();
    }
}
