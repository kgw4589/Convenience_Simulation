using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class GrabbableSetter
{
    public void SetGrabbable(Transform transform, Rigidbody rigidbody)
    {
        // Grabbable 세팅
        Grabbable grabbable = transform.gameObject.AddComponent<Grabbable>();
        grabbable.InjectOptionalRigidbody(rigidbody);
        grabbable.InjectOptionalTargetTransform(transform);
        
        // Hand Grab Interactable 세팅
        GameObject child = new GameObject("HandGrabInteractable");
        child.transform.SetParent(transform);
        child.transform.position = Vector3.zero;
        child.transform.rotation = Quaternion.identity;
        child.transform.localScale = Vector3.one;

        HandGrabInteractable handGrabInteractable = child.AddComponent<HandGrabInteractable>();
        handGrabInteractable.InjectOptionalPointableElement(grabbable);
        handGrabInteractable.InjectRigidbody(rigidbody);

        GrabInteractable grabInteractable = child.AddComponent<GrabInteractable>();
        grabInteractable.InjectOptionalPointableElement(grabbable);
        grabInteractable.InjectRigidbody(rigidbody);
    }
}
