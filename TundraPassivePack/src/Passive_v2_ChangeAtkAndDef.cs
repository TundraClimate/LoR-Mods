using LOR_DiceSystem;

public class PassiveAbility_TundraPassivePack_ChangeAtkAndDef : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        var cards = base.owner.allyCardDetail.GetAllDeck()
            .Filter(card => card.GetSpec().Ranged is not CardRange.FarArea and not CardRange.FarAreaEach);

        foreach (var card in cards)
        {
            var behList = card.XmlData.DiceBehaviourList;

            for (var i = 0; behList.Count > i; i++)
            {
                var beh = behList[i];

                if (beh.Type is BehaviourType.Standby)
                {
                    continue;
                }

                if (beh.Detail is BehaviourDetail.Guard)
                {
                    behList[i] = beh.Copy();

                    behList[i].Also(beh =>
                    {
                        beh.Type = BehaviourType.Atk;
                        beh.Detail = BehaviourDetail.Penetrate;
                        beh.MotionDetail = MotionDetail.Z;
                    });
                }
                else if (beh.Type is BehaviourType.Atk)
                {
                    behList[i] = beh.Copy();

                    behList[i].Also(beh =>
                    {
                        beh.Type = BehaviourType.Def;
                        beh.Detail = BehaviourDetail.Guard;
                        beh.MotionDetail = MotionDetail.G;
                    });
                }
            }
        }
    }
}
