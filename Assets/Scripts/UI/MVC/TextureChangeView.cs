using TMPro;
using UI.MVC.Bases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVC
{
    public class TextureChangeView : BaseView
    {
        [Header("Canvas Group")]
        [SerializeField] private CanvasGroup targetCanvasGroup;

        [Header("Buttons")]
        [SerializeField] private Button chooseImageButton;
        [SerializeField] private Button chooseXMLPathButton;
        [SerializeField] private Button startButton;

        [Header("Texts")]
        [SerializeField] private TMP_Text chooseImageText;
        [SerializeField] private TMP_Text chooseXMLPathText;

        public Button ChooseImageButton => chooseImageButton;
        public Button ChooseXMLPathButton => chooseXMLPathButton;
        public Button StartButton => startButton;

        public TMP_Text ChooseImageText => chooseImageText;
        public TMP_Text ChooseXMLPathText => chooseXMLPathText;

        public override void SetUp()
        {
            mainCanvasGroup = targetCanvasGroup;

            ChooseImageButton.onClick.RemoveAllListeners();
            ChooseXMLPathButton.onClick.RemoveAllListeners();
            StartButton.onClick.RemoveAllListeners();

            ChooseImageText.text = "File path...";
            ChooseXMLPathText.text = "Folder path...";
        }
    }
}
