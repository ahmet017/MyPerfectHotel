using UnityEngine;

public class CleanManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoomCleanObject"))
            other.GetComponent<CleanIndicator>().StartCleanCount();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RoomCleanObject"))
            other.GetComponent<CleanIndicator>().StopCleanCount();
    }
}
