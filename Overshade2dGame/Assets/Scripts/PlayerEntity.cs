using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public float health;    //don't set a value in the unity editor | set the maxHealth instead
    public float maxHealth;
    public int money;

    private void Start()
    {
        health = maxHealth;
    }
}
