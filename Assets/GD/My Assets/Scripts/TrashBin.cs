using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashType binType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Here I get the TrashCollection component from the player and call SetNearbyTrashBin method
            TrashCollection trashCollection = other.GetComponent<TrashCollection>();
            if (trashCollection != null)
            {
                trashCollection.SetNearbyTrashBin(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TrashCollection trashCollection = other.GetComponent<TrashCollection>();
            if (trashCollection != null)
            {
                trashCollection.ClearNearbyTrashBin();
            }
        }
    }
}
