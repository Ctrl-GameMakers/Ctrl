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
            //Debug.Log($"Ű ���� �Է� �Ϸ� {unitInstanceID}  ��ǥ{unitPositionDic[unitInstanceID].x}, {unitPositionDic[unitInstanceID].z}  {Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z]}");
        }
        else
        {
            Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z] = Define.TileType.Empty;

            //Debug.Log($"Ű ���� Empty �Ϸ� {unitInstanceID}  ��ǥ{unitPositionDic[unitInstanceID].x}, {unitPositionDic[unitInstanceID].z}  {Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z]}");

            tempPos.x = posX;
            tempPos.z = posZ;

            unitPositionDic[unitInstanceID] = tempPos;
            Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z] = Define.TileType.InUnit;

            //Debug.Log($"Ű ���� InUnit �Ϸ� {unitInstanceID}  ��ǥ{unitPositionDic[unitInstanceID].x}, {unitPositionDic[unitInstanceID].z}  {Tiles[unitPositionDic[unitInstanceID].x, unitPositionDic[unitInstanceID].z]}");
        }
    }

    public Pos GetUnitTilePosition(int instanceID)
    {
        //Debug.Log($"���� ã���� �ϴ� Ű�� {instanceID} ���� ���� {unitPositionDic[instanceID].x}, {unitPositionDic[instanceID].z}  {Tiles[unitPositionDic[instanceID].x, unitPositionDic[instanceID].z]}");
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
