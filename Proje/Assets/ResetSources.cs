using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSources : MonoBehaviour
{
    
    void Start()
    {
        KaynakYoneticisi.FoodAmount=600;
        KaynakYoneticisi.IronAmount=120;
        KaynakYoneticisi.WarPower=0;
        KaynakYoneticisi.WoodAmount=400;
        KaynakYoneticisi.StoneAmount=400;
        KaynakYoneticisi.GoldAmount=600;

    }

    
}
