using System.Collections;
using UnityEngine;
using DeviceOfHermes;

public class FarAreaEffect_Eazy : EasyAreaEffect
{
    protected override void GetAction_Easy(Unit attacker, Victims victims)
    {
        // ズームするやつ
        FollowUnit(attacker.faction.FaceTo());

        ChangeMotion(ActionDetail.Default);

        // 敵を1人ずつ取得する
        while (victims.TakeRandomOne(out var target))
        {
            // targetの前方に0秒で移動(=テレポート)
            MoveTo(Unit.Front(target), 0f);

            // 敵の方向を向く
            UpdateDirection(Unit.Center(target));

            // モーション変更
            ChangeMotion(ActionDetail.Slash);

            // 敵に向けてDiceEffectを再生
            PlayDiceEffect("FX_Mon_Argalia_Slash_Down_Small", target);

            // 音声を再生
            PlaySound("Battle/Blue_Argalria_Far_Atk1");

            // そのまま背後に0.1秒で突き抜ける
            // Easingが有効 → fastin-slowout
            MoveTo(Unit.Back(target), 0.1f, opts: MoveOpts.Easing);

            // 出目で勝利したならダメージを与える
            GiveDamage(target);

            // 攻撃ごとのクールタイム
            WaitForSecs(0.02f);
        }

        // 攻撃後0.5秒待機
        WaitForSecs(0.5f);

        // ズームもどす
        FollowUnit(Unit.All);
    }
}

public class FarAreaEffect_Test : FarAreaEffect
{
    public override bool HasIndependentAction => true;

    public override void Init(BattleUnitModel self, params object[] args)
    {
        base.Init(self);

        this.state = EffectState.Start;
        this.isRunning = false;

        BattleCamManager.Instance.FollowUnits(false, [self]);
    }

    public override bool ActionPhase(float deltaTime, BattleUnitModel attacker, List<BattleFarAreaPlayManager.VictimInfo> victims, ref List<BattleFarAreaPlayManager.VictimInfo> defenseVictims)
    {
        if (!_init)
        {
            _init = true;

            _actions.Push(Action(attacker, victims));
        }

        if (_elapsed >= _arriveTime)
        {
            _elapsed = 0f;
            _arriveTime = 0f;
        }
        else
        {
            _elapsed += deltaTime;

            return false;
        }

        if (_actions.TryPeek(out var top))
        {
            var res = top.MoveNext();

            if (top.Current is float ws)
            {
                _arriveTime = ws;
            }

            if (top.Current is IEnumerator nested)
            {
                _actions.Push(nested);
            }

            if (!res)
            {
                _actions.Pop();
            }
        }

        return _actions.Count == 0 && _arriveTime == 0f;
    }

    IEnumerator Action(BattleUnitModel attacker, List<BattleFarAreaPlayManager.VictimInfo> victims)
    {
        _remains.AddRange(victims.Filter(v => !v.unitModel.IsDead()));

        yield return null;

        while (_remains.Count != 0)
        {
            var target = RandomUtil.SelectOne(_remains);

            _sign = -_sign;

            var shift = new Vector3(HexagonalMapManager.Instance.tileSize * 4f * _self.view.transform.localScale.x / 1.5f, 0f, 0f) * (float)this._sign;

            var src = target.unitModel.view.WorldPosition + shift + shift;
            var dst = target.unitModel.view.WorldPosition - shift - shift;

            attacker.view.WorldPosition = src;

            attacker.UpdateDirection(target.unitModel.view.WorldPosition);
            attacker.view.charAppearance.ChangeMotion(ActionDetail.Slash);

            var resource = (RandomUtil.Range(0, 1) == 0) ? "FX_Mon_Argalia_Slash_Up" : "FX_Mon_Argalia_Slash_Down_Small";

            DiceEffectManager.Instance.CreateBehaviourEffect(resource, 1f, attacker.view, target.unitModel.view, 1f)?.SetLayer("Effect");

            if ((attacker?.currentDiceAction?.currentBehavior?.DiceResultValue ?? -1) > (target.unitModel?.currentDiceAction?.currentBehavior?.DiceResultValue ?? -1))
            {
                attacker?.currentDiceAction?.currentBehavior?.GiveDamage(target.unitModel);
                target.unitModel.view.charAppearance.ChangeMotion(ActionDetail.Damaged);
            }

            _remains.Remove(target);

            yield return Moving(attacker.view, src, dst, 1f);

            yield return 0.1f;

            if (_remains.All(v => v.unitModel.IsDead()))
            {
                break;
            }
        }

        attacker.moveDetail.Move(Vector3.zero, 200f, true, true);

        yield return 0.5f;

        BattleCamManager.Instance.FollowUnits(false, BattleObjectManager.instance.GetAliveList(false));
        UnityEngine.Object.Destroy(this);
    }

    IEnumerator Moving(BattleUnitView view, Vector3 src, Vector3 dst, float duration)
    {
        var elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            var t = elapsed / duration;

            view.WorldPosition = Vector3.Lerp(src, dst, t);

            yield return null;
        }
    }

    private bool _init;

    private Stack<IEnumerator> _actions = new();

    private List<BattleFarAreaPlayManager.VictimInfo> _remains = new();

    private float _elapsed;

    private float _arriveTime;

    private int _sign = 1;
}
