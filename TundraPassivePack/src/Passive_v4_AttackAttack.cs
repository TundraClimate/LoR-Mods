using LOR_DiceSystem;
using HarmonyLib;
using HarmonyExtension;

public class PassiveAbility_TundraPassivePack_AttackAttack : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        var cards = base.owner.allyCardDetail.GetAllDeck()
            .Filter(card => card.GetSpec().Ranged is not CardRange.FarArea and not CardRange.FarAreaEach);

        foreach (var card in cards)
        {
            ref var data = ref _xmlInfoRef(card);

            data = data.Copy(true);

            var behList = data.DiceBehaviourList;
            List<DiceBehaviour> bin = new();

            foreach (var beh in behList)
            {
                if (beh.Type is BehaviourType.Def)
                {
                    bin.Add(beh);

                    continue;
                }
            }

            behList.RemoveAll(b => bin.Contains(b));

            behList.Add(new DiceBehaviour { Min = 4, Dice = 6, Type = BehaviourType.Atk, Detail = BehaviourDetail.Slash, MotionDetail = MotionDetail.J, MotionDetailDefault = MotionDetail.N });
        }
    }

    static AccessTools.FieldRef<BattleDiceCardModel, DiceCardXmlInfo> _xmlInfoRef
        = typeof(BattleDiceCardModel).FieldRefAccess<DiceCardXmlInfo>("_xmlData");
}
