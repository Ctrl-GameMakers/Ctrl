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

    private Item_Cube _current_select_cube;
    public Item_Cube source_cube;

    public void Awake()
    {
        _material_index = -1;
        _materials = Resources.LoadAll<Material>("SkyBox");
        source_cube.gameObject.SetActive(false);
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
        if (source_cube.gameObject.activeSelf)
            source_cube.transform.localPosition = Input.mousePosition;
    }

    public void onBtnShowMenu()
    {
        bool active = !tr_popup_menus.gameObject.activeSelf;
        tr_popup_menus.gameObject.SetActive(active);
    }

    public void onBtnCreateCube()
    {
        UIManager.getinstance<UIMapTool_MaterialList>().show();
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

    public void select_cube(Item_Cube item_Cube)
    {
        source_cube.gameObject.SetActive(true);
        source_cube.setup(item_Cube.getMaterial, placement : true);
    }
}
