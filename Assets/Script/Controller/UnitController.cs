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

    public int PosZ { get; set; }
    public int PosX { get; set; }

    public int DestZ { get;  set; }
    public int DestX { get;  set; }

    public Vector3 _nextMovePos;
    public float _moveSpeed = 2.0f;

    public bool _onField;

    Define.State _state = Define.State.Idle;

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

        TileManager.Instance.SetUnitTilePosition((int)tr.position.x, (int)tr.position.z, tr.GetInstanceID());
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
                            Debug.Log($"{this.go.name}On Attack!");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }



    private Define.State Root()
    {
        _targetTransform = UnitManager.Instance.TargetFinder(go.GetInstanceID(), SkillManager.Instance.GetSkillData(_status.normalSkillID));

        if (_targetTransform == null)
        {
            return Idle();
        }            

        if (Vector3.Distance(transform.position, _targetTransform.position) <= 1.45f)
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
 
    void MoveAction()
    {               
        _nextUnitMovePos = UnitManager.Instance.NextPos(tr.GetInstanceID(), _targetTransform.GetInstanceID());

        TileManager.Instance.SetUnitTilePosition((int)_nextUnitMovePos.x, (int)_nextUnitMovePos.z, tr.GetInstanceID());

        _action.Move(_nextUnitMovePos);       
    }
}