using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Utility.SurpriseCube
{
    public class ImageLoader : MonoBehaviour
    {
        public delegate void TextureApplied();
        public static event TextureApplied OnTextureApplied;

        [Header("Parameters")]
        [SerializeField][Range(0, 10)] private uint topColorsToBeSelected;

        [Header("References")]
        [SerializeField] private List<SurpriseCubeProperties> targetProperties;

        [Header("Utility")]
        private static Action<string, string> _LoadImageHelper;

        private Texture2D _loadedTexture;

        private bool _imageLoadSuccess;

        private void Awake()
        {
            _LoadImageHelper += Load;
        }

        #region Behaviors

        public static void LoadImage(string filePath, string folderPath)
        {
            _LoadImageHelper?.Invoke(filePath, folderPath);
        }

        #endregion

        #region Utility

        private async void Load(string path, string folderPath)
        {
            _loadedTexture = null;
            _imageLoadSuccess = false;

            await LoadImageTask(path);

            if (_imageLoadSuccess)
            {
                foreach (var property in targetProperties)
                {
                    property.ChangeTexture(_loadedTexture);
                }

                OnTextureApplied?.Invoke();

                ExcelExporter.ExportExcel(folderPath, GetTop3Colors(_loadedTexture, topColorsToBeSelected));
            }
        }

        #endregion

        #region Functionality

        private async Task LoadImageTask(string path)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(path);

            var asyncOp = request.SendWebRequest();

            while (!asyncOp.isDone)
            {
                await Task.Yield();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
#if UNITY_EDITOR
                Debug.LogError("Error al obtener textura: " + request.error);
#endif
                _imageLoadSuccess = false;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Textura cargada!");
#endif
                _loadedTexture = DownloadHandlerTexture.GetContent(request);
                _loadedTexture.wrapMode = TextureWrapMode.Repeat;

                _imageLoadSuccess = true;
            }
        }

        private List<Color32> GetTop3Colors(Texture2D texture, uint topColorCount)
        {
            if (texture == null)
            {
                Debug.LogError("Textura invalida!");
                return null;
            }

            Color32[] pixels = texture.GetPixels32();
            Dictionary<Color32, int> colorCount = new Dictionary<Color32, int>();

            foreach (Color32 pixel in pixels)
            {
                Color32 roundedColor = new Color32(
                    (byte)(pixel.r / 10 * 10),
                    (byte)(pixel.g / 10 * 10),
                    (byte)(pixel.b / 10 * 10),
                    255
                );

                if (colorCount.ContainsKey(roundedColor))
                {
                    colorCount[roundedColor]++;
                }
                else
                {
                    colorCount[roundedColor] = 1;
                }
            }

            List<KeyValuePair<Color32, int>> topColors = colorCount
                .OrderByDescending(c => c.Value)
                .Take((int)topColorCount)
                .ToList();

            List<Color32> result = new List<Color32>();

            foreach (KeyValuePair<Color32, int> kvp in topColors)
            {
                result.Add(kvp.Key);
            }

            return result;
        }

        #endregion
    }
}
