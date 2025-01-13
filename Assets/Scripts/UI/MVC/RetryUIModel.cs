using UI.MVC.Bases;
using UnityEngine;

namespace UI.MVC
{
    public class RetryUIModel : UIModel
    {
        [Header("View")]
        [SerializeField] private RetryView retryView;

        private void Awake()
        {
            Controller = new RetryController(retryView);
        }

        protected override void BindModel()
        {
            UIManager.AddModel(ModelType.Retry, this);
            Controller.Initialize();
        }
    }
}
