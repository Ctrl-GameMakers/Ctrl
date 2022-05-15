using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapTool_MaterialList : UIBase
{
    private int _current_select_menu;

    private const int _cube_list = 0;
    private const int _structure_list = 1;

    public RectTransform[] rt_root_tap;
    public RectTransform rt_tap_select;

    public override void initUI()
    {
        base.initUI();

        _current_select_menu = _cube_list;
    }

    public override void show()
    {
        base.show();

        _setup();
    }

    private void _setup()
    {
        onBtnTapSelect(_current_select_menu);
    }

    private void _refresh_cube_list()
    {

    }

    private void _refresh_sturcture_list()
    {

    }

    public void onBtnTapSelect(int tap_index)
    {
        switch (tap_index)
        {
            case _cube_list:
                {
                    _current_select_menu = _cube_list;
                    _refresh_cube_list();
                }
                break;

            case _structure_list:
                {
                    _current_select_menu = _structure_list;
                    _refresh_sturcture_list();
                }
                break;
        }

        rt_tap_select.localPosition = rt_root_tap[_current_select_menu].localPosition;
    }

    public void onBtnSelectMaterial()
    {

    }

    public void onBtnClose()
    {
        _current_select_menu = _cube_list;

        hide();
    }
}
