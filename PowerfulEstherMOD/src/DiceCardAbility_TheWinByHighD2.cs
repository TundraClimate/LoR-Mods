using System.Collections.Generic;

public class DiceCardAbility_TheWinByHighD2 : DiceCardAbilityBase
{
    public static string Desc = "[的中] ランダムな相手にこのページを再使用(最大1回)。";

    public override void OnSucceedAttack()
    {
        if (base.owner != null && base.behavior != null && base.behavior.card != null)
        {
            List<BattleUnitModel> alives = BattleObjectManager.instance.GetAliveList(base.owner.faction == Faction.Player ? Faction.Enemy : Faction.Player);

            if (alives.Count != 0)
            {
                BattleUnitModel target = RandomUtil.SelectOne<BattleUnitModel>(alives);

                BattlePlayingCardDataInUnitModel card = new BattlePlayingCardDataInUnitModel();

                card.owner = base.owner;
                card.card = BattleDiceCardModel.CreatePlayingCard(ItemXmlDataList.instance.GetCardItem(new LorId(PowerfulEstherMOD.packageId, 271)));

                Singleton<StageController>.Instance.AddAllCardListInBattle(card, target, -1);
            }
        }
    }
}
