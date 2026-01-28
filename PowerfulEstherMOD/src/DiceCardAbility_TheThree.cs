public class DiceCardAbility_TheThree : DiceCardAbilityBase
{
    public static string Desc = "[的中] このダイスを1回再利用";

    public override void OnSucceedAttack()
    {
        if (base.behavior == null)
        {
            return;
        }

        if (this._isUsed)
        {
            return;
        }

        this._isUsed = true;

        base.ActivateBonusAttackDice();
    }

    private bool _isUsed = false;
}
