using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject zombie;
    public float currentTime,timeTillMeteor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > timeTillMeteor)
        {
            InstantiateMeteor();
        }
    }

    public void InstantiateMeteor()
    {
        Instantiate(zombie, transform.position, Quaternion.identity);
        timeTillMeteor = Random.Range(15, 20);
        currentTime = 0;
    }
}
