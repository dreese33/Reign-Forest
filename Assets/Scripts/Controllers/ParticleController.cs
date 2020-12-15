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


    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        collisionEvents = new List<ParticleCollisionEvent>();
        enemyGenerator = generatorObject.GetComponent<GenerateEnemies>();
        CameraController.scoreController = FindObjectOfType<ScoreController>();
    }


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
            //IScorable zombieScore = zombie.GetComponent<IScorable>();
            zombie.UpdateHealth(50);

            if (zombie.GetHealth() <= 0)
            {
                CameraController.scoreController.Score += zombie.EnemyDeathScore(collision.colliderComponent);
                objectPooler.ReleaseToPool(other, "Zombie");
                enemyGenerator.numberOfZombies--;
            } else { 
                CameraController.scoreController.Score += zombie.CollisionScore(collision.colliderComponent);
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
}
