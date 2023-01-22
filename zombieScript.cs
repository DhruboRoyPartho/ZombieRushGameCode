using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieScript : MonoBehaviour
{
    public Rigidbody zombieBody;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform player;

    private float attackRange = 6f;
    public LayerMask playerLayer;
    public bool InAttackRange = false;

    public bool alreadyAttack = false;
    private float attackDelay = 1f;

    private float maxHealth = 50f;
    private float currentHealth;

    Vector3 playerTempPos;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("PlayerBody").transform;
        zombieBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        //float distance = Vector3.Distance(gameObject.transfrom.position, player.position);
        //LookToward(player.position, distance);
        if(InAttackRange){
            //zombieBody.isKinematic = true;
            Attack();
        }
        if(!InAttackRange)
        {
            //zombieBody.isKinematic = false;
            Chasing();
        }
    }

    void Attack()
    {
        if(agent.enabled == true){
            agent.SetDestination(player.position);
            transform.LookAt(player);
        }
        if(!alreadyAttack){
            alreadyAttack = true;
            anim.SetBool("isAttack", true);
            Invoke(nameof(resetAttack), attackDelay);
        }
    }

    void Chasing()
    {
        if(agent.enabled == true){
            agent.SetDestination(player.position);
            anim.SetBool("isAttack", false);
        }
    }

    void resetAttack()
    {
        alreadyAttack = false;
    }


    //Take Damage
    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth<=0f){
            agent.isStopped = true;
            agent.enabled = false;
            gameObject.GetComponent<zombieDie>().enabled = true;
            gameObject.GetComponent<zombieScript>().enabled = false;
        }
        else{
            
            agent.enabled = false;
            anim.SetBool("Damage", true);
            StartCoroutine(zombieWaiter());
            
        }
    }

    IEnumerator zombieWaiter()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Damage", false);
        agent.enabled = true;
        playerTempPos = transform.position;
        transform.position = playerTempPos;
        agent.SetDestination(player.position);
    }


    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
