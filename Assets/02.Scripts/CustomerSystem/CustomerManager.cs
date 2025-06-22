using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>
{
    public List<CustomerRootInfo> customerRoots = new List<CustomerRootInfo>();
    public Transform customerOrigin;

    public bool isBuyOk = false;

    private BaseCustomer _currentCustomer;

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
        _currentCustomer = customer.GetComponent<BaseCustomer>();

        for (int i = 0; i < currentCustomerInfo.enableEvents.Count; i++)
        {
            currentCustomerInfo.enableEvents[i].SetActive(true);
        }
        
        _currentCustomer.OnReady(currentCustomerInfo.rootPositions);
    }

    public void StartCustomer()
    {
        _currentCustomer.OnStart();
    }

    public void EndCustomer()
    {
        _currentCustomer.OnEnd();
    }

    public void EndCustomerByBuy()
    {
        if (isBuyOk)
        {
            _currentCustomer.OnStart();
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