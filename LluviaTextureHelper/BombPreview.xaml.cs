using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LluviaTextureHelper
{
    /// <summary>
    /// Interaction logic for BombPreview.xaml
    /// </summary>
    public partial class BombPreview : Window
    {
        private List<CroppedBitmap> _frames = new();
        private int _timePerFrame = 0;

        public BombPreview(BitmapImage bombSheet, Bomb bombInfo)
        {
            InitializeComponent();

            _timePerFrame = bombInfo.duration / bombInfo.frameQuantity;

            int startX = 0;

            for (int i = 0; i < bombInfo.frameQuantity; ++i)
            {
                _frames.Add(new CroppedBitmap(bombSheet, new Int32Rect(startX, 0, bombInfo.frameWidth, bombInfo.frameHeight)));
                startX += bombInfo.frameWidth;
            }

            image_bombFrame.Source = _frames[0];
            image_bombFrame.Width = bombInfo.width;
            image_bombFrame.Height = bombInfo.height;
        }

        private async void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (CroppedBitmap frame in _frames)
            {
                await ReplaceFrame(frame);
            }

            image_bombFrame.Source = _frames[0];
        }

        private async Task ReplaceFrame(CroppedBitmap frame)
        {
            image_bombFrame.Source = frame;
            await Task.Delay(_timePerFrame);
        }
    }

    public struct Bomb
    {
        public int frameQuantity = 0;
        public int frameWidth = 0;
        public int frameHeight = 0;
        public int duration = 0;
        public int width = 0;
        public int height = 0;

        public Bomb()
        {
        }
    }
}
