using WarpClassFive;

namespace WarpClassFive_Passive
{
    public class PassiveAbility_Class5 : PassiveAbilityBase
    {
        public override void OnRoundStart()
        {
            this.owner.allyCardDetail.ExhaustAllCards();

            int pat = this._elapsed % 3;

            if (pat == 0) {
                this._diceAdder = 3;
                
                this.AddNewCard(6);
                this.AddNewCard(5);
                this.AddNewCard(5);
                this.AddNewCard(3);
                this.AddNewCard(2);
            }

            if (pat == 1) {
                this._diceAdder = 2;

                this.AddNewCard(4);
                this.AddNewCard(4);
                this.AddNewCard(1);
                this.AddNewCard(2);
            }

            if (pat == 2) {
                this._diceAdder = 2;
                
                this.AddNewCard(3);
                this.AddNewCard(3);
                this.AddNewCard(1);
                this.AddNewCard(2);
            }

            this._elapsed++;
        }

        public override int SpeedDiceNumAdder()
        {
            return this._diceAdder;
        }

        private void AddNewCard(int id)
        {
            BattleDiceCardModel battleDiceCardModel = this.owner.allyCardDetail.AddTempCard(new LorId(WarpClassFiveMod.packageId, id));
            if (battleDiceCardModel != null)
            {
               battleDiceCardModel.SetCostToZero(true);
            }
        }

        private int _elapsed = 0;
        private int _diceAdder = 2;
    }
}
