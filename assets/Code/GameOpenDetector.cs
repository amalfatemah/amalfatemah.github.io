using System.Collections;
using System.Collections.Generic;
#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif
using UnityEngine;

public class GameOpenDetector : MonoBehaviour
{
    public GameObject AnotherInstancePanel;
    bool isSecondInstance;

    static bool InstanceChecked = false;
#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern bool DetectSecondInstance();

    [DllImport("__Internal")]
    private static extern void CloseWindow();

    public void TestSecondInstance()
    {
        isSecondInstance = DetectSecondInstance();
        Debug.Log("IS SECOND INSTANCE: " + isSecondInstance);
        InstanceChecked = true;
    }

    private void OnEnable()
    {
        //TODO: Check only once per session
        if (!InstanceChecked)
        {
            TestSecondInstance();
            if (isSecondInstance)
            {
                AnotherInstancePanel.SetActive(true);
                if (isSecondInstance)
                    StartCoroutine(CloseWindowUnity());
            }
            else
                AnotherInstancePanel.SetActive(false);
        }

    }

    IEnumerator CloseWindowUnity()
    {
        yield return new WaitForSeconds(3);
        CloseWindow();
    }
#endif

    /*private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private bool isAlreadyOpen;

    public void CheckIfGameOpen(string result)
    {
        isAlreadyOpen = bool.Parse(result);

        if (isAlreadyOpen)
        {
            // Display a message or take appropriate action
            Debug.Log("Game is already open in another tab or browser.");
        }
        else
        {
            // Continue with game initialization or login process
            Debug.Log("Game is not open in another tab or browser.");
        }
    }*/
}
