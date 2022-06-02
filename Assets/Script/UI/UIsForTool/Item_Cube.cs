using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Cube : MonoBehaviour
{
    public Image img_cube;
    public Image img_select;

    public void setup(Material material)
    {
        img_cube.color = material.color;
        refresh_select(false);
    }

    public void onBtnSelect()
    {
        refresh_select(!img_select.gameObject.activeSelf);

        UIManager.getinstance<UIMapTool_MaterialList>().select_cube(this);
    }

    public void refresh_select(bool active)
    {
        img_select.gameObject.SetActive(active);
    }
}
