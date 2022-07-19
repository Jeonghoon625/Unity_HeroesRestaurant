using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSellectManager : MonoBehaviour
{
    public HeroList heroList;
    public void SellectAyran()
    {
        heroList.isSellect[0] = !heroList.isSellect[0];
    }

    public void SellectCouAuVin()
    {
        heroList.isSellect[1] = !heroList.isSellect[1];
    }
    public void SellectFondue()
    {
        heroList.isSellect[2] = !heroList.isSellect[2];
    }

    public void SellectLimu()
    {
        heroList.isSellect[3] = !heroList.isSellect[3];
    }
}
