using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitManager : MonoBehaviour
{
    public struct UnitInformation
    {
        public GameObject unit;
        public Transform transform;
        public UnitController unitController;
        public UnitStatus unitStatus;

        public UnitInformation(GameObject go, Transform tr, UnitController controller, UnitStatus status) { unit = go; transform = tr; unitController = controller; unitStatus = status; }
    }

    static UnitManager s_instance;
    public static UnitManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType<UnitManager>();
                Debug.Log("find UnitManager");
            }
            return s_instance;
        }
    }

    List<UnitInformation> _controllerList = new List<UnitInformation>();

    private bool _isBattleMode;
    public bool isBattleMode { get => _isBattleMode; }

    private Dictionary<int, UnitInformation> dic = new Dictionary<int, UnitInformation>();

    private TargetFinder targetFinder;

    private Pos myTilePos;
    private Pos targetTilePos;

    void Start()
    {
        targetFinder = GetComponent<TargetFinder>();
    }

    void Update()
    {
        if (!isBattleMode)
        {
            BattleStart();
        }
    }


    public void BattleStart()
    {
        for (int i = 0; i < _controllerList.Count; i++)
        {
            if (_controllerList[i].unitController._onField)
            {
                dic.Add(_controllerList[i].unit.GetInstanceID(), _controllerList[i]);
            }
        }
        _isBattleMode = true;
    }

    public void AddUnitInformation(UnitController controller)
    {
        _controllerList.Add(new UnitInformation(controller.gameObject, controller.transform, controller, controller.GetComponent<UnitStatus>()));
    }

    public Transform TargetFinder(int instanceID, SkillData skillData)
    {
        return targetFinder.TargetUnitFinder(instanceID, dic, skillData.targetRange, skillData.targetGroup, skillData.targetSortingBase);
    }

    public Vector3 NextPos(int posZ, int posX, int destZ, int destX)
    {
        List<AstarManager.Pos> myPoints = AstarManager.Instance.FindAstar(posZ, posX, destZ, destX);

        return new Vector3(myPoints[1].x, 0, myPoints[1].z);
    }


    public Vector3 NextPos(int myInstanceID, int targetInstanceID)
    {
        myTilePos = TileManager.Instance.GetUnitTilePosition(myInstanceID);
        targetTilePos = TileManager.Instance.GetUnitTilePosition(targetInstanceID);

        Debug.Log($"{myInstanceID} 위치 = {myTilePos.x}, {myTilePos.z} // {targetInstanceID} 위치 = {targetTilePos.x}, {targetTilePos.z}" );

        List<AstarManager.Pos> myPoints = AstarManager.Instance.FindAstar(myTilePos.x, myTilePos.z, targetTilePos.x, targetTilePos.z);

        if(myPoints.Count <= 2)
        {
            return new Vector3(myPoints[0].x, 0, myPoints[0].z);
        }
        else
        {
            return new Vector3(myPoints[1].x, 0, myPoints[1].z);
        }

    }
}