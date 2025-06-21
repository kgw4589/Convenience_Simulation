using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsValidLayer(GameObject target, LayerMask layerMask)
    {
        int checkingLayer = layerMask.value;
        
        return (checkingLayer & (1 << target.layer)) != 0;
    }
}
