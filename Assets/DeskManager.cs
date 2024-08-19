using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskManager : MonoBehaviour
{
    public Image fillImage;
    public MoneyCollector moneyCollector;
    public Customer customer;
    public bool isPlayerOnDesk;
    private Room room;
    public Animator KeyFill;
    private bool isReceptionistOnDesk;
    public AudioSource attendCustAudio;
    public CustomerSpawner customerSpawner;
    public Transform customerStandPos;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Receptionist"))
            isReceptionistOnDesk = true;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            if (other.gameObject.GetComponent<Customer>().gotoRoom)
                return;

            customer = other.gameObject.GetComponent<Customer>();

            if (isPlayerOnDesk || isReceptionistOnDesk)
                CheckRoomsAvailable();
        }

        if (other.CompareTag("Receptionist"))
        {
            isReceptionistOnDesk = true;

            PlayerPrefs.SetString("Receptionist", "");

            CheckRoomsAvailable();
        }

        if (other.CompareTag("Player") && !isReceptionistOnDesk)
        {
            isPlayerOnDesk = true;

            CheckRoomsAvailable();
        }
    }

    public void CheckRoomsAvailable()
    {
        if (customer == null)
            return;

        if (!isPlayerOnDesk && !isReceptionistOnDesk)
            return;

        foreach (Room _room in FindObjectsOfType<Room>())
        {
            if (_room.transform.GetChild(0).gameObject.activeSelf)
            {
                if (!_room.isRoomBusy)
                {
                    room = _room;
                    SendCustomerToRoom();
                    print("Test");
                    break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isReceptionistOnDesk)
        {
            isPlayerOnDesk = false;
            KeyFill.enabled = false;
        }
    }

    public void SendCustomerToRoom()
    {
        print("FillKey");
        KeyFill.enabled = true;
    }

    public void FillKeyComplete()
    {
        room.isRoomBusy = true;

        KeyFill.Rebind();
        KeyFill.enabled = false;

        AttendCustomer();
    }

    private void AttendCustomer()
    {
        attendCustAudio.Play();

        customer.GotoRoom(room);
        customer = null;

        customerSpawner.customers.RemoveAt(0);
        customerSpawner.ArrangeCustomersInQue();
        customerSpawner.SpawnCustomer();

        moneyCollector.AddMoney(customerStandPos.position);
    }
}
