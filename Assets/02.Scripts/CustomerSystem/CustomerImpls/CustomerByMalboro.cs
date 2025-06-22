using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerByMalboro : BaseCustomer
{
    public override void OnReady(List<Transform> rootInfos)
    {
        base.OnReady(rootInfos);

        CustomerManager.Instance.isBuyOk = true;
    }
}
