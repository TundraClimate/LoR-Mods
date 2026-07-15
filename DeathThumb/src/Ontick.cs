internal static class Ontick
{
    internal static void Init()
    {
        BattleTickAction.OnTick += OnTick;
    }

    static void OnTick()
    {
    }
}
