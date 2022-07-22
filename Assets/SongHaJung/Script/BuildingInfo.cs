using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo
{
    public BuildingInfo(int _activIndex, float _positionX, bool _Istrue)
    {
        activIndex = _activIndex;
        positionX = _positionX;
        Istrue = _Istrue;
    }
    public int activIndex;
    public float positionX;
    public bool Istrue;
}

public class GameInfo
{
    public List<BuildingInfo> mainsInfo;
    public GameInfo()
    {
        mainsInfo = new List<BuildingInfo>();
    }
}
