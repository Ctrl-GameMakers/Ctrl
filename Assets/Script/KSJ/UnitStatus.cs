using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int _maxHP = 100;
    [SerializeField] private int _nowHP = 100;

    [SerializeField] private int _normalSkillID = 1111;
    [SerializeField] private int _specialSkillID = 2222;

    public int maxHP { get => _maxHP; }
    public int nowHP { get => _nowHP; }
    public int normalSkillID { get => _normalSkillID; }
    public int specialSkillID { get => _specialSkillID; }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
