public class PassiveAbility_TundraPassivePack_Poverty : AdvancedPassiveBase
{
    public override void OnRoundStartFirst()
    {
        foreach (var unit in base.owner.faction.AliveUnits)
        {
            if (unit == base.owner || unit.bufListDetail.HasBuf<BattleUnitBuf_Poverty>())
            {
                continue;
            }

            unit.bufListDetail.AddBuf(new BattleUnitBuf_Poverty());
        }
    }

    public class BattleUnitBuf_Poverty : AdvancedUnitBuf
    {
        private bool alive
            => base._owner.faction.AliveUnits.Exists(unit => unit.passiveDetail.HasPassive<PassiveAbility_TundraPassivePack_Poverty>());

        public override void OnRoundStart()
        {
            if (alive)
            {
                base._owner.breakDetail.TakeBreakDamage(8, DamageType.Passive);

                base._owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Weak, 2);
                base._owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Binding, 1);
            }
            else
            {
                base._owner.cardSlotDetail.RecoverPlayPoint(1);
                base._owner.allyCardDetail.DrawCards(1);

                base._owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1);
                base._owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Quickness, 1);
            }
        }
    }
}
