using System;
using UnityEngine;

namespace Level.SpawnableObjects
{
    public class SpawnableObject : MonoBehaviour
    {
        [SerializeField] private float _spawnWeight;

        public float SpawnWeight => _spawnWeight;
        protected Player.Player Player { get; private set; }

        private Action _releaseAction;
        
        public void Setup(Player.Player player, Vector3 spawnPosition, Action onReleaseBullet)
        {
            Player = player;
            transform.position = spawnPosition;
            _releaseAction = onReleaseBullet;
        }

        public void Release()
        {
            _releaseAction();
        }
    }
}