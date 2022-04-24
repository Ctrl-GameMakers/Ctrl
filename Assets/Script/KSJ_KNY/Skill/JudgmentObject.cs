using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentObject : MonoBehaviour
{
    bool isTrial = true;

    Transform tr;
    JudgmentObjectPoolMgr judgmentObjectPoolMgr;

    private int skillID;
    private int casterInstanceID;
    private int targetInstanceID;

    private List<int> judgmentTargetsInstanceID = new List<int>();
    private IEnumerator _judgmentDelay;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        judgmentObjectPoolMgr = GetComponentInParent<JudgmentObjectPoolMgr>();
    }

    //최초로 만들어 졌을 경우에만 자동으로 비활성화 (최초 Pool제작 시)
    //그 외에는 기존 계산해 두었던 타겟리스트 초기화
    private void OnEnable()
    {
        if (!isTrial)
        {
            judgmentTargetsInstanceID.Clear();
        }
    }

    private void Start()
    {
        if(isTrial)
        {
            isTrial = false;
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ActiveSkill(int skillID, int casterInstanceID, int targetInstanceID)
    {
        gameObject.SetActive(true);

        this.skillID = skillID;
        this.casterInstanceID = casterInstanceID;
        this.targetInstanceID = targetInstanceID;

        if (SkillManager.Instance.GetSkillData(skillID).judgmentTime.Equals(0.0f))
        {
            FindJudgmentTarget();
        }
        else
        {
            _judgmentDelay = JudgmentDelay();
            StartCoroutine(_judgmentDelay);
        }
    }

    //판정 딜레이만큼 대기 후 판정 진행
    IEnumerator JudgmentDelay()
    {
        Debug.Log("오긴 하니?");
        if(SkillManager.Instance.GetSkillData(skillID).skillCenterPoint.HasFlag(SkillCenterPoint.OfStartTime))
        {
            if (SkillManager.Instance.GetSkillData(skillID).skillCenterPoint.HasFlag(SkillCenterPoint.TargetLocation))
                tr.position = UnitManager.Instance.GetUnitPosition(targetInstanceID);
            else
                Debug.Log("error : can't cope this skillcenterpoint ");

            yield return new WaitForSeconds(SkillManager.Instance.GetSkillData(skillID).judgmentTime);

            FindJudgmentTarget();            
        }
        else if(SkillManager.Instance.GetSkillData(skillID).skillCenterPoint.HasFlag(SkillCenterPoint.OfJudgmentTime))
        {
            yield return new WaitForSeconds(SkillManager.Instance.GetSkillData(skillID).judgmentTime);

            if (SkillManager.Instance.GetSkillData(skillID).skillCenterPoint.HasFlag(SkillCenterPoint.TargetLocation))
                tr.position = UnitManager.Instance.GetUnitPosition(targetInstanceID);
            else
                Debug.Log("error : can't cope this skillcenterpoint ");

            FindJudgmentTarget();
        }
        else if (SkillManager.Instance.GetSkillData(skillID).skillCenterPoint.HasFlag(SkillCenterPoint.Target))
        {
            tr.position = UnitManager.Instance.GetUnitPosition(targetInstanceID);
            yield return new WaitForSeconds(SkillManager.Instance.GetSkillData(skillID).judgmentTime);

            CalculationJudgment();
        }
    }

    //스킬정보 참조해서 범위 내 타겟 판정
    private void FindJudgmentTarget()
    {
        switch (SkillManager.Instance.GetSkillData(skillID).areaForm)
        {
            case SkillAreaForm.Circle:
                var tempJudgmentTargets = Physics2D.OverlapCircleAll(transform.position, SkillManager.Instance.GetSkillData(skillID).areaLength);
                for (int i = 0; i < tempJudgmentTargets.Length; i++)
                {
                    if (!tempJudgmentTargets[i].gameObject.GetInstanceID().Equals(casterInstanceID))
                    {
                        judgmentTargetsInstanceID.Add(tempJudgmentTargets[i].gameObject.GetInstanceID());
                    }
                }
                //CalculationJudgments(skillID, judgmentTargets);
                break;
            default:
                break;
        }
    }

    //판정 후 효과처리 요청
    //타겟에게 처리되는 효과의 경우 CalculationJudgmentEffect에 처리 요청
    //기타 부가효과 발생 시, 해당 함수에서 처리
    private void CalculationJudgment()
    {
        JudgmentCalculater.Instance.CalculationJudgment(skillID, casterInstanceID, targetInstanceID);

        gameObject.SetActive(false);
    }
    

    private void CalculationJudgments(int _id, List<Collider2D> _targetList)
    {
        JudgmentCalculater.Instance.CalculationJudgment(skillID, casterInstanceID, targetInstanceID);

        gameObject.SetActive(false);
    }
    


    //비활성화 시 저장된 정보 삭제
    private void OnDisable()
    {
        skillID = 0;
        casterInstanceID = 0;

        if(_judgmentDelay != null)
        {
            StopCoroutine(_judgmentDelay);
            _judgmentDelay = null;
        }

        judgmentObjectPoolMgr.EnqueueObject(this);
    }
}
