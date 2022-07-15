using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void IEnter(Heros hero);
    void IUpdate();

    void IFixedUpdate();
    void IExit();    
}
