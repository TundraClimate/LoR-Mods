public class DiceCardSelfAbility_TheHealBreakLifeSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[戦闘終了前] このページの反撃ダイスが残っているなら、指令対象に攻撃ダイスとして使用する。";

    public override void OnStartBattle()
    {
        if (base.owner != null && !base.owner.bufListDetail.HasBuf<BattleUnitBuf_TheHealBreakLifeHold>())
        {
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_TheHealBreakLifeHold());
        }
    }

    private class BattleUnitBuf_TheHealBreakLifeHold : BattleUnitBuf
    {
        public override bool Hide
        {
            get
            {
                return true;
            }
        }

        public override void OnRollDice(BattleDiceBehavior behavior)
        {
            if (base._owner != null && behavior != null)
            {
                if (!behavior.card.isKeepedCard)
                {
                    return;
                }

                if (behavior.abilityList.Exists(abi => abi is DiceCardAbility_TheHealBreakLifeD3) || behavior.abilityList.Exists(abi => abi is DiceCardAbility_TheHealBreakLifeD4))
                {
                    this._startConnectDice = behavior;
                }
            }
        }

        public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
        {
            if (this._startConnectDice == null)
            {
                return;
            }

            BattleKeepedCardDataInUnitModel keepCard = base._owner.cardSlotDetail.keepCard;

            if (keepCard == null)
            {
                keepCard = new BattleKeepedCardDataInUnitModel(base._owner);
            }

            keepCard.AddDiceFront(this._startConnectDice);

            this._startConnectDice = null;
        }

        public override void OnRoundEnd()
        {
            base.Destroy();
        }

        private BattleDiceBehavior _startConnectDice = null;
    }
}
