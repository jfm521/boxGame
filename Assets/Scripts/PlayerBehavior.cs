using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public float speed;
    float xDir;
    float yDir;

    /* float xVel = 0;
    float yVel = 0;
    public float acc;
    public float dec;
    public float drag; */

    Rigidbody2D myBody;
    BoxCollider2D myCollider;
    SpriteRenderer myRenderer;

    public Sprite walkSprite;

    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        CheckKeys();
        HandleMovement();
    }

    void CheckKeys()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            //Debug.Log("W");
            yDir = 1;
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            //Debug.Log("A");
            xDir = -1;
            //myRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            yDir = -1;
            //Debug.Log("S");
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            xDir = 1;
            //Debug.Log("D");
            //myRenderer.flipX = false;
        }
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            yDir = 0;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            xDir = 0;
        }
    }

    void HandleMovement()
    {
        myBody.velocity = new Vector3(xDir * speed, yDir * speed);
    }

}
