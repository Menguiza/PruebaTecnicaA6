using UnityEngine;

namespace Utility.SurpriseCube
{
    [CreateAssetMenu(fileName = "New SurpriseCubeProperties", menuName = "SurpriseCube/SurpriseCubeProperties")]
    public class SurpriseCubeProperties : ScriptableObject
    {
        public delegate void TextureUpdate();
        public event TextureUpdate OnTextureUpdated;

        [Header("Parameters")]
        [SerializeField] private Texture2D defaultTexture;
        [SerializeField] private Texture2D texture;
        [SerializeField][Range(0, 100)] private float animationSpeed;

        public Texture2D Texture => texture ? texture : defaultTexture;
        public float AnimationSpeed => animationSpeed;

        public void ChangeTexture(Texture2D texture)
        {
            this.texture = texture;

            OnTextureUpdated?.Invoke();
        }
    }
}
