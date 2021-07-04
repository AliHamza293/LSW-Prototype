using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private Rigidbody2D rb;
    private Animator anim;
    Vector2 movement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Shop")
        {
            print("Shop");
            other.transform.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}
