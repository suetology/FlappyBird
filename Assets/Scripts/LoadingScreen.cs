using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private static LoadingScreen instance;
    public static LoadingScreen GetInstance() => instance;

    private void Awake()
    {
        instance = this;
        loadingCanvasAnimator = GetComponent<Animator>();
    }
    private Animator loadingCanvasAnimator;


    public float GetAnimationLength()
    {
        return loadingCanvasAnimator.GetCurrentAnimatorStateInfo(0).length;
    }
    public void LoadingScreedDarken()
    {
        loadingCanvasAnimator.SetTrigger("end");
    }
}
