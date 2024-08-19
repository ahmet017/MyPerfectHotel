using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillKey : MonoBehaviour
{
    public DeskManager deskManager;

    private void Start()
    {
        deskManager = FindObjectOfType<DeskManager>();
    }

    public void KeyFillComplete()
    {
        deskManager.FillKeyComplete();
    }
}
