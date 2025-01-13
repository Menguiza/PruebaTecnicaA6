using UI.MVC.Bases;
using AnotherFileBrowser.Windows;
using UnityEngine;
using Utility.SurpriseCube;

namespace UI.MVC
{
    public class TextureChangeController : BaseController
    {
        [Header("Properties")]
        private BrowserProperties _bp;

        [Header("Utility")]
        private string _imagePath;
        private string _folderPath;

        private bool _visible;

        public TextureChangeController(BaseView view) : base(view)
        {
            _bp = new BrowserProperties();
            _bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All Files (*.*)|*.*";
            _bp.filterIndex = 0;
        }

        public override void Initialize()
        {
            base.Initialize();

            ((TextureChangeView)MyView).ChooseImageButton.onClick.AddListener(() => {
                new FileBrowser().OpenFileBrowser(_bp, path => {
                    _imagePath = path;
                    ((TextureChangeView)MyView).ChooseImageText.text = path;
                });
            });

            ((TextureChangeView)MyView).ChooseXMLPathButton.onClick.AddListener(() => {
                new FileBrowser().OpenFolderBrowser(_bp, path => {
                    _folderPath = path;
                    ((TextureChangeView)MyView).ChooseXMLPathText.text = path;
                });
            });

            ((TextureChangeView)MyView).StartButton.onClick.AddListener(() => {
                ImageLoader.LoadImage(_imagePath, _folderPath);
            });

            BoxController.OnBoxStateChanged += open => { 
                if(!open) Show();
            };

            ImageLoader.OnTextureApplied += Hide;
        }

        public override void Show()
        {
            base.Show();

            ((TextureChangeView)MyView).ChooseImageText.text = "File path...";
            ((TextureChangeView)MyView).ChooseXMLPathText.text = "Folder path...";

            _imagePath = "";
            _folderPath = "";

            UIManager.ToggleCanvas(MyView.MainCanvasGroup, true);
        }

        public override void Hide()
        {
            base.Hide();

            UIManager.ToggleCanvas(MyView.MainCanvasGroup, false);
        }
    }
}
