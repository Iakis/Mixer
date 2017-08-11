using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tut4 : MonoBehaviour
{

    public GameObject text;
    public Component mouse;
    public Component bob;

    private float red, blue, yellow;
    private Color c;

    public int i;
    // Use this for initialization
    void Start()
    {
        text = GameObject.Find("Canvas").transform.Find("asdf").gameObject;
        mouse = gameObject.GetComponent<Mouse>();
        bob = gameObject.GetComponent<Bob>();
        i = 0;
        red = gameObject.GetComponent<Mouse>().red;
        blue = gameObject.GetComponent<Mouse>().blue;
        yellow = gameObject.GetComponent<Mouse>().yellow;
        c = gameObject.GetComponent<Mouse>().mix(red, blue, yellow);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Mouse>().value == 1)
        {
            i = GameObject.Find("Bob").GetComponent<Bob>().check(gameObject.GetComponent<Mouse>().c);
            Debug.Log(i);
            Debug.Log(GameObject.Find("Bob").GetComponent<Bob>().co);
        }
        if (i == 1)
        {
            text.GetComponent<Text>().text = "Nice!";
        }
        else if (i == 3)
        {
            text.GetComponent<Text>().text = "Try again!";
        }
    }

    void OnDestroy()
    {
        if (gameObject.GetComponent<Mouse>().value == 1 && i == 3)
        {
            SceneManager.LoadScene("Tut4");
        }
        else if (gameObject.GetComponent<Mouse>().value == 1)
        {
            
            SceneManager.LoadScene("Info");
        }
    }
}
