using UnityEngine;

public class ZombieColliderNotFoundException : System.Exception {
    public ZombieColliderNotFoundException() { Debug.LogError("Zombie contains unknown colliders."); }
}