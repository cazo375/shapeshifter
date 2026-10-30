using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObject : MonoBehaviour
{
    private GameController gc;

    private Rigidbody2D rb;
    public Animator anim;

    //to be private 
    public int shapeOfHole;
    public float xOffset;
    public float dropSpeed;

    // Start is called before the first frame update
    void Start() 
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        shapeOfHole = (int)Random.Range(0, 3);
        anim.SetInteger("ShapeOfHole", shapeOfHole);
        xOffset = Random.Range(-2.25f, 2.25f);
        transform.position = new Vector2(xOffset, transform.position.y);
    }

    // Update is called once per frame
    void Update() 
    {
        rb.velocity = new Vector2(0, dropSpeed);
    }

}
