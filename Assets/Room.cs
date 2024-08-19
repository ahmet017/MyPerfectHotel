using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Animator lightAnim, bedAnim, sideTable, curtain;
    private int cleanCount;
    public GameObject cleaningSign;
/*    [HideInInspector]*/ public bool isRoomDirty, isCleaning, isRoomBusy;
    private AudioSource cleanAudio;
    public Transform roomBedPoint, toiletPoint;

    private void Start()
    {
        cleanAudio = GetComponent<AudioSource>();
    }

    public void EnterRoom()
    {
        lightAnim.SetTrigger("LightOff");
    }

    public void ExitRoom()
    {
        bedAnim.SetBool("IsDirty", true);
        sideTable.SetBool("IsDirty", true);
        curtain.SetBool("IsDirty", true);

        bedAnim.transform.Find("IndicatorClear").gameObject.SetActive(true);
        sideTable.transform.Find("IndicatorClear").gameObject.SetActive(true);
        curtain.transform.Find("IndicatorClear").gameObject.SetActive(true);

        lightAnim.SetTrigger("LightOn");
        cleaningSign.SetActive(true);
        isRoomDirty = true;

        CallCleaners();
    }

    private void CallCleaners()
    {
        Cleaner[] allCleaners = FindObjectsOfType<Cleaner>();

        foreach (Cleaner cleaner in allCleaners)
        {
            cleaner.FindDirtyRooms();
        }
    }

    public void Cleaned()
    {
        cleanAudio.Play();

        cleanCount++;

        if(cleanCount == 3)
        {
            cleanCount = 0;
            GetComponentInChildren<ParticleSystem>().Play();
            cleaningSign.SetActive(false);

            isRoomBusy = false;

                isRoomDirty = false;
                isCleaning = false;
                FindObjectOfType<DeskManager>().CheckRoomsAvailable();
        }
    }
}
