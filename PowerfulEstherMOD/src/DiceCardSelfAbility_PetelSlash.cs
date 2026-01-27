using LOR_DiceSystem;

public class DiceCardSelfAbility_PetelSlash : DiceCardSelfAbilityBase
{
    public static string Desc = "[使用時] 指令の印が刻まれているなら、貫通ダイス(2~4)を追加。";

    public override void OnUseCard()
    {
        BattlePlayingCardDataInUnitModel currentPage = base.owner.currentDiceAction;

        if (base.owner == null || currentPage == null)
        {
            return;
        }

        if (currentPage.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            DiceBehaviour dice = currentPage.GetDiceBehaviourXmlList()[2].Copy();

            if (dice != null)
            {
                dice.Min = 2;
                dice.Dice = 4;
                dice.Type = BehaviourType.Atk;
                dice.Detail = BehaviourDetail.Penetrate;

                currentPage.AddDice(new BattleDiceBehavior()
                {
                    behaviourInCard = dice,
                });
            }
        }
    }
}
