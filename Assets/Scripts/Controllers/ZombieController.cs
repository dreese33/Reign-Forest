using UnityEngine;

public class ZombieController : MonoBehaviour, IPooledObject, IScorable
{
    Animation anim;
    CameraController controller;
    float speed = 0.0f;
    GameObject player;
    float rand;
    int health;
    float healthPercent = 1.0f;
    float originalScale = 2.0f;
    float maxHealth;
    bool rootZombie = false;

    [SerializeField]
    GameObject male;

    [SerializeField]
    GameObject female;

    //Constants
    readonly float ROTATE_SPEED = 20.0f;
    private readonly Quaternion DEFAULT_ROTATION = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);

    //Health bar
    [SerializeField]
    GameObject healthBar;


    //IScorable methods 
    //TODO -- needs further implementation based on colliders
    public int CollisionScore(Component collider) {
        //Debug.Log("Collision: " + collider.name);
        return 50;
    }


    public int EnemyDeathScore(Component collider) {
        //Debug.Log("On Death: " + collider.name);
        return 100;
    }


    Vector3 GetRandomPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Random.Range(12.5f, 82.5f);

        rand = Random.Range(300.0f, 850.0f);
        pos.z = player.transform.position.z + rand;
        return pos;
    }
    

    bool InRadius()
    {
        return Vector3.Distance(transform.position, player.transform.position) < 100.0f;
    }


    bool PastPlayer()
    {
        return (transform.position.z - player.transform.position.z) < 15.0f;
    }


    void RotateZombiePlayer()
    {
        //Possibly needs optimization (many zombies will spawn)
        Vector3 rotation = transform.eulerAngles;
        float deltaZ = transform.position.z - player.transform.position.z;
        float deltaX = transform.position.x - player.transform.position.x;
        float optimalAngle = Mathf.Atan(deltaX / deltaZ) * Mathf.Rad2Deg + 180.0f;

        if (Mathf.Abs(transform.eulerAngles.y - optimalAngle) > 1.0f) 
        {
            if (transform.eulerAngles.y < optimalAngle)
            {
                rotation.y += ROTATE_SPEED * Time.deltaTime;
            } else
            {
                rotation.y -= ROTATE_SPEED * Time.deltaTime;
            }
        }

        transform.eulerAngles = rotation;
    }


    int GetRandomHealth()
    {
        return Random.Range(50, 150);
    }


    public void UpdateHealth(int damage)
    {
        //Update health values
        health -= damage;
        healthPercent = health / maxHealth;

        //Update health bar
        //healthBar.GetComponent<Renderer>().material.SetFloat("_Percent", healthPercent);
        Vector3 scale = healthBar.transform.localScale;
        scale.x = healthPercent * originalScale;
        healthBar.transform.localScale = scale;
    }


    public int GetHealth()  
    {
        return health;
    }


    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }


    public void OnObjectSpawn() {
        switch (Statics.Gender)
        {
            case CharacterType.MALE:
                player = male;
                break;
            case CharacterType.FEMALE:
                player = female;
                break;
        }

        anim = gameObject.GetComponent<Animation>();
        
        controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
        transform.position = GetRandomPosition();
        transform.rotation = DEFAULT_ROTATION;
        health = GetRandomHealth();
        maxHealth = health;

        if (name == "ZombieLowQuality")
        {
            //Out of sight out of mind
            transform.position = new Vector3(-100.0f, transform.position.y, -100.0f);
            rootZombie = true;
        }
    }


    void Update()
    {
        if (!rootZombie)
        {
            //anim.Play("Zombie|Walk");
            anim.Play("Monsterwithbones|MonsterwithbonesAction");

            if (PastPlayer())
            {
                Statics.PlayerAlive = false;
            } else
            {
                transform.position += speed * transform.forward * Time.deltaTime;
            }

            if (InRadius())
            {
                RotateZombiePlayer();
            }
        }
    }
}
