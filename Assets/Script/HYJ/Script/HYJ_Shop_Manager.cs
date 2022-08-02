using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HYJ_Shop_Manager : MonoBehaviour
{
    //////////  Getter & Setter //////////

    //////////  Method          //////////
    public void HYJ_On(bool _isActive)
    {
        //HYJ_Relic_SettingBtns();
        //HYJ_Item_SettingBtns();
        //HYJ_Potion_SettingBtns();
        this.gameObject.SetActive(_isActive);
    }

    public void HYJ_SetActive(bool _isActive)
    {
        this.gameObject.SetActive(_isActive);
    }

    void HYJ_SettingBtns(
        List<HYJ_Shop_Button> _btns,
        //
        int _count, HYJ_Shop_Button _btn, Transform _parent)
    {
        while (_btns.Count > 1)
        {
            Destroy(_btns[0]);
            _btns.RemoveAt(0);
        }

        //
        for (int i = 0; i < _count; i++)
        {
            HYJ_Shop_Button element = Instantiate(_btn, _parent);
            element.HYJ_Default_Transform(_btn.transform, i);

            //
            _btns.Add(element);
        }
    }

    //////////  Default Method  //////////
    // Start is called before the first frame update
    void Start()
    {
        HYJ_Relic_SettingBtns();
        HYJ_Item_SettingBtns();
        HYJ_Potion_SettingBtns();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

partial class HYJ_Shop_Manager
{
    [Header("RELIC")]
    [Header("INPUT")]
    [SerializeField] Transform Relic_parent;
    [SerializeField] HYJ_Shop_Relic Relic_btn;

    [Header("SET")]
    [SerializeField] List<HYJ_Shop_Button> Relic_btns;

    //////////  Getter & Setter //////////

    //////////  Method          //////////
    void HYJ_Relic_SettingBtns()
    {
        HYJ_SettingBtns(
            Relic_btns,
            4, Relic_btn, Relic_parent);
    }

    //////////  Default Method  //////////
}

partial class HYJ_Shop_Manager
{
    [Header("ITEM")]
    [Header("INPUT")]
    [SerializeField] Transform Item_parent;
    [SerializeField] HYJ_Shop_Item Item_btn;

    [Header("SET")]
    [SerializeField] List<HYJ_Shop_Button> Item_btns;

    //////////  Getter & Setter //////////

    //////////  Method          //////////
    void HYJ_Item_SettingBtns()
    {
        HYJ_SettingBtns(
            Item_btns,
            6, Item_btn, Item_parent);
    }

    //////////  Default Method  //////////
}

partial class HYJ_Shop_Manager
{
    [Header("POTION")]
    [Header("INPUT")]
    [SerializeField] Transform Potion_parent;
    [SerializeField] HYJ_Shop_Potion Potion_btn;

    [Header("SET")]
    [SerializeField] List<HYJ_Shop_Button> Potion_btns;

    //////////  Getter & Setter //////////

    //////////  Method          //////////
    void HYJ_Potion_SettingBtns()
    {
        HYJ_SettingBtns(
            Potion_btns,
            6, Potion_btn, Potion_parent);
    }

    //////////  Default Method  //////////
}