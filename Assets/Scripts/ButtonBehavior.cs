using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    float currentColliders = 0;
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
        currentColliders += 1;
        Debug.Log("button pressed");
        if (doorFriend != null)
        {
            Destroy(doorFriend);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        currentColliders -= 1;
            if (currentColliders == 0)
        {
            GameObject newDoor = Instantiate(doorPrefab, doorPos, transform.rotation);
            doorFriend = newDoor;
        }
        
    }
}
