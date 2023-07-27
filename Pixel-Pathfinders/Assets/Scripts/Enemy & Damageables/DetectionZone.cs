using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedObjects = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            detectedObjects.Add(col);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            detectedObjects.Remove(col);
        }
    }
}
