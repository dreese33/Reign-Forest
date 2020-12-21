using UnityEngine;


interface IScorable {
    int CollisionScore(ZombieCollider collider);
    int EnemyDeathScore(ZombieCollider collider);
    int GetHealth();
    void UpdateHealth(ZombieCollider collider);
}