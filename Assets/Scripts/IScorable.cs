using UnityEngine;


interface IScorable {
    int CollisionScore(Component collider);
    int EnemyDeathScore(Component collider);
}