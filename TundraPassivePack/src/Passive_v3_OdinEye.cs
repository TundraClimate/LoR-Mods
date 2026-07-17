using DeviceOfHermes.CustomDice;
using UnityEngine;

public class PassiveAbility_TundraPassivePack_OdinEye : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        base.owner?.AddBufStack<BattleUnitBuf_OdinEye>(0);
    }

    public class BattleUnitBuf_OdinEye : AdvancedUnitBuf
    {
        protected override string keywordId => !overheated ? "Tundra_OdinEye" : "Tundra_OdinEye_Heat";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);

            base.stack = 30;
        }

        public override void OnRoundStart()
        {
            if (overheated)
            {
                ChangeStack(10);
            }
        }

        public override BattlePlayingCardDataInUnitModel? BeforeTakeOneSideAction(BattleUnitModel actor)
        {
            if (!overheated)
            {
                ChangeStack(-3);

                return base._owner.CreatePlayingCard(ItemXmlDataList.instance.GetCardItem(new LorId(TundraPassivePack.packageId, 1)), actor);
            }

            return null;
        }

        public override void OnStartParrying(BattlePlayingCardDataInUnitModel card)
        {
            if (card.card.XmlData.id == new LorId(TundraPassivePack.packageId, 1) || overheated)
            {
                return;
            }

            _reserve = true;

            var playcard = base._owner.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(new LorId(TundraPassivePack.packageId, 1)),
                card.target
            );

            SecondlyDice.AddSecondlyDice(card, playcard.cardBehaviorQueue.ToList());
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            if (_reserve && !overheated)
            {
                ChangeStack(-5);

                _reserve = false;
            }
        }

        public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
        {
            _reserve = false;
            _uses = false;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (_reserve && !overheated && !_uses && behavior.abilityList.Exists(abi => abi is SecondlyDice))
            {
                ChangeStack(-3);

                _uses = true;
            }
        }

        private void ChangeStack(int add)
        {
            base.stack += add;

            base.stack = Mathf.Clamp(base.stack, 0, 30);

            if (overheated && base.stack == 30)
            {
                overheated = false;
            }
            else if (!overheated && stack == 0)
            {
                overheated = true;
            }
        }

        private bool _reserve;

        private bool _uses;

        public bool overheated;
    }
}

public class DiceCardAbility_TundraPassivePack_Secondly : SecondlyDice
{
    public override void BeforeRollDice()
    {
        var buf = base.owner?.GetBuf<PassiveAbility_TundraPassivePack_OdinEye.BattleUnitBuf_OdinEye>();

        if (buf is not null && !buf.overheated)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus { min = buf.stack / 5, max = buf.stack / 5 });
        }
    }
}
