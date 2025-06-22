using System.Collections.Generic;
using UnityEngine;

public class CustomerByNormalBuy : BaseCustomer
{
    public override void OnReady(List<Transform> rootInfos)
    {
        base.OnReady(rootInfos);

        CustomerManager.Instance.isBuyOk = true;
    }
}
