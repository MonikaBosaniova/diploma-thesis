using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// Abstract base class for UI controllers that use UI Toolkit documents with stylesheet management
    /// </summary>
    public abstract class UIControllerBaseContext : UIControllerBase
    {
        #region Runtime

        #region [ShowInInspector] protected UIDocument _UIDocument;

        /// <summary>
        /// The UI document of this controller
        /// </summary>
        [Tooltip("The UI document of this controller")]
        [FormerlySerializedAs("_UIDocument")]
        protected UIDocument _UIDocument;

        #endregion

        #region [ShowInInspector] protected VisualElement _Root;

        /// <summary>
        /// The reference to the root <see cref="VisualElement"/> of <see cref="_UIDocument"/>
        /// </summary>
        [Tooltip("The reference to the root VisualElement of UIDocument")]
        [FormerlySerializedAs("_Root")]
        protected static VisualElement _Root;

        #endregion

        #endregion
        
        private List<StyleSheet> _StyleSheets = new();
        
        /// <summary>
        /// Activates the GameObject and waits until the controller is prepared
        /// </summary>
        /// <returns>Coroutine that waits for IsPrepared to become true</returns>
        public override IEnumerator Setup()
        {
            gameObject.SetActive(true);
            yield return new WaitUntil(() => IsPrepared);
        }
        
        /// <summary>
        /// Adds a stylesheet to the UI document
        /// </summary>
        /// <param name="sheet"></param>
        public void AddStyleSheet(StyleSheet sheet)
        {
            if (!_StyleSheets.Contains(sheet))
            {
                _StyleSheets.Add(sheet);
            }

            if (_Root != null && !_Root.styleSheets.Contains(sheet))
            {
                _Root?.styleSheets.Add(sheet);
            }
        }

        /// <summary>
        /// Removes a stylesheet from the UI document
        /// </summary>
        /// <param name="sheet"></param>
        public void RemoveStyleSheet(StyleSheet sheet)
        {
            if (_StyleSheets.Contains(sheet))
            {
                _StyleSheets.Remove(sheet);
            }

            if (_Root != null && _Root.styleSheets.Contains(sheet))
            {
                _Root?.styleSheets.Remove(sheet);
            }
        }

        /// <summary>
        /// Binds the UI elements
        /// </summary>
        protected virtual void Bind()
        {
            
        }
        
        /// <summary>
        /// Initializes the controller by getting the UIDocument and setting up the root visual element
        /// </summary>
        public override void Initialize()
        {
            IsPrepared = false;
            _UIDocument = GetComponent<UIDocument>();
            Initialize(_UIDocument.rootVisualElement);
        }

        /// <summary>
        /// Internal initialization that sets the root element, applies stylesheets and binds UI elements
        /// </summary>
        /// <param name="root">Root visual element of the UI document</param>
        private void Initialize(VisualElement root)
        {
            IsPrepared = true;
            _Root = root;
            SetupStyleSheets();
            //SetupLocalization(root);
            // if(AllLocalizedLabelsInUIDocument.Count > 0)
            //     RefreshAllLabelsDependedOnLocale();
            Bind();
        }

        /// <summary>
        /// Applies all registered stylesheets to the root visual element
        /// </summary>
        private void SetupStyleSheets()
        {
            foreach (var sheet in _StyleSheets)
            {
                if (!_Root.styleSheets.Contains(sheet))
                {
                    _Root.styleSheets.Add(sheet);
                }
            }
        }
    }
}