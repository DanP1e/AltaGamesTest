using UnityEngine;
using UnityEngine.UI;

namespace AltaGamesTest.Effects
{
    public class ImageColorProvider : MonoBehaviour, IColorProvider
    {
        [SerializeField] private Image _image;

        public Color Color { get => _image.color; set => _image.color = value; }
    }
}
