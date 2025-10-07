using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableObject : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        rb.isKinematic = false; // Make sure it can move while grabbed
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        rb.isKinematic = false; // Allow physics when dropped
    }
}
