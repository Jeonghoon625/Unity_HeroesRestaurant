using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

public class JsonManager : MonoBehaviour
{
    public List<int> currentStageInfo;

    private FileInfo fileInfo;

    public void LoadStageData()
    {
        string fileName = "StageInfo";
        string path = Application.dataPath + "/" + "Json" + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            Debug.Log($"[DataManager] 세이브 데이터 없음");

            fileName = "DefaultStageInfo";
            path = Application.dataPath + "/" + "Json" + "/" + fileName + ".Json";

            string json = File.ReadAllText(path);

            currentStageInfo = JsonConvert.DeserializeObject<List<int>>(json);
        }
        else
        {
            string json = File.ReadAllText(path);
            currentStageInfo = JsonConvert.DeserializeObject<List<int>>(json);
        }

        Debug.Log($"[DataManager] 불러온 파일명 :  {fileName}");

    }

    public void SaveStageData()
    {
        string fileName = "StageInfo";
        string path = Application.dataPath + "/" + "Json" + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(currentStageInfo);
        File.WriteAllText(path, setJson);
        Debug.Log($"[DataManager] 저장한 파일명 : {fileName}");
    }

    public void DeleteStageData()
    {
        string fileName = "StageInfo";
        string path = Application.dataPath + "Json" + "/" + fileName + ".Json";

        File.Delete(path);
    }
}