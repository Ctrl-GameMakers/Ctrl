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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"{_skillDataBase.GetSkillData(1111).targetGroup} {_skillDataBase.GetSkillData(1111).skillDuration} {_skillDataBase.GetSkillData(1234).id}");
        }
    }


    //��ų Ÿ��(����, ����ü ��)�� ���� ���õ� ������Ʈ�� �����ϴ� PoolMgr�� ��û
    public void UseSkill(int _id, int _casterInstanceID, Vector3 _pivotPosition, Quaternion _pivotRotation)
    {
        if(_skillDataBase.GetSkillData(_id).areaForm.HasFlag(SkillAreaForm.Area))
        {
            _judgmentObjectPoolMgr.ActiveSkill(_id, _casterInstanceID, _pivotPosition, _pivotRotation);
        }
        else if(_skillDataBase.GetSkillData(_id).areaForm.HasFlag(SkillAreaForm.Projectile))
        {
            //_projectilePoolMgr.ActiveSkill(_id, _casterInstanceID, _pivotPosition, _pivotRotation);
        }
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
                break;
        }
        /*
        if (_skillDataBase.GetSkillData(_id).areaForm.HasFlag(SkillAreaForm.Area))
        {
            _judgmentObjectPoolMgr.ActiveSkill(_id, _casterInstanceID, _pivotPosition, _pivotRotation);
        }
        else if (_skillDataBase.GetSkillData(_id).areaForm.HasFlag(SkillAreaForm.Projectile))
        {
            //_projectilePoolMgr.ActiveSkill(_id, _casterInstanceID, _pivotPosition, _pivotRotation);
        }*/
    }



    //SkillDB�� ���� �ܺο� ��ų���� ��ȯ 
    public SkillData GetSkillData(int _id)
    {
        return _skillDataBase.GetSkillData(_id);
    }



}
