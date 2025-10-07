using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class ThrowableObject : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    private Rigidbody rb;
    private Vector3 previousPosition;
    private Vector3 throwVelocity;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Track object movement to calculate velocity when released
        if (isSelected)
        {
            throwVelocity = (transform.position - previousPosition) / Time.fixedDeltaTime;
            previousPosition = transform.position;
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        rb.isKinematic = false;  // Allows physics once picked up
        previousPosition = transform.position;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        rb.isKinematic = false;
        rb.linearVelocity = throwVelocity; // Apply throw force
    }
}
