using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    Button levelUpBtn;
    private static int playerLevel;
    private static int playerExp;
    private static int[] expToLevelup = new int[100];
    private static int[] reducingMana = new int[100];

    private static int mana;

    void Start(){
        InitPlayerInfo();

        levelUpBtn = this.transform.GetComponent<Button>();
        if(levelUpBtn != null){
            levelUpBtn.onClick.AddListener(LevelUpBtnClick);
        }

    }

    void InitPlayerInfo(){
        playerLevel=1;
        playerExp=0;
        mana=20;

        for(int i=0; i<expToLevelup.Length; i++){
            expToLevelup[i] = 20;
        }

        for(int i=0; i<expToLevelup.Length; i++){
            reducingMana[i] = (i+1)*2;
        }

    }

    public static int GetPlayerLevel(){
        return playerLevel;
    }

    public static int GetPlayerExp(){
        return playerExp;
    }

    public static int GetMana(){
        return mana;
    }
    
    void LevelUpBtnClick(){
        if(mana >= reducingMana[playerLevel-1]){
            LevelUp();
            mana -= reducingMana[playerLevel-1];
        }
        else{
            Debug.Log("Mana 부족!");
        }
        Debug.Log("현재 마나: "+mana+"Exp: "+ playerExp+"level: "+ playerLevel);
    }

    void LevelUp(){
        playerExp += 4;
        if(playerExp>=expToLevelup[playerLevel-1]){
            playerExp %= expToLevelup[playerLevel-1];
            playerLevel++;
            Debug.Log("level up!");
        }
        
    }

    public static int ReduceMana(int reducingMana){
        mana -= reducingMana;
        return mana;
    }

}
