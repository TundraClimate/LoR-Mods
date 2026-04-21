using System.Reflection;
using HarmonyLib;
using LOR_XML;
using DeviceOfHermes;
using DeviceOfHermes.AdvancedBase;
using DeviceOfHermes.CustomDice;
using DeviceOfHermes.Resource;

public class TestMOD : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "TestMOD";
        }
    }

    public override void OnInitializeMod()
    {
        TestMOD.MutePatch();
        TestMOD.ApplyHarmonyPatch();
        DebugConsole.Open();

        /* ModResource.LoadAdditionals(); */

        new AdditonalOnlyCard(new LorId(260004)).AddCards(new LorId(705011));
        var path = Path.Combine(typeof(TestMOD).GetAsmDirectory(), "Artwork", "BattleUnitBuf", "TestCustomBuf.png");

        Artwork.SetBattleUnitBufSprite(path);
        Artwork.SetBattleUnitBufSprite("Strength", path, true);
        Artwork.SetStoryIconSprite("MolarOffice", HermesConstants.RevengeDiceHit, replace: true);

        TextModel.OnLoadLocalize += lang =>
        {
            Hermes.Say($"Loaded by {lang}");
            if (lang == "en")
            {
                TextModel.SetBattleEffectText(new BattleEffectText()
                {
                    ID = "Strength",
                    Name = "Unlock",
                    Desc = "Is power +{0}"
                }, true);
            }
            else if (lang == "jp")
            {
                TextModel.SetBattleEffectText(new BattleEffectText()
                {
                    ID = "Strength",
                    Name = "解禁",
                    Desc = "解禁の皮を被ったパワー 攻撃威力+{0}"
                }, true);
                TextModel.SetBattleCardAbilityDesc(new BattleCardAbilityDesc()
                {
                    id = "drawCard",
                    desc = ["ページをなんかいっぱい引く。引いた枚数だけ引いたページの中からページを消し、ページを1枚引く。"],
                }, true);
                TextModel.SetBattleCardDesc(new BattleCardDesc()
                {
                    cardID = 602008,
                    cardName = "ゴールデンナックル",
                    ability = "ピッカピカだぜ".Yellow(),
                    behaviourDescList = new(),
                }, replace: true);
                TextModel.SetCharacterDialog(new BattleDialogCharacter()
                {
                    characterID = "Named",
                    dialogTypeList = [
                         new BattleDialogType()
                         {
                             dialogType = DialogType.START_BATTLE,
                             dialogList = [
                                 new BattleDialog()
                                 {
                                     dialogID = "START_BATTLE_0",
                                     dialogContent = "限り無く苦しめて殺してやるアラ...",
                                 }
                             ],
                         }
                    ],
                }, "AwlOfNight", true);
                TextModel.SetBookDesc(new BookDesc()
                {
                    bookID = 250051,
                    bookName = "ヤンのページ",
                    texts = [
                        "ﾔｰﾝ"
                    ],
                    passives = [],
                }, replace: true);
                TextModel.SetCharacterName(new LorId(148), "ねじヤン", true);
                TextModel.SetStageName(new LorId(50014), "ねじヤン", true);
                TextModel.SetDropBookName(new LorId(250037), "ねじヤンの本");
                TextModel.SetTextData("ui_invitation_context", "お前を本にします。", true);
                TextModel.SetPassiveDescs([
                    new PassiveDesc()
                    {
                        _id = 10008,
                        name = "高貴なるファン",
                        desc = "俺の名はファン、お前はファン。".Bold().Italic(),
                    },
                    new PassiveDesc()
                    {
                        _id = 250001,
                        name = "高貴さ",
                        desc = "決闘としよう。もし、俺が勝ったのなら...".Bold().Italic(),
                    },
                    new PassiveDesc()
                    {
                        _id = 243005,
                        name = "血香",
                        desc = "は....久々の馳走ということか....".Bold().Italic(),
                    },
                    new PassiveDesc()
                    {
                        _id = 243105,
                        name = "したたたか",
                        desc = "ここにテキストを入力".Bold().Strikethrough(),
                    },
                    new PassiveDesc()
                    {
                        _id = 243205,
                        name = "1級式 傷裂き",
                        desc = "思っていたより面白いやつらだな...。".Bold().Mark("#FF000080"),
                    },
                ], true);

                var tmp = Path.Combine(typeof(TestMOD).GetAsmDirectory(), "Temp.xml");

                var eff = ReadXmlParser.Read<BattleEffectText>(tmp);

                eff?.Let(eff => TextModel.SetBattleEffectText(eff, true));
            }
        };
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(TestMOD.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }

    private static void MutePatch()
    {
        Harmony harmony = new Harmony(TestMOD.packageId + ".MutePatch");

        MethodInfo postfix = typeof(TestMOD).GetMethod("MuteSameAssembly", BindingFlags.Static | BindingFlags.NonPublic);

        harmony.Patch(typeof(Mod.ModContentManager).GetMethod("GetErrorLogs"), postfix: new HarmonyMethod(postfix));
    }

    private static void MuteSameAssembly(ref List<string> __result)
    {
        List<string> bin = new List<string>();

        foreach (string err in __result)
        {
            if (err.Contains("The same assembly name already exists."))
            {
                bin.Add(err);
            }
        }

        foreach (string trash in bin)
        {
            __result.Remove(trash);
        }
    }

    public class DiceCardAbility_TestAdvDice : AdvancedDiceBase
    {
        public override void OnAddToKeeped()
        {
            UnityEngine.Debug.Log("Added");
        }

        public override bool IsKeeps()
        {
            UnityEngine.Debug.Log("Keeps");

            return true;
        }

        public override ParryingResult GetParryingResult(ParryingResult origin)
        {
            if (origin == ParryingResult.Lose)
            {
                return ParryingResult.Win;
            }

            return base.GetParryingResult(origin);
        }

        public override int GetDiceFinalResultValue(int origin)
        {
            return origin - 1;
        }

        public override int GetFinalResultBreakDamageValue(int origin)
        {
            return origin + 100;
        }
    }

    public class DiceCardAbility_Unbreakable : UnbreakableDice
    {
        public static string Desc = "破壊不能ダイス".Red();

        public override void OnLoseParrying()
        {
            base.behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = 5,
            });
        }

        public override void OnUseBreaked(BattlePlayingCardDataInUnitModel card)
        {
            base.owner.view.Say("思っていたより面白いやつらだな……。", 1f);
        }
    }

    public class DiceCardAbility_Revenge : RevengeDice
    {
        public static string Desc = "復讐ダイス".Purple();

        public override void OnSucceedAttack(BattleUnitModel target)
        {
        }

        public override void OnBeforeRevenge(BattlePlayingCardDataInUnitModel card, BattleDiceBehavior revengeBy)
        {
        }

        public override void OnRevenge(BattlePlayingCardDataInUnitModel card)
        {
            base.owner.view.Say("は、ルール違反ということか....", 1f);
        }
    }

    public class DiceCardSelfAbility_TestAdvCard : AdvancedCardBase
    {
        public override bool IsClashable => true;

        public override bool IsIgnoreSpeedByMatch => true;

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.owner.ConsumeAmmo<TestAmmoBuf>(6);
        }
    }

    public class PassiveAbility_TestAdvPassive : AdvancedPassiveBase
    {
        public override void OnRoundStartFirst()
        {
            UnityEngine.Debug.Log("RoundStartFirst");
        }

        public override void OnRoundStart()
        {
            UnityEngine.Debug.Log("RoundStart");

            var buf = base.owner.GetBufAndInitIfNull(() => new BattleUnitBuf_TestCustomBuf());
            var ammo = base.owner.GetBufAndInitIfNull(() => new TestAmmoBuf());
            var reload = base.owner.GetBufAndInitIfNull(() => new ReloadAmmoBuf<TestAmmoBuf>());
        }

        public override void OnRoundStartAfter()
        {
            UnityEngine.Debug.Log("RoundStartAfter");
        }

        public override void OnRoundStartLast()
        {
            UnityEngine.Debug.Log("RoundStartLast");
        }

        public override bool IsAllowRoundEnd()
        {
            UnityEngine.Debug.Log("Allow end");

            return base.IsAllowRoundEnd();
        }

        public override void OnActivatedBuf(BattleUnitBuf activate)
        {
            Hermes.Say($"Buf activated: {activate.ToPrettyString()}");
        }

        public override void OnChangeBufStack(BattleUnitBuf changed, int last)
        {
            Hermes.Say($"Buf stack changed: {changed.bufType} {last} -> {changed.stack}");
        }
    }

    public class TestAmmoBuf : BattleAmmoBuf
    {
        protected override string keywordId => "TestCustomBuf";

        public override int DefaultStack => 6;

        public override bool DiceBlockWithNotConsumable => true;

        public override void OnBeforeConsume(ref int require)
        {
            Hermes.Say($"Aaay, needs {require} ammo!");
        }

        public override void OnConsume(int consumed)
        {
            Hermes.Say($"Wowow consumed {consumed} stack");
        }
    }
}
