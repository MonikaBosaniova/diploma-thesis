using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    /// <summary>
    /// A base class for the UI controllers
    /// </summary>
    public abstract class UIControllerBase : MonoBehaviour
    {
        #region [SerializeField] public bool IsPrepared {get; protected set;}

        /// <summary>
        /// If the controller is prepared to be set as running
        /// </summary>
        [field: Tooltip("If the controller is prepared to be set as running")]
        [field: SerializeField]
        [field: FormerlySerializedAs("IsPrepared")]
        public bool IsPrepared { get; protected set; }

        #endregion

        /// <summary>
        /// Setups the controller
        /// </summary>
        public virtual IEnumerator Setup()
        {
            yield break;
        }

        /// <summary>
        /// Disables the UIDocument's game object
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Enables the UIDocument's game object
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public abstract void Initialize();
    }
}
