using Unity.VisualScripting;
using UnityEngine;

public struct PlayerComponent
{
    public Transform Transform;
    public Rigidbody RB;
    public CapsuleCollider Collider;
    public float OffsetStepLength;
}