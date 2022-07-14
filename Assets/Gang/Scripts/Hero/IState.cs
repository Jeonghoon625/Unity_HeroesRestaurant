using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void IEnter(Unit unit);
    void IUpdate();

    void IFixedUpdate();
    void IExit();    
}
