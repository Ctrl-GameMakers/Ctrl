using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapTool_MaterialList : UIBase
{
    private int _current_select_menu;

    private const int _cube_list = 0;
    private const int _structure_list = 1;

    public void show(int select_menu)
    {
        base.show();
        
        _current_select_menu = select_menu;

        _setup();
    }

    private void _setup()
    {
        switch (_current_select_menu)
        {
            case _cube_list:
                {

                }
                break;

            case _structure_list:
                {

                }
                break;
        }
    }

    public void onBtnClose()
    {
        hide();
    }
}
