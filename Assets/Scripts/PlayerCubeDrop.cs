using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubeDrop : MonoBehaviour
{
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newCube = Instantiate(cube, transform.position, transform.rotation);
            newCube.transform.SetParent(gameObject.transform);
            newCube.transform.localPosition = new Vector3(0, 0);
        }
    }
}
