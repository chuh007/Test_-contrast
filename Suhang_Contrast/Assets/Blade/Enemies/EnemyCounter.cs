using System;
using UnityEngine;
using System.Linq;

namespace Blade.Enemies
{
    public class EnemyCounter : MonoBehaviour
    {
        public int enemyCount = 0;
        private void Awake()
        {
            Enemy[] enemys = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToArray();
            foreach (var enemy in enemys)
            {
                enemy.OnDead.AddListener(HandleDead);
            }
            enemyCount = enemys.Length;
        }

        private void HandleDead()
        {
            enemyCount--;
            if (enemyCount == 0)
            {
                Debug.Log("전멸");
            }
        }
    }
}