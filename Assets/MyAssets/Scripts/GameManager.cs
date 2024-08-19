using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int collectedMoney;
    private CanvasUiManager _CanvasUiManager;

    private void Start()
    {
        Application.targetFrameRate = 60;

        _CanvasUiManager = FindObjectOfType<CanvasUiManager>();
        collectedMoney = PlayerPrefs.GetInt("MoneyAmount", 0);
        _CanvasUiManager.SetMoneyText(collectedMoney);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            AddMoney(50000);
        }
    }

    public void AddMoney(int amount)
    {
        collectedMoney += amount;
        ShowAndSave();
    }

    public void LessMoney(int amount)
    {
        collectedMoney -= amount;
        ShowAndSave();
    }

    public void ShowAndSave()
    {
        _CanvasUiManager.SetMoneyText(collectedMoney);
        PlayerPrefs.SetInt("MoneyAmount", collectedMoney);
    }
}
