using DeviceOfHermes.CustomDice;

public class PassiveAbility_TundraPassivePack_EqualDuel : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        base.owner.allyCardDetail.AddNewCardToDeck(new LorId(TundraPassivePack.packageId, 2));
    }
}

public class DiceCardAbility_TundraPassivePack_EqualDuel_1 : EqualDice
{
    public override string[] Keywords => ["Tundra_EqualDice"];

    public static string Desc = "公平ダイス".SizeAbs(60).Hex("#9696FF") + "\n[マッチ開始] 威力が10増加";

    public override void BeforeRollDice()
    {
        if (base.behavior.TargetDice is not null)
        {
            base.behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 10 });
        }
    }
}

public class DiceCardAbility_TundraPassivePack_EqualDuel_Etc : EqualDice
{
    public override string[] Keywords => ["Tundra_EqualDice"];

    public static string Desc = "公平ダイス".SizeAbs(60).Hex("#9696FF");
}
