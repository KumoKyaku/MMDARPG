using UnityEngine;
using System.Collections;
using System;

public class Random_go : MonoBehaviour {

    public GameObject[] golist;
	// Use this for initialization
	void Start () {
        if (golist.Length != 0)
        {
            golist[new System.Random().Next(100) % golist.Length].SetActive(true);
        }
	}
}
