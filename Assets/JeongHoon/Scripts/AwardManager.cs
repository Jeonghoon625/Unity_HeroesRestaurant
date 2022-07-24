using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardManager
{
    public void Award(int currencyId, int sum)
    {
        GameManager.Instance.goodsManager.currencyReserve[currencyId] += sum;
    }
}
