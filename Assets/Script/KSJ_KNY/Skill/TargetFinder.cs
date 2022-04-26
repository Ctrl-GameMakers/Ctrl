using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct InstanceIDContainer
{
    private int _targetInstanceID;
    private bool _isExist;

    public int targetInstanceID { get => _targetInstanceID; }
    public bool isExist { get => _isExist; }

    public InstanceIDContainer(int targetInstanceID, bool isExist) { _targetInstanceID = targetInstanceID; _isExist = isExist; }
}


public class TargetFinder : MonoBehaviour
{
    public struct TargetInfo
    {
        public Transform transform;
        public bool targetFind;

        public void Clear()
        {
            transform = null;
            targetFind = false;
        }
    }

    bool _chkGroup;
    bool _findTarget;

    int _targetInstanceID;

    TargetInfo tempTargetFind;
    float value;

    public InstanceIDContainer TargetUnitFinder(int myinstanceID, Dictionary<int, UnitManager.UnitInformation> unitDic, int targetRange, SkillUnitGroup targetGroup, SkillUnitSortingBase sortingBase)
    {
        //�������� �ʱ�ȭ
        tempTargetFind.Clear();
        _findTarget = false;
        _targetInstanceID = 0;

        if (sortingBase.HasFlag(SkillUnitSortingBase.Ascending))
            value = 999999.0f;
        else
            value = 0;


        foreach (KeyValuePair<int, UnitManager.UnitInformation> unitsPair in unitDic)
        {
            if (!UnitManager.Instance.GetBattleUnitIsDeath(unitsPair.Key))
            {
                //Ÿ�� Ž���������� �ָ� �ִٸ� Continue
                if (Mathf.Abs(unitsPair.Value.transform.position.x - unitDic[myinstanceID].transform.position.x) > targetRange ||
                    Mathf.Abs(unitsPair.Value.transform.position.z - unitDic[myinstanceID].transform.position.z) > targetRange)
                    continue;


                //Ÿ�� ������ �������� �ʴٸ� Continue
                _chkGroup = false;

                switch (targetGroup)
                {
                    case SkillUnitGroup.Ally:
                        _chkGroup = unitsPair.Value.unit.tag.Equals(unitDic[myinstanceID].unit.tag);
                        break;

                    case SkillUnitGroup.Enemy:
                        _chkGroup = !unitsPair.Value.unit.tag.Equals(unitDic[myinstanceID].unit.tag);
                        break;

                    default:
                        break;
                }

                if (!_chkGroup)
                    continue;


                //������� ������ ��찡 �ִٸ� �ּ��� Ÿ���� �˻��Ǿ��� �Ǵ�
                _findTarget = true;

                //�������ǿ� ���� ����Ÿ�� �˻�
                switch(sortingBase)
                {
                    case SkillUnitSortingBase.DistanceAscending:
                        if (value > Mathf.Abs(unitsPair.Value.transform.position.x - unitDic[myinstanceID].transform.position.x) + Mathf.Abs(unitsPair.Value.transform.position.z - unitDic[myinstanceID].transform.position.z))
                        {
                            value = Mathf.Abs(unitsPair.Value.transform.position.x - unitDic[myinstanceID].transform.position.x) + Mathf.Abs(unitsPair.Value.transform.position.z - unitDic[myinstanceID].transform.position.z);
                            _targetInstanceID = unitsPair.Key;
                        }
                        break;

                    case SkillUnitSortingBase.DistanceDecending:
                        if (value < Mathf.Abs(unitsPair.Value.transform.position.x - unitDic[myinstanceID].transform.position.x) + Mathf.Abs(unitsPair.Value.transform.position.z - unitDic[myinstanceID].transform.position.z))
                        {
                            value = Mathf.Abs(unitsPair.Value.transform.position.x - unitDic[myinstanceID].transform.position.x) + Mathf.Abs(unitsPair.Value.transform.position.z - unitDic[myinstanceID].transform.position.z);
                            _targetInstanceID = unitsPair.Key;

                        }
                        break;

                    case SkillUnitSortingBase.HealthPointAscending:
                        if(value > unitsPair.Value.unitStatus.nowHP)
                        {
                            value = unitsPair.Value.unitStatus.nowHP;
                            _targetInstanceID = unitsPair.Key;
                        }
                        break;

                    case SkillUnitSortingBase.HealthPointDecending:
                        if (value < unitsPair.Value.unitStatus.nowHP)
                        {
                            value = unitsPair.Value.unitStatus.nowHP;
                            _targetInstanceID = unitsPair.Key;
                        }
                        break;

                    default:
                        break;

                }
            }
        }

        return new InstanceIDContainer(_targetInstanceID, _findTarget);
    }


}
