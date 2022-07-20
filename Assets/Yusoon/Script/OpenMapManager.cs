using UnityEngine;
using UnityEngine.UI;

public class OpenMapManager : MonoBehaviour
{
    public Image map2;
    public Image map3;

    private void Start()
    {
    }
    public void OnEnable()
    {
        Debug.Log("Actived");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
            map2.enabled = !map2.enabled;


        if (Input.GetKeyDown(KeyCode.Alpha3))
            map3.enabled = !map3.enabled;
    }
}
