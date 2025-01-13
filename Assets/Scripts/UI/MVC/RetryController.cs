using UI.MVC.Bases;
using UnityEngine;

namespace UI.MVC
{
    public class RetryController : BaseController
    {
        public delegate void RetryRequested();
        public static event RetryRequested OnRetryRequested;

        public RetryController(BaseView view) : base(view)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            BoxController.OnBoxStateChanged += open => { if (open) Show(); };

            ((RetryView)MyView).RetryButton.onClick.AddListener(() => { 
                OnRetryRequested?.Invoke();
                Hide();
            });
        }

        public override void Show()
        {
            base.Show();

            UIManager.ToggleCanvas(MyView.MainCanvasGroup, true);
        }

        public override void Hide()
        {
            base.Hide();

            UIManager.ToggleCanvas(MyView.MainCanvasGroup, false);
        }
    }
}
