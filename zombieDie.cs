using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieDie : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isDeath", true);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 2.5f);
    }
}
