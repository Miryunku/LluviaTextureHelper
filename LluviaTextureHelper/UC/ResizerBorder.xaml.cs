using System;
using System.Windows.Controls;

namespace LluviaTextureHelper.UC
{
    public enum ResizerHandles
    {
        None, Left, Top, Right, Bottom
    }

    /// <summary>
    /// Interaction logic for ResizerBorder.xaml
    /// </summary>
    public partial class ResizerBorder : UserControl
    {
        private ResizerHandles _selectedHandle = ResizerHandles.None;

        public ResizerBorder()
        {
            InitializeComponent();
        }

        public ResizerHandles SelectedHandle
        {
            get { return _selectedHandle; }
            set { _selectedHandle = value; }
        }
    }
}
