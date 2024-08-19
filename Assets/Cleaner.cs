using UnityEngine;
using UnityEngine.AI;

public class Cleaner : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform dirtyRoom;
    private CleanIndicator dirtyObj;
    public Transform cleanerPos;
    private bool hasReachedDestination = true, hasReachedCleanerPos;
    public Animator anim;
    public float reachThreshold; // Adjust this threshold to your needs

    void Start()
    {
        FindDirtyRooms();
    }

    public void FindDirtyRooms()
    {
        if (dirtyObj != null)
            return;

        Room[] allRooms = FindObjectsOfType<Room>();

        foreach (Room room in allRooms)
        {
            if (room.isRoomDirty && !room.isCleaning)
            {
                dirtyRoom = room.transform;
                FindDirtyObjInRoom();
                break;
            }
        }

        if (dirtyObj == null)
            agent.SetDestination(cleanerPos.position);
    }

    private void FindDirtyObjInRoom()
    {
        anim.SetBool("Clean", false);

        dirtyObj = dirtyRoom.GetComponentInChildren<CleanIndicator>();

        if (dirtyObj == null)
            FindDirtyRooms();
        else
        {
            if(!dirtyRoom.GetComponent<Room>().isCleaning)
                dirtyRoom.GetComponent<Room>().isCleaning = true;

            agent.SetDestination(dirtyObj.transform.position);
            hasReachedDestination = false;
            hasReachedCleanerPos = false;
            anim.SetBool("Walk", true);
        }
    }

    private void Update()
    {
        if (!hasReachedDestination && agent.remainingDistance <= agent.stoppingDistance)
        {
            // The agent has reached the target position
            hasReachedDestination = true;

            StartCleanCount();

            anim.SetBool("Clean", true);
        }

        float distanceToTarget = Vector3.Distance(transform.position, cleanerPos.position);

        if (!hasReachedCleanerPos && distanceToTarget <= reachThreshold)
        {
            hasReachedCleanerPos = true;
            anim.SetBool("Walk", false);
        }
    }

    public void StartCleanCount()
    {
        print("StartCleanCount");
        Invoke(nameof(FindDirtyObjInRoom), 2);
    }
}
