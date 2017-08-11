using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Move : MonoBehaviour {

    public AudioClip click;
    public AudioClip drop;
    AudioSource audioSource;

    public bool mov;
    public bool cont;
    public bool tut;

    public Vector3 mousePosition;
    public Vector3 savedPos;
    public float moveSpeed = 0.5f;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        mov = false;
        cont = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //audioSource.PlayOneShot(click, 0.03F);
            if (Cursor.visible == false)
            {
                Cursor.visible = true;
            }
            RaycastHit hit = new RaycastHit();
            bool hitinfo = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            if (hitinfo)
            {
                
                if (hit.transform.gameObject == this.gameObject && mov == false && this.cont == false)
                {
                    if (!tut)
                    {
                        GameObject.Find("Comp").GetComponent<Comp>().bucket = this.gameObject;
                        GameObject.Find("Comp").GetComponent<Comp>().init();
                    }
                    mov = true;
                }

            }
        }
        if (mov == true && Input.GetMouseButton(0))  
        {
            Cursor.visible = false;
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = -1;
            transform.position = Vector3.Lerp(transform.position, mousePosition, 1f);
        }
    }

    void OnMouseUp()
    {
        Cursor.visible = true;
        if (mov == true)
        {
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            mov = false;
        }
        Invoke("check", 0.1f);
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (mov == false && col.gameObject.GetComponent<Move>().mov == false) {
            cont = true;
        }
    }

    void check ()
    {
        if (cont == true && gameObject.GetComponent<Mouse>().touch != null)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            cont = false;
        }
    }
}
