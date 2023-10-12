using System.Collections;
using System.Collections.Generic;
using SharpNeat.Phenomes;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float health = 100;
    public float attackRange = 0.5f;
    public float attackCooldown;
    public float damage = 50;
    public float Speed = .5f;

    private float atkTimer;

    private float healTimer;
    private float healCooldown = 5f;
    private float healAmount = 40;
    
    public enum State {Attack,Defense,Heal}
    
    [SerializeField]
    private State currentState;

    private int enemiesDamaged = 0;

    [SerializeField] 
    private GameManager _gameManager;
    
    private bool IsRunning;
    IBlackBox box;

    // Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
        float[] inputs;
        if(currentState == State.Attack) inputs = AttackInputCalc();
        else if(currentState == State.Defense) inputs = DefenseInputCalc();
        else if (currentState == State.Heal) inputs = HealInputCalc();
        else
        {inputs = new[] {0f};}
        

        ISignalArray inputArr = box.InputSignalArray;
        inputArr[0] = inputs[0];
        inputArr[1] = inputs[1];
        inputArr[2] = inputs[2];
        
        box.Activate();

        ISignalArray outputArr = box.OutputSignalArray;
        
         
        if(currentState == State.Attack)
            AttackOutput(outputArr);
        else if (currentState == State.Defense)
            DefenseOutput(outputArr);
        else if (currentState == State.Heal)
            HealOutput(outputArr);
        
    }

    public void Activate(IBlackBox box)
    {
        this.box = box;
        this.IsRunning = true;
    }

    private float[] AttackInputCalc()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            
        Vector3 closestEnemy = Vector3.zero;
        float shortestDistance = 1000000;
        foreach (var enemy in enemyList)
        {
            float tmpDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (tmpDistance<shortestDistance)
            {
                closestEnemy = enemy.transform.position - transform.position;
                shortestDistance = tmpDistance;
            }
        }

        float[] inputs = new float[4];
            
        inputs[0] = closestEnemy.x;
        inputs[1] = closestEnemy.y;
        inputs[2] = shortestDistance;
        inputs[3] = attackRange;

        return inputs;
    }
    
    private float[] DefenseInputCalc()
    {
        GameObject[] objective = GameObject.FindGameObjectsWithTag("Finish");

        Vector3 basePos = objective[0].transform.position - transform.position;
        
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        float enemyInRange = 0;
        foreach (var enemy in enemyList)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < attackRange)
                enemyInRange = 1;
            break;

        }
        
        float[] inputs = new float[3];
            
        inputs[0] = basePos.x;
        inputs[1] = basePos.y;
        inputs[2] = attackRange;

        return inputs;
    }
    
    private float[] HealInputCalc()
    {
        float[] atkInputs = AttackInputCalc();
        float[] defInputs = DefenseInputCalc();
        
        float[] inputs = new float[6];
            
        inputs[0] = defInputs[0];
        inputs[1] = defInputs[1];
        inputs[2] = health;
        inputs[3] = atkInputs[0];
        inputs[4] = atkInputs[1];
        inputs[5] = attackRange;
        

        return inputs;
    }

    private void AttackOutput(ISignalArray outputArr)
    {
        float dirX = (float)outputArr[0] * 2 - 1;
        float dirY = (float)outputArr[1] * 2 - 1;
        float atkAction = (float)outputArr[2] * 2 - 1;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(dirX,dirY,0));
        rb.velocity = rb.velocity.normalized * Speed;

        if (atkAction > 0.2f && Time.time > atkTimer)
        {
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

            GameObject closestEnemy = null;
            float shortestDistance = 1000000;
            foreach (var enemy in enemyList)
            {
                float tmpDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if (tmpDistance<shortestDistance)
                {
                    closestEnemy = enemy;
                    shortestDistance = tmpDistance;
                }
            }

            if (shortestDistance < attackRange)
            {
                closestEnemy.GetComponent<Enemy>().TakeDamage(damage);
                atkTimer = Time.time + attackCooldown;
            }
        }
    }
    
    private void DefenseOutput(ISignalArray outputArr)
    {
        float dirX = (float)outputArr[0] * 2 - 1;
        float dirY = (float)outputArr[1] * 2 - 1;
        float atkAction = (float)outputArr[2] * 2 - 1;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(dirX,dirY,0));
        rb.velocity = rb.velocity.normalized * Speed;

        if (atkAction > 0.2f && Time.time > atkTimer)
        {
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

            GameObject closestEnemy = null;
            float shortestDistance = 1000000;
            foreach (var enemy in enemyList)
            {
                float tmpDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if (tmpDistance<shortestDistance)
                {
                    closestEnemy = enemy;
                    shortestDistance = tmpDistance;
                }
            }

            if (shortestDistance < attackRange)
            {
                closestEnemy.GetComponent<Enemy>().TakeDamage(damage); 
                atkTimer = Time.time + attackCooldown;
            }
        }
    }
    
    private void HealOutput(ISignalArray outputArr)
    {
        float dirX = (float)outputArr[0] * 2 - 1;
        float dirY = (float)outputArr[1] * 2 - 1;
        float healAction = (float)outputArr[2] * 2 - 1;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(dirX,dirY,0));
        rb.velocity = rb.velocity.normalized * Speed;

        if (healAction > 0.2f && Time.time > healTimer)
        {
            health = Mathf.Clamp(health + 40, 0, 100);
            healTimer = Time.time + healCooldown;
        }
    }
}
