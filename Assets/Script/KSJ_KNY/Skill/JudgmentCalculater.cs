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
    
    public void CalculationJudgment(int skillID, int casterInstanceID, int judgmentTargetInstanceID, float damageAmount)
    {
        //데미지 적용
        if (!damageAmount.Equals(0.0f))
        {
            if(UnitManager.Instance.GetUnitStatus(casterInstanceID).criticalChance > 0.0f &&  Random.Range(0.0f, 1.0f) >= UnitManager.Instance.GetUnitStatus(casterInstanceID).criticalChance)
            {
                Debug.Log("치명타!!!!!!");
                FeedbackManager.Instance.Damage(casterInstanceID, judgmentTargetInstanceID, damageAmount * UnitManager.Instance.GetUnitStatus(casterInstanceID).criticalMultiplier);
            }
            else
            {
                FeedbackManager.Instance.Damage(casterInstanceID, judgmentTargetInstanceID, damageAmount);
            }
            //PlayerManager.Instance.PlayerDamage(skillID, casterInstanceID, _target.gameObject.GetInstanceID());
        }
    }

    public float AttackDamageCalculater(int skillID, int casterInstanceID)
    {
        switch (SkillManager.Instance.GetSkillData(skillID).applyStatus)
        {
            case SkillApplyStatus.AttackPower:
                return (UnitManager.Instance.GetUnitStatus(casterInstanceID).attackPower * SkillManager.Instance.GetSkillData(skillID).coefficientAmount) 
                    + SkillManager.Instance.GetSkillData(skillID).baseAmount;

            case SkillApplyStatus.SpellPower:
                return (UnitManager.Instance.GetUnitStatus(casterInstanceID).spellPower * SkillManager.Instance.GetSkillData(skillID).coefficientAmount)
                    + SkillManager.Instance.GetSkillData(skillID).baseAmount;

            default:
                Debug.Log("error : This applystatus type is notting");
                return 0.0f;
        }
    }
}
