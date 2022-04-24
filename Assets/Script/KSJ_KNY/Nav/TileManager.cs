using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager s_instance = null;
    public static TileManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType<TileManager>();
            }
            return s_instance;
        }
    }

	UnitController _unit;

    Dictionary<int, Pos> unitPositionDic = new Dictionary<int, Pos>();

    private int _boardSize = 6;
   
    public int boardSize { get => _boardSize;}

    public Define.TileType[,] Tiles { get; private set; }

    public Pos tempPos;

    private void Awake()
    {
        if (Tiles == null)
        {
            Tiles = new Define.TileType[boardSize, boardSize];
        }
    }

    public void SetUnitTilePosition(int posX, int posZ, int unitInstanceID)
	{
        if (!unitPositionDic.ContainsKey(unitInstanceID))
        {
            unitPositionDic.Add(unitInstanceID, new Pos(posX, posZ));
            Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z] = Define.TileType.InUnit;
            //Debug.Log($"키 정보 입력 완료 {unitInstanceID}  좌표{unitPositionDic[unitInstanceID].x}, {unitPositionDic[unitInstanceID].z}  {Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z]}");
        }
        else
        {
            Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z] = Define.TileType.Empty;

            //Debug.Log($"키 정보 Empty 완료 {unitInstanceID}  좌표{unitPositionDic[unitInstanceID].x}, {unitPositionDic[unitInstanceID].z}  {Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z]}");

            tempPos.x = posX;
            tempPos.z = posZ;

            unitPositionDic[unitInstanceID] = tempPos;
            Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z] = Define.TileType.InUnit;

            //Debug.Log($"키 정보 InUnit 완료 {unitInstanceID}  좌표{unitPositionDic[unitInstanceID].x}, {unitPositionDic[unitInstanceID].z}  {Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z]}");
        }
    }

    public Pos GetUnitTilePosition(int instanceID)
    {
        //Debug.Log($"지금 찾고자 하는 키는 {instanceID} 정보 전달 {unitPositionDic[instanceID].x}, {unitPositionDic[instanceID].z}  {Tiles[unitPositionDic[instanceID].x, unitPositionDic[instanceID].z]}");
        return unitPositionDic[instanceID];
    }
    /*

    void SetTileData(int unitDestZ, int unitDestX, UnitController unit)
    {
        _unit = unit;
        Tiles[_unit.PosZ, _unit.PosX] = Define.TileType.InUnit;
        Tiles[unitDestZ, unitDestX] = Define.TileType.InUnit;
    }
    */
}
