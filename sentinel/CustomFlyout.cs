using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sentinel
{
    /// <summary>
    /// Flyout au contenu customisé.
    /// </summary>
    public class CustomFlyout
    {
        /// <summary>
        /// Titre du flyout
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets/sets whether this flyout stays open when the user clicks outside of it.
        /// </summary>
        public bool IsPinned { get; set; }

        /// <summary>
        /// Gets/sets whether this flyout is modal.
        /// </summary>
        public bool IsModal { get; set; }

        /// <summary>
        /// Contenu du flyout (ViewModel)
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// Position du flyout
        /// </summary>
        public object Position { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CustomFlyout()
        {
            IsPinned = false;
            IsModal = false;
        }
    }
}
