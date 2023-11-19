using UnityEngine;
public class CheckpointObject : MonoBehaviour
{
    // [Header("PickupObject")]

    // [Tooltip("New Gameobject (a VFX for example) to spawn when you trigger this PickupObject")]
    // public GameObject spawnPrefabOnPickup;

    // [Tooltip("Destroy the spawned spawnPrefabOnPickup gameobject after this delay time. Time is in seconds.")]
    // public float destroySpawnPrefabDelay = 10;

    // [Tooltip("Destroy this gameobject after collectDuration seconds")]
    // public float collectDuration = 0f;

    [HideInInspector]
    public bool crossedByPlayer = false;
    public bool crossedByAI = false;

    void Start()
    {
        // Register();
    }

    void OnCollect()
    {
        // if (CollectSound)
        // {
        //     AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        // }

        // if (spawnPrefabOnPickup)
        // {
        //     var vfx = Instantiate(spawnPrefabOnPickup, CollectVFXSpawnPoint.position, Quaternion.identity);
        //     Destroy(vfx, destroySpawnPrefabDelay);
        // }

        // Objective.OnUnregisterPickup(this);

        // TimeManager.OnAdjustTime(TimeGained);

        // Destroy(gameObject, collectDuration);
    }

    void OnTriggerEnter(Collider other)
    {
        // if ((layerMask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
        // {
        //     OnCollect();
        // }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has passed checkpoint");
            crossedByPlayer = true;
        }
        if (other.CompareTag("AI"))
        {
            crossedByAI = true;
            Debug.Log("AI has passed checkpoint");
        }
    }
}
