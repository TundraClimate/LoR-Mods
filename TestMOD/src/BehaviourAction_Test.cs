using UnityEngine;

public class BehaviourAction_Test : BehaviourActionBase
{
    public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
    {
        var effect = new GameObject().AddComponent<FarAreaEffect_Eazy>();

        effect.Init(self, []);

        return effect;
    }
}
