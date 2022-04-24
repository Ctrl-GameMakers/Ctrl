using System.Collections;
using System.Collections.Generic;
using UnityEngine;




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

    public Transform TargetUnitFinder(int myinstanceID, Dictionary<int, UnitManager.UnitInformation> dic, int targetRange, SkillUnitGroup targetGroup, SkillUnitSortingBase sortingBase)
    {
        //�������� �ʱ�ȭ
        tempTargetFind.Clear();
        _findTarget = false;
        _targetInstanceID = 0;

        if (sortingBase.HasFlag(SkillUnitSortingBase.Ascending))
            value = 999999.0f;
        else
            value = 0;


        foreach (KeyValuePair<int, UnitManager.UnitInformation> testDic in dic)
        {
            if (testDic.Key != myinstanceID)
            {
                Debug.Log(myinstanceID);
                //Ÿ�� Ž���������� �ָ� �ִٸ� Continue
                if (Mathf.Abs(testDic.Value.transform.position.x - dic[myinstanceID].transform.position.x) > targetRange ||
                    Mathf.Abs(testDic.Value.transform.position.z - dic[myinstanceID].transform.position.z) > targetRange)
                    continue;


                //Ÿ�� ������ �������� �ʴٸ� Continue
                _chkGroup = false;

                switch (targetGroup)
                {
                    case SkillUnitGroup.Ally:
                        _chkGroup = testDic.Value.unit.tag.Equals(dic[myinstanceID].unit.tag);
                        break;

                    case SkillUnitGroup.Enemy:
                        _chkGroup = !testDic.Value.unit.tag.Equals(dic[myinstanceID].unit.tag);
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
                        if (value > Mathf.Abs(testDic.Value.transform.position.x - dic[myinstanceID].transform.position.x) + Mathf.Abs(testDic.Value.transform.position.z - dic[myinstanceID].transform.position.z))
                        {
                            value = Mathf.Abs(testDic.Value.transform.position.x - dic[myinstanceID].transform.position.x) + Mathf.Abs(testDic.Value.transform.position.z - dic[myinstanceID].transform.position.z);
                            _targetInstanceID = testDic.Key;
                        }
                        break;

                    case SkillUnitSortingBase.DistanceDecending:
                        if (value < Mathf.Abs(testDic.Value.transform.position.x - dic[myinstanceID].transform.position.x) + Mathf.Abs(testDic.Value.transform.position.z - dic[myinstanceID].transform.position.z))
                        {
                            value = Mathf.Abs(testDic.Value.transform.position.x - dic[myinstanceID].transform.position.x) + Mathf.Abs(testDic.Value.transform.position.z - dic[myinstanceID].transform.position.z);
                            _targetInstanceID = testDic.Key;

                        }
                        break;

                    case SkillUnitSortingBase.HealthPointAscending:
                        if(value > testDic.Value.unitStatus.nowHP)
                        {
                            value = testDic.Value.unitStatus.nowHP;
                            _targetInstanceID = testDic.Key;
                        }
                        break;

                    case SkillUnitSortingBase.HealthPointDecending:
                        if (value < testDic.Value.unitStatus.nowHP)
                        {
                            value = testDic.Value.unitStatus.nowHP;
                            _targetInstanceID = testDic.Key;
                        }
                        break;

                    default:
                        break;

                }
            }
        }

        if (_findTarget)
        {
            return dic[_targetInstanceID].transform;
        }
        else
            return null;
    }


}
