using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAction : MonoBehaviour
{
    Transform tr;
    GameObject go;

    float _moveSpeed = 1.0f;
    public bool _onAction;

    Vector3 _goalPos;
    int _targetInstanceID;

    public IEnumerator _action;

    void Awake()
    {
        tr = GetComponent<Transform>();
        go = gameObject;
    }
    void Update()
    {
        
    }

    public void Move(Vector3 goalPos)
    {
        if (_action != null)
        {
            StopCoroutine(_action);
            _action = null;
        }

        _action = MoveAction();
        _goalPos = goalPos;
        StartCoroutine(_action);        
    }

    public void Attack(int targetInstanceID, int skillID)
    {
        if (_action != null)   
        {
            StopCoroutine(_action);
            _action = null;
        }
        
        _action = AttackAction(skillID);
        _targetInstanceID = targetInstanceID;
        StartCoroutine (_action);
    }

    

    IEnumerator AttackAction(int skillID)
    {
        _onAction = true;

        SkillManager.Instance.UseSkill(skillID, go.GetInstanceID(), _targetInstanceID);
        yield return new WaitForSeconds(SkillManager.Instance.GetSkillData(skillID).judgmentTime + SkillManager.Instance.GetSkillData(skillID).afterDelay);

        _onAction = false;
    }

    IEnumerator MoveAction()
    {
        _onAction = true;

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _goalPos, _moveSpeed * Time.deltaTime);

            yield return null;

            if (transform.position == _goalPos)
            {
                yield return new WaitForSeconds(0.5f);
                break;
            }
                
        }

        _onAction = false;
    }
}
