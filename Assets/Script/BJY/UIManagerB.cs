using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManagerB : MonoBehaviour
{
    private const int maxTimeValue = 1;
    private const float maxTime = 0.1f * 300;
    private string stageStatus;
    public Slider _timeBar;
    public Text _timeText;
    float limitTime;

    // Start is called before the first frame update
    void Start()
    {
        setInitalTime("Ready");
    }

    // Update is called once per frame
    void Update()
    {
        if(limitTime > 0)
            setTime();
        else if(limitTime == 0 && stageStatus == "Ready"){
            setInitalTime("Battle");
        }
        else if(limitTime == 0 && stageStatus == "Battle"){
            setInitalTime("BattleExtension");
        }
    }

    void setTime(){
        limitTime -= Time.deltaTime;
        _timeText.text = Mathf.Round(limitTime).ToString();
        _timeBar.value -= maxTimeValue/maxTime * Time.deltaTime;

    }

    void setInitalTime(string s){
        stageStatus = s;
        switch(stageStatus){
            case "Ready":
            case "Battle":
                limitTime = maxTime;
                break;

            case "BattleExtension":
                limitTime = 0.1f * 100;
                break;

        }

        _timeBar.value = maxTimeValue;

    }

}
