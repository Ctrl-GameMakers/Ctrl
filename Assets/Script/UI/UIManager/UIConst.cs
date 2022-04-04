using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIConst
{
    public const int ui_type_main = 0;
    public const int ui_type_popup_1 = 1;
    public const int ui_type_popup_2 = 2;

    public static System.Type[] main_ui_classes = new System.Type[]
    {
        typeof(UIStage),
    };

    public static System.Type[] popup_1_ui_classes = new System.Type[]
    {
        
    };

    public static System.Type[] popup_2_ui_classes = new System.Type[]
    {

    };
}
