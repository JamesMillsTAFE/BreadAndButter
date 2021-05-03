using System.Collections;
using System.Collections.Generic;
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
            // Safety measure to prevent connecting to something that don't exist yo.
            if(collidingObject == null)
                return;

            heldObject = collidingObject;
            collidingObject = null;
            FixedJoint joint = AddJoint(heldObject.Rigidbody);

            if(heldObject.AttachPoint != null)
            {
                heldObject.transform.position = 
                    transform.position - (heldObject.AttachPoint.position - heldObject.transform.position);
                heldObject.transform.rotation =
                    transform.rotation * Quaternion.Euler(heldObject.AttachPoint.localEulerAngles);
            }
            else
            {
                heldObject.transform.position = transform.position;
                heldObject.transform.rotation = transform.rotation;
            }

            grabbed.Invoke(new InteractEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
            heldObject.OnObjectGrabbed(input.Controller);
        }

        private void ReleaseObject()
        {
            RemoveJoint(gameObject.GetComponent<FixedJoint>());
            released.Invoke(new InteractEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
            heldObject.OnObjectReleased(input.Controller);
            heldObject = null;
        }

        private FixedJoint AddJoint(Rigidbody _rigidbody)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.breakForce = 20000;
            joint.breakTorque = 20000;
            joint.connectedBody = _rigidbody;
            return joint;
        }

        private void RemoveJoint(FixedJoint _joint)
        {
            if(_joint != null)
            {
                _joint.connectedBody = null;
                Destroy(_joint);
                heldObject.Rigidbody.velocity = input.Controller.Velocity;
                heldObject.Rigidbody.angularVelocity = input.Controller.AngularVelocity;
            }
        }
    }
}