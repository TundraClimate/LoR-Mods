public class PassiveAbility_UltraSandBagMOD_End : AdvancedPassiveBase
{
    public override void OnClickUnit(ClickType ty)
    {
        if (ty is ClickType.Right)
        {
            _count += 1;

            if (_count >= 3)
            {
                StageController.Instance.GetStageModel().GetWave(StageController.Instance.CurrentWave).Defeat();
                StageController.Instance.EndBattle();
            }
        }
    }

    private int _count;
}
