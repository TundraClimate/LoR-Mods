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
}
