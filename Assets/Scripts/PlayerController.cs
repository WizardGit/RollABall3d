using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float jumpStat = 200.0f;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int jumpCount = 0;
    private float ballHeight = 0.5f;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballHeight = transform.position.y;
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    public void OnJump()
    {    
        if (transform.position.y == ballHeight)
            jumpCount = 0;
        jumpCount++;

        //Debug.Log("y is " + y);
        //Debug.Log("jumpCount is " + jumpCount);
        if (jumpCount <= 2)
        {
            Vector3 jump = new Vector3(movementX, jumpStat, movementY);
            rb.AddForce(jump);
        }                
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }        
    }
}
