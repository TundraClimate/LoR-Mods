using System.Reflection;
using System.Reflection.Emit;
using Sound;
using UnityEngine;
using HarmonyLib;

namespace IndexPrescriptUtil;

public class IndexPrescriptBuf : BattleUnitBuf
{
    protected override string keywordIconId => this.PrescriptType switch
    {
        IndexPrescriptType.Paper => "Index_Prescript_Keyword",
        IndexPrescriptType.Terminal => "Index_Prescript_Keyword_Terminal",
        _ => this.keywordId,
    };

    public virtual IndexPrescriptType PrescriptType => IndexPrescriptType.Paper;

    public virtual bool DefaultSuccession => false;

    public bool IsSuccess => this._success;

    public void InitPrescript()
    {
        this.stack = 0;
        this._success = this.DefaultSuccession;
    }

    public void Success()
    {
        this._success = true;

        if (GetSelfPrescriptPassives() is IEnumerable<IndexPrescriptPassiveBase> passives)
        {
            foreach (var passive in passives)
            {
                passive.OnSuccess(this);
            }
        }

        this.PlayParticle();
    }

    public void Failure()
    {
        this._success = false;

        if (GetSelfPrescriptPassives() is IEnumerable<IndexPrescriptPassiveBase> passives)
        {
            foreach (var passive in passives)
            {
                passive.OnFailure(this);
            }
        }
    }

    public virtual void OnReleaseCheck()
    {
    }

    private IEnumerable<IndexPrescriptPassiveBase> GetSelfPrescriptPassives()
    {
        return this._owner?.passiveDetail?.PassiveList?.OfType<IndexPrescriptPassiveBase>();
    }

    private void PlayParticle()
    {
        var prefab = Resources.Load("Prefabs/Battle/SpecialEffect/IndexRelease_ActivateParticle");

        if (prefab is not null)
        {
            var particle = UnityEngine.Object.Instantiate(prefab, this._owner.view.charAppearance.transform.parent) as GameObject;

            particle.transform.localPosition = Vector3.zero;
            particle.transform.localRotation = Quaternion.identity;
            particle.transform.localScale = Vector3.one;
        }

        SoundEffectManager.Instance.PlayClip("Buf/Effect_Index_Unlock", false, 1f, null);
    }

    private bool _success;
}

public enum IndexPrescriptType
{
    Paper,
    Terminal,
    Other,
}

public class IndexPrescriptPassiveBase : PassiveAbilityBase
{
    public virtual void OnSuccess(IndexPrescriptBuf prescriptBuf)
    {
    }

    public virtual void OnFailure(IndexPrescriptBuf prescriptBuf)
    {
    }

    public virtual void OnReleaseCheck(IndexPrescriptBuf prescript)
    {
    }
}

public class BattleUnitBuf_PrescriptPrevTurn : BattleUnitBuf
{
    public IndexPrescriptBuf prescript;
}

public static class IndexPrescriptUtil
{
    internal static void ApplyHarmonyPatch()
    {
        var harmony = new Harmony("IndexPrescriptUtil.Harmony.Patch");

        foreach (var patch in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(patch).Patch();
        }
    }

    public static int GetPrescriptReleasedIndexThisRound(Faction faction)
    {
        return BattleObjectManager.instance.GetAliveList(faction)
            .FindAll(unit =>
                unit.bufListDetail.GetActivatedBufList().Find(buf => buf is IndexPrescriptBuf) is IndexPrescriptBuf buf && buf.IsSuccess
            )
            .Count;
    }

    public static int GetPrescriptReleasedIndexPrevRound(Faction faction)
    {
        return BattleObjectManager.instance.GetAliveList(faction)
            .FindAll(unit =>
                unit.bufListDetail.GetActivatedBufList().Find(buf => buf is BattleUnitBuf_PrescriptPrevTurn) is BattleUnitBuf_PrescriptPrevTurn buf && buf.prescript.IsSuccess
            )
            .Count;
    }
}

internal class PatchClass
{
    [HarmonyPatch]
    class PatchOnInit
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(BattleUnitBufListDetail), "AddBuf");
            yield return AccessTools.Method(typeof(BattleUnitBufListDetail), "AddReadyBuf");
            yield return AccessTools.Method(typeof(BattleUnitBufListDetail), "AddReadyReadyBuf");
            yield return AccessTools.Method(typeof(BattleUnitBufListDetail), "AddBufWithoutDuplication");
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions);

            matcher.MatchStartForward(
                new CodeMatch(il => il.Calls(AccessTools.Method(typeof(BattleUnitBuf), "Init")))
            )
                .Insert(
                    new CodeInstruction(OpCodes.Ldarg_1),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PatchOnInit), "InjectMethod"))
                );

            return matcher.Instructions();
        }

        static void InjectMethod(BattleUnitBuf buf)
        {
            if (buf is IndexPrescriptBuf prescriptBuf)
            {
                prescriptBuf.InitPrescript();
            }
        }
    }

    [HarmonyPatch(typeof(BattleUnitBufListDetail), "OnRoundEndTheLast")]
    class PatchOnRoundEndLast
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions);

            matcher.MatchEndForward(
                new CodeMatch(il => il.IsLdloc()),
                new CodeMatch(il => il.Calls(AccessTools.Method(typeof(BattleUnitBuf), "OnRoundEndTheLast")))
            )
                .Advance(1)
                .Insert(
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BattleUnitBufListDetail), "_self")),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PatchOnRoundEndLast), "InjectMethod"))
                );

            return matcher.Instructions();
        }

        static void InjectMethod(BattleUnitModel _self)
        {
            var passives = _self.passiveDetail.PassiveList.OfType<IndexPrescriptPassiveBase>();

            foreach (var buf in _self.bufListDetail.GetActivatedBufList().OfType<IndexPrescriptBuf>())
            {
                buf.OnReleaseCheck();

                foreach (var passive in passives)
                {
                    passive.OnReleaseCheck(buf);
                }
            }
        }
    }
}
