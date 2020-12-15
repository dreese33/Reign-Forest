using UnityEngine;


interface IScorable {
    int CollisionScore(Component collider);
    int EnemyDeathScore(Component collider);
    int GetHealth();
    void UpdateHealth(int damage);
}