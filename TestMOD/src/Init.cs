using System.Reflection;
using HarmonyLib;
using LOR_XML;
using DeviceOfHermes;
using DeviceOfHermes.UI;
using DeviceOfHermes.AdvancedBase;
using DeviceOfHermes.CustomDice;
using DeviceOfHermes.Resource;
using UnityEngine;

class UITest : BattleUIBehaviour
{
    void Awake()
    {
        InitUI(1003);
    }

    void Start()
    {
        gameObject.AddContainer(head =>
        {
            head.Also(s => s.name = "DonHead")
                .MoveTo(new Vector2(0.51f, 0.95f))
                .SetImage(TestMOD.DonHead);
        });
    }
}

public class TestMOD : ModInitializer, ModPackage
{
    public static Sprite DonHead = Artwork.CreateSprite(Path.Combine(typeof(TestMOD).GetAsmDirectory(), "Artwork", "DonHead.png"));

    public string packageId
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

        StoryLineMaker.EnableUnrestrictedMap();

        StoryLineMaker.RegisterLine(-1350f, 7830f, -1350f, 7130f);
        StoryLineMaker.RegisterLine(-1350f, 7570f, -920f, 7540f);
        StoryLineMaker.RegisterLine(-980f, 7840f, -920f, 7800f);
        StoryLineMaker.RegisterLine(-940, 7940f, -860f, 7860f);

        StoryLineMaker.RegisterLine(-770, 7700f, -660f, 7620f);
        StoryLineMaker.RegisterLine(-700, 7200f, -150f, 7840f);

        StoryLineMaker.RegisterSpecialIcon("TestMOD_Test", -660f, 750f, DonHead, () => "シン・ドンファンを本にしますか？");

        var container = EnvContainer.Register<TestMOD>();

        var crate = container.AddCrate("Crate1");

        crate["Value1"] = "Text";
        crate["Value2"] = 2;

        crate.SetData("Image1", "Image1.png", new byte[0]);

        Hermes.Say($"Value1: {crate["Value1"]}");
        Hermes.Say($"Value2: {crate["Value2"]}");
        Hermes.Say($"Image1: {crate.GetData("Image1")}");

        container.Save();

        ScheduleRunner.AddSchedule(ScheduleTiming.RoundStart, () =>
        {
            Faction.Player.AliveUnits.ForEach(unit => unit.allyCardDetail.AddTempCard(new LorId(packageId, 1)));
        });

        DynamicAbilityCfg.AddBattleUnitBufRoute<BattleUnitBuf_TestCustomBuf>("TestMOD_TestCustomBuf");

        AssemblyManager.Instance.CreateInstance_DiceCardSelfAbility("Card-TestAdvCard-UseCard(estatbonus('power', -5), takedmg(1), allgain('KeywordBuf_Burn', 99), cardexhaust('', 505007))-Desc('このページは444回使用する')-Debug()");

        new AdditonalOnlyCard(new LorId(260004)).AddCards(new LorId(705011));
        var path = Path.Combine(typeof(TestMOD).GetAsmDirectory(), "Artwork", "BattleUnitBuf", "TestCustomBuf.png");

        Artwork.SetBattleUnitBufSprite(path);
        Artwork.SetBattleUnitBufSprite("Strength", path, true);
        Artwork.SetStoryIconSprite("MolarOffice", HermesConstants.RevengeDiceHit, replace: true);
        Artwork.SetStoryIconSprite("TestMOD_Test", DonHead, replace: true);

        Artwork.LoadBattleUnitBufSprites(Path.GetDirectoryName(Path.GetDirectoryName(path)));

        VannilaUnitBuf.AddAltId<BattleUnitBuf_bleeding>("DonHead", (_, _) => true);

        StageLibrarianList.SetUnit(new LorId(packageId, 1), 4, new LorId(packageId, 1), "野ドン");
        BattleManagerUI.Instance.AddBehaviour<UITest>("testUI");

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

                var eff = Serde.FromXmlFile<BattleEffectText>(tmp);

                Hermes.Say(Serde.ToXmlStr(eff));
                Hermes.Say(Serde.ToJsonStr(eff));

                eff = Serde.FromJsonStr<BattleEffectText>(Serde.ToJsonStr(eff));

                eff?.Let(eff => TextModel.SetBattleEffectText(eff, true));
            }
        };
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(PackageInfo<TestMOD>.Id);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }

    private static void MutePatch()
    {
        Harmony harmony = new Harmony(PackageInfo<TestMOD>.Id + ".MutePatch");

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
            base.owner.AddRencounterEvent(RencounterEvent.TakeDamaged, () => base.owner.view.Say("てめぇ...", 1f));
        }

        public override void OnRevenge(BattlePlayingCardDataInUnitModel card)
        {
            base.owner.view.Say("は、ルール違反ということか....", 1f);
        }
    }

    public class DiceCardAbility_Secondly : SecondlyDice
    {
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

    class SpeedDiceBuf_Test : SpeedDiceBuf
    {
        public override string keywordId => "TestCustomBuf";

        public override string keywordIconId => "BurnSpread";

        public override void OnUseCard()
        {
            Hermes.Say("OnUseCard by SpeedDiceBuf");
        }

        public override void OnRoundEnd()
        {
            Destroy();
        }
    }

    public class PassiveAbility_TestAdvPassive : AdvancedPassiveBase
    {
        public override int? HealthStopperLine => -50;

        public override int DrawCardAddr => 0;

        public override void OnClickUnit(ClickType ty)
        {
            Hermes.Say($"A unit clicked: {ty}");
        }

        public override void OnDropCard(BattlePlayingCardDataInUnitModel playcard)
        {
            if (playcard.target is null || playcard.target.IsDead())
            {
                var t = BattleObjectManager.instance.GetAliveList_random(playcard.owner.faction.FaceTo(), 1);

                if (t.Count != 0)
                {
                    playcard.target = t[0];

                    StageController.Instance.GetAllCards().Insert(0, playcard);
                }
            }
        }

        public override void OnWaveStart()
        {
            BattleMapChanger.SetMap(DonHead, DonHead, null, [
                "C_roland_Oz_1",
                "C_roland_Oz_2",
                "C_roland_Oz_3",
                "C_roland_Oz_4",
                "C_roland_Oz_5",
                "C_roland_Oz_6",
                "C_roland_Oz_7",
                "C_roland_Oz_8",
                "C_roland_Oz_9",
            ], Color.blue);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            curCard.target.view.AddEffect(TestMOD.DonHead, new Vector2(0.5f, 0.52f), 0f, 3f);
        }

        public override void OnRoundStartFirst()
        {
            UnityEngine.Debug.Log("RoundStartFirst");
        }

        public override void OnRoundStart()
        {
            base.owner?.speedDiceBufDetail?.AddBuf(0, new SpeedDiceBuf_Test());

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
            if (_elapsed < Mathf.Epsilon)
            {
                base.owner.view.Say("は、解禁ということか...", 3f);
            }

            _elapsed += Time.deltaTime;

            if (_elapsed > 5f)
            {
                _elapsed = 0f;
                f1 = f2 = f3 = false;

                return true;
            }

            if (_elapsed > 3f && !f3)
            {
                f3 = true;

                base.owner.view.AddEffect(TestMOD.DonHead, new Vector2(0.5f, 0.52f), 0f, 0.9f, sizeScale: 5f);

                return false;
            }

            if (_elapsed > 2f && !f2)
            {
                f2 = true;

                base.owner.view.AddEffect(TestMOD.DonHead, new Vector2(0.5f, 0.52f), 0f, 0.9f, sizeScale: 3f);

                return false;
            }

            if (_elapsed > 1f && !f1)
            {
                f1 = true;

                base.owner.view.AddEffect(TestMOD.DonHead, new Vector2(0.5f, 0.52f), 0f, 0.9f, sizeScale: 1f);

                return false;
            }

            return false;
        }

        public override void OnActivatedBuf(BattleUnitBuf activate)
        {
            Hermes.Say($"Buf activated: {activate.ToPrettyString()}");
        }

        public override void OnChangeBufStack(BattleUnitBuf changed, int last)
        {
            Hermes.Say($"Buf stack changed: {changed.bufType} {last} -> {changed.stack}");
        }

        private float _elapsed;

        private bool f1;
        private bool f2;
        private bool f3;
    }

    public class TestAmmoBuf : BattleAmmoBuf
    {
        protected override string keywordId => "TestCustomBuf";

        public override int DefaultStack => 6;

        public override bool DiceBlockWithNotConsumable => false;

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
