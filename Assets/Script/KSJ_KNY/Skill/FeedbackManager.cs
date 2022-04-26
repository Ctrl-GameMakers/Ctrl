using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{

    private static FeedbackManager s_instance = null;
    public static FeedbackManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType<FeedbackManager>();
            }
            return s_instance;
        }
    }


    public void Damage(int casterInstanceID, int judgmentTargetInstanceID, float amount)
    {
        UnitManager.Instance.GetUnitFeedback(judgmentTargetInstanceID).Damage(amount);
    }

}
