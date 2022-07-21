using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo
{
    public int activeselectionIndex;
    public float positionX;
    public bool Istrue;

    public BuildingInfo(int _activeselectionIndex, float _positionX = 0, bool _Istrue)
    {
        this.activeselectionIndex = _activeselectionIndex;
        this.positionX = _positionX;
        this.Istrue = _Istrue;
    }
}
