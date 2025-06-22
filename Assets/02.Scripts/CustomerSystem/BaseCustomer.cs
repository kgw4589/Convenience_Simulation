using UnityEngine;

public abstract class BaseCustomer : MonoBehaviour, ICustomerable
{
    
    
    public void OnStart()
    {
        
    }

    public void OnEvent()
    {
        throw new System.NotImplementedException();
    }

    public void OnEnd()
    {
        throw new System.NotImplementedException();
    }
}
