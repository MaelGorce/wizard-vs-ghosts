using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    public enum EChoosingIntensity : ushort
    {
        eSilver = 0,
        eGold = 1,
        eRubis = 2,
        eDiamnd = 3
    }
    public enum EChoosingAugment : ushort
    {
        eSkip = 0,
        ePlayerSpeed = 10,
        ePlayerHealth = 11,
        eSpell1CD = 100,
        eSpell1Damage = 101,
        eSpell2CD = 200,
        eSpell2Damage = 201,
        eSpell2LivingDuration = 202,
        eSpell2Speed = 203
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
