using UI.MVC.Bases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVC
{
    public class RetryView : BaseView
    {
        [Header("Canvas Group")]
        [SerializeField] private CanvasGroup targetCanvasGroup;

        [Header("Buttons")]
        [SerializeField] private Button retryButton;

        public Button RetryButton => retryButton;

        public override void SetUp()
        {
            mainCanvasGroup = targetCanvasGroup;

            RetryButton.onClick.RemoveAllListeners();
        }
    }
}
