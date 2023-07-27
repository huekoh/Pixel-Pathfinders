using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float timeToLive = 0.5f;
    float timeElapsed = 0.0f;
    public Vector3 direction = new Vector3(0, 0.5f, 0);
    public float floatSpeed = 5;
    Color originalColor;
    TextMeshPro textMeshPro;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        rectTransform = GetComponent<RectTransform>();
        originalColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        rectTransform.position += direction * floatSpeed * Time.deltaTime;

        // Text fades over time
        textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - (timeElapsed/ timeToLive));

        if (timeElapsed > timeToLive) {
            Destroy(gameObject);
        }
    }
}
