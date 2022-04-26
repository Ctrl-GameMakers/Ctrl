using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct UnitData
{
    private int _id;
    private string _nameKor;

    private float _maxHP;
    private float _maxMP;

    private float _attackPower;
    private float _spellPower;
    private float _defence;
    private float _spellResistance;

    private float _criticalChance;
    private float _criticalMultiplier;

    private int _normalSkillID;
    private int _specialSkillID;

    private GameObject _unitVisualObjectPrefab;

    public int id { get => _id; }
    public string nameKor { get => _nameKor; }

    public float maxHP { get => _maxHP; }
    public float maxMP { get => _maxMP; }

    public float attackPower { get => _attackPower; }
    public float spellPower { get => _spellPower; }
    public float defence { get => _defence; }
    public float spellResistance { get => _spellResistance; }

    public float criticalChance { get => _criticalChance; }
    public float criticalMultiplier { get => _criticalMultiplier; }

    public int normalSkillID { get => _normalSkillID; }
    public int specialSkillID { get => _specialSkillID; }

    public GameObject unitVisualObjectPrefab { get => _unitVisualObjectPrefab; }



    public UnitData SetUnitData(Dictionary<string, object> _unitData)
    {
        SetValue(ref _id, _unitData["ID"]);
        _nameKor = _unitData["NameKor"].ToString();

        SetValue(ref _maxHP, _unitData["MaxHP"]);
        SetValue(ref _maxMP, _unitData["MaxMP"]);

        SetValue(ref _attackPower, _unitData["AttackPower"]);
        SetValue(ref _spellPower, _unitData["SpellPower"]);
        SetValue(ref _defence, _unitData["Defence"]);
        SetValue(ref _spellResistance, _unitData["SpellResistance"]);

        SetValue(ref _criticalChance, _unitData["CriticalChance"]);
        SetValue(ref _criticalMultiplier, _unitData["CriticalMultiplier"]);

        SetValue(ref _normalSkillID, _unitData["NormalSkillID"]);
        SetValue(ref _specialSkillID, _unitData["SpecialSkillID"]);

        SetValue(ref _unitVisualObjectPrefab, _unitData["UnitVisualObjectPrefab"]);

        return this;
    }

    private void SetValue(ref int variableName, object dataName)
    {
        if (!dataName.ToString().Equals("-"))
        {
            variableName = (int)dataName;
        }
    }
    private void SetValue(ref float variableName, object dataName)
    {
        if (!dataName.ToString().Equals("-"))
        {
            variableName = (float)dataName;
        }
    }

    private void SetValue(ref GameObject variableName, object dataName)
    {
        if (!dataName.ToString().Equals("-"))
        {
            variableName = Resources.Load("UnitPrefab/" + dataName.ToString()) as GameObject;
        }
    }
}


public class UnitDataBase : MonoBehaviour
{
    private Dictionary<int, UnitData> unitDB = new Dictionary<int, UnitData>();

    private void Awake()
    {
        List<Dictionary<string, object>> unitDataDialog = CSVReader.Read("DataBase/UnitDataBase");

        for (int i = 0; i < unitDataDialog.Count; i++)
        {
            var tempUnitData = new UnitData();
            tempUnitData = new UnitData().SetUnitData(unitDataDialog[i]);
            unitDB.Add((int)unitDataDialog[i]["ID"], tempUnitData);
        }

        //필요없는건 정리 정리
        for (int i = 0; i < unitDataDialog.Count; i++)
        {
            unitDataDialog[i].Clear();
        }
        unitDataDialog.Clear();

        Debug.Log($"세팅완료.");
    }

    public UnitData GetUnitData(int _id)
    {
        Debug.Log($"{_id} 요청들어옴.");
        return unitDB[_id];
    }
}
