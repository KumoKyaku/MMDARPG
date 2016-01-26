using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour
{

    public string fieldPath;
    GameObject fieldpreab;
    void Awake()
    {
        if (string.IsNullOrEmpty(fieldPath))
        {
            return;
        }

        transform.ReSet();

        fieldpreab = Loader.Instantiate(fieldPath,transform);
    }
}
