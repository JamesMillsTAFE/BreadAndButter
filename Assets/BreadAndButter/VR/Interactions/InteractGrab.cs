using UnityEngine;

namespace BreadAndButter.VR.Interaction
{
    [RequireComponent(typeof(VrControllerInput))]
    public class InteractGrab : MonoBehaviour
    {
        public InteractionEvent grabbed = new InteractionEvent();
        public InteractionEvent released = new InteractionEvent();

        private VrControllerInput input;

        private InteractableObject collidingObject;
        private InteractableObject heldObject;

        // The held object's original parent before it got reparented to this controller
        private Transform heldOriginalParent;

        // Start is called before the first frame update
        void Start()
        {
            input = gameObject.GetComponent<VrControllerInput>();

            input.OnGrabPressed.AddListener((_args) => { if (collidingObject != null) GrabObject(); });
            input.OnGrabReleased.AddListener((_args) => { if (heldObject != null) ReleaseObject(); });
        }

        private void SetCollidingObject(Collider _other)
        {
            InteractableObject interactable = _other.GetComponent<InteractableObject>();
            if(collidingObject != null || interactable == null) return;
            collidingObject = interactable;
        }

        private void OnTriggerEnter(Collider _other) => SetCollidingObject(_other);

        private void OnTriggerExit(Collider _other)
        {
            if(collidingObject == _other.GetComponent<InteractableObject>()) collidingObject = null;
        }

        private void GrabObject()
        {
            heldObject = collidingObject;
            collidingObject = null;

            heldOriginalParent = heldObject.transform.parent;

            heldObject.Rigidbody.isKinematic = true;
            SnapObject(heldObject.transform, heldObject.AttachPoint);
        
            heldObject.OnObjectGrabbed(input.Controller);
            grabbed.Invoke(new InteractEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
        }

        private void ReleaseObject()
        {
            heldObject.Rigidbody.isKinematic = false;
            heldObject.transform.SetParent(heldOriginalParent);

            heldObject.Rigidbody.velocity = input.Controller.Velocity;
            heldObject.Rigidbody.angularVelocity = input.Controller.AngularVelocity;

            heldObject.OnObjectReleased(input.Controller);
            released.Invoke(new InteractEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
            heldObject = null;
        }

        private void SnapObject(Transform _object, Transform _snapHandle)
        {
            Rigidbody attachPoint = input.Controller.Rigidbody;
            _object.transform.SetParent(transform);
            if(_snapHandle == null)
            {
                // Reset it to the same as the controllers position + rotation
                _object.localPosition = Vector3.zero;
                _object.localRotation = Quaternion.identity;
            }
            else
            {
                // Calculate the correct position and rotation based on the snap handle
                _object.rotation = attachPoint.transform.rotation * Quaternion.Euler(_snapHandle.localEulerAngles);
                _object.position = attachPoint.transform.position - (_snapHandle.position - _object.position);
            }
        }
    }
}