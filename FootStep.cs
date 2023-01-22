using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    public AudioSource footStep;

    float X,Z;
    void Start()
    {
        footStep = GetComponent<AudioSource>();
    }
    public void Update()
    {
        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical");
        if(X != 0f || Z != 0f)
            footStep.enabled = true;
        else
            footStep.enabled = false;
    }
}
