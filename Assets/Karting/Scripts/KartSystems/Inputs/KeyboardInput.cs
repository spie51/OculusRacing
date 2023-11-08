using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public HingeJoint wheel;
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";

        public override InputData GenerateInput() {

            // Wheel Rotation to Turn Output Logic
            float steeringNormal = Mathf.InverseLerp(-0.35f, 0.35f, wheel.transform.localRotation.x);
            float steeringRange = -1 * Mathf.Lerp(-1, 1, steeringNormal);
            return new InputData
            {
                Accelerate = Input.GetButton(AccelerateButtonName),
                Brake = Input.GetButton(BrakeButtonName),
                TurnInput = (Mathf.Abs(steeringRange) < 0.2f) ? Input.GetAxis("Horizontal") : steeringRange

            };
        }
    }
}
