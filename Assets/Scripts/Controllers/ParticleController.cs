using System.Collections.Generic;
using UnityEngine;

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
    

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        enemyGenerator = generatorObject.GetComponent<GenerateEnemies>();
    }


    void OnParticleCollision(GameObject other)
    {
        GameObject otherObject = other.transform.root.gameObject;
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            SplatterParticles(collisionEvents[i]);
            TestZombieCollision(otherObject);
        }
    }


    void TestZombieCollision(GameObject other)
    {
        if (other.name.Contains("ZombieLowQuality"))
        {
            ZombieController zombie = other.GetComponent<ZombieController>();
            zombie.SubtractFromHealth(50);

            if (zombie.GetHealth() <= 0)
            {
                Destroy(other);
                enemyGenerator.numberOfZombies--;
            }
        }
    }


    void SplatterParticles(ParticleCollisionEvent collisionEvent)
    {
        splatterParticles.transform.position = collisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(collisionEvent.normal);
        splatterParticles.Clear();
        splatterParticles.Play();
    }
}
