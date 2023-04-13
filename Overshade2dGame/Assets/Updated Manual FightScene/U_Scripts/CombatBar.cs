using System;
using UnityEngine;
using UnityEngine.UI;

public class CombatBar : MonoBehaviour
{
    [SerializeField]private GameObject combatBar;               //slider bar
    [Range(0,1)][SerializeField] private float refillRate;      //refill rate per second
    [Range(0, 1)][SerializeField] private float dropDownRate;   //drop down rate per attack
    [Range(0, 10)][SerializeField] private float refillWaitTime;//wait time before refill

    private float combatValue = 1f;                             //current value of the slider (0 - 1 range)
    private float lastAttackTime = 0f;                          //last time the player attacked

    void Update()
    {
        lastAttackTime += Time.deltaTime;
        if (lastAttackTime >= refillWaitTime)
        {//refill
            combatValue += refillRate * Time.deltaTime;
            combatValue = Mathf.Min(1, combatValue);
        }
        combatBar.GetComponent<Slider>().value = combatValue;
    }

    public void reduceValue()
    {
        lastAttackTime = 0;
        combatValue -= dropDownRate;
        combatValue = Mathf.Max(0,combatValue);
    }
    
    public bool canAttack()
    {
        return combatValue >= dropDownRate;
    }

}
