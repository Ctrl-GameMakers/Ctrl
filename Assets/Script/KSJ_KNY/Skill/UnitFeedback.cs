using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFeedback : MonoBehaviour
{
    List<StatusEffect> _effectList = new List<StatusEffect>();

    //���κ���
    float beforeTime;
    Coroutine statusEffect;

    [Header("Setting")]
    [SerializeField] bool isTermFix;
    [Range(0.1f, 1.0f)]
    [SerializeField] float applyTerm;

    [Header("Required Component")]
    [SerializeField] UnitStatus _unitStatus;
    private void Start()
    {
        _unitStatus = GetComponent<UnitStatus>();
    }

    //������� ��� ����
    public void Damage(float amount, SkillEffectType skillDamageType)
    {
        if(skillDamageType.HasFlag(SkillEffectType.Physic))
        {
            //Debug.Log($"���� �׽�Ʈ{amount} => {amount * (100.0f / (100.0f + _unitStatus.defence))}");
            _unitStatus.IncreaseHP(-(amount * (100.0f / (100.0f + _unitStatus.defence))));
            _unitStatus.IncreaseMP(5.0f);
            return;
        }

        if(skillDamageType.HasFlag(SkillEffectType.Magic))
        {
            //Debug.Log($"�ֹ����׷� �׽�Ʈ{amount} => {amount * (100.0f / (100.0f + _unitStatus.spellResistance))}");
            _unitStatus.IncreaseHP(-(amount * (100.0f / (100.0f + _unitStatus.spellResistance))));
            _unitStatus.IncreaseMP(5.0f);
            return;
        }
    }

    public void RecoveryHP(float amount)
    {
        Debug.Log($"ȸ���Ѵ�. {gameObject.GetInstanceID()} �� {amount}��ŭ ȸ���Ѵ�.");
        _unitStatus.IncreaseHP(amount);
    }

    public void RecoveryMP(float amount)
    {
        _unitStatus.IncreaseMP(amount);
    }


    //Dot�� StatusEffect List�� �߰�
    public void DamageOverTime(int casterInstanceID, float amount, float duration)
    {
        _effectList.Add(new DamageOverTime());
        _effectList[_effectList.Count - 1].SetValue(casterInstanceID, _unitStatus, amount, duration);
        _effectList[_effectList.Count - 1].SetActive(true);
        ActiveEffect(_effectList[_effectList.Count - 1]);
    }

    //StatusEffect List�� ����� �����̻� ����
    //����� startTime�� �������� ���ӽð��� �����ϰ�, �������� �����̻� ȿ���� ����
    IEnumerator StatusEffect()
    {
        beforeTime = Time.time;
        while(true)
        {  
            //���ӽð� �ٵ� �����̻� ����
            for (int i = 0; i < _effectList.Count; i++)
            {
                while((Time.time - _effectList[i].startTime) >= _effectList[i].effectDuration)
                {
                    _effectList[i].SetActive(false);
                    _effectList.RemoveAt(i);
                    if(i >= _effectList.Count)
                    {
                        break;
                    }
                }                
            }

            if(_effectList.Count.Equals(0))
            {
                statusEffect = null;
                break;
            }

            //���� �����̻� ������
            for (int i = 0; i < _effectList.Count; i++)
            {
                _effectList[i].Apply();
            }

            //�����̻� üũ �ð����� ����
            if (isTermFix)
            {
                yield return new WaitForSeconds(applyTerm + (beforeTime - Time.time));
                beforeTime += applyTerm;
            }
            else
            {
                yield return null;
            }
        }
    }

    //���� StatusEffect Coroutine�� ���������� �ʴٸ� ����
    //���� ��� ȿ���� ��Ÿ���� �ϴ� �����̻��� ��� StatusEffect Coroutine�� ������ ��� �ѹ� ��� ����
    void ActiveEffect(StatusEffect _effect)
    {
        if(statusEffect == null)
        {
            statusEffect = StartCoroutine(StatusEffect());
        }
        else
        {/*
            switch (_effect.effectType)
            {
                case SkillEffectType.Slow:
                    if (_unitStatus.nowDecreaseMoveSpeed < _effect.effectAmount)
                    {
                        _effect.Apply();
                    }
                    break;
                default:
                    break;
            }*/
        }    
    }

}

//======================================== �����̻� Ŭ���� (StatusEffect���� �پ��� �����̻� ���) ================================


abstract public class StatusEffect
{
    protected bool _active;
    protected float _startTime;
    protected SkillAddEffectType _effectType;
    protected UnitStatus _unitStatus;

    protected int _effectCasterInstanceID;
    protected float _effectAmount;
    protected float _effectDuration;

    public bool active { get => _active; }
    public float startTime { get => _startTime; }
    public SkillAddEffectType effectType { get => _effectType; }
    public UnitStatus unitStatus { get => _unitStatus; }
    public int casterInstanceID { get => _effectCasterInstanceID; }
    public float effectAmount { get => _effectAmount; }
    public float effectDuration { get => _effectDuration; }

    public void SetValue(int casterInstanceID, UnitStatus unitStatus, float amount, float duration)
    {        
        _effectCasterInstanceID = casterInstanceID;
        _unitStatus = unitStatus;
        _startTime = Time.time;
        _effectAmount = amount;
        _effectDuration = duration;
        SetType();
    }

    public abstract void SetType();
    public abstract void SetActive(bool _value);
    public abstract void Apply();
}


//DOT ����(StatusEffect ���)
public class DamageOverTime : StatusEffect
{
    private float _beforeTime;
    private int _damageCount;
    public float beforeTime { get => _beforeTime; }
    public int damageCount { get => _damageCount; }

    public int nowCount;

    public override void SetType()
    {
        _effectType = SkillAddEffectType.DamageOverTime;
    }
    public override void SetActive(bool _value)
    {
        if (!active.Equals(_value))
        {
            switch (_value)
            {
                case true:
                    _active = true;
                    _damageCount = (int)_effectDuration;
                    nowCount = 0;
                    //���� �ǵ�� ó���κ�
                    break;

                case false:
                    _active = false;
                    //���� �ǵ�� ����κ�
                    break;
            }
        }
    }
    public override void Apply()
    {
        if (startTime + nowCount <= Time.time)
        {
            if (_unitStatus.nowHP > 0.0f)
            {
                _unitStatus.IncreaseHP(-effectAmount);
            }

            nowCount++;
            if (nowCount >= damageCount)
            {
                SetActive(false);
            }
        }
    }
}
