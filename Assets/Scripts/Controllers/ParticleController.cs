using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticles;

    List<ParticleCollisionEvent> collisionEvents;
    private bool played = false;
    public GameObject generatorObject;
    private GenerateEnemies enemyGenerator;

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

            if (otherObject.name.Contains("ZombieLowQuality"))
            {
                ZombieController zombie = otherObject.GetComponent<ZombieController>();
                zombie.SubtractFromHealth(50);

                if (zombie.GetHealth() <= 0)
                {
                    Destroy(otherObject);
                    enemyGenerator.numberOfZombies--;
                }
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
