using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameController gc;

    private Rigidbody2D rb;
    public Animator anim;

    public float xOffset;
    public float dropSpeed;

    // Start is called before the first frame update
    void Start() 
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        dropSpeed = gc.currentDropSpeed;
        rb = GetComponent<Rigidbody2D>();
        xOffset = Random.Range(-2.25f, 2.25f);
        transform.position = new Vector2(xOffset, transform.position.y);
    }


    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, dropSpeed);
    }
}
