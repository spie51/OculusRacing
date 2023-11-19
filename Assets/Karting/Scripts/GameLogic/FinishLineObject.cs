using UnityEngine;
public class FinishLineObject : MonoBehaviour
{
    // [Header("LapObject")]
    // [Tooltip("Is this the first/last lap object?")]
    // public bool finishLap;

    [HideInInspector]
    public bool crossedByPlayer = false;
    public bool crossedByAI = false;
    public bool playerFinished = false;
    public bool AIFinished = false;

    public CheckpointObject checkpointObject;

    void Start()
    {
        checkpointObject = FindObjectOfType<CheckpointObject>();
        DebugUtility.HandleErrorIfNullFindObject<CheckpointObject, FinishLineObject>(checkpointObject, this);
        // Register();
    }

    void OnEnable()
    {
        // crossedByPlayer = false;
        // crossedByAI = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (!((layerMask.value & 1 << other.gameObject.layer) > 0 && other.CompareTag("Player")))
        //     return;

        // Objective.OnUnregisterPickup?.Invoke(this);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has passed finish line");
            crossedByPlayer = true;
            if (checkpointObject.crossedByPlayer)
            {
                playerFinished = true;
                Debug.Log("Player has finished race");
            }
        }
        if (other.CompareTag("AI"))
        {
            crossedByAI = true;
            Debug.Log("AI has passed finish line");
            if (checkpointObject.crossedByAI)
            {
                AIFinished = true;
                Debug.Log("AI has finished race");
            }
        }
    }
}
