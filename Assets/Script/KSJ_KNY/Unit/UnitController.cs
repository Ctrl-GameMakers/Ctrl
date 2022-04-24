using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Pos
{
    public int x;
    public int z;
    public Pos(int x, int z) { this.x = x; this.z = z; }
}

public class UnitController : MonoBehaviour
{
    Transform tr;
    GameObject go;
    
    public Vector3 _nextMovePos;
    public float _moveSpeed = 2.0f;

    public bool _onField;

    Define.State _state = Define.State.Idle;

    InstanceIDContainer _targetContainer;
    Transform _targetTransform;
    Vector3 _nextUnitMovePos;

    UnitAction _action;
    UnitStatus _status;

    void Start()
    {
        UnitManager.Instance.AddUnitInformation(this);

        tr = GetComponent<Transform>();
        go = gameObject;
        _action = GetComponent<UnitAction>();
        _status = GetComponent<UnitStatus>();

        TileManager.Instance.SetUnitTilePosition((int)tr.position.x, (int)tr.position.z, go.GetInstanceID());
    }

    void Update()
    {
        if (UnitManager.Instance.isBattleMode)
        {
            if (!_action._onAction)
            {
                _state = Root();
                switch (_state)
                {
                    case Define.State.Idle:
                        {

                        }
                        break;
                    case Define.State.Moving:
                        {
                            MoveAction();
                        }
                        break;
                    case Define.State.Attack:
                        {
                            AttackAction();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }


    void MoveAction()
    {
        _nextUnitMovePos = UnitManager.Instance.NextPos(go.GetInstanceID(), _targetContainer.targetInstanceID);

        TileManager.Instance.SetUnitTilePosition((int)_nextUnitMovePos.x, (int)_nextUnitMovePos.z, go.GetInstanceID());

        _action.Move(_nextUnitMovePos);
    }

    void AttackAction()
    {
        _action.Attack(_targetContainer.targetInstanceID, _status.normalSkillID);
    }



    private Define.State Root()
    {
        //Å¸°Ù °Ë»ö
        _targetContainer = UnitManager.Instance.TargetFinder(go.GetInstanceID(), SkillManager.Instance.GetSkillData(_status.normalSkillID));

        if (!_targetContainer.isExist)
        {
            return Idle();
        }            

        if (Vector3.Distance(transform.position, UnitManager.Instance.GetUnitPosition(_targetContainer.targetInstanceID)) 
            <= SkillManager.Instance.GetSkillData(_status.normalSkillID).skillRange)
        {
            return Attack();
        }
        else
        {
            return Move();
        }

    }
    private Define.State Idle()
    {
        return Define.State.Idle;
    }
    
    private Define.State Move()
    {
        return Define.State.Moving;
    }

    private Define.State Attack()
    {
        return Define.State.Attack;
    }

}