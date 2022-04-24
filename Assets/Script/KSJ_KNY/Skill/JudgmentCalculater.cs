using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentCalculater : MonoBehaviour
{
    //singleton
    private static JudgmentCalculater mInstance;
    public static JudgmentCalculater Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<JudgmentCalculater>();
            }
            return mInstance;
        }
    }
    
    public void CalculationJudgment(int skillID, int casterInstanceID, int judgmentTargetInstanceID)
    {
        //������ ����
        if (!SkillManager.Instance.GetSkillData(skillID).baseDamage.Equals(0.0f))
        {
            Debug.Log($"{casterInstanceID}�� {judgmentTargetInstanceID}���� �ϴ�{SkillManager.Instance.GetSkillData(skillID).baseDamage}�� �����");

            //PlayerManager.Instance.PlayerDamage(skillID, casterInstanceID, _target.gameObject.GetInstanceID());
        }
    }
}
