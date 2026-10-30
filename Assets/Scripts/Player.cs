using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameController gc;
    private Animator anim;
    private Rigidbody2D body;

    float horizontal;
    float vertical;

    public int currentShape = 0;
   
    public float animationSpeed = 1.0f;
    public float runSpeed = 20.0f;
    public float MaxHoizontalPosition = 2.5f;

    private float screenWidth;
    private float screenHeight;

    private bool isFrozen;
    private int currentCoins;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        currentShape = 0;
        anim = GetComponent<Animator>();
        anim.speed = animationSpeed;

        anim.SetInteger("CurrentShape", currentShape);
        body = GetComponent<Rigidbody2D>();

        screenWidth = (float)Screen.width / 2.0f;
        screenHeight = (float)Screen.height / 2.0f;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if(isFrozen) return;
        if (Input.GetMouseButtonDown(0))
        {
            CycleShape();
            anim.SetInteger("CurrentShape", currentShape);
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 pos = Input.mousePosition;
            //Debug.Log("mousepos  X: " +  pos.x.ToString() + " Y: " + pos.y.ToString());
            pos.x = (pos.x - screenWidth) / screenWidth;
            pos.y = (pos.y - screenHeight) / screenHeight;
            //Debug.Log("Manipulatedpos  X: " +  pos.x.ToString() + " Y: " + pos.y.ToString());

            body.position = new Vector2(pos.x * MaxHoizontalPosition, body.position.y);            
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                CycleShape();
            }

            Vector2 pos = touch.position;
            //Debug.Log("mousepos  X: " +  pos.x.ToString() + " Y: " + pos.y.ToString());
            pos.x = (pos.x - screenWidth) / screenWidth;
            pos.y = (pos.y - screenHeight) / screenHeight;
            //Debug.Log("Manipulatedpos  X: " +  pos.x.ToString() + " Y: " + pos.y.ToString());

            body.position = new Vector2(pos.x * MaxHoizontalPosition, body.position.y);       
        }
        
    }

    void CycleShape()
    {
        if(isFrozen) return;

        if((currentShape + 1) > 2)
        {
            currentShape = 0;
        }
        else 
        {
            currentShape = currentShape + 1;
        }
    }

    public void FreezePlayer()
    {
        isFrozen = true;
    }

    public void UnfreezePlayer()
    {
        isFrozen = false;
    }

    public int GetCurrentCoins()
    {
        return currentCoins;
    }

    public void ResetCurrentCoins()
    {
        currentCoins = 0;
    }

    void OnTriggerEnter2D (Collider2D coll) 
    {
		if(coll.gameObject.tag == "Target" ) 
        {
            int wallHoleShape = coll.gameObject.GetComponentInParent<WallObject>().shapeOfHole;
            //Debug.Log("player is align with hole");
            if(currentShape == wallHoleShape)
            {
                //Debug.Log("player is align with hole and shapes match.");
                gc.IncrementScore(1);
                gc.PlayWallPassSound();
                FreezePlayer();
            }
            else 
            {
                //Debug.Log("Wrong Shape! player:" + currentShape.ToString() + "  wall: " + wallHoleShape.ToString());
                gc.SpawnExplosion();
                gc.PlayCollideSound();
                gc.GameOver();
                //Destroy(this);
            }
    
        }
        else if(coll.gameObject.tag == "Wall" )
        {
            //Debug.Log("Hit Wall!");
            gc.SpawnExplosion();
            gc.PlayCollideSound();
            gc.GameOver();
            //Destroy(this);
        }

        else if(coll.gameObject.tag == "Coin" )
        {
            //Debug.Log("Hit Coin!");
            gc.PlayCoinPickupSound();
            currentCoins++;
            gc.DecrementCoinCount();
            gc.UpdateText();
            Destroy(coll.gameObject);
        }
    }


    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Target" ) 
        {
            UnfreezePlayer();
            //explode wall
            gc.SpawnWallExplosion();
        }
    }
}
