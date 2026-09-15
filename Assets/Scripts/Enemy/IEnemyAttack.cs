using UnityEngine;

public interface IEnemyAttack
{
    float AttackRange { get; }
    bool CanStartAttack(Transform player);
    void Attack();
}