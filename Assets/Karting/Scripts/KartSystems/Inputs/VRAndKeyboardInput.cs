using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public bool hasVrSteering;
        public HingeJoint wheel;
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";

        public override InputData GenerateInput() {
            float steeringNormal, steeringRange;

            // Wheel Rotation to Turn Output Logic
            if (hasVrSteering)
            {
                steeringNormal = Mathf.InverseLerp(-0.35f, 0.35f, wheel.transform.localRotation.x);
                steeringRange =  -1 * Mathf.Lerp(-1, 1, steeringNormal);
            }
            else
            {
                steeringRange = 0;
            }
            return new InputData
            {
                Accelerate = Input.GetButton(AccelerateButtonName),
                Brake = Input.GetButton(BrakeButtonName),
                TurnInput = (Mathf.Abs(steeringRange) < 0.2f) ? Input.GetAxis("Horizontal") : steeringRange

            };
        }
    }
}
