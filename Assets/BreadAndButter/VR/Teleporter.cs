using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadAndButter.VR
{
    [RequireComponent(typeof(Pointer))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Pointer pointer;

        /// <summary>
        /// Called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// </summary>
        void OnValidate()
        {
            pointer = gameObject.GetComponent<Pointer>();
        }

        // Start is called before the first frame update
        void Start()
        {
            if(pointer == null)
                pointer = gameObject.GetComponent<Pointer>();

            pointer.controller.Input.OnTeleportPressed.AddListener(_args =>
            {
                if(pointer.Endpoint != Vector3.zero)
                {
                    VrRig.instance.PlayArea.position = pointer.Endpoint;
                }
            });
        }
    }
}