using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentObjectPoolMgr : MonoBehaviour
{
    [SerializeField] float startCreateCount;
    [SerializeField] GameObject judgmentObject;

    private List<JudgmentObject> judgmentObjectList = new List<JudgmentObject>();
    Queue<JudgmentObject> judgmentObjectQueue = new Queue<JudgmentObject>();

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

    
    public void ActiveSkill(int skillID, int casterInstanceID, int targetInstanceID)
    {
        ChkjudgmentObjectQueue();
        judgmentObjectQueue.Dequeue().ActiveSkill(skillID, casterInstanceID, targetInstanceID);
    }

    private void ChkjudgmentObjectQueue()
    {
        if (judgmentObjectQueue.Count.Equals(0))
        {
            judgmentObjectList.Add(Instantiate(judgmentObject, transform).GetComponent<JudgmentObject>());
            judgmentObjectQueue.Enqueue(judgmentObjectList[judgmentObjectList.Count - 1]);
        }
    }

    public void EnqueueObject(JudgmentObject judgmentObject)
    {
        judgmentObjectQueue.Enqueue(judgmentObject);
    }
}
