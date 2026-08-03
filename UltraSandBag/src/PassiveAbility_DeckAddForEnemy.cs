public class PassiveAbility_UltraSandBagMOD_DeckAddForEnemy : AdvancedPassiveBase
{
    public List<LorId> ids = [
        new LorId(UltraSandBagMOD.packageId, 1),
        new LorId(UltraSandBagMOD.packageId, 2),
        new LorId(UltraSandBagMOD.packageId, 3),
    ];

    public override void OnRoundStart()
    {
        foreach (var unit in base.owner.faction.FaceTo().AliveUnits)
        {
            var personal = unit.personalEgoDetail.GetCardAll();

            foreach (var id in ids)
            {
                if (!personal.Exists(p => ids.Contains(p.GetID())))
                {
                    unit.personalEgoDetail.AddCard(id);
                }
            }
        }
    }
}
