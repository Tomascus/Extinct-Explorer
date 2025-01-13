using UnityEngine;

public class TrashItem : MonoBehaviour
{
    public TrashType trashType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Here I get the TrashCollection component from the player
            TrashCollection trashCollection = other.GetComponent<TrashCollection>();
            if (trashCollection != null)
            {
                trashCollection.SetNearbyTrashItem(this);
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
                trashCollection.ClearNearbyTrashItem();
            }
        }
    }
}
