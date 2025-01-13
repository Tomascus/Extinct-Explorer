using UnityEngine;

public class TrashCollection : MonoBehaviour
{
    private TrashItem nearbyTrashItem; // This is the trash item the player is near - for pickup 
    private TrashBin nearbyTrashBin;   // This is the trash bin the player is near - for deposit  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Checks to see if the player is near a trash item or a trash bin
            if (nearbyTrashItem != null)
            {
                CollectTrash();
            }
            else if (nearbyTrashBin != null)
            {
                DepositTrash();
            }
        }
    }

    // This method uses switch to determine the type of trash and increment corresponding trash count on the UI
    private void CollectTrash()
    {
        switch (nearbyTrashItem.trashType)
        {
            case TrashType.Plastic:
                TrashManager.Instance.plasticCount++;
                break;
            case TrashType.Hazardous:
                TrashManager.Instance.hazardousCount++;
                break;
            case TrashType.Organic:
                TrashManager.Instance.organicCount++;
                break;
            case TrashType.Paper:
                TrashManager.Instance.paperCount++;
                break;
            case TrashType.Oil:
                TrashManager.Instance.oilCount++;
                break;

            default: return;
        }

        Destroy(nearbyTrashItem.gameObject);
        nearbyTrashItem = null;

        TrashManager.Instance.UpdateUI(); // Update UI for all trash across characters
    }

    // This method uses switch to determine the type of trash and decrement corresponding trash count on the UI
    private void DepositTrash()
    {
        switch (nearbyTrashBin.binType)
        {
            case TrashType.Plastic:
                TrashManager.Instance.depositedPlastic += TrashManager.Instance.plasticCount;
                TrashManager.Instance.plasticCount = 0;
                break;
            case TrashType.Hazardous:
                TrashManager.Instance.depositedHazardous += TrashManager.Instance.hazardousCount;
                TrashManager.Instance.hazardousCount = 0;
                break;
            case TrashType.Organic:
                TrashManager.Instance.depositedOrganic += TrashManager.Instance.organicCount;
                TrashManager.Instance.organicCount = 0;
                break;
            case TrashType.Paper:
                TrashManager.Instance.depositedPaper += TrashManager.Instance.paperCount;
                TrashManager.Instance.paperCount = 0;
                break;
            case TrashType.Oil:
                TrashManager.Instance.depositedOil += TrashManager.Instance.oilCount;
                TrashManager.Instance.oilCount = 0;
                break;

            default: return;
        }

        TrashManager.Instance.UpdateUI(); 
    }

    // These methods are used by TrashItem and TrashBin to set and clear the nearby trash item and trash bin - for pickup and deposit 
    public void SetNearbyTrashItem(TrashItem trashItem)
    {
        nearbyTrashItem = trashItem;
    }

    public void ClearNearbyTrashItem()
    {
        nearbyTrashItem = null;
    }

    public void SetNearbyTrashBin(TrashBin trashBin)
    {
        nearbyTrashBin = trashBin;
    }

    public void ClearNearbyTrashBin()
    {
        nearbyTrashBin = null;
    }
}
