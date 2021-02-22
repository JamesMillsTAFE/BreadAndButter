using UnityEngine;

using NullReferenceException = System.NullReferenceException;
using InvalidOperationException = System.InvalidOperationException;

namespace BreadAndButter.Mobile
{
    public class MobileInput : MonoBehaviour
    {
        // Has the mobile input system been initialised
        public static bool Initialised => instance != null;

        // Singleton reference instance
        private static MobileInput instance = null;

        /// <summary>
        /// If the system isn't already setup, this will instantiate the mobile input prefab and assign
        /// the static reference.
        /// </summary>
        public static void Initialise()
        {
            // If the mobile input is already initialised, throw an Exception to tell the user they dun goofed.
            if(Initialised)
            {
                throw new InvalidOperationException("Mobile Input already initialised!");
            }

            // Load the Mobile Input prefab and instantiate it, setting the instance
            MobileInput prefabInstance = Resources.Load<MobileInput>("Mobile Input Prefab");
            instance = Instantiate(prefabInstance);

            // Changed the instantiated objects name and mark it to not be destroyed
            instance.gameObject.name = "Mobile Input";
            DontDestroyOnLoad(instance.gameObject);
        }

        /// <summary>
        /// Returns the value of the joystick from the joystick module if it is valid
        /// </summary>
        /// <param name="_axis">The axis to get the input from, Horizontal = x; Vertical = y</param>
        public static float GetJoystickAxis(JoystickAxis _axis)
        {
            // If the mobile input isn't initialised, thrown an InvalidOperationException
            if(!Initialised)
            {
                throw new InvalidOperationException("Mobile Input not initialised.");
            }

            // If the joystick input module isn't set, throw a NullReferenceException
            if(instance.joystickInput == null)
            {
                throw new NullReferenceException("Joystick Input reference not set.");
            }

            // Switch on the passed axis and return the appropriate value
            switch (_axis)
            {
                case JoystickAxis.Horizontal: return instance.joystickInput.Axis.x;
                case JoystickAxis.Vertical: return instance.joystickInput.Axis.y;
                default: return 0;
            }
        }

        /// <summary>
        /// Attempts to retrieve the relevant swipe information relating the the passed ID.
        /// Swiper no swiping! // DORA DORA DORA THE EXPLORER, DORA!
        /// </summary>
        /// <param name="_index">The fingerID we are attempting to get the swipe for.</param>
        /// <returns>The corresponding swipe if it exists, otherwise null.</returns>
        public static SwipeInput.Swipe GetSwipe(int _index)
        {
            // If the mobile input isn't initialised, thrown an InvalidOperationException
            if(!Initialised)
            {
                throw new InvalidOperationException("Mobile Input not initialised.");
            }

            // If the swipe input module isn't set, throw a NullReferenceException
            if(instance.swipeInput == null)
            {
                throw new NullReferenceException("Swipe Input reference not set.");
            }

            // Retrieve the swipe for this index from the swipe input manager
            return instance.swipeInput.GetSwipe(_index);
        }

        public static void GetFlickData(out float _flickPower, out Vector2 _flickDirection)
        {
            // If the mobile input isn't initialised, thrown an InvalidOperationException
            if(!Initialised)
            {
                throw new InvalidOperationException("Mobile Input not initialised.");
            }

            // If the swipe input module isn't set, throw a NullReferenceException
            if(instance.swipeInput == null)
            {
                throw new NullReferenceException("Swipe Input reference not set.");
            }

            // Set the out parameters to their corresponding values in the swipe input class
            _flickPower = instance.swipeInput.FlickPower;
            _flickDirection = instance.swipeInput.FlickDirection;
        }

        [SerializeField]
        private JoystickInput joystickInput;
        [SerializeField]
        private SwipeInput swipeInput;
    }
}