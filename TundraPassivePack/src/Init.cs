global using DeviceOfHermes;
global using DeviceOfHermes.AdvancedBase;
using LOR_XML;
using HarmonyLib;
using DeviceOfHermes.Resource;

public class TundraPassivePack : ModInitializer
{
    public static string packageId => "TundraPassivePack";

    public override void OnInitializeMod()
    {
        Artwork.LoadBattleUnitBufSprites(Path.Combine(typeof(TundraPassivePack).GetAsmDirectory(), "Artwork", "BattleUnitBuf"));

        ApplyHarmonyPatch();

        TextModel.SetBattleEffectTexts([
            new BattleEffectText {
                ID = "Tundra_OdinEye",
                Name = "予知眼",
                Desc =
"""
一方攻撃を受ける、もしくはマッチに敗北した時、「予知」ページを使用する(ページごとに1回)

一方攻撃により「予知」使用時、数値が3減少
マッチ敗北により「予知」使用時、数値が5減少

値が0なら加熱状態になり、上記の効果が非活性化

最大値: 30
"""
            },
            new BattleEffectText {
                ID = "Tundra_OdinEye_Heat",
                Name = "予知眼-加熱",
                Desc =
"""
幕の開始時、10増加

数値が最大値なら、「予知眼」に変更

最大値: 30
"""
            },
            new BattleEffectText {
                ID = "Tundra_EqualDice",
                Name = "公平ダイス",
                Desc =
"""
マッチ中の互いのダイス最小値・最大値がそれぞれ平均化される
この効果は既存のダイス最低値・最大値を変更する効果適用後に適用される
"""
            },
            new BattleEffectText {
                ID = "Tundra_ShinFellVoid",
                Name = "シン(心)-奈落",
                Desc =
"""
敵に付与する火傷・振動・沈潜付与数が1増加

自身の獲得する呼吸の値が1増加

自身の呼吸3につき、与えるダメージ量が10%増加(端数切り捨て、最大50%)
与えるダメージ量が(25 + 失った体力の比率)%だけ増加(最大50%)
"""
            },
        ]);

        VannilaUnitBuf.AddMaxIf<BattleUnitBuf_warpCharge>(80, (_, owner) => owner?.passiveDetail?.HasPassive<PassiveAbility_TundraPassivePack_HeavyBattery>() == true);
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(TundraPassivePack.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
