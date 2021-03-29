using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    Vector3 target;
    float sightDist;
    public float sightDistMod;

    GameObject storedObj;
    public GameObject xMark;
    public GameObject xMarkOrange;
    GameObject currentXMark;

    bool objStored = false;
    bool mousePressed = false;

    Vector3 mousePos;
    Vector3 rayMousePos;
    Vector3 dropPos;

    public GameObject interactible;

    public List<GameObject> dots = new List<GameObject>();
    public float dotDist;
    float dotNumber;
    public GameObject dot;
    float dotX;
    float dotY;

    // Start is called before the first frame update
    void Start()
    {

        //this stops the code from dividing by 0 later (IMPORTANT!)
        if (dotDist <= 0)
        {
            dotDist = 1;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
        }
    }

    private void FixedUpdate()
    {
        //if the mouse has been clicked, play the GrabObject script (which stores and dumps objects)
        StoredObject();
    }

    void StoredObject()
    {
        Debug.Log("Ready to grab");

        //mousePos is the mouse's position in relation to the world origin (use for instantiating game objects)
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //rayMousePos is the mouse's position relative to the player (use for raycasting)
        rayMousePos = new Vector3(mousePos.x - gameObject.transform.position.x, mousePos.y - gameObject.transform.position.y, 1);

        //sightDist is the raycast's distance. this sets it to the mouse's current distance from the player
        sightDist = Mathf.Sqrt(Mathf.Pow(rayMousePos.x, 2) + Mathf.Pow(rayMousePos.y, 2));

        //target is the raycast's target position. this sets it to the mouse's current (x, y) position
        target = new Vector3(rayMousePos.x, rayMousePos.y, 1f);

        //this casts the ray and stores the info of its nearest collision
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target, sightDist);
        Debug.DrawRay(transform.position, target, Color.red);

        //dropPos is either the mouse's position (if the ray didn't collide with anything) or the point at which the ray collided with something
        if (hit.collider == null)
        {
            dropPos = new Vector3(mousePos.x, mousePos.y, 1);
        }
        else
        {
            dropPos = new Vector3(hit.point.x, hit.point.y, 1);
        }


        if (mousePressed)
        {
            //if there is no object currently stored, store the one you just collided with
            if (objStored == false)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Interactible")
                    {
                        Debug.Log("Hit interactible");
                        storedObj = interactible;
                        Destroy(hit.collider.gameObject);
                        objStored = true;
                    }
                }
            }

            //if there is an object stored, drop it at dropPos
            else
            {
                GameObject newObjA = Instantiate(storedObj, dropPos, transform.rotation);
                objStored = false;
            }

            mousePressed = false;
        }

        //destroys the current xmark
        Destroy(currentXMark);

        //places an xmark at the mouse's position or the collision position
        if (objStored)
        {
            GameObject newObjB = Instantiate(xMarkOrange, dropPos, transform.rotation);
            currentXMark = newObjB;
        }
        else
        {
            GameObject newObjB = Instantiate(xMark, dropPos, transform.rotation);
            currentXMark = newObjB;
        }

        //destroys the current dots and clears the list
        if (dots.Count != 0)
        {
            for(int j = dots.Count; j > 0; j--)
            {
                Destroy(dots[j-1]);
            }
            dots.Clear();
        }

        //puts down dots to form a line between the player and dropPos
        dotNumber = Mathf.Floor(sightDist / dotDist);

        dotX = (dropPos.x - transform.position.x)/dotNumber;
        dotY = (dropPos.y - transform.position.y)/dotNumber;
        for(float i = 1; i <= dotNumber; i++)
        {
            GameObject newObjC = Instantiate(dot, new Vector3(transform.position.x + (dotX * i), transform.position.y + (dotY * i), 1), transform.rotation);
            dots.Add(newObjC);
        }
    }
}
