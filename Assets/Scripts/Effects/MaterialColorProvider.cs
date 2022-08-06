using UnityEngine;

namespace AltaGamesTest.Effects
{
    public class MaterialColorProvider : MonoBehaviour, IColorProvider
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private string _shaderBaseColorVarName = "_Color";

        public Color Color 
        { 
            get => _meshRenderer.material.GetColor(_shaderBaseColorVarName); 
            set => _meshRenderer.material.SetColor(_shaderBaseColorVarName, value);
        }
    }
}
