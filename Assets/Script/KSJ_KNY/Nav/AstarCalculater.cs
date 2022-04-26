using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarCalculater : MonoBehaviour
{
    static AstarCalculater s_instance;
    public static AstarCalculater Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType<AstarCalculater>();
            }
            return s_instance;
        }
    }
    
    struct PQNode : IComparable<PQNode>   // priorityQueue�� �� ���
    {
        public int F;
        public int G;
        public int Z;
        public int X;

        public int CompareTo(PQNode other)
        {
            if (F == other.F)
                return 0;
            return F < other.F ? 1 : -1;
        }
    }

    // �����Ͽ� �����ϱ� ���� �迭
    // U L D R UL DL DR UR
    int[] deltaZ = new int[] { -1, 0, 1, 0, -1, 1, 1, -1 };
    int[] deltaX = new int[] { 0, -1, 0, 1, -1, -1, 1, 1 };
    int[] cost = new int[] { 10, 10, 10, 10, 14, 14, 14, 14 }; // U L D R UL DL DR UR�� ���� ���

    bool[,] closed = new bool[15, 15];
    int[,] open = new int[15, 15];
    IntVector2[,] parent = new IntVector2[15, 15];

    PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();

    int nextX;
    int nextZ;

    int g;
    int h;

    int destX;
    int destZ;

    IntVector2 pos;

    List<IntVector2> _points = new List<IntVector2>();


    private void Awake()
    {
        Instance.Init();
    }
    private void Init(){}


    public List<IntVector2> FindAstar(int posX, int posZ, int destX, int destZ)
    {
        for (int x = 0; x < TileManager.Instance.boardSize; x++)
        {
            for (int z = 0; z < TileManager.Instance.boardSize; z++)
            {
                closed[x, z] = false;
                open[x, z] = int.MaxValue;
                parent[x, z].x = x;
                parent[x, z].z = z;
            }
        }

        pq.Clear();
        
        // ������ �߰� (���� ����)
        open[posX, posZ] = 10 * (Math.Abs(destZ - posZ) + Math.Abs(destX - posX));
        pq.Push(new PQNode() { F = 10 * (Math.Abs(destZ - posZ) + Math.Abs(destX - posX)), G = 0, Z = posZ, X = posX });
        parent[posX, posZ] = new IntVector2(posX, posZ);

        while (pq.Count > 0)
        {
            // ���� ���� �ĺ��� ã�´�.
            PQNode node = pq.Pop();

            // �湮�Ѵ�.
            closed[node.X, node.Z] = true;

            // ������ ���������� �ٷ�����
            if (node.Z == destZ && node.X == destX)
                break;

            // �����¿� �� �̵��� �� �ִ� ��ǥ���� Ȯ���ؼ� ����(open)�Ѵ�.
            for (int i = 0; i < deltaZ.Length; i++)
            {
                nextX = node.X + deltaX[i];
                nextZ = node.Z + deltaZ[i];

                // ��ȿ������ ������� ��ŵ
                if (nextX < 0 || nextX >= TileManager.Instance.boardSize || nextZ < 0 || nextZ >= TileManager.Instance.boardSize)
                    continue;

                // �̹� �湮�� ���̶�� ��ŵ
                if (closed[nextX, nextZ])
                    continue;

                // ������ ������ �� �� ������ Close ���� �� ��ŵ
                if (TileManager.Instance.Tiles[nextX, nextZ].Equals(Define.TileType.Wall))
                {
                    closed[nextX, nextZ] = true;
                    continue;
                }

                // �ڸ��� ������ �ְ�, �� ���ֿ� Ÿ���� ��ġ�ϰ� ���� �ʴٸ� Close ���� �� ��ŵ
                if (TileManager.Instance.Tiles[nextX, nextZ].Equals(Define.TileType.InUnit) && (nextZ != destZ || nextX != destX))
                {
                    closed[nextX, nextZ] = true;
                    continue;
                }


                // �����
                g = node.G + cost[i];
                h = 10 * (Math.Abs(destZ - nextZ) + Math.Abs(destX - nextX));

                // �׷��� �ٸ� ��ο��� �� ������ �̹� ã������ ��ŵ�Ѵ�.
                if (open[nextX, nextZ] < g + h)
                    continue;

                // ���� ����
                open[nextX, nextZ] = g + h;
                pq.Push(new PQNode() { F = g + h, G = g, Z = nextZ, X = nextX });
                parent[nextX, nextZ] = new IntVector2(node.X, node.Z);
            }
        }

        return CalcPathFromParent(parent, destZ, destX);
    }
    List<IntVector2> CalcPathFromParent(IntVector2[,] parent, int destZ, int destX)
    {
        _points.Clear();        

        this.destZ = destZ;
        this.destX = destX;

        while (parent[this.destX, this.destZ].z != this.destZ || parent[this.destX, this.destZ].x != this.destX)
        {
            _points.Add(new IntVector2(this.destX, this.destZ));
            pos = parent[this.destX, this.destZ];

            this.destZ = pos.z;
            this.destX = pos.x;
        }
        _points.Add(new IntVector2(this.destX, this.destZ));
        _points.Reverse();

        //return _unit.SetNextPath(_points);
        return _points;
    }
}
