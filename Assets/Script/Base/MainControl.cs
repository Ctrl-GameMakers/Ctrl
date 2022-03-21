using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    private static MainControl _instance;
    public static MainControl instance => _instance;

    public UIRoot ui_root;

    public void Awake()
    {
        _instance = this;

        ui_root.Init();

        UIManager.hideAllUI();

        StartCoroutine(_init());
    }

    private IEnumerator _init()
    {
        UIManager.init(ui_root);

        yield break;
    }


    public void OnDestory()
    {
        _instance = null;
    }
}
