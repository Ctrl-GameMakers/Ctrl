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
        //데미지 적용
        if (!SkillManager.Instance.GetSkillData(skillID).baseDamage.Equals(0.0f))
        {
            Debug.Log($"{casterInstanceID}가 {judgmentTargetInstanceID}에게 일단{SkillManager.Instance.GetSkillData(skillID).baseDamage}의 대미지");

            //PlayerManager.Instance.PlayerDamage(skillID, casterInstanceID, _target.gameObject.GetInstanceID());
        }
    }
}
