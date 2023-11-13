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
    //Creates seriaized feilds that will help with detectinf if the player is on the ground
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    //A boolean to check facing direction of player. Will be used for sprite
    private bool isFacingRight;

    //Variables for keeping track of melons
    public int melonCount;
    int maxMelons;

    [SerializeField] private Text UI;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        melonCount = 0;
        maxMelons = 1;
    }

    // Update is called at a fixed rate
    void FixedUpdate()
    {
        //Move left
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(accelerationPower * -transform.right, ForceMode2D.Force);
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(accelerationPower * transform.right, ForceMode2D.Force);
        }
        //Jump
        if (Input.GetKey(KeyCode.W)&&isGrounded())
        {
            body.AddForce(jumpPower * transform.up, ForceMode2D.Force);
        }
        UI.text = "Melons: " + melonCount.ToString() + "/" + maxMelons.ToString();
    }

    //Function that chacks if the player is grounded
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Gain a melon for grabbing a melon
        if (collision.gameObject.tag == "Melon")
        {
            melonCount++;
            Debug.Log("Current melons" + melonCount);
        }
    }
}
