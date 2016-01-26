using UnityEngine;
using System.Collections;

public class ImageMove : MonoBehaviour
{

    public Vector3 startPos = Vector3.zero;
    public Vector3 endPos;

    [Range(0.02f, 10.00f)]
    public float duration = 1.00f;


    Vector3 step;
    int count;
    void Start()
    {
        transform.localPosition = startPos;
        count =(int)(duration / 0.02f);
        step = (endPos - startPos) / count;
    }

    public bool Move = true;
    public void FixedUpdate()
    {
        if (Move && count >= 0)
        {
            count--;
            transform.localPosition += step;
        }
        else
        {
            SplashImage.SplashDone = true;
        }
    }
}
