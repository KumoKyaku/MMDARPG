using UnityEngine;
using Poi;

public class KeyCodeListener : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GM.Exit(1);
        }
    }
}
