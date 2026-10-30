using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertismentController : MonoBehaviour
{
    bool testMode = false;
    private int deathsSinceLastAdd;
    int nextAddQue;

    // Start is called before the first frame update
    void Start()
    {
        //Advertisement.Initialize(googlePlay_id, testMode);
        deathsSinceLastAdd = 0;
        nextAddQue = (int)Random.Range(10,25);
    }

    public void ShowAnAdd()
    {
        if(deathsSinceLastAdd >= nextAddQue)
        {
            //Advertisement.Show();
            deathsSinceLastAdd = 0;
            nextAddQue = (int)Random.Range(10,25);
        }
        else 
        {
            deathsSinceLastAdd++;
        }
        
    }
}
