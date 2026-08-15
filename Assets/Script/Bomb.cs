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
    [SerializeField] private float moveSpeedB = 25f;
    [SerializeField] private float moveSpeedL = 3f;
    [SerializeField] private float gravityScale = -5f;
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask affectedLayer;
    [SerializeField] private TMP_Text buildingDestroyed;
    [SerializeField] private TMP_Text moneyGained;
    [SerializeField] private TMP_Text totalMoney;

    
    
    private bool letGo = false;
    private bool canMove = true;
    private int numDestroyed = 0;

    private Coroutine explodeCoroutine = null;
    
    void Start()
    {
        physic = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
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
            physic.velocity = new Vector3(movement.x * moveSpeedB, physic.velocity.y, movement.z * moveSpeedB);
        } else if (letGo)
        {
            physic.velocity = new Vector3(movement.x * moveSpeedL, physic.velocity.y, movement.z * moveSpeedL);
        }

        ApplyGravity();
    }

    void ApplyGravity()
    {
        if(!letGo) return;
        physic.AddForce(Vector3.down * gravityScale);
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


        var objectsInExplosion = Physics.SphereCastAll(transform.position, radius, Vector3.up, radius, affectedLayer);
        foreach(var obj in objectsInExplosion)
        {
            Destroy(obj.transform.gameObject);
            numDestroyed += 1;
            
        }
        Debug.Log(numDestroyed);
        buildingDestroyed.text = "Things Destroyed: " + numDestroyed;
        GetComponent<MeshRenderer>().enabled = false;
        MoneySystem.instance.AddMoney(numDestroyed);
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
