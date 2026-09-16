using LimbufOfHermes;

public class PassiveAbility_TundraPassivePack_FellVoid : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        base.owner.bufListDetail.AddBuf(new Shin());
    }

    class Shin : BattleUnitBuf_Limbuf_Shin
    {
        protected override string keywordId => "Tundra_ShinFellVoid";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new AdvancedDiceStatBonus
            {
                kwdBufModifier = (origin, ref res, target) =>
                {
                    if (origin.bufType == KeywordBuf.Burn || origin.bufType == LimKeywordBuf.Tremor || origin.bufType == LimKeywordBuf.Sinking)
                    {
                        res?.stack += 1;
                    }
                },

                dmgRate = 50.Min(((base._owner.bufListDetail.GetActivatedBuf(LimKeywordBuf.Poise)?.stack ?? 0) / 3) * 10)
                    + 50.Min(25 + (int)(base._owner.hp / (float)base._owner.MaxHp * 100)),
            });
        }

        public override void OnAddBufAll(BattleUnitBuf buf, int addedStack)
        {
            if (buf.bufType == LimKeywordBuf.Poise)
            {
                buf.stack += 1;
            }
        }
    }
}
