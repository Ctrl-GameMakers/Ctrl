using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitManager : MonoBehaviour
{
    public struct UnitInformation
    {
        private GameObject _unit;
        private Transform _transform;
        private UnitController _unitController;
        private UnitStatus _unitStatus;
        private UnitFeedback _unitFeedback;

        private int _instanceID;

        public GameObject unit { get => _unit; }
        public Transform transform { get => _transform; }
        public UnitController unitController { get => _unitController; }
        public UnitStatus unitStatus { get => _unitStatus; }
        public UnitFeedback unitFeedback { get => _unitFeedback; }
        public float instanceID { get => _instanceID; }

        public UnitInformation(GameObject go, Transform tr, UnitController controller, UnitStatus status, UnitFeedback feedback)
        { _unit = go; _transform = tr; _unitController = controller; _unitStatus = status; _unitFeedback = feedback; _instanceID = go.GetInstanceID(); }

    }
    [Header("Unit parent transform")]
    [SerializeField] private Transform _unitparent;

    [Header("Unit base prefab")]
    [SerializeField] private GameObject _unitBase;

    [Header("Unit database")]
    [SerializeField] UnitDataBase _unitDataBase;

    List<UnitInformation> _controllerList = new List<UnitInformation>();

    private bool _isBattleMode;
    public bool isBattleMode { get => _isBattleMode; }

    private Dictionary<int, UnitInformation> battleUnitDic = new Dictionary<int, UnitInformation>();
    private Dictionary<int, bool> battleUnitDeathDic = new Dictionary<int, bool>();

    private TargetFinder _targetFinder;

    private IntVector2 myTilePos;
    private IntVector2 targetTilePos;

    static UnitManager minstance;
    public static UnitManager Instance
    {
        get
        {
            if (minstance == null)
            {
                minstance = FindObjectOfType<UnitManager>();
            }
            return minstance;
        }
    }

    void Awake()
    {
        _targetFinder = GetComponent<TargetFinder>();
        _unitDataBase = GetComponent<UnitDataBase>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (!isBattleMode)
            {
                BattleStart();
            }
        }
    }




    public void AddUnitInformation(UnitController controller)
    {
        _controllerList.Add(new UnitInformation(controller.gameObject, controller.transform, controller, controller.GetComponent<UnitStatus>(), controller.GetComponent<UnitFeedback>()));
    }

    public void CreateUnit(int unitID, Vector3 pos)
    {

    }

    #region Astar Nav
    public Vector3 NextPos(int myInstanceID, int targetInstanceID)
    {
        myTilePos = TileManager.Instance.GetUnitTilePosition(myInstanceID);
        targetTilePos = TileManager.Instance.GetUnitTilePosition(targetInstanceID);

        List<IntVector2> myPoints = AstarCalculater.Instance.FindAstar(myTilePos.x, myTilePos.z, targetTilePos.x, targetTilePos.z);

        if(myPoints.Count <= 2)
        {
            return new Vector3(myPoints[0].x, 0, myPoints[0].z);
        }
        else
        {
            return new Vector3(myPoints[1].x, 0, myPoints[1].z);
        }

    }
    #endregion

    #region BattleMode
    public void BattleStart()
    {
        battleUnitDic.Clear();
        battleUnitDeathDic.Clear();

        for (int i = 0; i < _controllerList.Count; i++)
        {
            if (_controllerList[i].unitController._onField)
            {
                battleUnitDic.Add(_controllerList[i].unit.GetInstanceID(), _controllerList[i]);
                battleUnitDeathDic.Add(_controllerList[i].unit.GetInstanceID(), false);

                TileManager.Instance.SetUnitTilePosition
                    ((int)battleUnitDic[_controllerList[i].unit.GetInstanceID()].transform.position.x, (int)battleUnitDic[_controllerList[i].unit.GetInstanceID()].transform.position.z,
                    _controllerList[i].unit.GetInstanceID(), Define.TileType.InUnit);
            }
        }

        _isBattleMode = true;
    }

    public InstanceIDContainer TargetFinder(int instanceID, SkillData skillData)
    {
        return _targetFinder.TargetUnitFinder(instanceID, battleUnitDic, skillData.targetRange, skillData.targetGroup, skillData.targetSortingBase);
    }

    public void SetUnitDeath(int instancdID)
    {
        battleUnitDeathDic[instancdID] = true;
        TileManager.Instance.SetTileExitUnit(instancdID);
    }
    #endregion

    public Vector3 GetUnitPosition(int instanceID) { return battleUnitDic[instanceID].transform.position; }
    public UnitFeedback GetUnitFeedback(int instanceID) { return battleUnitDic[instanceID].unitFeedback; }
    public UnitStatus GetUnitStatus(int instanceID) { return battleUnitDic[instanceID].unitStatus; }
    public UnitController GetUnitController(int instanceID) { return battleUnitDic[instanceID].unitController; }

    public bool GetBattleUnitIsDeath(int instancdID) { return battleUnitDeathDic[instancdID]; }
    public UnitData GetUnitData(int _id) { return _unitDataBase.GetUnitData(_id); }
}