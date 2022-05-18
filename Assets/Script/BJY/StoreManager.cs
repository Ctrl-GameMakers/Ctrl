using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    GameObject[] PlayerArray = new GameObject[5];
    GameObject[] StorePlayerArray = new GameObject[5];
    public Button _rerollBtn;
    Vector3 _newPos;
    System.Random random = new System.Random();

    //storeplayer 하위에 모델링 생성, 

    void Start()
    {
        _newPos.y = 7.5f;
        _newPos.z = 1.0f;

        for(int i = 0; i<5; i++){
            PlayerArray[i] = Resources.Load("BJYPrefab/StorePlayer(" + (i+1) + ")") as GameObject;
        }

        if(_rerollBtn != null){
            _rerollBtn.onClick.AddListener(RerollBtnClick);
        }

        CreateStorePlayer();
    }

    void RerollBtnClick(){
        if(PlayerManager.GetMana()>=2){
            DestroyAllStorePlayer();
            CreateStorePlayer();
            PlayerManager.ReduceMana(2);
        }
        else{
            Debug.Log("Mana 부족 - reroll");
        }
    }

    void CreateStorePlayer(){
        int n;
        for(int i = 0; i<5; i++){
            _newPos.x = -5.0f + 2*i;
            n = random.Next(0,1);

            StorePlayerArray[i] = Instantiate(PlayerArray[n]) as GameObject;
            StorePlayerArray[i].transform.position = _newPos;
            StorePlayerArray[i].SetActive(true);

            var a = UnitManager.Instance.CallUnitModelingObject(random.Next(10001,10005), StorePlayerArray[i].transform).GetComponentsInChildren<Transform>();
            for(int j=0;j<a.Length;j++){
                a[j].gameObject.layer = 6;
            }

        }
    }

    void DestroyAllStorePlayer(){
        foreach(GameObject toDestroy in StorePlayerArray)
            Destroy(toDestroy);
    }

}
