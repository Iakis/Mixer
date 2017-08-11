using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class MatChange : MonoBehaviour {

    public AudioClip one;
    public AudioClip bad;
    AudioSource audioSource;

    public float value;
    public float value2;
    public float denom;
    public Score score;

    public int a;

    public bool tut;

    public Renderer rend;
    //Vector3 done;

    public GameObject spawn;
    public GameObject timer;
    public GameObject bob;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        init();
        spawn = GameObject.Find("Spawner");
        timer = GameObject.Find("Time");
        bob = GameObject.Find("Bob");

    }

    public void init()
    {
        if (!tut)
        {
            score = GameObject.Find("Canvas").GetComponentInChildren<Score>().GetComponent<Score>();
        }
        value = gameObject.GetComponent<Mouse>().value;
        value2 = value;
        denom = GCD((value * 100f), 100f);

        //done = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 2.0f);
        if (value == 1)
        {
            gameObject.GetComponentInChildren<TextMeshPro>().text = value.ToString();
            gameObject.GetComponentInChildren<TextMeshPro>().fontSize = 6;
        }
        else {
            gameObject.GetComponentInChildren<TextMeshPro>().text = (value * 100 / denom).ToString() + "/" + (100 / denom).ToString();
            gameObject.GetComponentInChildren<TextMeshPro>().fontSize = 4;
        }
        
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        if (value == 1)
        {
            audioSource.PlayOneShot(one, 0.3F);

            gameObject.transform.Find("Lid").gameObject.SetActive(true);
            gameObject.transform.Find("Lid").gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            gameObject.transform.Find("Cylinder").gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            gameObject.transform.Find("Cylinder (1)").gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            gameObject.transform.Find("Cylinder (2)").gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);

            gameObject.transform.Find("Lid").gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
            gameObject.transform.Find("Cylinder").gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
            gameObject.transform.Find("Cylinder (1)").gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
            gameObject.transform.Find("Cylinder (2)").gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);

            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.GetComponent<Move>().enabled = false;
            Destroy(gameObject, 3);

            if (!tut)
            {
                spawn.GetComponent<Spawn>().current -= 1;
                int index = spawn.GetComponent<Spawn>().buckets.IndexOf(this.gameObject);
                spawn.GetComponent<Spawn>().buckets.Remove(this.gameObject);
                spawn.GetComponent<Spawn>().cVals.RemoveAt(index);
                spawn.GetComponent<Spawn>().buckets.TrimExcess();
                spawn.GetComponent<Spawn>().cVals.TrimExcess();

                a = bob.GetComponent<Bob>().check(gameObject.GetComponent<Mouse>().colour);
                if (a == 1)
                {
                    //Debug.Log("nice");
                    score.score += 15;
                    timer.GetComponent<Timer>().time += 5;
                    bob.GetComponent<Bob>().rand();
                }
                else if (a == 2)
                {
                    //Debug.Log("close");
                    //score.score += 10;
                    //timer.GetComponent<Timer>().time += 2;
                    //bob.GetComponent<Bob>().rand();
                    score.score += 1;
                    bob.GetComponent<Bob>().rand();
                }
                else if (a == 3)
                {
                    score.score += 1;
                    //Debug.Log("Nope");
                    bob.GetComponent<Bob>().rand();
                }
            }
        }
        else if (value > 1)
        {
            audioSource.PlayOneShot(bad, 0.3F);
            //rend.sharedMaterial = bad;
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.GetComponent<Move>().enabled = false;
            //gameObject.GetComponent<Mouse>().enabled = false;
            //transform.position = Vector3.LerpUnclamped(transform.position, done, 0.5f);
            gameObject.transform.Find("Cylinder").gameObject.SetActive(false);
            gameObject.transform.Find("Cylinder (1)").gameObject.SetActive(false);
            gameObject.transform.Find("Cylinder (2)").gameObject.SetActive(false);
            Vector3 tempv = new Vector3(0, 0, 0.5f);
            gameObject.transform.Translate(tempv);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject, 3);
            score.score -= 10;
            spawn.GetComponent<Spawn>().current -= 1;
        }
    }

    float GCD(float a, float b)
    {
        
        return b == 0f ? a : GCD(b, a % b);
    }

    // Update is called once per frame
    void Update () {

    }
}
