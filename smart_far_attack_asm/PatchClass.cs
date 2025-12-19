using System.Collections.Generic;
using HarmonyLib;

namespace SmartFarAttackMod
{
    public class PatchClass
    {
        [HarmonyPatch(typeof(BattleUnitCardsInHandUI), "ApplySelectedCard")]
        public class ApplySelectedCard_Patch
        {
            public static bool Prefix(BattleUnitCardsInHandUI __instance, BattleUnitModel target, int targetSlot, ref BattleUnitModel ____selectedUnit, ref List<BattleDiceCardUI> ____cardList)
            {
                BattleDiceCardUI selectedCard = __instance.GetSelectedCard();
                if (selectedCard != null)
                {
                    selectedCard.transform.localScale = selectedCard.scaleOrigin;
                    selectedCard.gameObject.SetActive(false);
                }
                SingletonBehavior<BattleSoundManager>.Instance.PlaySound(EffectSoundType.CARD_APPLY, false);
                ____selectedUnit.cardSlotDetail.AddCard(selectedCard.CardModel, target, targetSlot, false);
                if (____selectedUnit != null)
                {
                    ____selectedUnit.view.speedDiceSetterUI.ReleaseSelection();
                    ____selectedUnit = null;
                }
                __instance.Deactivate();
                foreach (BattleDiceCardUI battleDiceCardUI in ____cardList)
                {
                    battleDiceCardUI.gameObject.SetActive(false);
                }
                SingletonBehavior<BattleManagerUI>.Instance.ui_TargetArrow.UpdateTargetList();
                SingletonBehavior<BattleManagerUI>.Instance.selectedAllyDice = null;
                SingletonBehavior<BattleManagerUI>.Instance.selectedEnemyDice = null;

                return false;
            }
        }
    }
}
