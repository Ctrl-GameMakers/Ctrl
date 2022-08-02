using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_Shop_Relic : HYJ_Shop_Button
{
    //////////  Getter & Setter //////////

    //////////  Method          //////////
    public override void HYJ_Default_Transform(Transform _trans, int _num)
    {
        this.gameObject.SetActive(true);

        Vector3 pos = this.transform.localPosition;
        pos.y += _trans.localPosition.z * (float)_num;
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
