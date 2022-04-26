using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    // Start is called before the first frame update
    private UnitController _unitController;

    [SerializeField] private int _unitID;

    [SerializeField] private float _maxHP;
    [SerializeField] private float _nowHP;
    [SerializeField] private float _maxMP;
    [SerializeField] private float _nowMP;

    [SerializeField] private float _attackPower;
    [SerializeField] private float _spellPower;
    [SerializeField] private float _defence;
    [SerializeField] private float _spellResistance;

    [SerializeField] private float _criticalChance;
    [SerializeField] private float _criticalMultiplier;

    [SerializeField] private int _normalSkillID;
    [SerializeField] private int _specialSkillID;

    public int unitID { get => _unitID; }
    public float maxHP { get => _maxHP; }
    public float nowHP { get => _nowHP; }
    public float maxMP { get => _maxMP; }
    public float nowMP { get => _nowMP; }

    public float attackPower { get => _attackPower; }
    public float spellPower { get => _spellPower; }
    public float defence { get => _defence; }
    public float spellResistance { get => _spellResistance; }

    public float criticalChance { get => _criticalChance; }
    public float criticalMultiplier { get => _criticalMultiplier; }

    public int normalSkillID { get => _normalSkillID; }
    public int specialSkillID { get => _specialSkillID; }


    private UnitData unitBaseData;
    [SerializeField] private GameObject unitModelingObject;

    private bool _settingComplete;
    public bool settingComplete { get => _settingComplete; }


    void Awake()
    {
        _unitController = GetComponent<UnitController>();
    }

    private void Start()
    {
        SetUnitBaseStatus(UnitManager.Instance.GetUnitData(_unitID));
    }

    public void IncreaseHP(float amount)
    {
        if (nowHP + amount > 0)
        {
            if(nowHP + amount >= maxHP)
                _nowHP = maxHP;
            else
                _nowHP += amount;
        }
        else
        {
            _nowHP = 0.0f;
            _unitController.Death();
        }
    }

    public void IncreaseMP(float amount)
    {
        if (nowMP + amount > maxMP)
        {
            _nowMP = maxMP;
        }
        else if (nowMP + amount < 0)
        {
            _nowMP = 0.0f;
        }
        else
        {
            _nowMP += amount;
        }
    }

    public void ResetNowMP(float value)
    {
        if (value > maxMP)
        {
            _nowMP = maxMP;
        }
        else if (value < 0)
        {
            _nowMP = 0.0f;
        }
        else
        {
            _nowMP = value;
        }
    }

    public void SetUnitBaseStatus(UnitData unitData)
    {
        if(!_settingComplete)
        {
            if (!unitBaseData.id.Equals(unitData.id))
            {
                unitBaseData = unitData;
            }

            _maxHP = unitBaseData.maxHP;
            _nowHP = maxHP;

            _maxMP = unitBaseData.maxMP;
            _nowMP = 0.0f;

            _attackPower = unitBaseData.attackPower;
            _spellPower = unitBaseData.spellPower;
            _defence = unitBaseData.defence;
            _spellResistance = unitBaseData.spellResistance;

            _criticalChance = unitBaseData.criticalChance;
            _criticalMultiplier = unitBaseData.criticalMultiplier;

            _normalSkillID = unitBaseData.normalSkillID;
            _specialSkillID = unitBaseData.specialSkillID;

            unitModelingObject = Instantiate(unitBaseData.unitVisualObjectPrefab, transform);
            unitModelingObject.transform.localPosition = Vector3.zero;
            unitModelingObject.transform.localRotation = Quaternion.identity;

            _settingComplete = true;
        }      
    }
}
