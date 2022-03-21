using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static Dictionary<Type, UIBase> _dic_ui_container = new Dictionary<Type, UIBase>();

    public static UIRoot ui_root { get; private set; }

    public static void init(UIRoot set_ui_root)
    {
        ui_root = set_ui_root;
    }

    public static void hideAllUI()
    {
        var d_enum = _dic_ui_container.GetEnumerator();
        while(d_enum.MoveNext())
        {
            d_enum.Current.Value.hide();
        }
    }

    public static void show<T>() where T : UIBase
    {
        Type t = typeof(T);
        if (!_dic_ui_container.ContainsKey(t))
        {
            prepareUI<T>();
        }

        _dic_ui_container[t].show();
    }

    public static void hide<T>() where T : UIBase
    {
        Type t = typeof(T);
        if (!_dic_ui_container.ContainsKey(t))
        {
            prepareUI<T>();
        }

        _dic_ui_container[t].hide();
    }

    public static bool activeSelf<T>() where T : UIBase
    {
        Type t = typeof(T);
        if (!_dic_ui_container.ContainsKey(t))
        {
            return false;
        }

        return _dic_ui_container[t].gameObject.activeSelf;
    }

    private static void prepareUI<T>() where T : UIBase
    {
        Type t = typeof(T);
        string name = t.Name;
    }
}
