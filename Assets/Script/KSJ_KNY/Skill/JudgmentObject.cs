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

    //���ʷ� ����� ���� ��쿡�� �ڵ����� ��Ȱ��ȭ (���� Pool���� ��)
    //�� �ܿ��� ���� ����� �ξ��� Ÿ�ٸ���Ʈ �ʱ�ȭ
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

    //���� �����̸�ŭ ��� �� ���� ����
    IEnumerator JudgmentDelay()
    {
        Debug.Log("���� �ϴ�?");
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

    //��ų���� �����ؼ� ���� �� Ÿ�� ����
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

    //���� �� ȿ��ó�� ��û
    //Ÿ�ٿ��� ó���Ǵ� ȿ���� ��� CalculationJudgmentEffect�� ó�� ��û
    //��Ÿ �ΰ�ȿ�� �߻� ��, �ش� �Լ����� ó��
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
    


    //��Ȱ��ȭ �� ����� ���� ����
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
