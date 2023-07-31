using Leopotam.EcsLite;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    public EcsWorld World;

    private void OnTriggerEnter(Collider other)
    {
        var hit = World.NewEntity();

        var hitPool = World.GetPool<HitComponent>();
        hitPool.Add(hit);
        ref var hitComponent = ref hitPool.Get(hit);

        hitComponent.First = transform.root.gameObject;
        hitComponent.Other = other.gameObject;
    }
}