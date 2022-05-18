using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAction : MonoBehaviour
{
    Transform tr;
    GameObject go;

    UnitStatus unitStatus;

    float _moveSpeed = 2.0f;
    float _rotateSpeed = 20.0f;

    public bool _onAction;

    Vector3 _goalPos;
    int _targetInstanceID;

    public IEnumerator _action;
    public IEnumerator _lookAt;

    Quaternion dir;


    void Awake()
    {
        tr = GetComponent<Transform>();
        go = gameObject;

        unitStatus = GetComponent<UnitStatus>();
    }

    public void Move(Vector3 goalPos)
    {
        if (_lookAt != null)
        {
            StopCoroutine(_lookAt);
            _lookAt = null;
        }
        _lookAt = FollowTarget(goalPos);
        StartCoroutine(_lookAt);

        if (_action != null)
        {
            StopCoroutine(_action);
            _action = null;
        }
        _action = MoveAction();
        _goalPos = goalPos;

        //tr.LookAt(goalPos);


        StartCoroutine(_action);        
    }

    public void Attack(int targetInstanceID, int skillID)
    {
        if(!targetInstanceID.Equals(go.GetInstanceID()))
        {
            if (_lookAt != null)
            {
                StopCoroutine(_lookAt);
                _lookAt = null;
            }
            _lookAt = FollowTarget(UnitManager.Instance.GetUnitPosition(targetInstanceID));
            StartCoroutine(_lookAt);
        }


        if (_action != null)   
        {
            StopCoroutine(_action);
            _action = null;
        }

        _action = AttackAction(skillID);
        _targetInstanceID = targetInstanceID;

        //tr.LookAt(UnitManager.Instance.GetUnitPosition(targetInstanceID));

        if (skillID.Equals(unitStatus.specialSkillID))
        {
            unitStatus.ResetNowMP(0.0f);
        }
        StartCoroutine (_action);
    }
    



    IEnumerator FollowTarget(Vector3 point)
    {
        dir = Quaternion.LookRotation(point - tr.position);

        while (true)
        {
            tr.rotation = Quaternion.Lerp(tr.rotation, dir, Time.deltaTime * 20.0f);

            yield return null;

            if (dir.eulerAngles.y.Equals(tr.rotation.y))
            {
                break;
            }
        }
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
