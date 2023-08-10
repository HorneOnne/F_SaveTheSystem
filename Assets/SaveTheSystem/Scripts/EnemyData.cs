using UnityEngine;

namespace SaveTheSystem
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "SaveTheSystem/EnemyData", order = 50)]
    public class EnemyData : ScriptableObject
    {
        [Header("Enemy Properties")]
        public int Health;
        public float MoveSpeed;

        [Header("Others")]
        public int Score;
    }
}
