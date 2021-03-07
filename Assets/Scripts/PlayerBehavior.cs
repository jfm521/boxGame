using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public float speed;
    public float jumpHeight;
    public float gravityMultiplier;
    public float jumpMultiplier;
    public float starGoal;

    Rigidbody2D myBody;
    BoxCollider2D myCollider;
    SpriteRenderer myRenderer;

    public GameObject winScreen;

    public Sprite jumpSprite;
    public Sprite walkSprite;

    float moveDir = 1;
    float starCount = 0;

    bool onFloor = true;
    bool wonGame = false;

    public static bool faceRight = true;

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

    void FixedUpdate()
    {
        if (wonGame)
        {

        }
        else
        {

            if (onFloor && myBody.velocity.y > 0)
            {
                onFloor = false;
            }

            CheckKeys();
            HandleMovement();
            JumpPhysics();
        }
    }

    void CheckKeys()
    {
        if (Input.GetKey(KeyCode.D))
        {
            moveDir = 1;
            faceRight = true;
            myRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDir = -1;
            faceRight = false;
            myRenderer.flipX = true;
        }
        else
        {
            moveDir = 0;
        }

        if (Input.GetKey(KeyCode.W) && onFloor)
        {
            myRenderer.sprite = jumpSprite;
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);
        }
        else if (!Input.GetKey(KeyCode.W) && myBody.velocity.y > 0)
        {
            myBody.velocity += Vector2.up * Physics.gravity.y * (jumpMultiplier - 1f) * Time.deltaTime;
        }
    }

    void JumpPhysics()
    {
        if(myBody.velocity.y < 0)
        {
            myBody.velocity += Vector2.up * Physics.gravity.y * (gravityMultiplier - 1f) * Time.deltaTime;
        }
    }

    void HandleMovement()
    {
        myBody.velocity = new Vector3(moveDir * speed, myBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Floor")
        {
            myRenderer.sprite = walkSprite;
            onFloor = true;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (other.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "Star")
        {
            Destroy(other.gameObject);
            starCount += 1;
            if (starCount >= starGoal)
            {
                Vector3 winPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -11);
                GameObject newWinScreen = Instantiate(winScreen, transform.position, transform.rotation);
            }
        }
    }
}
