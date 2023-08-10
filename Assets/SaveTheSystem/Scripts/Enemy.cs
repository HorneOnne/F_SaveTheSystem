using UnityEngine;

namespace SaveTheSystem
{

    public class Enemy : MonoBehaviour
    {
        public static event System.Action OnEnemyDeath;
        public event System.Action OnEnemyTakeDamaged;

        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private ParticleSystem _diePs;
        private int _currentHealth;

        // Cached
        private Rigidbody2D _rb;
        private Transform _target;
        private Vector2 _moveDirection;


        #region Properties
        public int Health { get => _currentHealth; }
        #endregion


        private void OnEnable()
        {
            GameplayManager.OnGameOver += DestroyEnemy;
        }

        private void OnDisable()
        {
            GameplayManager.OnGameOver -= DestroyEnemy;
        }

        private void Start()
        {
            _target = Generator.Instance.Target;
            _rb = GetComponent<Rigidbody2D>();
            _currentHealth = _enemyData.Health;
        }


        private void FixedUpdate()
        {
            if (_enemyData == null) return;

            _moveDirection = (_target.position - transform.position).normalized;
            _rb.MovePosition(_rb.position + _moveDirection * _enemyData.MoveSpeed * Time.fixedDeltaTime);
        }


        public void LoadEnemyData(EnemyData enemyData)
        {
            this._enemyData = enemyData;
        }

        public void TakeDamage(int damageAmount)
        {
            _currentHealth -= damageAmount;
            CameraShake.Instance.Shake();
            OnEnemyTakeDamaged?.Invoke();

            if (_currentHealth <= 0 )
            {
                Die();
            }        
        }

        private void Die()
        {
            GameManager.Instance.ScoreUp(_enemyData.Score);            
            DestroyEnemy();
        }

        public void DestroyEnemy()
        {
            var diePs = Instantiate(_diePs, transform.position, Quaternion.identity);
            Destroy(diePs.gameObject, 1f);

            OnEnemyDeath?.Invoke();
            Destroy(gameObject);
        }

        private void OnMouseDown()
        {
            TakeDamage(1);
            SoundManager.Instance.PlaySound(SoundType.HitEnemy, false);
        }
    }
}
