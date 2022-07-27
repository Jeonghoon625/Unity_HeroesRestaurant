using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField][Header("Tutorials items")] TutorialsItemControl[] items;
    int itemIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (items == null)
            return;

        if (items.Length == 0)
            return;

        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
        }

        itemIndex = -1;
        ActiveNextItem();
    }

    public void ActiveNextItem()
    {
        // 현재 아이템 비활성화
        if (itemIndex > -1 && itemIndex < items.Length)
        {
            items[itemIndex].gameObject.SetActive(false);
        }

        // 인덱스 변경
        itemIndex++;

        if (itemIndex > -1 && itemIndex < items.Length)
        {
            items[itemIndex].gameObject.SetActive(true);
        }
    }
}
