using AltaGamesTest.Gameplay;
using InspectorAddons;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AltaGamesTest.Utilities
{
    public class InfectableComponentsProvider : MonoBehaviour, IComponentsProvider<IInfectable>
    {
        [Header("Use context menu to find all infectable objects on scene.")]
        [SerializeField] private List<InterfaceComponent<IInfectable>> _infectableComponents;

        private List<IInfectable> _infectableObects = new List<IInfectable>();

        [Inject]
        public void Construct()
        {
            FindAllInfectableComponentsOnScene();
        }

        public List<IInfectable> GetComponents()
            => _infectableObects;

        protected void OnEnable()
        {
            FindAllInfectableComponentsOnScene();
        }

        [ContextMenu("Find all infectable components on scene.")]
        private void FindAllInfectableComponentsOnScene()
        {
            _infectableComponents = new List<InterfaceComponent<IInfectable>>();
            foreach (var infectableComponent in ComponentsFinder.Find<IInfectable>())
            {
                if (infectableComponent is Component)
                    _infectableComponents.Add(
                        new InterfaceComponent<IInfectable>()
                        { Object = (Component)infectableComponent });
            }

            _infectableObects = InfectableComponentsToInerfaceList();
        }

        private List<IInfectable> InfectableComponentsToInerfaceList() 
        {
            List<IInfectable> infectableObects = new List<IInfectable>();
            foreach (var infectableComponent in _infectableComponents)
                infectableObects.Add(infectableComponent.Interface);

            return infectableObects;
        }
    }
}
