using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapTool : UIBase
{
    public Transform tr_popup_menus;

    private Material[] _materials;
    private Material _current_material;
    private int _material_index;

    public void Awake()
    {
        _material_index = -1;
        _materials = Resources.LoadAll<Material>("SkyBox");
    }

    public override void show()
    {
        base.show();

        _setup();
    }

    private void _setup()
    {
        tr_popup_menus.gameObject.SetActive(false);
    }

    public void Update()
    {

    }

    public void onBtnShowMenu()
    {
        bool active = !tr_popup_menus.gameObject.activeSelf;
        tr_popup_menus.gameObject.SetActive(active);
    }

    public void onBtnCreateCube()
    {
        //show cube list
        UIManager.getinstance<UIMapTool_MaterialList>().show(0);
    }

    public void onBtnCreateStructure()
    {
        //show structure list
        UIManager.getinstance<UIMapTool_MaterialList>().show(1);
    }

    public void onBtnChangeSkyBox()
    {
        _material_index++;
        if (_materials.Length <= _material_index)
        {
            _material_index = 0;
        }
        RenderSettings.skybox = _materials[_material_index];
    }
}
