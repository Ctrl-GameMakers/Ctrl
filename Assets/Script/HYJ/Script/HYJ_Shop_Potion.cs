using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_Shop_Potion : HYJ_Shop_Button
{
    //////////  Getter & Setter //////////

    //////////  Method          //////////
    public override void HYJ_Default_Transform(Transform _trans, int _num)
    {
        this.gameObject.SetActive(true);

        //
        int x = (_num % 3) - 1;
        int y = _num / 3;

        float posX = _trans.localPosition.x + this.transform.parent.parent.localPosition.x;

        Vector3 pos = this.transform.localPosition;
        pos.x = posX * (float)x + 246.0f;
        pos.y += _trans.localPosition.z * (float)y;
        pos.z = 0.0f;
        this.transform.localPosition = pos;
    }

    public override void HYJ_Default_Buy()
    {
        Debug.Log("HYJ_Shop_Relic");
    }

    //////////  Default Method  //////////
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}