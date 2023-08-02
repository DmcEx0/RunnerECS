using Leopotam.EcsLite;

public class WalletInitSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        var walletEntity = world.NewEntity();

        var walletPool = world.GetPool<WalletComponent>();
        walletPool.Add(walletEntity);
        ref var walletComponent = ref walletPool.Get(walletEntity);

        walletComponent.NumberOfCoin = 0;
    }
}
