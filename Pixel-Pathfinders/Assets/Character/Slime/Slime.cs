using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float Health {
        set {}
    }
    public float health = 3;
    void OnHit(float damage) {
        Debug.Log("Slime hit for " + damage);
    }
}
