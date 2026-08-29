using System.Collections;
using TMPro;
using UnityEngine;



public class Bomb : MonoBehaviour
{
    private Vector3 movement;
    private Rigidbody physic;
    [SerializeField] private ParticleSystem thrust;
    [SerializeField] private ParticleSystem explode;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private Animator flashbangAnim;
    [SerializeField] private Animator explodeAnim;
    [SerializeField] private LayerMask affectedLayer;
    [SerializeField] private TMP_Text buildingDestroyed;
    [SerializeField] private TMP_Text moneyGained;
    [SerializeField] private TMP_Text totalMoney;

    public BombData data;
    
    
    private bool letGo = false;
    private bool canMove = false;
    private int numDestroyed = 0;

    private Coroutine explodeCoroutine = null;
    
    void Start()
    {
        physic = GetComponent<Rigidbody>();
        GetComponent<MeshFilter>().mesh = data.bombMesh;
    }

    void Update()
    {
        if(!letGo && !canMove)
        {
            if(GameManager.instance != null) canMove = true;
            else return;
        }
        GetInputs();
    }

    protected void GetInputs()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && !letGo)
        {
            letGo = true;
            physic.useGravity = true;
            movement.y = -1f;
            thrust.Play();

        }
        movement.Normalize();
    }

    void FixedUpdate()
    {
        if(!canMove) return;

        if (!letGo)
        {
            physic.velocity = new Vector3(movement.x * data.moveSpeedBeforeDrop, physic.velocity.y, movement.z * data.moveSpeedBeforeDrop);
        } else if (letGo)
        {
            physic.velocity = new Vector3(movement.x * data.moveSpeedDuringDrop, physic.velocity.y, movement.z * data.moveSpeedDuringDrop);
        }

        ApplyGravity();
    }

    void ApplyGravity()
    {
        if(!letGo) return;
        physic.AddForce(Vector3.down * data.gravityScale);
    }

    void OnCollisionEnter(Collision collision)
    {
        canMove = false;
        if(explodeCoroutine == null)
        explodeCoroutine = StartCoroutine(ExplodeBomb());
    }

    IEnumerator ExplodeBomb()
    {
        thrust.Stop();
        GameManager.instance.SetAnimTrigger(flashbangAnim, "Flash");


        var objectsInExplosion = Physics.SphereCastAll(transform.position, data.explosionRadius, Vector3.up, data.explosionRadius, affectedLayer);
        foreach(var obj in objectsInExplosion)
        {
            Destroy(obj.transform.gameObject);
            numDestroyed += 1;
            
        }
        Debug.Log(numDestroyed);
        buildingDestroyed.text = "Things Destroyed: " + numDestroyed;
        GetComponent<MeshRenderer>().enabled = false;
        MoneySystem.instance.AddMoney(numDestroyed * data.moneyMult);
        moneyGained.text = "Money Gained: $" + numDestroyed;
        totalMoney.text = "Total Money: $" + MoneySystem.instance.CheckingMoney();
        Debug.Log(MoneySystem.instance.CheckingMoney());

        
        

        Instantiate(explode, transform.position, transform.rotation);
        Instantiate(fire, transform.position, transform.rotation);
        GameManager.instance.explodeDone = true;
        yield return new WaitForSeconds(3f);
        GameManager.instance.SetAnimTrigger(explodeAnim, "Explode");
        explodeCoroutine = null;
        
    }


    
}
