using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Cube : MonoBehaviour
{
    public Image img_cube;

    public void setup(Material material)
    {
        img_cube.color = material.color;
    }
}
