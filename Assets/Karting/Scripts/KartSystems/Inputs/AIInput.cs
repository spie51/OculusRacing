using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KartGame.KartSystems {

    public class AIInput : BaseInput
    {

        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";

        public enum AIMode { followPlayer, followWaypoints };
        
        public AIMode aiMode;
        Vector3 targetPosition = Vector3.zero;
        Transform targetTransform = null;
        WaypointNode currentWaypoint = null;
        WaypointNode[] allWaypoints;

        void Awake(){
            allWaypoints = FindObjectsOfType<WaypointNode>();
        }

        public override InputData GenerateInput() {
            // FollowPlayer();

            return new InputData
            {
                Accelerate = true,
                Brake = false,
                TurnInput = TurnTowardTarget() * 1.0f
            };
        }

        void FollowPlayer()
        {
            if(targetTransform == null)
            {
                targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
            if(targetTransform != null)
                targetPosition = targetTransform.position;
            
            // Debug.Log("targetPosition " + targetPosition);
        }

        void FollowWaypoints()
        {
            if (currentWaypoint == null)
                currentWaypoint = allWaypoints.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();
            
            if(currentWaypoint != null)
            {
                targetPosition = currentWaypoint.transform.position;

                float distanceToWaypoint = (targetPosition - transform.position).magnitude;
                // Debug.Log("Distance From " + targetPosition + ": " + distanceToWaypoint);

                if(distanceToWaypoint <= currentWaypoint.minDistanceToReachWaypoint)
                {
                    // Debug.Log("Switching Waypoints");
                    currentWaypoint = currentWaypoint.next[0];
                }

            }

            // Debug.Log("targetPosition " + targetPosition);

        }


        float TurnTowardTarget()
        {
            if(aiMode == AIMode.followPlayer)
            {
                FollowPlayer();
            }
            else if(aiMode == AIMode.followWaypoints)
            {
                FollowWaypoints();
            }
            

            Vector3 vectorToTarget = targetPosition - transform.position;
            vectorToTarget.Normalize();

            Vector3 forward = transform.forward;
            // float angleToTarget = Vector3.SignedAngle(vectorToTarget, forward, Vector3.up);
            float angleToTarget = Vector3.SignedAngle(vectorToTarget, forward, Vector3.up);
            // Debug.Log(vector)
            // Debug.Log("Forward + Up " + Vector3.SignedAngle(vectorToTarget, transform.forward, Vector3.up));
            // Debug.Log("Right + Up " + Vector3.SignedAngle(vectorToTarget, transform.right, Vector3.up));
            // Debug.Log("Up + Up " + Vector3.SignedAngle(vectorToTarget, transform.up, Vector3.up));


            // Debug.Log("Forward + Forward " + Vector3.SignedAngle(vectorToTarget, transform.forward, Vector3.forward));


            // Debug.Log("Right + Forward " + Vector3.SignedAngle(vectorToTarget, transform.right, Vector3.forward));
            // Debug.Log("Up + Forward " + Vector3.SignedAngle(vectorToTarget, transform.up, Vector3.forward));


            // Debug.Log("Forward + Right " + Vector3.SignedAngle(vectorToTarget, transform.forward, Vector3.right));


            // Debug.Log("Right + Right " + Vector3.SignedAngle(vectorToTarget, transform.right, Vector3.right));
            // Debug.Log("Up + Right " + Vector3.SignedAngle(vectorToTarget, transform.up, Vector3.right));
            

            angleToTarget *= -1;

            float steerAmount = angleToTarget / 45.0f;
            steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);
            // Debug.Log("Steering " + steerAmount);
            
            return steerAmount;
        }
    }
}
