using UnityEngine;
using UnityEngine.UI;

public class WaiterSpawner : MonoBehaviour
{
    public GameObject waiterPrefab;
    public Transform spawnPos;
    //public FoodSpawner _FoodSpawner;
    public int srNo;
    private int waiterIncreasePurchaseAmount = 50, buyAmount = 50;
    private int waitersCount = 0, tempCount = 0;
    public GameObject buyWaiterMenu;
    public Text buyWaiterAmount;
    private GameManager _GameManager;
    public Button buyButton;

    private void Start()
    {
        _GameManager = FindObjectOfType<GameManager>();

        buyAmount = PlayerPrefs.GetInt(srNo + "WaiterSpawnerBuyAmount", buyAmount);
        waitersCount = PlayerPrefs.GetInt(srNo + "WaitersCount", waitersCount);

        print(waitersCount + "waitersCount");

        if (tempCount < waitersCount)
        {
            InvokeRepeating("Spawn", .5f, .5f);
            print("testWaiter");
        }
    }

    public void SpawnWaiter()
    {
        if (_GameManager.collectedMoney >= buyAmount)
        {
            FindObjectOfType<AudioManager>().Play("WaiterSpawned");
            Spawn();

            _GameManager.collectedMoney -= buyAmount;
            _GameManager.ShowAndSave();

            buyAmount += waiterIncreasePurchaseAmount;
            PlayerPrefs.SetInt(srNo + "WaiterSpawnerBuyAmount", buyAmount);

            waitersCount++;
            PlayerPrefs.SetInt(srNo + "WaitersCount", waitersCount);
            print(waitersCount + "waitersCountplus");

            ShowBuyWaiterAmount();
            CheckButtonActive();
            Invoke("ShowAds", 1);
        }
    }
    
    public void CheckButtonActive()
    {
        ShowBuyWaiterAmount();

        if (_GameManager.collectedMoney >= buyAmount)
            buyButton.interactable = true;
        else
            buyButton.interactable = false;
    }

    private void Spawn()
    {
        tempCount++;

        if (tempCount == waitersCount)
        {
            CancelInvoke("Spawn");
        }

            GameObject waiter = Instantiate(waiterPrefab, spawnPos.position, spawnPos.rotation);
            //waiter.GetComponent<Waiter>()._FoodSpawner = _FoodSpawner;
            waiter.transform.parent = transform.parent.transform;
    }

    private void ShowBuyWaiterAmount()
    {
        buyWaiterAmount.text = buyAmount.ToString();
    }

    private void ShowAds()
    {
        FindObjectOfType<AdsManager>().ShowInterstitialAd();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SpawnWaiter();
    }
}
