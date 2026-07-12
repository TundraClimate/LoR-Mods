using Sound;

public class PassiveAbility_TundraPassivePack_Boom : AdvancedPassiveBase
{
    public override void OnStartBattle()
    {
        if (base.owner?.bufListDetail?.GetActivatedBuf(KeywordBuf.Burn)?.stack >= 10)
        {
            SoundEffectManager.Instance.PlayClip("Creature/MatchGirl_Explosion", false, 6f, null)
                .SetGlobalPosition(base.owner.view.WorldPosition);

            SingletonBehavior<DiceEffectManager>.Instance
                .CreateCreatureEffect("1/MatchGirl_Footfall", 3f, base.owner.view, null, 2f)
                .AttachEffectLayer();

            BattleCamManager.Instance?.StartCoroutine(CommonCoroutine.EarthQuake());

            base.owner.Die();

            foreach (var unit in BattleObjectManager.instance.GetAliveList())
            {
                if (unit == base.owner)
                {
                    continue;
                }

                unit.TakeDamage(20, DamageType.Passive, base.owner);

                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 20, base.owner);
            }
        }
    }
}
