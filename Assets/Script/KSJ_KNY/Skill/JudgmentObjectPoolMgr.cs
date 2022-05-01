using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentObjectPoolMgr : MonoBehaviour
{
    [SerializeField] float startCreateCount;
    [SerializeField] GameObject judgmentObject;

    Queue<JudgmentObject> judgmentObjectQueue = new Queue<JudgmentObject>();

    void Awake()
    {
        judgmentObjectQueue.Clear();

        for (int i = 0; i < startCreateCount; i++)
        {
            judgmentObjectQueue.Enqueue(Instantiate(judgmentObject, transform).GetComponent<JudgmentObject>());
        }
    }

    
    public void CallSkillObject(int skillID, int casterInstanceID, int targetInstanceID)
    {
        ChkjudgmentObjectQueue();
        judgmentObjectQueue.Dequeue().ActiveSkill(skillID, casterInstanceID, targetInstanceID);
    }

    private void ChkjudgmentObjectQueue()
    {
        if (judgmentObjectQueue.Count.Equals(0))
        {
            judgmentObjectQueue.Enqueue(Instantiate(judgmentObject, transform).GetComponent<JudgmentObject>());
        }
    }

    public void EnqueueObject(JudgmentObject judgmentObject)
    {
        judgmentObjectQueue.Enqueue(judgmentObject);
    }
}
