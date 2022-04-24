using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentObjectPoolMgr : MonoBehaviour
{
    [SerializeField] float startCreateCount;
    [SerializeField] GameObject judgmentObject;

    private List<JudgmentObject> judgmentObjectList = new List<JudgmentObject>();
    Queue<JudgmentObject> judgmentObjectQueue = new Queue<JudgmentObject>();

    //�����ص� startCreateCount��ŭ JudgmentObject ��������
    void Awake()
    {
        judgmentObjectList.Clear();
        judgmentObjectQueue.Clear();

        for (int i = 0; i < startCreateCount; i++)
        {
            judgmentObjectList.Add(Instantiate(judgmentObject, transform).GetComponent<JudgmentObject>());
            judgmentObjectQueue.Enqueue(judgmentObjectList[i]);
        }
    }


    //������û�� �ްԵǸ� ����� �� JudgmentObject Pool���� ������� �ƴ�(��Ȱ��) ������Ʈ�� �������� �����ϰ� Ȱ��ȭ
    //>> ���� ������ �� Pool�� ��� ������� ��� �߰� Pool�� �����ϴ� �ڵ� �ʿ�
    public void ActiveSkill(int _id, int _casterInstanceID, Vector3 _position, Quaternion _rotation)
    {
        if(judgmentObjectQueue.Count.Equals(0))
        {
            judgmentObjectList.Add(Instantiate(judgmentObject, transform).GetComponent<JudgmentObject>());
            judgmentObjectQueue.Enqueue(judgmentObjectList[judgmentObjectList.Count - 1]);
        }

        //judgmentObjectQueue.Dequeue().ActiveSkill(_id, _casterInstanceID, _position, _rotation);
    }
}
