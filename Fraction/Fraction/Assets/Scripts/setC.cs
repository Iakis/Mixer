using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setC : MonoBehaviour {

    Color c;
	// Use this for initialization
	void Start () {
        c = gameObject.GetComponent<Mouse>().colour;
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", c);
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", c);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
