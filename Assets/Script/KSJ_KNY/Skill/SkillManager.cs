using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Header("Required Component")]
    [SerializeField] SkillDataBase _skillDataBase;
    //[SerializeField] ProjectilePoolMgr _projectilePoolMgr;
    [SerializeField] JudgmentObjectPoolMgr _judgmentObjectPoolMgr;


    //singleton
    private static SkillManager mInstance;
    public static SkillManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<SkillManager>();
            }
            return mInstance;
        }
    }
    
    public void Awake()
    {
        _skillDataBase = GetComponent<SkillDataBase>();
        //_projectilePoolMgr = GetComponentInChildren<ProjectilePoolMgr>();
        _judgmentObjectPoolMgr = GetComponentInChildren<JudgmentObjectPoolMgr>();
    }

    public void UseSkill(int skillID, int _casterInstanceID, int _targetInstanceID)
    {
        Debug.Log($"{_casterInstanceID}�� {_targetInstanceID}���� {skillID}�� �����.");

        switch (GetSkillData(skillID).skillCenterPoint)
        {
            case SkillCenterPoint.Target:
                _judgmentObjectPoolMgr.ActiveSkill(skillID, _casterInstanceID, _targetInstanceID);
                break;
                
            case SkillCenterPoint.TargetLocation:
                break;

            default:
                Debug.Log("error : This skillcenterpoint type is notting.");
                break;
        }
    }

    //SkillDB�� ���� �ܺο� ��ų���� ��ȯ 
    public SkillData GetSkillData(int _id) { return _skillDataBase.GetSkillData(_id); }
}
