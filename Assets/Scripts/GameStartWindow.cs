using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartWindow : MonoBehaviour
{
    private static GameStartWindow instance;
    public static GameStartWindow GetInstance() => instance;

    public void CloseStartWindow()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }


}
