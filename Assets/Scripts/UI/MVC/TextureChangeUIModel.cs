using UI.MVC.Bases;
using UnityEngine;

namespace UI.MVC
{
    public class TextureChangeUIModel : UIModel
    {
        [Header("View")]
        [SerializeField] private TextureChangeView textureChangeView;

        private void Awake()
        {
            Controller = new TextureChangeController(textureChangeView);
        }

        protected override void BindModel()
        {
            UIManager.AddModel(ModelType.TextureChange, this);
            Controller.Initialize();
        }
    }
}
