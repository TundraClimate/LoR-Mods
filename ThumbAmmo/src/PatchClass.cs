using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

public static class PatchClass
{
    [HarmonyPatch(typeof(BattleAllyCardDetail), "AddNewCard", new[] { typeof(int), typeof(bool) })]
    public class PrefixPatch_AddNewCardVannila
    {
        public static bool Prefix(BattleUnitModel ____self, int cardId)
        {
            if (ThumbBulletClass.IsBulletId(new LorId(cardId)))
            {
                if (cardId == 602020)
                {
                    AmmoBuf.ApplyOrAdd(____self, "IronAmmo", new DiceCardSelfAbility_thumbBullet1.BattleUnitBuf_bullet1());
                }
                else if (cardId == 602021)
                {
                    AmmoBuf.ApplyOrAdd(____self, "FreezeAmmo", new DiceCardSelfAbility_thumbBullet2.BattleUnitBuf_bullet2());
                }
                else if (cardId == 602022)
                {
                    AmmoBuf.ApplyOrAdd(____self, "FireAmmo", new DiceCardSelfAbility_thumbBullet3.BattleUnitBuf_bullet3());
                }

                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(BattleAllyCardDetail), "GetHand")]
    public class Postfix_GetHand
    {
        public static void Postfix(ref List<BattleDiceCardModel> __result, BattleUnitModel ____self)
        {
            if (____self != null && ____self.bufListDetail.HasBuf<AmmoBuf>())
            {
                List<BattleUnitBuf> ammos = ____self.bufListDetail.GetActivatedBufList().FindAll(buf => buf is AmmoBuf);

                foreach (AmmoBuf ammo in ammos)
                {
                    for (int i = 0; ammo.stack > i; i++)
                    {
                        if (ammo.GetTextId() == "FireAmmo")
                        {
                            __result.Add(BattleDiceCardModel.CreatePlayingCard(ItemXmlDataList.instance.GetCardItem(602022)));
                        }
                        else if (ammo.GetTextId() == "FreezeAmmo")
                        {
                            __result.Add(BattleDiceCardModel.CreatePlayingCard(ItemXmlDataList.instance.GetCardItem(602021)));
                        }
                        else if (ammo.GetTextId() == "IronAmmo")
                        {
                            __result.Add(BattleDiceCardModel.CreatePlayingCard(ItemXmlDataList.instance.GetCardItem(602020)));
                        }
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(BattleAllyCardDetail), "DiscardACardByAbility", new[] { typeof(List<BattleDiceCardModel>) })]
    public class PrefixPatch_DiscardByAbi
    {
        public static void Prefix(BattleUnitModel ____self, ref List<BattleDiceCardModel> cardList, ref List<BattleDiceCardModel> ____cardInHand)
        {
            List<BattleDiceCardModel> bin = new List<BattleDiceCardModel>();

            foreach (BattleDiceCardModel card in cardList)
            {
                if (ThumbBulletClass.IsBulletId(card.GetID()))
                {
                    switch (card.GetID().id)
                    {
                        case 602022:
                            AmmoBuf ammoFire = (AmmoBuf)____self.bufListDetail.GetActivatedBufList().Find(buf => buf is AmmoBuf && ((AmmoBuf)buf).GetTextId() == "FireAmmo");

                            if (ammoFire != null)
                            {
                                UnityEngine.Debug.Log("Use fire ammo");
                                ammoFire.UseStack(1);
                            }
                            else
                            {
                                UnityEngine.Debug.Log("null fire ammo");
                            }

                            break;
                        case 602021:
                            AmmoBuf ammoFreeze = (AmmoBuf)____self.bufListDetail.GetActivatedBufList().Find(buf => buf is AmmoBuf && ((AmmoBuf)buf).GetTextId() == "FreezeAmmo");

                            if (ammoFreeze != null)
                            {
                                UnityEngine.Debug.Log("Use freeze ammo");
                                ammoFreeze.UseStack(1);
                            }
                            else
                            {
                                UnityEngine.Debug.Log("null freeze ammo");
                            }

                            break;
                        case 602020:
                            AmmoBuf ammoIron = (AmmoBuf)____self.bufListDetail.GetActivatedBufList().Find(buf => buf is AmmoBuf && ((AmmoBuf)buf).GetTextId() == "IronAmmo");

                            if (ammoIron != null)
                            {
                                UnityEngine.Debug.Log("Use iron ammo");
                                ammoIron.UseStack(1);
                            }
                            else
                            {
                                UnityEngine.Debug.Log("null iron ammo");
                            }

                            break;
                    }

                    bin.Add(card);
                }
            }

            foreach (BattleDiceCardModel trash in ____cardInHand)
            {
                if (ThumbBulletClass.IsBulletId(trash.GetID()))
                {
                    UnityEngine.Debug.Log("Clean " + trash.GetID());
                    bin.Add(trash);
                }
            }

            foreach (BattleDiceCardModel trash in bin)
            {
                UnityEngine.Debug.Log("Rm " + trash.GetID());
                cardList.Remove(trash);
            }
        }
    }

    [HarmonyPatch(typeof(BattleAllyCardDetail), "DiscardACardLowest")]
    public class PrefixPatch_DiscardByLower
    {
        public static void Prefix(BattleUnitModel ____self, ref List<BattleDiceCardModel> ____cardInHand)
        {
            ____cardInHand = ____self.allyCardDetail.GetHand();
        }
    }

    [HarmonyPatch(typeof(BattleUnitCardsInHandUI), "UpdateCardList")]
    public class Transpiler_HandUI
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            MethodInfo target = AccessTools.Method(typeof(BattleAllyCardDetail), "GetHand");
            MethodInfo inject = AccessTools.Method(typeof(PatchClass.Transpiler_HandUI), "ExcludeBullet");

            foreach (CodeInstruction inst in instructions)
            {
                if (inst.Calls(target))
                {
                    yield return new CodeInstruction(OpCodes.Call, inject);
                }
                else
                {
                    yield return inst;
                }
            }
        }

        private static List<BattleDiceCardModel> ExcludeBullet(BattleAllyCardDetail ally)
        {
            List<BattleDiceCardModel> hand = ally.GetHand();

            hand.RemoveAll(card => ThumbBulletClass.IsBulletId(card.GetID()));

            return hand;
        }
    }
}
