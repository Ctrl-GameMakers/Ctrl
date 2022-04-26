using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct IntVector2
{
    public int x;
    public int z;
    public IntVector2(int x, int z) { this.x = x; this.z = z; }
}

public class UnitController : MonoBehaviour
{
    Transform tr;
    GameObject go;
    
    public Vector3 _nextMovePos;

    public bool _onField;

    Define.UnitState _state = Define.UnitState.Idle;
    public Define.UnitState state { get => _state; }

    InstanceIDContainer _targetContainer;
    Vector3 _nextUnitMovePos;
    int _chkSkillID;

    JudgmentObject _nowUsingJudgmentObject;
    public JudgmentObject nowUsingJudgmentObject { get => _nowUsingJudgmentObject; }

    UnitAction _action;
    UnitStatus _status;

    void Start()
    {
        tr = GetComponent<Transform>();
        go = gameObject;
        _action = GetComponent<UnitAction>();
        _status = GetComponent<UnitStatus>();

        UnitManager.Instance.AddUnitInformation(this);
    }

    void Update()
    {
        if (UnitManager.Instance.isBattleMode)
        {
            if (!_state.Equals(Define.UnitState.Die))
            {
                if (!_action._onAction)
                {
                    _state = Root();
                    switch (_state)
                    {
                        case Define.UnitState.Idle:
                            {

                            }
                            break;
                        case Define.UnitState.Moving:
                            {
                                MoveAction();
                            }
                            break;
                        case Define.UnitState.Attack:
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
    }


    #region Unit Action
    void MoveAction()
    {
        _nextUnitMovePos = UnitManager.Instance.NextPos(go.GetInstanceID(), _targetContainer.targetInstanceID);
        TileManager.Instance.SetUnitTilePosition((int)_nextUnitMovePos.x, (int)_nextUnitMovePos.z, go.GetInstanceID(), Define.TileType.InUnit);

        _action.Move(_nextUnitMovePos);
    }

    void AttackAction()
    {
        _action.Attack(_targetContainer.targetInstanceID, _chkSkillID);
    }
    #endregion

    #region Unit AI
    private Define.UnitState Root()
    {
        if (_status.nowMP >= _status.maxMP)
            _chkSkillID = _status.specialSkillID;
        else
            _chkSkillID = _status.normalSkillID;        

        //Å¸°Ù °Ë»ö
        _targetContainer = UnitManager.Instance.TargetFinder(go.GetInstanceID(), SkillManager.Instance.GetSkillData(_chkSkillID));


        if (!_targetContainer.isExist)
        {
            return Idle();
        }            

        if (Vector3.Distance(transform.position, UnitManager.Instance.GetUnitPosition(_targetContainer.targetInstanceID)) 
            <= SkillManager.Instance.GetSkillData(_chkSkillID).skillRange)
        {
            return Attack();
        }
        else
        {
            return Move();
        }

    }
    private Define.UnitState Idle()
    {
        return Define.UnitState.Idle;
    }
    
    private Define.UnitState Move()
    {
        return Define.UnitState.Moving;
    }

    private Define.UnitState Attack()
    {
        return Define.UnitState.Attack;
    }
    #endregion

    public void SetUsingJudgmentObject(JudgmentObject judgmentObject) => _nowUsingJudgmentObject = judgmentObject;
    public void ResetJudgmentObject() => _nowUsingJudgmentObject = null;
    public void Death()
    {
        if(_nowUsingJudgmentObject != null)
        {
            _nowUsingJudgmentObject.DisableSkill();
            ResetJudgmentObject();
        }
        _state = Define.UnitState.Die;
        UnitManager.Instance.SetUnitDeath(go.GetInstanceID());
    }
}