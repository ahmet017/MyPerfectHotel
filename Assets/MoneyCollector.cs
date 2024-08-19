using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollector : MonoBehaviour
{
    public GameObject moneyPrefab;
    public Transform moneyTargetTra;
    private Vector3 moneyTargetPos;
    private int moneyCount, custPayMoneyAmo;
    private Vector3 moneySpawnPos;
    public List<Money> spawnedMoney = new List<Money>();
    private float giveMoneyTime = .2f;
    private Transform player;
    public GameManager gameManager;
    private AudioManager audioManager;
    GameObject moneyObj;
    Money money;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void AddMoney(Vector3 _moneySpawnPos)
    {
        moneySpawnPos = _moneySpawnPos;
        SpawnMoney();
        SpawnMoney();
    }

    private void SpawnMoney()
    {
        StartCoroutine("Test");
    }

    IEnumerator Test()
    {
        moneyObj = Instantiate(moneyPrefab, moneySpawnPos, Quaternion.identity, transform);
        money = moneyObj.GetComponent<Money>();
        spawnedMoney.Add(money);

        moneyObj = null;

        MoveMoneyTargetPos();

        money.GotoMoneyCollecter(transform.position, moneyTargetTra.position);

        yield return null;
    }

    private void MoveMoneyTargetPos()
    {
        moneyTargetPos = moneyTargetTra.localPosition;

        moneyTargetPos.y += .1f;

        moneyTargetTra.localPosition = moneyTargetPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {         
            player = other.transform;
            InvokeRepeating(nameof(GiveMoneyToPlayer), giveMoneyTime, giveMoneyTime);
        }
    }

    public void GiveMoneyToPlayer()
    {
        print("GiveMoneyToPlayer");

        if (spawnedMoney.Count == 0)
            return;

        ResetMoneyTargetPos();
        Money money = spawnedMoney[spawnedMoney.Count - 1];
        spawnedMoney.RemoveAt(spawnedMoney.Count - 1);
        money.GotoPlayer();
        audioManager.Play("MoneyCollect");
    }

    public void ResetMoneyTargetPos()
    {
        moneyTargetPos = moneyTargetTra.localPosition;
        moneyTargetPos.y -= .1f;
        moneyTargetTra.localPosition = moneyTargetPos;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            CancelInvoke(nameof(GiveMoneyToPlayer));
        }
    }
}
