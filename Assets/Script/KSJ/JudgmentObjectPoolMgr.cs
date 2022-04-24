using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentObjectPoolMgr : MonoBehaviour
{
    [SerializeField] float startCreateCount;
    [SerializeField] GameObject judgmentObject;

    private List<JudgmentObject> judgmentObjectList = new List<JudgmentObject>();
    Queue<JudgmentObject> judgmentObjectQueue = new Queue<JudgmentObject>();

    //설정해둔 startCreateCount만큼 JudgmentObject 사전생성
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


    //시전요청을 받게되면 만들어 둔 JudgmentObject Pool에서 사용중이 아닌(비활성) 오브젝트에 관련정보 제공하고 활성화
    //>> 추후 제작해 둔 Pool이 모두 사용중인 경우 추가 Pool을 생성하는 코드 필요
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
