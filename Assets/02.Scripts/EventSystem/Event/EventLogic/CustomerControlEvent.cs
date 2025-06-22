using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class CustomerControlEvent : IEventable
{
    public void EventAction(EventElementData eventElement)
    {
        if (eventElement.customerControlType is EventElementData.CustomerControlType.Ready)
        {
            CustomerManager.Instance.ReadyCustomer();
        }
        else if (eventElement.customerControlType is EventElementData.CustomerControlType.Start)
        {
            CustomerManager.Instance.StartCustomer();
        }
        else if (eventElement.customerControlType is EventElementData.CustomerControlType.End)
        {
            CustomerManager.Instance.EndCustomer();
        }
    }
}
