using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkillDataBase))]
public class SkillManager : MonoBehaviour
{
    [Header("Required Component")]
    [SerializeField] SkillDataBase _skillDataBase;
    //[SerializeField] ProjectilePoolMgr _projectilePoolMgr;
    [SerializeField] JudgmentObjectPool _judgmentObjectPool;


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

        if (GetComponentInChildren<JudgmentObjectPool>() == null)
        {
            GameObject newGameObject = new GameObject("@JudgmentObjectPool");
            newGameObject.GetComponent<Transform>().parent = transform;
            newGameObject.AddComponent<JudgmentObjectPool>();
        }

        _judgmentObjectPool = GetComponentInChildren<JudgmentObjectPool>();

    }

    public void UseSkill(int skillID, int _casterInstanceID, int _targetInstanceID)
    {
        //Debug.Log($"{_casterInstanceID}�� {_targetInstanceID}���� {skillID}�� �����.");

        switch (GetSkillData(skillID).skillCenterPoint)
        {
            case SkillCenterPoint.Target:
                _judgmentObjectPool.CallSkillObject(skillID, _casterInstanceID, _targetInstanceID);
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
