using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleController : MonoBehaviour
{

    [SerializeField]
    ParticleSystem particleLauncher;

    [SerializeField]
    ParticleSystem splatterParticles;

    [SerializeField]
    GameObject generatorObject;

    List<ParticleCollisionEvent> collisionEvents;
    GenerateEnemies enemyGenerator;
    
    //Pooling
    ObjectPooler objectPooler;


    void SplatterParticles(ParticleCollisionEvent collisionEvent)
    {
        splatterParticles.transform.position = collisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(collisionEvent.normal);
        splatterParticles.Clear();
        splatterParticles.Play();
    }


    void TestZombieCollision(GameObject other, ParticleCollisionEvent collision)
    {
        if (other.name.Contains("ZombieLowQuality"))
        {
            ZombieController zombie = other.GetComponent<ZombieController>();
            ZombieCollider collider = zombie.GetZombieCollider(collision.colliderComponent.name);
            zombie.UpdateHealth(collider);

            if (zombie.GetHealth() <= 0)
            {
                Statics.ScoreController.Score += zombie.EnemyDeathScore(collider);
                objectPooler.ReleaseToPool(other, "Zombie");
                enemyGenerator.numberOfZombies--;
            } else {
                Statics.ScoreController.Score += zombie.CollisionScore(collider);
            }
        }
    }


    void OnParticleCollision(GameObject other)
    {
        GameObject otherObject = other.transform.root.gameObject;
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            SplatterParticles(collisionEvents[i]);
            TestZombieCollision(otherObject, collisionEvents[i]);
        }
    }


    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        collisionEvents = new List<ParticleCollisionEvent>();
        enemyGenerator = generatorObject.GetComponent<GenerateEnemies>();
        Statics.ScoreController = FindObjectOfType<ScoreController>();
    }
}
