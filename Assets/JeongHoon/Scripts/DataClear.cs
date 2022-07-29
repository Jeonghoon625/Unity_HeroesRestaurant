using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DataClear : MonoBehaviour
{
    public void OnClickMenu(GameObject ui)
    {
        ui.SetActive(true);
    }

    public void OffClickMenu(GameObject ui)
    {
        ui.SetActive(false);
    }

    public void OnClickClear()
    {
        foreach (var directory in Directory.GetDirectories(Application.persistentDataPath))
        {
            DirectoryInfo data_dir = new DirectoryInfo(directory);
            data_dir.Delete(true);
        }

        foreach (var file in Directory.GetFiles(Application.persistentDataPath))
        {
            FileInfo file_info = new FileInfo(file);
            file_info.Delete();
        }

        GameManager.Instance.Clear();
        SceneLoader.LoadScene("TitleScene");
    }
}
