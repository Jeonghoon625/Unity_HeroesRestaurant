using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public void OnSave()
    {

    }

    public void OnLoad()
    {

    }

    public void OnActive(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void OffActive(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
