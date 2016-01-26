using System.Collections;
using UnityEngine;

public class SplashImage : MonoBehaviour
{
    public static bool SplashDone = false;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (SplashDone)
        {
            GM.SplashDone = true;

            Destroy(gameObject);
        }
    }
}
