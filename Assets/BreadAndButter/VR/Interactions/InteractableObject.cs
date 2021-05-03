using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace BreadAndButter.VR.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableObject : MonoBehaviour
    {
        public Rigidbody Rigidbody => rigidbody;
        public Collider Collider => collider;
        public Transform AttachPoint => attachPoint;

        [SerializeField] private bool isGrabbable = true;
        [SerializeField] private bool isTouchable = false;
        [SerializeField] private bool isUsable = false;
        [SerializeField] private SteamVR_Input_Sources allowedSource = SteamVR_Input_Sources.Any;

        [Space]

        [SerializeField, Tooltip("The point on the interactable object we actually want to grab, if not set, will use origin.")] 
        private Transform attachPoint;

        [Space]

        public InteractionEvent onGrabbed = new InteractionEvent();
        public InteractionEvent onReleased = new InteractionEvent();
        public InteractionEvent onTouched = new InteractionEvent();
        public InteractionEvent onStopTouching = new InteractionEvent();
        public InteractionEvent onUsed = new InteractionEvent();
        public InteractionEvent onStopUsing = new InteractionEvent();

        private new Collider collider;
        private new Rigidbody rigidbody;

        // Start is called before the first frame update
        void Start()
        {
            collider = gameObject.GetComponent<Collider>();
            if(collider == null)
            { 
                collider = gameObject.AddComponent<BoxCollider>();
                Debug.LogError($"Object {name} does not have a collider, adding BoxCollider", gameObject);
            }

            rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private InteractEventArgs GenerateArgs(VrController _controller)
            => new InteractEventArgs(_controller, rigidbody, collider);

        public void OnObjectGrabbed(VrController _controller)
        {
            if(isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onGrabbed.Invoke(GenerateArgs(_controller));
        }

        public void OnObjectReleased(VrController _controller)
        {
            if(isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onReleased.Invoke(GenerateArgs(_controller));
        }

        public void OnObjectTouched(VrController _controller)
        {
            if(isTouchable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onTouched.Invoke(GenerateArgs(_controller));
        }

        public void OnObjectStopTouching(VrController _controller)
        {
            if(isTouchable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onStopTouching.Invoke(GenerateArgs(_controller));
        }

        public void OnObjectUsed(VrController _controller)
        {
            if(isUsable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onUsed.Invoke(GenerateArgs(_controller));
        }

        public void OnObjectStopUsing(VrController _controller)
        {
            if(isUsable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onStopUsing.Invoke(GenerateArgs(_controller));
        }
    }
}