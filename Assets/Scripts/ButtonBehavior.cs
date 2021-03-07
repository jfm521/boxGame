using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    bool collideFeet = false;
    bool collideCube = false;
    public GameObject doorFriend;
    public GameObject doorPrefab;
    Vector3 doorPos = new Vector3(0, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        doorPos = new Vector3(doorFriend.transform.position.x, doorFriend.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Feet")
        {
            collideFeet = true;
        }
        if (other.gameObject.tag == "Cube")
        {
            collideCube = true;
        }
        if (other.gameObject.tag == "Feet" || other.gameObject.tag == "Cube")
        {
            Debug.Log("button pressed");
            if (doorFriend != null)
            {
                Destroy(doorFriend);
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Feet")
        {
            collideFeet = false;
        }
        if (other.gameObject.tag == "Cube")
        {
            collideCube = false;
        }
        if (other.gameObject.tag == "Feet" || other.gameObject.tag == "Cube")
        {
            if (collideCube == false && collideFeet == false)
            {
                GameObject newDoor = Instantiate(doorPrefab, transform.position, transform.rotation);
                newDoor.transform.localPosition = doorPos;
                doorFriend = newDoor;
            }
        }
    }
}
