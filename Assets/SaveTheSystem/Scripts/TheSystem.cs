using UnityEngine;
using DG.Tweening;

namespace SaveTheSystem
{
    public class TheSystem : MonoBehaviour
    {
        public static event System.Action OnTheSystemTakeDamaged;


        [Header("Properties")]
        [SerializeField] private int _currentHealth;
        [SerializeField] private float _torque;
        [SerializeField] private Rigidbody2D _rb;


        #region Properties
        public int Health { get => _currentHealth; }
        #endregion


        private void Start()
        {
            RotateKinematicObject();
        }
        private void RotateKinematicObject()
        {
            // Rotate the Rigidbody2D kinematic object using DOTween
            _rb.transform.DORotate(new Vector3(0f, 0f, 360f), 2.0f, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Incremental);
        }


        public void TakeDamage(int damageAmount)
        {
            _currentHealth -= damageAmount;
            CameraShake.Instance.Shake();
            OnTheSystemTakeDamaged?.Invoke();

            if (_currentHealth <= 0)
            {
                Gameover();
            }
        }

        private void Gameover()
        {
            GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                TakeDamage(1);
                SoundManager.Instance.PlaySound(SoundType.Hit, false);
                collision.GetComponent<Enemy>().DestroyEnemy();
            }
        }
    }
}
