using System.Collections.Generic;
using Sound;
using UnityEngine;

public class PassiveAbility_Unlock : PassiveAbilityBase
{
    public override void OnRoundStartAfter()
    {
        if (base.owner == null)
        {
            return;
        }

        BattleUnitBufListDetail bufList = base.owner.bufListDetail;

        if (!bufList.HasBuf<BattleUnitBuf_GraceOfPrescript>())
        {
            return;
        }

        List<BattleUnitBuf> activeBufList = bufList.GetActivatedBufList();

        BattleUnitBuf_GraceOfPrescript grace = (BattleUnitBuf_GraceOfPrescript)activeBufList.Find(buf => buf is BattleUnitBuf_GraceOfPrescript);

        BattleUnitBuf_Unlock unlock = (BattleUnitBuf_Unlock)activeBufList.Find(buf => buf is BattleUnitBuf_Unlock);
        BattleUnitBuf_Unlock2 unlock2 = (BattleUnitBuf_Unlock2)activeBufList.Find(buf => buf is BattleUnitBuf_Unlock2);
        BattleUnitBuf_Unlock3 unlock3 = (BattleUnitBuf_Unlock3)activeBufList.Find(buf => buf is BattleUnitBuf_Unlock3);

        if (grace.stack == 9)
        {
            if (unlock != null)
            {
                unlock.Lock();
            }

            if (unlock2 != null)
            {
                unlock2.Lock();
            }

            if (unlock3 == null)
            {
                bufList.AddBuf(new BattleUnitBuf_Unlock3());
            }

            if (this._level < 3)
            {
                this.PlayUnlockParticle();
                SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Buf/Effect_Index_Unlock", false, 2f, null);

                if (this._auraSecond == null)
                {
                    this._auraSecond = CreateAura(false);
                }
            }

            this._level = 3;
        }
        else if (9 > grace.stack && grace.stack >= 6)
        {
            if (unlock != null)
            {
                unlock.Lock();
            }

            if (unlock2 == null)
            {
                bufList.AddBuf(new BattleUnitBuf_Unlock2());
            }

            if (unlock3 != null)
            {
                unlock3.Lock();
            }

            if (this._level < 2)
            {
                this.PlayUnlockParticle();
                SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Buf/Effect_Index_Unlock", false, 1.5f, null);

                if (this._aura == null)
                {
                    this._aura = this.CreateAura();
                }

                if (this.owner.customBook.BookId == new LorId(PowerfulEstherMOD.packageId, 1))
                {
                    this.SetAltMotion();
                }
            }

            this._level = 2;
        }
        else if (6 > grace.stack && grace.stack >= 3)
        {
            if (unlock == null)
            {
                bufList.AddBuf(new BattleUnitBuf_Unlock());
            }

            if (unlock2 != null)
            {
                unlock2.Lock();
            }

            if (unlock3 != null)
            {
                unlock3.Lock();
            }

            if (this._level < 1)
            {
                this.PlayUnlockParticle();
                SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Buf/Effect_Index_Unlock", false, 1f, null);
            }

            this._level = 1;
        }
        else
        {
            if (unlock != null)
            {
                unlock.Lock();
            }

            if (unlock2 != null)
            {
                unlock2.Lock();
            }

            if (unlock3 != null)
            {
                unlock3.Lock();
            }

            this._level = 0;
        }
    }

    private GameObject CreateAura(bool initial = true)
    {
        UnityEngine.Object @object = Resources.Load("Prefabs/Battle/SpecialEffect/IndexRelease_Aura");

        if (@object != null)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(@object) as GameObject;
            gameObject.transform.parent = this.owner.view.charAppearance.transform;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = Vector3.one;
            IndexReleaseAura component = gameObject.GetComponent<IndexReleaseAura>();
            if (component != null && initial)
            {
                component.Init(this.owner.view);
            }

            return gameObject;
        }

        return null;
    }

    private void PlayUnlockParticle()
    {
        UnityEngine.Object @object = Resources.Load("Prefabs/Battle/SpecialEffect/IndexRelease_ActivateParticle");

        if (@object != null)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(@object) as GameObject;
            gameObject.transform.parent = this.owner.view.charAppearance.transform;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = Vector3.one;
        }
    }

    private void SetAltMotion()
    {
        this.owner.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.Hit2);
        this.owner.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.Slash2);
        this.owner.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.Penetrate2);
    }

    private int _level = 0;

    private GameObject _aura;

    private GameObject _auraSecond;
}
