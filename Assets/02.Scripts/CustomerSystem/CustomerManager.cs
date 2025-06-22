using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomerManager : Singleton<CustomerManager>
{
    public List<CustomerRootInfo> customerRoots = new List<CustomerRootInfo>();
    public Transform customerOrigin;

    public BaseCustomer currentCustomer;

    public bool isBuyOk = false;

    private float _readyDelay = 2f;
    
    public void ReadyCustomer()
    {
        if (GameManager.Instance.currentScore >= GameManager.Instance.dirScore)
        {
            return;
        }

        StartCoroutine(WaitReady());
    }

    protected virtual IEnumerator WaitReady()
    {
        yield return new WaitForSeconds(_readyDelay);

        CustomerRootInfo currentCustomerInfo = customerRoots[GameManager.Instance.currentScore];
        
        GameObject customer = Instantiate(currentCustomerInfo.customerFactory);
        customer.transform.position = customerOrigin.position;
        customer.transform.rotation = customerOrigin.rotation;
        currentCustomer = customer.GetComponent<BaseCustomer>();
        
        for (int i = 0; i < currentCustomerInfo.enableEvents.Count; i++)
        {
            currentCustomerInfo.enableEvents[i].SetActive(true);
        }
        
        currentCustomer.OnReady(currentCustomerInfo.rootPositions);
    }

    public void StartCustomer()
    {
        currentCustomer.OnStart();
    }

    public void EndCustomer()
    {
        currentCustomer.OnEnd();
    }

    public void EndCustomerByBuy()
    {
        if (isBuyOk && CounterManager.Instance.IsBuyOk())
        {
            currentCustomer.OnStart();
        }
    }
}

[Serializable]
public class CustomerRootInfo
{
    public GameObject customerFactory;
    public List<Transform> rootPositions;
    public List<GameObject> enableEvents;
}