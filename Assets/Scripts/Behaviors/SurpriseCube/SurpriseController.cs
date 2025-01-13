using System.Collections;
using UI.MVC;
using UnityEngine;
using Utility.SurpriseCube;

namespace Behaviors.SurpriseCube
{
    public class SurpriseController : MonoBehaviour
    {
        [Header("Surprise")]
        [SerializeField] private SurpriseCubeProperties properties;
        [SerializeField] private Material assignedMaterial;

        [SerializeField] private Animator surpriseAnimator;

        [Header("Box")]
        [SerializeField] private BoxController boxController;

        private bool _hasDisappeared;

        private void Awake()
        {
            if (properties)
                properties.OnTextureUpdated += Open;

            RetryController.OnRetryRequested += Close;
        }

        private void Open()
        {
            StartCoroutine(OpenProcess());
        }

        private void Close()
        {
            StartCoroutine(CloseProcess());
        }

        private IEnumerator OpenProcess()
        {
            assignedMaterial?.SetTexture("_TextureApplied", properties.Texture);

            boxController?.BoxAnimator?.SetTrigger("Open");

            if(boxController) yield return new WaitUntil(() => boxController.IsOpen);

            if(surpriseAnimator && properties) surpriseAnimator.speed = properties.AnimationSpeed;
            surpriseAnimator?.SetTrigger("Appear");
        }

        private IEnumerator CloseProcess()
        {
            _hasDisappeared = false;

            surpriseAnimator?.SetTrigger("Disappear");

            yield return new WaitUntil(() => _hasDisappeared);

            boxController?.BoxAnimator?.SetTrigger("Close");

            assignedMaterial?.SetTexture("_TextureApplied", null);
        }

        #region Utility

        private void IsDisappeared()
        {
            _hasDisappeared = true;
        }

        #endregion
    }
}
