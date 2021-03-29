using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadAndButter.VR
{
    public class VrRig : MonoBehaviour
    {
        public static VrRig instance = null;

        public Transform LeftController => leftController;
        public Transform RightController => rightController;
        public Transform Headset => headset;
        public Transform PlayArea => playArea;

        [SerializeField] private Transform leftController;
        [SerializeField] private Transform rightController;
        [SerializeField] private Transform headset;
        [SerializeField] private Transform playArea;

        private VrController left;
        private VrController right;

        // This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only)
        private void OnValidate()
        {
            // Check if the set object isn't a VrController, if it isn't, unset it and warn the user.
            if(leftController != null && leftController.GetComponent<VrController>() == null)
            {
                // The object set to this variable is not of type VRController
                leftController = null;
                Debug.LogWarning("The object you are trying to set to leftController does not have VrController component on it!");
            }

            // Check if the set object isn't a VrController, if it isn't, unset it and warn the user.
            if (rightController != null && rightController.GetComponent<VrController>() == null)
            {
                // The object set to this variable is not of type VRController
                rightController = null;
                Debug.LogWarning("The object you are trying to set to rightController does not have VrController component on it!");
            }
        }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Validate all the transform components // Stupid us proofing
            ValidateComponent(leftController);
            ValidateComponent(rightController);
            ValidateComponent(headset);
            ValidateComponent(playArea);

            // Get the VrControllerComponents from the relevant controllers
            left = leftController.GetComponent<VrController>();
            right = rightController.GetComponent<VrController>();

            // Initialise the two controllers
            left.Initialise();
            right.Initialise();
        }

        private void ValidateComponent<T>(T _component) where T : Component
        {
            // If the component is null then log out the name of the component in an error
            if(_component == null)
            {
                Debug.LogError($"Component {nameof(_component)} is null! This has to be set.");
#if UNITY_EDITOR
                // The component was null and we are in the editor so stop the editor from playing
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
    }
}