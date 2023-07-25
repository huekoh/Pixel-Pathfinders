using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private ItemGiver itemGiver;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ResetItemGiver);
    }

    private void ResetItemGiver()
    {
        itemGiver.Reset();
    }
}