using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tut3 : MonoBehaviour
{

    public GameObject text;
    public Component mouse;
    public Component bob;

    public int i;
    // Use this for initialization
    void Start()
    {
        text = GameObject.Find("Canvas").transform.Find("asdf").gameObject;
        mouse = gameObject.GetComponent<Mouse>();
        bob = gameObject.GetComponent<Bob>();
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Mouse>().value == 1)
        {
            i = GameObject.Find("Bob").GetComponent<Bob>().check(gameObject.GetComponent<Mouse>().colour);
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
        if (gameObject.GetComponent<Mouse>().value == 1 && i == 1)
        {
            SceneManager.LoadScene("Tut4");
        }
        else if (gameObject.GetComponent<Mouse>().value == 1)
        {
            SceneManager.LoadScene("Tut3");
        }
    }
}
