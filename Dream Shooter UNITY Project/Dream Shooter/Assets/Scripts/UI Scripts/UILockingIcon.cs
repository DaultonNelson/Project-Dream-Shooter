using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI_Scripts
{
    /// <summary>
    /// A class holding a UI Locking Icon data.
    /// </summary>
    public class UILockingIcon : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The background Sprite of this Locking Icon.
        /// </summary>
        public Image backgroundSprite;
        /// <summary>
        /// The lock Sprite of this Locking Icon.
        /// </summary>
        public Image lockSprite;
        /// <summary>
        /// The input key Sprite of this Locking Icon.
        /// </summary>
        public Image inputKeySprite;

        /// <summary>
        /// The initial color of the BG graphic.
        /// </summary>
        public Color initBGColor { get; private set; }
        /// <summary>
        /// The initial color of the lock.
        /// </summary>
        public Color initLockColor { get; private set; }
        /// <summary>
        /// The initial color of the key.
        /// </summary>
        public Color initKeyColor { get; private set; }
        #endregion

        private void Awake()
        {
            initBGColor = backgroundSprite.color;
            initLockColor = lockSprite.color;
            initKeyColor = inputKeySprite.color;
        }
    }
}