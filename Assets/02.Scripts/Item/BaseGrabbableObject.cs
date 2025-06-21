using UnityEngine;

public abstract class BaseGrabbableObject : MonoBehaviour
{
    public Rigidbody rigidbody;
    
    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        
        GrabbableSetter grabbableSetter = new GrabbableSetter();
        grabbableSetter.SetGrabbable(transform, rigidbody);
    }
}
