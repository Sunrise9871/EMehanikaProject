using Level.SpawnableObjects;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private Player.Player _player;
        [SerializeField] private SpawnableObject[] _spawnableObjects;

        public override void InstallBindings()
        {
            Container.Bind<Player.Player>().FromInstance(_player).AsSingle();
            Container.Bind<HealthSystem>().AsSingle().WithArguments(_maxHealth);
        }
    }
}