using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tut1 : MonoBehaviour {

    public GameObject text;
	// Use this for initialization
	void Start () {
        text = GameObject.Find("Canvas").transform.Find("asdf").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy ()
    {
        text.GetComponent<Text>().text = "Nice!";
        Debug.Log((Color32)gameObject.GetComponent<Mouse>().colour);
        if (gameObject.GetComponent<Mouse>().value == 1)
        {
            SceneManager.LoadScene("Tut2");
        }
        
    }
}
