using System;

namespace Player
{
    public class HealthSystem
    {
        private readonly int _maxHealth;

        public int Health { get; private set; }

        public event Action PlayerDamaged;
        public event Action PlayerHealed;
        public event Action PlayerDied;
        
        public HealthSystem(int maxHealth)
        {
            _maxHealth = maxHealth;
            Health = _maxHealth;
        }

        public void TakeDamage(int damageAmount)
        {
            if (damageAmount >= Health)
            {
                Health = 0;
                
                PlayerDied?.Invoke();
            }
            else
            {
                Health -= damageAmount;

                PlayerDamaged?.Invoke();
            }
        }

        public void TakeHealing(int healingAmount)
        {
            if (Health + healingAmount > _maxHealth)
            {
                Health = _maxHealth;
            }
            else
            {
                Health += healingAmount;

                PlayerHealed?.Invoke();
            }
        }
    }
}