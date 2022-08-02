using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HYJ_ScriptBridge
{
    //////////  Getter & Setter //////////

    //////////  Method          //////////

    //////////  Default Method  //////////
    public HYJ_ScriptBridge()
    {
        HYJ_Event_Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        HYJ_Static_Start();

        HYJ_Event_Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

partial class HYJ_ScriptBridge
{
    static HYJ_ScriptBridge Static_instance;

    //////////  Getter & Setter //////////
    public static HYJ_ScriptBridge HYJ_Static_instance
    {
        get
        {
            if (Static_instance == null)
            {
                Static_instance = new HYJ_ScriptBridge();
            }
            return Static_instance;
        }
    }

    //////////  Method          //////////

    //////////  Default Method  //////////
    void HYJ_Static_Start()
    {
        Static_instance = this;
    }

}

//
public enum HYJ_ScriptBridge_EVENT_TYPE
{
    MAP___CHEAPTER__SELECT_RESET,
    MAP___CHEAPTER__MOVE_CENTER
}

public delegate object HYJ_ScriptBridge_Event(params object[] _args);

partial class HYJ_ScriptBridge
{

    Dictionary<HYJ_ScriptBridge_EVENT_TYPE, HYJ_ScriptBridge_Event> Event_events;

    //////////  Getter & Setter //////////

    public object HYJ_Event_Get(HYJ_ScriptBridge_EVENT_TYPE _type, params object[] _args)
    {
        object res = null;

        if (Event_events.ContainsKey(_type))
        {
            res = Event_events[_type].Invoke(_args);
        }

        return res;
    }

    public void HYJ_Event_Set(HYJ_ScriptBridge_EVENT_TYPE _type, HYJ_ScriptBridge_Event _event)
    {
        if(Event_events.ContainsKey(_type))
        {
            Event_events[_type] = _event;
        }
        else
        {
            Event_events.Add(_type, _event);
        }
    }

    //////////  Method          //////////

    //////////  Default Method  //////////
    void HYJ_Event_Start()
    {
        Event_events = new Dictionary<HYJ_ScriptBridge_EVENT_TYPE, HYJ_ScriptBridge_Event>();
    }

}