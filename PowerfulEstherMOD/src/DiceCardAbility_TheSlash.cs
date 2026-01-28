using System.Collections.Generic;

public class DiceCardAbility_TheSlash : DiceCardAbilityBase
{
    public static string Desc = "[的中] 指令対象なら、ランダムな相手にこのページを3回再使用(最大1回)";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
        List<BattleUnitModel> alives = BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Player) ? Faction.Enemy : Faction.Player);

        if (_isUsed && _count == 3)
        {
            _isUsed = false;
            _count = 1;

            return;
        }

        if (_isUsed && _count < 3)
        {
            StageController.Instance.AddAllCardListInBattle(this.card, RandomUtil.SelectOne(alives), -1);

            _count++;

            return;
        }

        if (target == null)
        {
            return;
        }

        if (target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>() && !_isUsed)
        {
            if (alives.Count != 0)
            {
                _isUsed = true;

                StageController.Instance.AddAllCardListInBattle(this.card, RandomUtil.SelectOne(alives), -1);
            }
        }
    }

    private static bool _isUsed = false;

    private static int _count = 1;
}
