using LOR_DiceSystem;

public class PassiveAbility_TundraPassivePack_AttackAttack : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        var cards = base.owner.allyCardDetail.GetAllDeck()
            .Filter(card => card.GetSpec().Ranged is not CardRange.FarArea and not CardRange.FarAreaEach);

        foreach (var card in cards)
        {
            var behList = card.XmlData.DiceBehaviourList;
            List<DiceBehaviour> bin = new();

            foreach (ref var beh in behList.AsRef())
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
}
