using UnityEngine;
using TMPro;
using DG.Tweening;

namespace SaveTheSystem
{
    public class ZoomAnimation : MonoBehaviour
    {
        public TextMeshProUGUI _text;
        public float zoomAmount = 1.2f;
        public float animationDuration = 1.0f;

        private Sequence zoomSequence;

        private void Start()
        {
            zoomSequence = DOTween.Sequence();
            zoomSequence.Append(_text.transform.DOScale(zoomAmount, animationDuration).SetEase(Ease.OutQuad))
                .Append(_text.transform.DOScale(1.0f, animationDuration).SetEase(Ease.InQuad))
                .SetLoops(-1, LoopType.Yoyo);

            // Start the sequence
            zoomSequence.Play();
        }

        private void OnDestroy()
        {
            // Ensure that the sequence is properly cleaned up when the script is destroyed
            if (zoomSequence != null)
            {
                zoomSequence.Kill();
            }
        }
    }
}
