using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentObject : MonoBehaviour
{
    bool isTrial = true;

    Transform _transform;

    [SerializeField] private int skillID;
    [SerializeField] private int casterInstanceID;

    private List<Collider2D> judgmentTargets = new List<Collider2D>();
    private Coroutine _judgmentDelay;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    //최초로 만들어 졌을 경우에만 자동으로 비활성화 (최초 Pool제작 시)
    //그 외에는 기존 계산해 두었던 타겟리스트 초기화
    private void OnEnable()
    {
        if (!isTrial)
        {
            judgmentTargets.Clear();
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
    
    /*
    //활성화 요청 시 정보 저장하고 판정 딜레이만큼 대기
    public void ActiveSkill(int _id, int _casterInstanceID, Vector3 _position, Quaternion _rotation)
    {
        gameObject.SetActive(true);
        _transform.position = _position;
        _transform.rotation = _rotation;

        skillID = _id;

        casterInstanceID = _casterInstanceID;

        if(SkillManager.Instance.GetSkillData(skillID).judgmentTime.Equals(0.0f))
        {
            FindJudgmentTarget();
        }
        else
        {
            _judgmentDelay = StartCoroutine(JudgmentDelay());
        }
    }

    //판정 딜레이만큼 대기 후 판정 진행
    IEnumerator JudgmentDelay()
    {
        yield return new WaitForSeconds(SkillManager.Instance.GetSkillData(skillID).judgmentTime);
        FindJudgmentTarget();
    }

    //스킬정보 참조해서 범위 내 타겟 판정
    private void FindJudgmentTarget()
    {
        switch(SkillManager.Instance.GetSkillData(skillID).areaForm)
        {
            case SkillAreaForm.Circle:
                var tempJudgmentTargets = Physics2D.OverlapCircleAll(transform.position, SkillManager.Instance.GetSkillData(skillID).length, (int)LayerMarskEnum.Player);
                for (int i = 0; i < tempJudgmentTargets.Length; i++)
                {
                    if(!tempJudgmentTargets[i].gameObject.GetInstanceID().Equals(casterInstanceID))
                    {
                        judgmentTargets.Add(tempJudgmentTargets[i]);
                    }
                }
                CalculationJudgments(skillID, judgmentTargets);
                break;                
            default:
                break;
        }
    }
    
    //판정 후 효과처리 요청
    //>>타겟에게 처리되는 효과의 경우 CalculationJudgmentEffect에 처리 요청
    //>>CallSkill 등 기타 부가효과 발생 시, 해당 함수에서 처리
    private void CalculationJudgments(int _id, List<Collider2D> _targetList)
    {
        switch (SkillManager.Instance.GetSkillData(_id).effectType)
        {
            case SkillEffectType.CallSkill:
                SkillManager.Instance.UseSkill((int)SkillManager.Instance.GetSkillData(_id).effectPower, casterInstanceID, _transform);
                break;
            case SkillEffectType.Teleport:
                PlayerManager.Instance.PlayerTeleport(casterInstanceID, _transform.position);
                break;
            default:
                CalculationJudgmentEffect.Instance.CalculationJudgment(_id, casterInstanceID, _targetList, _transform.position);                
                break;
        }

        if (!SkillManager.Instance.GetSkillData(_id).linkSkillID.Equals(0))
        {
            CalculationJudgments(SkillManager.Instance.GetSkillData(_id).linkSkillID, _targetList);
        }

        gameObject.SetActive(false);
    }*/




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
    }
}
