using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private Rigidbody2D rb;
    // Start is called before the first frame update

    private Animator anim;
    private bool grounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputH * speed, rb.velocity.y);

        if (Input.GetKey(KeyCode.Space) && grounded) Jump();

        if (inputH > 0.01f) transform.right = new Vector3(1, 0, 0);
        if (inputH < -0.01f) transform.right = new Vector3(-1, 0, 0);

        anim.SetBool("run", inputH != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

}
