using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

namespace BreadAndButter.VR
{
    [RequireComponent(typeof(SteamVR_Behaviour_Pose))]
    [RequireComponent(typeof(VrControllerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class VrController : MonoBehaviour
    {
        public Rigidbody Rigidbody => rigidbody;
        public VrControllerInput Input => input;
        /// <summary>
        /// How fast the controller is moving in worldspace
        /// </summary>
        public Vector3 Velocity => pose.GetVelocity();
        /// <summary>
        /// How fast the controller is rotating and in which direction.
        /// </summary>
        public Vector3 AngularVelocity => pose.GetAngularVelocity();

        public SteamVR_Input_Sources InputSource => pose.inputSource;

        private SteamVR_Behaviour_Pose pose;
        private VrControllerInput input;
        private new Rigidbody rigidbody;

        public void Initialise()
        {
            pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
            input = gameObject.GetComponent<VrControllerInput>();
            rigidbody = gameObject.GetComponent<Rigidbody>();

            input.Initialise(this);
        }
    }
}