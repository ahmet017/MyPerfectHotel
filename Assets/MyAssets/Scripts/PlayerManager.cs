using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Animator camAnim;
    private bool camTurn;
    private bool camStraight = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuyPoint"))
            other.GetComponent<BuyPoint>().StartSpend();

        // For Right Rooms

        if (other.CompareTag("GateEnterRight"))
        {
            print("Exit");

            if (camTurn)
            {
                camStraight = true;
                camAnim.SetTrigger("StraightR");
                camTurn = false;
            }
        }

        if (other.CompareTag("RoomEnterRight"))
        {
            print("Enter");

            if (camStraight)
            {
                camTurn = true;
                camAnim.SetTrigger("TurnR");
                camStraight = false;
            }
        }

        // For Left Rooms
        if (other.CompareTag("GateEnterLeft"))
        {
            print("Exit");
            if (camTurn)
            {
                camStraight = true;
                camAnim.SetTrigger("StraightL");
                camTurn = false;
            }
        }

        if (other.CompareTag("RoomEnterLeft"))
        {
            print("Enter");
            if (camStraight)
            {
                camTurn = true;
                camAnim.SetTrigger("TurnL");
                camStraight = false;
            }
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BuyPoint"))
            other.GetComponent<BuyPoint>().StopSpend();
    }
}
