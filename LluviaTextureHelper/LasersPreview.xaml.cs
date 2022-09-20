using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
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
    /// Interaction logic for LasersPreview.xaml
    /// </summary>
    public partial class LasersPreview : Window
    {
        double laserWidth = 0;
        double laserHeight = 0;
        private Storyboard storyboard = new();

        public LasersPreview(Image laser, ObservableCollection<LaserAnime> animes, int laserHeight_, int duration)
        {
            InitializeComponent();

            laserWidth = laser.Width;
            laserHeight = laserHeight_;

            this.RegisterName(image_laser.Name, image_laser);

            image_laser.Source = laser.Source;
            image_laser.Loaded += Image_Loaded;

            TimeSpan animationDuration = TimeSpan.FromMilliseconds(duration);

            // Width animation

            {
                DoubleAnimationUsingKeyFrames animation = new();
                animation.Duration = animationDuration;
                animation.RepeatBehavior = RepeatBehavior.Forever;
                Storyboard.SetTargetName(animation, image_laser.Name);
                Storyboard.SetTargetProperty(animation, new PropertyPath(Image.WidthProperty));

                foreach (LaserAnime anime in animes)
                {
                    animation.KeyFrames.Add(new LinearDoubleKeyFrame(laserWidth * anime.WidthFactor, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(anime.Time))));
                }

                storyboard.Children.Add(animation);
            }

            // Height animation

            {
                DoubleAnimationUsingKeyFrames animation = new();
                animation.Duration = animationDuration;
                animation.RepeatBehavior = RepeatBehavior.Forever;
                Storyboard.SetTargetName(animation, image_laser.Name);
                Storyboard.SetTargetProperty(animation, new PropertyPath(Image.HeightProperty));

                foreach (LaserAnime anime in animes)
                {
                    animation.KeyFrames.Add(new LinearDoubleKeyFrame(laserHeight * anime.HeightFactor, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(anime.Time))));
                }

                storyboard.Children.Add(animation);
            }

            // Opacity animation

            {
                DoubleAnimationUsingKeyFrames animation = new();
                animation.Duration = animationDuration;
                animation.RepeatBehavior = RepeatBehavior.Forever;
                Storyboard.SetTargetName(animation, image_laser.Name);
                Storyboard.SetTargetProperty(animation, new PropertyPath(Image.OpacityProperty));

                foreach (LaserAnime anime in animes)
                {
                    animation.KeyFrames.Add(new LinearDoubleKeyFrame(anime.OpacityFactor, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(anime.Time))));
                }

                storyboard.Children.Add(animation);
            }
        }

        private void Image_Loaded(object sender, RoutedEventArgs args)
        {
            storyboard.Begin(this);
        }
    }
}
