using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour

{
    //Creats a rigid body object
    Rigidbody2D body;

    //Creates serialized fields for adjustable numbers
    [SerializeField] private float accelerationPower;
    [SerializeField] private float jumpPower;
    [SerializeField] private float springPower;
    //Creates seriaized feilds that will help with detectinf if the player is on the ground
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask springLayer;

    [SerializeField] private Vector2 SpawnPoint;

    //A boolean to check facing direction of player. Will be used for sprite
    private bool isFacingRight;

    //Variables for keeping track of melons
    public int melonCount;
    int maxMelons;

    [SerializeField] private Text UI;

    public GameObject Watermelon;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        melonCount = 0;
        maxMelons = 1;
        transform.position = SpawnPoint;
        Instantiate(Watermelon, new Vector2(3.5f,0.5f), Quaternion.identity);
    }

    // Update is called at a fixed rate
    void FixedUpdate()
    {
        //Move left
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            body.AddForce(accelerationPower * -transform.right, ForceMode2D.Force);
        }
        //Move Right
        if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            body.AddForce(accelerationPower * transform.right, ForceMode2D.Force);
        }
        //Jump
        if ((Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.Space))&& isGrounded())
        {
            body.AddForce(jumpPower * transform.up, ForceMode2D.Force);
        }
        //Debugging Code
        UI.text = "Melons: " + melonCount.ToString() + "/" + maxMelons.ToString();
    }

    //Function that chacks if the player is grounded
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    //Function for when player dies
    private void die()
    {
        transform.position = SpawnPoint;
        if(melonCount != 0)
        {
            Instantiate(Watermelon, new Vector2(3.5f, 0.5f), Quaternion.identity);
        }
        melonCount = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Gain a melon for grabbing a melon
        if (collision.gameObject.tag == "Melon")
        {
            melonCount++;
            Debug.Log("Current melons " + melonCount);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Spring Collision Code
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, springLayer))
        {
            body.AddForce(springPower * transform.up, ForceMode2D.Force);
        }
        if (collision.gameObject.tag == "Spikes")
        {
            die();
        }
    }
}
