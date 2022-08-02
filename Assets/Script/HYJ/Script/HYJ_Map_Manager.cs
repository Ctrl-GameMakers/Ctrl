using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HYJ_Map_Manager : MonoBehaviour
{
    //////////  Getter & Setter //////////

    //////////  Method          //////////

    //////////  Default Method  //////////
    // Start is called before the first frame update
    void Start()
    {
        HYJ_Cheapter_Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

partial class HYJ_Map_Manager
{
    [Header("CHEAPTER")]
    const int Cheapter_x = 8;
    const int Cheapter_y = 14;

    [SerializeField] Transform Cheapter_viewPort;
    [SerializeField] HYJ_Map_Stage Cheapter_stage;
    [SerializeField] int Cheapter_level;
    [SerializeField] List<HYJ_Map_Stage> Cheapter_stages;

    //////////  Getter & Setter //////////

    //////////  Method          //////////
    public void HYJ_Cheapter_Create(params object[] _args)
    {
        Cheapter_level = (int)_args[0];

        Cheapter_stages = new List<HYJ_Map_Stage>();
        for (int i = 0; i < Cheapter_x * Cheapter_y; i++)
        {
            Cheapter_stages.Add(null);
        }

        int x = Random.Range(4, 6);
        int y = 0;
        HYJ_Map_Stage element = null;
        // 챕터 생성
        {
            element = HYJ_Cheapter_Create_CreateStage(x, y);
            element.HYJ_Stage_type = HYJ_Map_Stage_TYPE.BASE_CAMP;

            //
            for (int i = 1; i < Cheapter_y - 1; i++)
            {
                x += Random.Range(0, 3) - 1;
                if (x.Equals(-1)) { x = 0; }
                else if (x.Equals(Cheapter_x)) { x = Cheapter_x - 1; }

                y++;

                element = HYJ_Cheapter_Create_CreateStage(x, y);
                element.HYJ_Stage_SettingType(Cheapter_level);
            }

            //
            x = Random.Range(4, 6);
            y = Cheapter_y - 1;
            element = HYJ_Cheapter_Create_CreateStage(x, y);
            element.HYJ_Stage_type = HYJ_Map_Stage_TYPE.BATTLE_BOSS;
        }

        // 루트 설정
        {
            for (y = 0; y < Cheapter_y - 1; y++)
            {
                for (x = 0; x < Cheapter_x; x++)
                {
                    element = Cheapter_stages[x + (y * Cheapter_x)];

                    if (element)
                    {
                        int checkY = y + 1;

                        int checkX = x - 1;
                        if (checkX >= 0)
                        {
                            HYJ_Cheapter_Create_SetRoot(
                                element,
                                checkX, checkY);
                        }

                        checkX = x;
                        HYJ_Cheapter_Create_SetRoot(
                            element,
                            checkX, checkY);

                        checkX = x + 1;
                        if (checkX < Cheapter_x)
                        {
                            HYJ_Cheapter_Create_SetRoot(
                                element,
                                checkX, checkY);
                        }
                    }
                }
            }
        }

        // 베이스캠프 활성화
        for (int i = 0; i < Cheapter_stages.Count; i++)
        {
            element = Cheapter_stages[i];

            if (element != null)
            {
                if(element.HYJ_Stage_type.Equals( HYJ_Map_Stage_TYPE.BASE_CAMP ))
                {
                    element.HYJ_Stage_Select();
                    break;
                }
            }
        }
    }

    HYJ_Map_Stage HYJ_Cheapter_Create_CreateStage(int _x, int _y)
    {
        Cheapter_viewPort.localPosition = new Vector3(0, 0, 0);

        //
        HYJ_Map_Stage res = null;

        //
        res = GameObject.Instantiate(Cheapter_stage);
        res.HYJ_Init(
            Cheapter_viewPort,
            _x, _y);
        Cheapter_stages[_x + (_y * Cheapter_x)] = res;

        //
        return res;
    }

    void HYJ_Cheapter_Create_SetRoot(
        HYJ_Map_Stage _stage,
        int _x, int _y)
    {
        HYJ_Map_Stage element = Cheapter_stages[_x + (_y * Cheapter_x)];

        if (element != null)
        {
            element.HYJ_Stage_AddRoot(_stage);
            _stage.HYJ_Stage_AddRoot(element);
        }
    }

    object HYJ_Cheapter_SelectReset(params object[] _args)
    {
        for (int i = 0; i < Cheapter_stages.Count; i++)
        {
            HYJ_Map_Stage element = Cheapter_stages[i];

            if(element != null)
            {
                element.HYJ_UI_Active(false);
            }
        }

        return null;
    }

    object HYJ_Cheapter_MoveCenter(params object[] _args)
    {
        Cheapter_viewPort.localPosition = new Vector3(-(float)_args[0], -(float)_args[1], -(float)_args[2]);

        return null;
    }

    //////////  Default Method  //////////
    void HYJ_Cheapter_Start()
    {
        if (HYJ_ScriptBridge.HYJ_Static_instance == null) Debug.Log("AA");
        HYJ_ScriptBridge.HYJ_Static_instance.HYJ_Event_Set( HYJ_ScriptBridge_EVENT_TYPE.MAP___CHEAPTER__SELECT_RESET,   HYJ_Cheapter_SelectReset    );
        HYJ_ScriptBridge.HYJ_Static_instance.HYJ_Event_Set( HYJ_ScriptBridge_EVENT_TYPE.MAP___CHEAPTER__MOVE_CENTER,    HYJ_Cheapter_MoveCenter     );

        HYJ_Cheapter_Create(1);
    }
}