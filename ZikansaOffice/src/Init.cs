using HarmonyLib;
using LOR_DiceSystem;
using DeviceOfHermes.AdvancedBase;
using DeviceOfHermes.CustomDice;

public class ZikansaOffice : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "ZikansaOffice";
        }
    }

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();

        var _ = new RevengeDice();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(ZikansaOffice.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}

public class DiceCardAbility_RevengeNormal : RevengeDice
{
}

public class DiceCardAbility_RevengeDebuf : RevengeDice
{
    public static string Desc = "[的中] 次の幕に麻痺3と出血2を付与";

    public override void OnSucceedAttack()
    {
        base.behavior?.card?.target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Paralysis, 3, base.owner);
        base.behavior?.card?.target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Bleeding, 2, base.owner);
    }
}

public class PassiveAbility_Zikansa : AdvancedPassiveBase
{
    public override bool IsAllowRoundEnd()
    {
        var queue = base.owner?.cardSlotDetail?.keepCard?.cardBehaviorQueue;

        if (queue?.Count != 0)
        {
            if (base.owner is null)
            {
                return true;
            }

            var cand = BattleObjectManager.instance.GetAliveList_random(base.owner.faction.FaceTo(), 1);

            if (cand.Count == 0)
            {
                return true;
            }

            var card = ItemXmlDataList.instance.GetCardItem(new LorId(ZikansaOffice.packageId, 1));
            var playcard = base.owner.CreatePlayingCard(card, cand[0], speedDiceResultValue: 1);

            playcard.cardBehaviorQueue.Clear();

            foreach (var c in queue ?? new())
            {
                if (c.Type == BehaviourType.Standby && ((int)c.Detail) is 0 or 1 or 2)
                {
                    var newBeh = c.behaviourInCard.Copy();

                    newBeh.Type = BehaviourType.Atk;

                    c.behaviourInCard = newBeh;
                    c.card = playcard;

                    foreach (var abi in c.abilityList)
                    {
                        abi.behavior = c;
                    }

                    playcard.cardBehaviorQueue.Enqueue(c);
                }
            }

            if (playcard.cardBehaviorQueue.Count != 0)
            {
                StageController.Instance.GetAllCards().Insert(0, playcard);

                return false;
            }
        }

        return true;
    }
}

public class PassiveAbility_ZikansaStock : AdvancedPassiveBase
{
    public override void OnRoundStart()
    {
        base.owner.TakeDamage(_dmg, attacker: base.owner);
        base.owner.TakeBreakDamage(_breakDmg, attacker: base.owner);

        this._dmg = 0;
        this._breakDmg = 0;
    }

    public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
    {
        if (attacker != base.owner)
        {
            this._dmg += dmg;

            return true;
        }

        return false;
    }

    public override bool BeforeTakeBreakDamage(BattleUnitModel attacker, int dmg)
    {
        if (attacker != base.owner)
        {
            this._breakDmg += dmg;

            return true;
        }

        return false;
    }

    private int _dmg = 0;

    private int _breakDmg = 0;
}
