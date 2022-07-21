using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingToTitle : MonoBehaviour
{
    public float time;
    public void Start()
    {
        Invoke("GoTitle", time);
    }
    public void GoTitle()
    {
        GameManager.Instance.ChanageScene("TitleScene");
    }
}
