using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    Vector3 target;
    float sightDist;
    public float sightDistMod;

    GameObject storedObj;

    bool objStored = false;
    bool mousePressed = false;

    Vector3 mousePos;
    Vector3 rayMousePos;
    Vector3 dropPos;

    public GameObject interactible;

    // Start is called before the first frame update
    void Start()
    {

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
        if (mousePressed == true)
        {
            mousePressed = false;
            StoredObject();
        }
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
        //if there is an object stored, drop it at the mouse's position
        else
        {
            if (hit.collider == null)
            {
            GameObject newObj = Instantiate(storedObj, new Vector3(mousePos.x, mousePos.y, 1), transform.rotation);
                objStored = false;
            }
            else
            {
                dropPos = new Vector3(hit.point.x, hit.point.y, 1);
                GameObject newObj = Instantiate(storedObj, dropPos, transform.rotation);
                objStored = false;
            }
        }
    }
}
