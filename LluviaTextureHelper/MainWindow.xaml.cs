using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace LluviaTextureHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private const int _texLowerLimit = 4;

        private string _jsonFilePath = string.Empty;
        private string _imageFilePath = string.Empty;

        private BitmapImage _textureAtlasPlayfield7k = new();

        /* Objects to represent the skin components */

        private Image _texAccompanyingArtwork = new() { Stretch = Stretch.Fill };
        private Image _texCompanion = new() { Stretch = Stretch.Fill };

        // oh my god...
        // The playfield. TODO: Add more rectangles for 9k and 10k.
        private Rectangle _playfieldColumn1 = new();
        private Rectangle _playfieldColumn2 = new();
        private Rectangle _playfieldColumn3 = new();
        private Rectangle _playfieldColumn4 = new();
        private Rectangle _playfieldColumn5 = new();
        private Rectangle _playfieldColumn6 = new();
        private Rectangle _playfieldColumn7 = new();
        private Rectangle _playfieldColumnSeparator1 = new();
        private Rectangle _playfieldColumnSeparator2 = new();
        private Rectangle _playfieldColumnSeparator3 = new();
        private Rectangle _playfieldColumnSeparator4 = new();
        private Rectangle _playfieldColumnSeparator5 = new();
        private Rectangle _playfieldColumnSeparator6 = new();
        private Rectangle _playfieldBorderLeft = new();
        private Rectangle _playfieldBorderRight = new();
        private Rectangle _playfieldBorderBottom = new();
        private Rectangle _playfieldVerdictLine = new();

        // private Image _bomb is not needed here

        private Image _texLaser_7k_1357 = new() { Stretch = Stretch.Fill };

        private Image? _texKeysBG = null;

        private Image _texKey_7k_1357_Sprite1 = new() { Stretch = Stretch.Fill };
        private Image _texKey_7k_1357_Sprite2 = new() { Stretch = Stretch.Fill };
        private Image _texKey_7k_1357_Sprite3 = new() { Stretch = Stretch.Fill };
        private Image _texKey_7k_1357_Sprite4 = new() { Stretch = Stretch.Fill };
        private Image _texKey_7k_26_Sprite1 = new() { Stretch = Stretch.Fill };
        private Image _texKey_7k_26_Sprite2 = new() { Stretch = Stretch.Fill };
        private Image _texKey_7k_4_Sprite1 = new() { Stretch = Stretch.Fill };

        private Image _texNote_7k_1357 = new() { Stretch = Stretch.Fill };
        private Image _texNote_7k_26 = new() { Stretch = Stretch.Fill };
        private Image _texNote_7k_4 = new() { Stretch = Stretch.Fill };
        private Image _texLN_7k_1357 = new() { Stretch = Stretch.Fill };
        private Image _texLN_7k_26 = new() { Stretch = Stretch.Fill };
        private Image _texLN_7k_4 = new() { Stretch = Stretch.Fill };

        private Image _texInjection_Replay = new() { Stretch = Stretch.Fill };
        private Image _texInjection_O2Hard = new() { Stretch = Stretch.Fill };
        private Image _texInjection_Mirror = new() { Stretch = Stretch.Fill };
        private Image _texInjection_AllLN = new() { Stretch = Stretch.Fill };
        private Image _texInjection_LR2NMJudge = new() { Stretch = Stretch.Fill };
        private Image _texInjection_NoSSC = new() { Stretch = Stretch.Fill };

        private Image? _texHealthBarBG = null;
        private Image _texHealthBar = new() { Stretch = Stretch.Fill };

        private Image _texRank_S = new() { Stretch = Stretch.Fill };

        private Image _texVerdictCOOL = new() { Stretch = Stretch.Fill };

        private Image? _texPlacard_Health = null;
        private Image? _texPlacard_BPM = null;
        private Image? _texPlacard_ScrollSpeed = null;
        private Image? _texPlacard_Offset = null;
        private Image? _texPlacard_MaxCombo = null;
        private Image? _texPlacard_Accuracy = null;
        private Image? _texPlacard_Time = null;
        private Image? _texPlacard_COOL = null;
        private Image? _texPlacard_GOOD = null;
        private Image? _texPlacard_OKAY = null;
        private Image? _texPlacard_BAD = null;
        private Image? _texPlacard_MISS = null;

        private Image _texNumbers_Health = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_BPM = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_ScrollSpeed = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_Offset = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_MaxCombo = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_Accuracy = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_Time = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_COOL = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_CounterCOOL = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_CounterGOOD = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_CounterOKAY = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_CounterBAD = new() { Stretch = Stretch.Fill };
        private Image _texNumbers_CounterMISS = new() { Stretch = Stretch.Fill };

        private Image? _texArbitraryTexture = null;

        /* **************************************************** */

        private bool _skinDataIsLoaded = false;
        private bool _playfieldInCanvas = false;

        private Skin.Playfield7k? _jsonPlayfield7k = null;

        private ObservableCollection<string> _wallpapers = new();

        private List<SkinComponent> _skinComponents = new();

        private ObservableCollection<LaserAnime> _laserAnimations = new();

        private bool _isKeysBackgroundUsed = false;
        private bool _isArbitraryTextureUsed = false;

        private int _verdictCOOLTextureWidth = 0;

        private Image? _clickedImage = null;
        private UC.ResizerBorder _resizerBorder = new();

        private Point _startPosition = new();
        private Point _clickedImage_StartPos = new(0.0, 0.0);
        private bool _onMoveOperation = false;
        private bool _onResizeOperation = false;

        public MainWindow()
        {
            InitializeComponent();

            _texKey_7k_1357_Sprite1.Stretch = Stretch.Fill;
            _texKey_7k_1357_Sprite2.Stretch = Stretch.Fill;
            _texKey_7k_1357_Sprite3.Stretch = Stretch.Fill;
            _texKey_7k_1357_Sprite4.Stretch = Stretch.Fill;

            _texKey_7k_26_Sprite1.Stretch = Stretch.Fill;
            _texKey_7k_26_Sprite2.Stretch = Stretch.Fill;

            _texKey_7k_4_Sprite1.Stretch = Stretch.Fill;

            _texNote_7k_1357.Stretch = Stretch.Fill;
            _texNote_7k_26.Stretch = Stretch.Fill;
            _texNote_7k_4.Stretch = Stretch.Fill;
            _texLN_7k_1357.Stretch = Stretch.Fill;
            _texLN_7k_26.Stretch = Stretch.Fill;
            _texLN_7k_4.Stretch = Stretch.Fill;

            textBlock_selectedJsonFile.Text = Properties.Resources.JsonFile + ": " + Properties.Resources.None;
            textBlock_selectedImageFile.Text = Properties.Resources.ImageFile + ": " + Properties.Resources.None;

            textBlock_componentSW_Title.Text = Properties.Resources.Width + ":";
            textBlock_componentSH_Title.Text = Properties.Resources.Height + ":";

            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;

            _resizerBorder.leftHandle.MouseLeftButtonDown += Resizer_leftHandle_MouseLeftButtonDown;
            _resizerBorder.topHandle.MouseLeftButtonDown += Resizer_topHandle_MouseLeftButtonDown;
            _resizerBorder.rightHandle.MouseLeftButtonDown += Resizer_rightHandle_MouseLeftButtonDown;
            _resizerBorder.bottomHandle.MouseLeftButtonDown += Resizer_bottomHandle_MouseLeftButtonDown;

            listBox_wallpapers.ItemsSource = _wallpapers;
            listBox_laserAnimations.ItemsSource = _laserAnimations;
        }

        #region HELPER METHODS

        // Pas = Position and size
        private void UpdatePasTextBlocks(double X, double Y, double w, double h)
        {
            textBlock_componentX.Text = "X:" + X.ToString();
            textBlock_componentY.Text = "Y:" + Y.ToString();
            textBlock_componentSW.Text = w.ToString();
            textBlock_componentSH.Text = h.ToString();
        }

        private Color ColorFromColor1(Skin.Color1 color1)
        {
            Color color = new();
            color.A = (byte)Math.Clamp(color1.A, 0, 255);
            color.R = (byte)Math.Clamp(color1.R, 0, 255);
            color.G = (byte)Math.Clamp(color1.G, 0, 255);
            color.B = (byte)Math.Clamp(color1.B, 0, 255);

            return color;
        }

        private Skin.Color1 Color1FromColor(Color color)
        {
            Skin.Color1 color1 = new();
            color1.A = Math.Clamp(color.A, (byte)0, (byte)255);
            color1.R = Math.Clamp(color.R, (byte)0, (byte)255);
            color1.G = Math.Clamp(color.G, (byte)0, (byte)255);
            color1.B = Math.Clamp(color.B, (byte)0, (byte)255);

            return color1;
        }

        // Does not include the border
        private double Calculate7kPlayfieldWidth(bool fromJson)
        {
            if (fromJson)
            {
                return (_jsonPlayfield7k.Playfield.Columns_1357_Width * 4) + (_jsonPlayfield7k.Playfield.Columns_246_Width * 3) + (_jsonPlayfield7k.Playfield.ColumnSeparatorsWidth * 6);
            }

            return (int.Parse(textBox_playfield1357ColumnsWidth.Text) * 4) + (int.Parse(textBox_playfield246ColumnsWidth.Text) * 3) + (int.Parse(textBox_playfieldColumnSeparatorsWidth.Text) * 6);
        }

        // This function only positions the elements, things like width are handled elsewhere
        private void DrawPlayfield()
        {
            if (!_skinDataIsLoaded || _playfieldInCanvas) { return; }

            canvas.Children.Add(_playfieldBorderLeft);
            canvas.Children.Add(_playfieldColumn1);
            canvas.Children.Add(_playfieldColumnSeparator1);
            canvas.Children.Add(_playfieldColumn2);
            canvas.Children.Add(_playfieldColumnSeparator2);
            canvas.Children.Add(_playfieldColumn3);
            canvas.Children.Add(_playfieldColumnSeparator3);
            canvas.Children.Add(_playfieldColumn4);
            canvas.Children.Add(_playfieldColumnSeparator4);
            canvas.Children.Add(_playfieldColumn5);
            canvas.Children.Add(_playfieldColumnSeparator5);
            canvas.Children.Add(_playfieldColumn6);
            canvas.Children.Add(_playfieldColumnSeparator6);
            canvas.Children.Add(_playfieldColumn7);
            canvas.Children.Add(_playfieldBorderRight);
            canvas.Children.Add(_playfieldVerdictLine);
            canvas.Children.Add(_playfieldBorderBottom);

            _playfieldInCanvas = true;
        }

        private void UndrawPlayfield()
        {
            if (!_skinDataIsLoaded || !_playfieldInCanvas) { return; }

            canvas.Children.Remove(_playfieldBorderLeft);
            canvas.Children.Remove(_playfieldBorderRight);
            canvas.Children.Remove(_playfieldBorderBottom);

            canvas.Children.Remove(_playfieldVerdictLine);

            canvas.Children.Remove(_playfieldColumn1);
            canvas.Children.Remove(_playfieldColumn2);
            canvas.Children.Remove(_playfieldColumn3);
            canvas.Children.Remove(_playfieldColumn4);
            canvas.Children.Remove(_playfieldColumn5);
            canvas.Children.Remove(_playfieldColumn6);
            canvas.Children.Remove(_playfieldColumn7);

            canvas.Children.Remove(_playfieldColumnSeparator1);
            canvas.Children.Remove(_playfieldColumnSeparator2);
            canvas.Children.Remove(_playfieldColumnSeparator3);
            canvas.Children.Remove(_playfieldColumnSeparator4);
            canvas.Children.Remove(_playfieldColumnSeparator5);
            canvas.Children.Remove(_playfieldColumnSeparator6);

            _playfieldInCanvas = false;
        }

        private void UpdatePlayfieldComponentsPosition()
        {
            // Create needed local variables

            int playfieldStartX = int.Parse(textBox_playfieldOffset.Text);
            int playfieldHeight = int.Parse(textBox_playfieldHeight.Text);

            // Draw things from left to right and lastly the bottom border

            Canvas.SetLeft(_playfieldBorderLeft, playfieldStartX);
            Canvas.SetTop(_playfieldBorderLeft, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumn1, Canvas.GetLeft(_playfieldBorderLeft) + _playfieldBorderLeft.Width);
            Canvas.SetTop(_playfieldColumn1, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumnSeparator1, Canvas.GetLeft(_playfieldColumn1) + _playfieldColumn1.Width);
            Canvas.SetTop(_playfieldColumnSeparator1, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumn2, Canvas.GetLeft(_playfieldColumnSeparator1) + _playfieldColumnSeparator1.Width);
            Canvas.SetTop(_playfieldColumn2, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumnSeparator2, Canvas.GetLeft(_playfieldColumn2) + _playfieldColumn2.Width);
            Canvas.SetTop(_playfieldColumnSeparator2, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumn3, Canvas.GetLeft(_playfieldColumnSeparator2) + _playfieldColumnSeparator2.Width);
            Canvas.SetTop(_playfieldColumn3, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumnSeparator3, Canvas.GetLeft(_playfieldColumn3) + _playfieldColumn3.Width);
            Canvas.SetTop(_playfieldColumnSeparator3, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumn4, Canvas.GetLeft(_playfieldColumnSeparator3) + _playfieldColumnSeparator3.Width);
            Canvas.SetTop(_playfieldColumn4, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumnSeparator4, Canvas.GetLeft(_playfieldColumn4) + _playfieldColumn4.Width);
            Canvas.SetTop(_playfieldColumnSeparator4, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumn5, Canvas.GetLeft(_playfieldColumnSeparator4) + _playfieldColumnSeparator4.Width);
            Canvas.SetTop(_playfieldColumn5, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumnSeparator5, Canvas.GetLeft(_playfieldColumn5) + _playfieldColumn5.Width);
            Canvas.SetTop(_playfieldColumnSeparator5, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumn6, Canvas.GetLeft(_playfieldColumnSeparator5) + _playfieldColumnSeparator5.Width);
            Canvas.SetTop(_playfieldColumn6, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumnSeparator6, Canvas.GetLeft(_playfieldColumn6) + _playfieldColumn6.Width);
            Canvas.SetTop(_playfieldColumnSeparator6, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldColumn7, Canvas.GetLeft(_playfieldColumnSeparator6) + _playfieldColumnSeparator6.Width);
            Canvas.SetTop(_playfieldColumn7, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldBorderRight, Canvas.GetLeft(_playfieldColumn7) + _playfieldColumn7.Width);
            Canvas.SetTop(_playfieldBorderRight, 0.0);

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldVerdictLine, Canvas.GetLeft(_playfieldColumn1));
            Canvas.SetTop(_playfieldVerdictLine, playfieldHeight - int.Parse(textBox_notesHeight.Text));

            /*
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
             */

            Canvas.SetLeft(_playfieldBorderBottom, Canvas.GetLeft(_playfieldBorderLeft));
            Canvas.SetTop(_playfieldBorderBottom, playfieldHeight);
        }

        private void UpdatePlayfieldHeight(int height)
        {
            _playfieldBorderLeft.Height = height;
            _playfieldBorderRight.Height = height;

            _playfieldColumn1.Height = height;
            _playfieldColumn2.Height = height;
            _playfieldColumn3.Height = height;
            _playfieldColumn4.Height = height;
            _playfieldColumn5.Height = height;
            _playfieldColumn6.Height = height;
            _playfieldColumn7.Height = height;

            _playfieldColumnSeparator1.Height = height;
            _playfieldColumnSeparator2.Height = height;
            _playfieldColumnSeparator3.Height = height;
            _playfieldColumnSeparator4.Height = height;
            _playfieldColumnSeparator5.Height = height;
            _playfieldColumnSeparator6.Height = height;
        }

        // This function only deals with position and not size.
        // This function assumes that the playfield is up-to-date (position and size)
        private void UpdateKeysLayout()
        {
            int margin1357 = 0;
            int.TryParse(textBox_keys1357margin.Text, out margin1357);

            int margin246 = 0;
            int.TryParse(textBox_keys246margin.Text, out margin246);

            Canvas.SetLeft(_texKey_7k_1357_Sprite1, Canvas.GetLeft(_playfieldColumn1));
            Canvas.SetTop(_texKey_7k_1357_Sprite1, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin1357);

            Canvas.SetLeft(_texKey_7k_1357_Sprite2, Canvas.GetLeft(_playfieldColumn3));
            Canvas.SetTop(_texKey_7k_1357_Sprite2, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin1357);

            Canvas.SetLeft(_texKey_7k_1357_Sprite3, Canvas.GetLeft(_playfieldColumn5));
            Canvas.SetTop(_texKey_7k_1357_Sprite3, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin1357);

            Canvas.SetLeft(_texKey_7k_1357_Sprite4, Canvas.GetLeft(_playfieldColumn7));
            Canvas.SetTop(_texKey_7k_1357_Sprite4, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin1357);

            // - - - - -

            Canvas.SetLeft(_texKey_7k_26_Sprite1, Canvas.GetLeft(_playfieldColumn2));
            Canvas.SetTop(_texKey_7k_26_Sprite1, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin246);

            Canvas.SetLeft(_texKey_7k_26_Sprite2, Canvas.GetLeft(_playfieldColumn6));
            Canvas.SetTop(_texKey_7k_26_Sprite2, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin246);


            // - - - - -

            Canvas.SetLeft(_texKey_7k_4_Sprite1, Canvas.GetLeft(_playfieldColumn4));
            Canvas.SetTop(_texKey_7k_4_Sprite1, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin246);
        }

        private void UpdateKeysWidth(int width1357, int width246)
        {
            _texKey_7k_1357_Sprite1.Width = width1357;
            _texKey_7k_1357_Sprite2.Width = width1357;
            _texKey_7k_1357_Sprite3.Width = width1357;
            _texKey_7k_1357_Sprite4.Width = width1357;

            _texKey_7k_26_Sprite1.Width = width246;
            _texKey_7k_26_Sprite2.Width = width246;

            _texKey_7k_4_Sprite1.Width = width246;
        }

        private void UpdateNotesPositionAndWidth()
        {
            _texNote_7k_1357.Width = _playfieldColumn1.Width;
            _texNote_7k_26.Width = _playfieldColumn6.Width;
            _texNote_7k_4.Width = _playfieldColumn4.Width;

            _texLN_7k_1357.Width = _playfieldColumn1.Width;
            _texLN_7k_26.Width = _playfieldColumn6.Width;
            _texLN_7k_4.Width = _playfieldColumn4.Width;

            Canvas.SetLeft(_texNote_7k_1357, Canvas.GetLeft(_playfieldColumn1));
            Canvas.SetTop(_texNote_7k_1357, Canvas.GetTop(_playfieldBorderBottom) - (_texNote_7k_1357.Height * 2.0));

            Canvas.SetLeft(_texNote_7k_26, Canvas.GetLeft(_playfieldColumn6));
            Canvas.SetTop(_texNote_7k_26, Canvas.GetTop(_playfieldBorderBottom) - (_texNote_7k_1357.Height * 2.0));

            Canvas.SetLeft(_texNote_7k_4, Canvas.GetLeft(_playfieldColumn4));
            Canvas.SetTop(_texNote_7k_4, Canvas.GetTop(_playfieldBorderBottom) - (_texNote_7k_1357.Height * 2.0));

            Canvas.SetLeft(_texLN_7k_1357, Canvas.GetLeft(_playfieldColumn1));
            Canvas.SetTop(_texLN_7k_1357, Canvas.GetTop(_playfieldBorderBottom) - (_texNote_7k_1357.Height * 4.0 + 80.0));

            Canvas.SetLeft(_texLN_7k_26, Canvas.GetLeft(_playfieldColumn6));
            Canvas.SetTop(_texLN_7k_26, Canvas.GetTop(_playfieldBorderBottom) - (_texNote_7k_1357.Height * 4.0 + 80.0));

            Canvas.SetLeft(_texLN_7k_4, Canvas.GetLeft(_playfieldColumn4));
            Canvas.SetTop(_texLN_7k_4, Canvas.GetTop(_playfieldBorderBottom) - (_texNote_7k_1357.Height * 4.0 + 80.0));
        }

        private void DrawKeys()
        {
            canvas.Children.Add(_texKey_7k_1357_Sprite1);
            canvas.Children.Add(_texKey_7k_1357_Sprite2);
            canvas.Children.Add(_texKey_7k_1357_Sprite3);
            canvas.Children.Add(_texKey_7k_1357_Sprite4);

            canvas.Children.Add(_texKey_7k_26_Sprite1);
            canvas.Children.Add(_texKey_7k_26_Sprite2);

            canvas.Children.Add(_texKey_7k_4_Sprite1);
        }

        private void UndrawKeys()
        {
            canvas.Children.Remove(_texKey_7k_1357_Sprite1);
            canvas.Children.Remove(_texKey_7k_1357_Sprite2);
            canvas.Children.Remove(_texKey_7k_1357_Sprite3);
            canvas.Children.Remove(_texKey_7k_1357_Sprite4);

            canvas.Children.Remove(_texKey_7k_26_Sprite1);
            canvas.Children.Remove(_texKey_7k_26_Sprite2);

            canvas.Children.Remove(_texKey_7k_4_Sprite1);
        }

        private bool IsNotAllowedToResizeInCanvas(Image image)
        {
            SkinComponent component = _skinComponents.Find(x => object.ReferenceEquals(x.Texture, image));

            switch (component.Id)
            {
                case ComponentIDs.Injection_AutoplayAndReplay:
                    return true;

                case ComponentIDs.Injection_Gauges:
                    return true;

                case ComponentIDs.Injection_PlacementModifiers:
                    return true;

                case ComponentIDs.Injection_AllLN:
                    return true;

                case ComponentIDs.Injection_LR2NormalJudge:
                    return true;

                case ComponentIDs.Injection_NoSSC:
                    return true;

                case ComponentIDs.Numbers_CounterCOOL:
                    return true;

                case ComponentIDs.Numbers_CounterGOOD:
                    return true;

                case ComponentIDs.Numbers_CounterOKAY:
                    return true;

                case ComponentIDs.Numbers_CounterBAD:
                    return true;

                case ComponentIDs.Numbers_CounterMISS:
                    return true;

                default:
                    return false;
            }
        }

        private void SetImagePositionAndSize(Image image, int x, int y, int w, int h)
        {
            image.Width = w;
            image.Height = h;
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
        }

        private void UpdateUIBasedOnImage(Image image)
        {
            SkinComponent component = _skinComponents.Find(x => object.ReferenceEquals(image, x.Texture));

            switch (component.Id)
            {
                case ComponentIDs.AccompanyingArtwork:
                    {
                        textBox_accompanyingArtwork_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_accompanyingArtwork_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_accompanyingArtwork_W.Text = image.Width.ToString();
                        textBox_accompanyingArtwork_H.Text = image.Height.ToString();
                    }
                    break;

                case ComponentIDs.Companion:
                    {
                        textBox_companionX.Text = Canvas.GetLeft(image).ToString();
                        textBox_companionY.Text = Canvas.GetTop(image).ToString();
                        textBox_companionSW.Text = image.Width.ToString();
                        textBox_companionSH.Text = image.Height.ToString();
                    }
                    break;

                case ComponentIDs.KeysBackground:
                    {
                        textBox_keysBG_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_keysBG_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_keysBG_W.Text = image.Width.ToString();
                        textBox_keysBG_H.Text = image.Height.ToString();
                    }
                    break;

                case ComponentIDs.Injection_AutoplayAndReplay:
                    {
                        if (comboBox_injections.SelectedIndex == 0)
                        {
                            textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                            textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.Injection_Gauges:
                    {
                        if (comboBox_injections.SelectedIndex == 1)
                        {
                            textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                            textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.Injection_PlacementModifiers:
                    {
                        if (comboBox_injections.SelectedIndex == 2)
                        {
                            textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                            textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.Injection_AllLN:
                    {
                        if (comboBox_injections.SelectedIndex == 3)
                        {
                            textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                            textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.Injection_LR2NormalJudge:
                    {
                        if (comboBox_injections.SelectedIndex == 4)
                        {
                            textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                            textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.Injection_NoSSC:
                    {
                        if (comboBox_injections.SelectedIndex == 5)
                        {
                            textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                            textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.HealthBarBackground:
                    {
                        textBox_healthBarBackgroundX.Text = Canvas.GetLeft(image).ToString();
                        textBox_healthBarBackgroundY.Text = Canvas.GetTop(image).ToString();
                        textBox_healthBarBackgroundSW.Text = image.Width.ToString();
                        textBox_healthBarBackgroundSH.Text = image.Height.ToString();
                    }
                    break;

                case ComponentIDs.HealthBar:
                    {
                        textBox_healthBarX.Text = Canvas.GetLeft(image).ToString();
                        textBox_healthBarY.Text = Canvas.GetTop(image).ToString();
                        textBox_healthBarSW.Text = image.Width.ToString();
                        textBox_healthBarSH.Text = image.Height.ToString();
                    }
                    break;

                case ComponentIDs.RankS:
                    {
                        textBox_rankX.Text = Canvas.GetLeft(image).ToString();
                        textBox_rankY.Text = Canvas.GetTop(image).ToString();
                        textBox_rankSW.Text = image.Width.ToString();
                        textBox_rankSH.Text = image.Height.ToString();
                    }
                    break;

                case ComponentIDs.VerdictCOOL:
                    {
                        textBox_verdicts_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_verdicts_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_verdicts_H.Text = image.Height.ToString();
                    }
                    break;

                case ComponentIDs.Placard_Health:
                    {
                        if (comboBox_placards.SelectedIndex == 0)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_BPM:
                    {
                        if (comboBox_placards.SelectedIndex == 1)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_ScrollSpeed:
                    {
                        if (comboBox_placards.SelectedIndex == 2)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_Offset:
                    {
                        if (comboBox_placards.SelectedIndex == 3)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_MaxCombo:
                    {
                        if (comboBox_placards.SelectedIndex == 4)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_Time:
                    {
                        if (comboBox_placards.SelectedIndex == 5)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_Accuracy:
                    {
                        if (comboBox_placards.SelectedIndex == 6)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_COOL:
                    {
                        if (comboBox_placards.SelectedIndex == 7)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_GOOD:
                    {
                        if (comboBox_placards.SelectedIndex == 8)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_OKAY:
                    {
                        if (comboBox_placards.SelectedIndex == 9)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_BAD:
                    {
                        if (comboBox_placards.SelectedIndex == 10)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Placard_MISS:
                    {
                        if (comboBox_placards.SelectedIndex == 11)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_placard_W.Text = image.Width.ToString();
                            textBox_placard_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_Health:
                    {
                        if (comboBox_numbers.SelectedIndex == 0)
                        {
                            textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_number_W.Text = image.Width.ToString();
                            textBox_number_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_BPM:
                    {
                        if (comboBox_numbers.SelectedIndex == 1)
                        {
                            textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_number_W.Text = image.Width.ToString();
                            textBox_number_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_ScrollSpeed:
                    {
                        if (comboBox_numbers.SelectedIndex == 2)
                        {
                            textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_number_W.Text = image.Width.ToString();
                            textBox_number_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_Offset:
                    {
                        if (comboBox_numbers.SelectedIndex == 3)
                        {
                            textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_number_W.Text = image.Width.ToString();
                            textBox_number_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_MaxCombo:
                    {
                        if (comboBox_numbers.SelectedIndex == 4)
                        {
                            textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_number_W.Text = image.Width.ToString();
                            textBox_number_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_Time:
                    {
                        if (comboBox_numbers.SelectedIndex == 5)
                        {
                            textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_number_W.Text = image.Width.ToString();
                            textBox_number_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_Accuracy:
                    {
                        if (comboBox_numbers.SelectedIndex == 6)
                        {
                            textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_number_W.Text = image.Width.ToString();
                            textBox_number_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_ComboCOOL:
                    {
                        if (comboBox_numbers.SelectedIndex == 7)
                        {
                            textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                            textBox_number_W.Text = image.Width.ToString();
                            textBox_number_H.Text = image.Height.ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_CounterCOOL:
                    {
                        if (comboBox_verdictCounterNumbers.SelectedIndex == 0)
                        {
                            textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_CounterGOOD:
                    {
                        if (comboBox_verdictCounterNumbers.SelectedIndex == 1)
                        {
                            textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_CounterOKAY:
                    {
                        if (comboBox_verdictCounterNumbers.SelectedIndex == 2)
                        {
                            textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_CounterBAD:
                    {
                        if (comboBox_verdictCounterNumbers.SelectedIndex == 3)
                        {
                            textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.Numbers_CounterMISS:
                    {
                        if (comboBox_verdictCounterNumbers.SelectedIndex == 4)
                        {
                            textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(image).ToString();
                            textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(image).ToString();
                        }
                    }
                    break;

                case ComponentIDs.ArbitraryTexture:
                    {
                        textBox_arbitraryTexture_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_arbitraryTexture_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_arbitraryTexture_W.Text = image.Width.ToString();
                        textBox_arbitraryTexture_H.Text = image.Height.ToString();
                    }
                    break;
            }
        }

        /*
         * 
         *  ░░░░░░░░░░░▄▄▀▀▀▀▀▀▀▀▄▄
         *  ░░░░░░░░▄▀▀░░░░░░░░░░░░▀▄▄
         *  ░░░░░░▄▀░░░░░░░░░░░░░░░░░░▀▄
         *  ░░░░░▌░░░░░░░░░░░░░▀▄░░░░░░░▀▀▄
         *  ░░░░▌░░░░░░░░░░░░░░░░▀▌░░░░░░░░▌
         *  ░░░▐░░░░░░░░░░░░▒░░░░░▌░░░░░░░░▐
         *  ░░░▌▐░░░░▐░░░░▐▒▒░░░░░▌░░░░░░░░░▌
         *  ░░▐░▌░░░░▌░░▐░▌▒▒▒░░░▐░░░░░▒░▌▐░▐
         *  ░░▐░▌▒░░░▌▄▄▀▀▌▌▒▒░▒░▐▀▌▀▌▄▒░▐▒▌░▌
         *  ░░░▌▌░▒░░▐▀▄▌▌▐▐▒▒▒▒▐▐▐▒▐▒▌▌░▐▒▌▄▐
         *  ░▄▀▄▐▒▒▒░▌▌▄▀▄▐░▌▌▒▐░▌▄▀▄░▐▒░▐▒▌░▀▄
         *  ▀▄▀▒▒▌▒▒▄▀░▌█▐░░▐▐▀░░░▌█▐░▀▄▐▒▌▌░░░▀
         *  ░▀▀▄▄▐▒▀▄▀░▀▄▀░░░░░░░░▀▄▀▄▀▒▌░▐
         *  ░░░░▀▐▀▄▒▀▄░░░░░░░░▐░░░░░░▀▌▐
         *  ░░░░░░▌▒▌▐▒▀░░░░░░░░░░░░░░▐▒▐
         *  ░░░░░░▐░▐▒▌░░░░▄▄▀▀▀▀▄░░░░▌▒▐
         *  ░░░░░░░▌▐▒▐▄░░░▐▒▒▒▒▒▌░░▄▀▒░▐
         *  ░░░░░░▐░░▌▐▐▀▄░░▀▄▄▄▀░▄▀▐▒░░▐
         *  ░░░░░░▌▌░▌▐░▌▒▀▄▄░░░░▄▌▐░▌▒░▐
         *  ░░░░░▐▒▐░▐▐░▌▒▒▒▒▀▀▄▀▌▐░░▌▒░▌
         *  ░░░░░▌▒▒▌▐▒▌▒▒▒▒▒▒▒▒▐▀▄▌░▐▒▒
         * 
         */

        private bool IsValidTexSize(Skin.FullLayout layout)
        {
            return layout.TW >= _texLowerLimit && layout.TH >= _texLowerLimit;
        }

        private bool IsValidSize(int number)
        {
            return number >= _texLowerLimit;
        }

        private bool IsValidSize(int width, int height)
        {
            return width >= _texLowerLimit && height >= _texLowerLimit;
        }

        private void ThrowIfNull(object? obj)
        {
            if (obj == null)
            {
                throw new ApplicationException(Properties.Resources.Error_InvalidData);
            }
        }

        private void ThrowIfEmpty(int number)
        {
            if (number == 0)
            {
                throw new ApplicationException(Properties.Resources.Error_InvalidData);
            }
        }

        private void ThrowIfNotValidTexSize(int width, int height)
        {
            if (width < _texLowerLimit || height < _texLowerLimit)
            {
                throw new ApplicationException(Properties.Resources.Error_InvalidData);
            }
        }

        private CroppedBitmap CreateTextureFromAtlas(Skin.FullLayout layout)
        {
            return new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
        }

        private void InitializeNumbers(Image image, Skin.FullLayout layout, ComponentIDs componentID, string byname)
        {
            // Numbers are not opcional
            ThrowIfNotValidTexSize(layout.TW, layout.TH);

            INTERNAL_InitializeImage(image, layout);
            _skinComponents.Add(new SkinComponent(componentID, byname, image));
        }

        private void InitializeCounterNumbers(Image image, Skin.FullLayout position, ComponentIDs componentID, string byname)
        {
            INTERNAL_InitializeImageSize(image, _jsonPlayfield7k.Numbers_VerdictCounter);
            INTERNAL_InitializeImagePosition(image, position);

            _skinComponents.Add(new SkinComponent(componentID, byname, image));
        }

        private void InitializeImage(Image image, Skin.FullLayout layout, ComponentIDs componentID, string byname)
        {
            INTERNAL_InitializeImage(image, layout);
            _skinComponents.Add(new SkinComponent(componentID, byname, image));
        }

        private void INTERNAL_InitializeImage(Image image, Skin.FullLayout layout)
        {
            INTERNAL_InitializeImageTexture(image, layout);
            INTERNAL_InitializeImageSize(image, layout);
            INTERNAL_InitializeImagePosition(image, layout);
        }

        private void INTERNAL_InitializeImageTexture(Image? image, Skin.FullLayout layout)
        {
            if (image == null)
            {
                image = new Image();
                image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
            }
            else
            {
                image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
            }
        }

        private void INTERNAL_InitializeImageSize(Image image, Skin.FullLayout layout)
        {
            image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
            image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;
        }

        private void INTERNAL_InitializeImagePosition(Image image, Skin.FullLayout layout)
        {
            Canvas.SetLeft(image, layout.X);
            Canvas.SetTop(image, layout.Y);
        }

        private void JSON_SetPositionAndSize(Skin.FullLayout layout, Image? image)
        {
            if (image != null)
            {
                layout.X = (int)Canvas.GetLeft(image);
                layout.Y = (int)Canvas.GetTop(image);
                layout.W = (int)image.Width;
                layout.H = (int)image.Height;
            }
        }

        private void JSON_SetPosition(Skin.FullLayout layout, Image? image)
        {
            if (image != null)
            {
                layout.X = (int)Canvas.GetLeft(image);
                layout.Y = (int)Canvas.GetTop(image);
            }
        }

        #endregion

        private void MenuItem_Click_OpenJsonFile(object sender, RoutedEventArgs args)
        {
            try
            {
                OpenFileDialog ofd = new();
                ofd.Filter = "JSON Files|*.json";

                bool? result = ofd.ShowDialog(this);
                if (result == true)
                {
                    _jsonFilePath = ofd.FileName;

                    textBlock_selectedJsonFile.Text = Properties.Resources.JsonFile + ": " + ofd.SafeFileName;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click_OpenImageFile(object sender, RoutedEventArgs args)
        {
            try
            {
                OpenFileDialog ofd = new();
                ofd.Filter = "PNG Files|*.png";

                bool? result = ofd.ShowDialog(this);
                if (result == true)
                {
                    _imageFilePath = ofd.FileName;

                    textBlock_selectedImageFile.Text = Properties.Resources.ImageFile + ": " + ofd.SafeFileName;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click_ExportJson(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            SaveFileDialog dlg = new();
            dlg.Filter = "JSON Files|*.json";
            dlg.DefaultExt = ".json";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (dlg.ShowDialog(this) == false) { return; }

            // I need to apply data validation to the controls.

            if (string.IsNullOrWhiteSpace(textBox_skinName.Text)) { return; }
            _jsonPlayfield7k.SkinName = textBox_skinName.Text;

            if (string.IsNullOrWhiteSpace(textBox_skinVersion.Text)) { return; }
            _jsonPlayfield7k.SkinVersion = textBox_skinVersion.Text;

            if (string.IsNullOrWhiteSpace(textBox_skinCreator.Text)) { return; }
            _jsonPlayfield7k.SkinCreator = textBox_skinCreator.Text;

            _jsonPlayfield7k.ScreenResolutionWidth = int.Parse(textBox_canvasWidth.Text);
            _jsonPlayfield7k.ScreenResolutionHeight = int.Parse(textBox_canvasHeight.Text);

            _jsonPlayfield7k.AccompanyingArtwork_X = int.Parse(textBox_accompanyingArtwork_X.Text);
            _jsonPlayfield7k.AccompanyingArtwork_Y = int.Parse(textBox_accompanyingArtwork_Y.Text);
            _jsonPlayfield7k.AccompanyingArtwork_W = int.Parse(textBox_accompanyingArtwork_W.Text);
            _jsonPlayfield7k.AccompanyingArtwork_H = int.Parse(textBox_accompanyingArtwork_H.Text);

            _jsonPlayfield7k.WallPapers.Clear();
            foreach (string s in _wallpapers) { _jsonPlayfield7k.WallPapers.Add(s); }

            _jsonPlayfield7k.Companion = textBox_companionName.Text;
            _jsonPlayfield7k.CompanionFramesQuantity = int.Parse(textBox_companionFrameQuantity.Text);
            _jsonPlayfield7k.CompanionFrameWidth = int.Parse(textBox_companionFrameWidth.Text);
            _jsonPlayfield7k.CompanionFrameHeight = int.Parse(textBox_companionFrameHeight.Text);
            _jsonPlayfield7k.Companion_X = int.Parse(textBox_companionX.Text);
            _jsonPlayfield7k.Companion_Y = int.Parse(textBox_companionY.Text);
            _jsonPlayfield7k.Companion_W = int.Parse(textBox_companionSW.Text);
            _jsonPlayfield7k.Companion_H = int.Parse(textBox_companionSH.Text);

            _jsonPlayfield7k.Playfield.StartX = int.Parse(textBox_playfieldOffset.Text);
            _jsonPlayfield7k.Playfield.Height = int.Parse(textBox_playfieldHeight.Text);
            _jsonPlayfield7k.Playfield.Columns_1357_Width = int.Parse(textBox_playfield1357ColumnsWidth.Text);
            _jsonPlayfield7k.Playfield.Columns_246_Width = int.Parse(textBox_playfield246ColumnsWidth.Text);
            _jsonPlayfield7k.Playfield.ColumnSeparatorsWidth = int.Parse(textBox_playfieldColumnSeparatorsWidth.Text);

            _jsonPlayfield7k.Playfield.Columns_1357_Color = Color1FromColor(((SolidColorBrush)_playfieldColumn1.Fill).Color);
            _jsonPlayfield7k.Playfield.Columns_246_Color = Color1FromColor(((SolidColorBrush)_playfieldColumn2.Fill).Color);
            _jsonPlayfield7k.Playfield.ColumnSeparatorsColor = Color1FromColor(((SolidColorBrush)_playfieldColumnSeparator1.Fill).Color);
            _jsonPlayfield7k.Playfield.VerdictLineColor = Color1FromColor(((SolidColorBrush)_playfieldVerdictLine.Fill).Color);
            _jsonPlayfield7k.Playfield.BorderColor = Color1FromColor(((SolidColorBrush)_playfieldBorderBottom.Fill).Color);

            _jsonPlayfield7k.BombName = textBox_bombName.Text;
            _jsonPlayfield7k.BombFrameQuantity = int.Parse(textBox_bombFrameQuantity.Text);
            _jsonPlayfield7k.BombFrameWidth = int.Parse(textBox_bombFrameWidth.Text);
            _jsonPlayfield7k.BombFrameHeight = int.Parse(textBox_bombFrameHeight.Text);
            _jsonPlayfield7k.BombDuration = int.Parse(textBox_bombDuration.Text);
            _jsonPlayfield7k.Bomb_W = int.Parse(textBox_bombSW.Text);
            _jsonPlayfield7k.Bomb_H = int.Parse(textBox_bombSH.Text);

            _jsonPlayfield7k.LaserHeight = int.Parse(textBox_laserSH.Text);
            _jsonPlayfield7k.LaserDuration = int.Parse(textBox_laserDuration.Text);
            _jsonPlayfield7k.LaserAnimations.Clear();
            foreach (LaserAnime anime in _laserAnimations)
            {
                Skin.LaserAnimation animation = new();
                animation.Time = anime.Time;
                animation.WidthFactor = anime.WidthFactor;
                animation.HeightFactor = anime.HeightFactor;
                animation.OpacityFactor = anime.OpacityFactor;

                _jsonPlayfield7k.LaserAnimations.Add(animation);
            }

            {
                int x;
                int.TryParse(textBox_keysBG_X.Text, out x);

                int y;
                int.TryParse(textBox_keysBG_Y.Text, out y);

                int w;
                int.TryParse(textBox_keysBG_W.Text, out w);

                int h;
                int.TryParse(textBox_keysBG_H.Text, out h);

                _jsonPlayfield7k.KeysBackground.X = x;
                _jsonPlayfield7k.KeysBackground.Y = h;
                _jsonPlayfield7k.KeysBackground.W = w;
                _jsonPlayfield7k.KeysBackground.H = h;
            }

            _jsonPlayfield7k.Keys_1357_Margin = int.Parse(textBox_keys1357margin.Text);
            _jsonPlayfield7k.Keys_1357_Height = int.Parse(textBox_keys1357SH.Text);
            _jsonPlayfield7k.Keys_246_Margin = int.Parse(textBox_keys246margin.Text);
            _jsonPlayfield7k.Keys_246_Height = int.Parse(textBox_keys246SH.Text);

            _jsonPlayfield7k.Notes_H = int.Parse(textBox_notesHeight.Text);

            _jsonPlayfield7k.Injection_Replay.X = (int)Canvas.GetLeft(_texInjection_Replay);
            _jsonPlayfield7k.Injection_Replay.Y = (int)Canvas.GetTop(_texInjection_Replay);

            _jsonPlayfield7k.Injection_O2Hard.X = (int)Canvas.GetLeft(_texInjection_O2Hard);
            _jsonPlayfield7k.Injection_O2Hard.Y = (int)Canvas.GetTop(_texInjection_O2Hard);

            _jsonPlayfield7k.Injection_Mirror.X = (int)Canvas.GetLeft(_texInjection_Mirror);
            _jsonPlayfield7k.Injection_Mirror.Y = (int)Canvas.GetTop(_texInjection_Mirror);

            _jsonPlayfield7k.Injection_AllLN.X = (int)Canvas.GetLeft(_texInjection_AllLN);
            _jsonPlayfield7k.Injection_AllLN.Y = (int)Canvas.GetTop(_texInjection_AllLN);

            _jsonPlayfield7k.Injection_LR2NMJudge.X = (int)Canvas.GetLeft(_texInjection_LR2NMJudge);
            _jsonPlayfield7k.Injection_LR2NMJudge.Y = (int)Canvas.GetTop(_texInjection_LR2NMJudge);

            _jsonPlayfield7k.Injection_NoSSC.X = (int)Canvas.GetLeft(_texInjection_NoSSC);
            _jsonPlayfield7k.Injection_NoSSC.Y = (int)Canvas.GetTop(_texInjection_NoSSC);

            _jsonPlayfield7k.Injections_W = int.Parse(textBox_injectionsSW.Text);
            _jsonPlayfield7k.Injections_H = int.Parse(textBox_injectionsSH.Text);

            _jsonPlayfield7k.HealthBarFillIsHorizontal = (bool)checkBox_healthBarFillIsHorizontal.IsChecked;

            if (_texHealthBarBG != null)
            {
                _jsonPlayfield7k.HealthBarBackground.X = int.Parse(textBox_healthBarBackgroundX.Text);
                _jsonPlayfield7k.HealthBarBackground.Y = int.Parse(textBox_healthBarBackgroundY.Text);
                _jsonPlayfield7k.HealthBarBackground.W = int.Parse(textBox_healthBarBackgroundSW.Text);
                _jsonPlayfield7k.HealthBarBackground.H = int.Parse(textBox_healthBarBackgroundSH.Text);
            }
            _jsonPlayfield7k.HealthBar.X = int.Parse(textBox_healthBarX.Text);
            _jsonPlayfield7k.HealthBar.Y = int.Parse(textBox_healthBarY.Text);
            _jsonPlayfield7k.HealthBar.W = int.Parse(textBox_healthBarSW.Text);
            _jsonPlayfield7k.HealthBar.H = int.Parse(textBox_healthBarSH.Text);

            _jsonPlayfield7k.Ranks_X = int.Parse(textBox_rankX.Text);
            _jsonPlayfield7k.Ranks_Y = int.Parse(textBox_rankY.Text);
            _jsonPlayfield7k.Ranks_W = int.Parse(textBox_rankSW.Text);
            _jsonPlayfield7k.Ranks_H = int.Parse(textBox_rankSH.Text);

            _jsonPlayfield7k.Verdicts_ClassicPosition = (bool)checkBox_judgementsClassicPosition.IsChecked;
            _jsonPlayfield7k.Verdicts_X = int.Parse(textBox_verdicts_X.Text);
            _jsonPlayfield7k.Verdicts_Y = int.Parse(textBox_verdicts_Y.Text);
            _jsonPlayfield7k.Verdicts_ScaleW = float.Parse(textBox_verdicts_ScaleW.Text);
            _jsonPlayfield7k.Verdicts_H = int.Parse(textBox_verdicts_H.Text);

            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_Health, _texPlacard_Health);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_BPM, _texPlacard_BPM);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_ScrollSpeed, _texPlacard_ScrollSpeed);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_Offset, _texPlacard_Offset);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_MaxCombo, _texPlacard_MaxCombo);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_Accuracy, _texPlacard_Accuracy);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_Time, _texPlacard_Time);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_COOL, _texPlacard_COOL);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_GOOD, _texPlacard_GOOD);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_OKAY, _texPlacard_OKAY);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_BAD, _texPlacard_BAD);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Placard_MISS, _texPlacard_MISS);

            JSON_SetPositionAndSize(_jsonPlayfield7k.Numbers_Health, _texNumbers_Health);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Numbers_BPM, _texNumbers_BPM);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Numbers_ScrollSpeed, _texNumbers_ScrollSpeed);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Numbers_Offset, _texNumbers_Offset);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Numbers_MaxCombo, _texNumbers_MaxCombo);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Numbers_Accuracy, _texNumbers_Accuracy);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Numbers_Time, _texNumbers_Time);
            JSON_SetPositionAndSize(_jsonPlayfield7k.Numbers_COOL, _texNumbers_COOL);

            // Verdict counters numbers

            _jsonPlayfield7k.Numbers_VerdictCounter.W = int.Parse(textBox_verdictCounterNumbers_W.Text);
            _jsonPlayfield7k.Numbers_VerdictCounter.H = int.Parse(textBox_verdictCounterNumbers_H.Text);

            JSON_SetPosition(_jsonPlayfield7k.CounterPosition_COOL, _texNumbers_CounterCOOL);
            JSON_SetPosition(_jsonPlayfield7k.CounterPosition_GOOD, _texNumbers_CounterGOOD);
            JSON_SetPosition(_jsonPlayfield7k.CounterPosition_OKAY, _texNumbers_CounterOKAY);
            JSON_SetPosition(_jsonPlayfield7k.CounterPosition_BAD, _texNumbers_CounterBAD);
            JSON_SetPosition(_jsonPlayfield7k.CounterPosition_MISS, _texNumbers_CounterMISS);

            /* Arbitrary texture */

            JSON_SetPositionAndSize(_jsonPlayfield7k.ArbitraryTexture, _texArbitraryTexture);

            Skin.MarshalPlayfield7k marshalThis = MarshalHelper.ToMarshalable(_jsonPlayfield7k);

            var options = new JsonSerializerOptions() { WriteIndented = true };
            using (FileStream fs = File.OpenWrite(dlg.FileName))
            {
                JsonSerializer.Serialize<Skin.MarshalPlayfield7k>(fs, marshalThis, options);
            }

            MessageBox.Show(this, Properties.Resources.Notification_Success_TaskCompleted, Properties.Resources.Notification, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_LoadSkinData(object sender, RoutedEventArgs args)
        {
            if (_skinDataIsLoaded)
            {
                MessageBox.Show(this, Properties.Resources.InvalidOp_SkinDataAlreadyLoaded, Properties.Resources.InvalidOperation, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrEmpty(_jsonFilePath) || string.IsNullOrEmpty(_imageFilePath))
            {
                MessageBox.Show(this, Properties.Resources.InvalidOp_LoadSkinData, Properties.Resources.InvalidOperation, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                /* Load JSON data */

                JsonSerializerOptions options = new();
                options.AllowTrailingCommas = false;
                options.ReadCommentHandling = JsonCommentHandling.Skip;

                try
                {
                    using (FileStream fs = File.OpenRead(_jsonFilePath))
                    {
                        _jsonPlayfield7k = JsonSerializer.Deserialize<Skin.Playfield7k>(fs, options);
                    }
                }
                catch (Exception e)
                {
                    throw new ApplicationException(Properties.Resources.Error_JsonCouldNotBeLoaded, e);
                }

                if (_jsonPlayfield7k == null)
                {
                    throw new ApplicationException(Properties.Resources.Error_JsonCouldNotBeLoaded);
                }

                /* Load texture atlas image */

                _textureAtlasPlayfield7k.BeginInit();
                _textureAtlasPlayfield7k.UriSource = new(_imageFilePath, UriKind.Absolute);
                _textureAtlasPlayfield7k.EndInit();

                /*** Load the info from the skin descriptor ***/

                _skinDataIsLoaded = true;

                /* Skin attributions */

                textBox_skinName.Text = _jsonPlayfield7k.SkinName;
                textBox_skinVersion.Text = _jsonPlayfield7k.SkinVersion;
                textBox_skinCreator.Text = _jsonPlayfield7k.SkinCreator;

                /* Canvas */

                textBox_canvasWidth.Text = _jsonPlayfield7k.ScreenResolutionWidth.ToString();
                textBox_canvasHeight.Text = _jsonPlayfield7k.ScreenResolutionHeight.ToString();

                canvas.Width = _jsonPlayfield7k.ScreenResolutionWidth;
                canvas.Height = _jsonPlayfield7k.ScreenResolutionHeight;

                /* Accompanying Artwork */

                {
                    _texAccompanyingArtwork.Source = new BitmapImage(new Uri("pack://application:,,,/ProvisionalAA.png", UriKind.Absolute));
                    _texAccompanyingArtwork.Width = IsValidSize(_jsonPlayfield7k.AccompanyingArtwork_W) ? _jsonPlayfield7k.AccompanyingArtwork_W : 256;
                    _texAccompanyingArtwork.Height = IsValidSize(_jsonPlayfield7k.AccompanyingArtwork_H) ? _jsonPlayfield7k.AccompanyingArtwork_H : 256;

                    Canvas.SetLeft(_texAccompanyingArtwork, _jsonPlayfield7k.AccompanyingArtwork_X);
                    Canvas.SetTop(_texAccompanyingArtwork, _jsonPlayfield7k.AccompanyingArtwork_Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.AccompanyingArtwork, Properties.Resources.AccompanyingArtwork, _texAccompanyingArtwork));

                    textBox_accompanyingArtwork_X.Text = _jsonPlayfield7k.AccompanyingArtwork_X.ToString();
                    textBox_accompanyingArtwork_Y.Text = _jsonPlayfield7k.AccompanyingArtwork_Y.ToString();
                    textBox_accompanyingArtwork_W.Text = _texAccompanyingArtwork.Width.ToString();
                    textBox_accompanyingArtwork_H.Text = _texAccompanyingArtwork.Height.ToString();
                }

                /* Wallpapers */

                foreach (string s in _jsonPlayfield7k.WallPapers)
                {
                    _wallpapers.Add(s);
                }

                /* Companion */

                {
                    _texCompanion.Source = new BitmapImage(new Uri("pack://application:,,,/ProvisionalCompanion.png", UriKind.Absolute));
                    _texCompanion.Width = IsValidSize(_jsonPlayfield7k.Companion_W) ? _jsonPlayfield7k.Companion_W : 150;
                    _texCompanion.Height = IsValidSize(_jsonPlayfield7k.Companion_H) ? _jsonPlayfield7k.Companion_H : 150;

                    Canvas.SetLeft(_texCompanion, _jsonPlayfield7k.Companion_X);
                    Canvas.SetTop(_texCompanion, _jsonPlayfield7k.Companion_Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Companion, Properties.Resources.Companion, _texCompanion));

                    textBox_companionName.Text = _jsonPlayfield7k.Companion;
                    textBox_companionFrameQuantity.Text = _jsonPlayfield7k.CompanionFramesQuantity.ToString();
                    textBox_companionFrameWidth.Text = _jsonPlayfield7k.CompanionFrameWidth.ToString();
                    textBox_companionFrameHeight.Text = _jsonPlayfield7k.CompanionFrameHeight.ToString();

                    textBox_companionX.Text = _jsonPlayfield7k.Companion_X.ToString();
                    textBox_companionY.Text = _jsonPlayfield7k.Companion_Y.ToString();

                    textBox_companionSW.Text = _texCompanion.Width.ToString();
                    textBox_companionSH.Text = _texCompanion.Height.ToString();
                }

                /* Playfield */

                textBox_playfieldOffset.Text = _jsonPlayfield7k.Playfield.StartX.ToString();
                textBox_playfieldHeight.Text = _jsonPlayfield7k.Playfield.Height.ToString();
                textBox_playfield1357ColumnsWidth.Text = _jsonPlayfield7k.Playfield.Columns_1357_Width.ToString();
                textBox_playfield246ColumnsWidth.Text = _jsonPlayfield7k.Playfield.Columns_246_Width.ToString();
                textBox_playfieldColumnSeparatorsWidth.Text = _jsonPlayfield7k.Playfield.ColumnSeparatorsWidth.ToString();

                // Playfield-related color information

                textBox_red.Text = _jsonPlayfield7k.Playfield.Columns_1357_Color.R.ToString();
                textBox_green.Text = _jsonPlayfield7k.Playfield.Columns_1357_Color.G.ToString();
                textBox_blue.Text = _jsonPlayfield7k.Playfield.Columns_1357_Color.B.ToString();
                textBox_opacity.Text = _jsonPlayfield7k.Playfield.Columns_1357_Color.A.ToString();
                rectangle_color.Fill = new SolidColorBrush(ColorFromColor1(_jsonPlayfield7k.Playfield.Columns_1357_Color));

                // Adjust the UIElements that compose the playfield in the canvas

                // Columns 1,3,5,7

                SolidColorBrush columns1357Color = new(ColorFromColor1(_jsonPlayfield7k.Playfield.Columns_1357_Color));

                _playfieldColumn1.Width = _jsonPlayfield7k.Playfield.Columns_1357_Width;
                _playfieldColumn1.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumn1.Fill = columns1357Color;

                _playfieldColumn3.Width = _jsonPlayfield7k.Playfield.Columns_1357_Width;
                _playfieldColumn3.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumn3.Fill = columns1357Color;

                _playfieldColumn5.Width = _jsonPlayfield7k.Playfield.Columns_1357_Width;
                _playfieldColumn5.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumn5.Fill = columns1357Color;

                _playfieldColumn7.Width = _jsonPlayfield7k.Playfield.Columns_1357_Width;
                _playfieldColumn7.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumn7.Fill = columns1357Color;

                // Columns 2,4,6

                SolidColorBrush columns246Color = new(ColorFromColor1(_jsonPlayfield7k.Playfield.Columns_246_Color));

                _playfieldColumn2.Width = _jsonPlayfield7k.Playfield.Columns_246_Width;
                _playfieldColumn2.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumn2.Fill = columns246Color;

                _playfieldColumn4.Width = _jsonPlayfield7k.Playfield.Columns_246_Width;
                _playfieldColumn4.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumn4.Fill = columns246Color;

                _playfieldColumn6.Width = _jsonPlayfield7k.Playfield.Columns_246_Width;
                _playfieldColumn6.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumn6.Fill = columns246Color;

                // Column separators

                SolidColorBrush columnSeparatorsColor = new(ColorFromColor1(_jsonPlayfield7k.Playfield.ColumnSeparatorsColor));

                _playfieldColumnSeparator1.Width = _jsonPlayfield7k.Playfield.ColumnSeparatorsWidth;
                _playfieldColumnSeparator1.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumnSeparator1.Fill = columnSeparatorsColor;

                _playfieldColumnSeparator2.Width = _jsonPlayfield7k.Playfield.ColumnSeparatorsWidth;
                _playfieldColumnSeparator2.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumnSeparator2.Fill = columnSeparatorsColor;

                _playfieldColumnSeparator3.Width = _jsonPlayfield7k.Playfield.ColumnSeparatorsWidth;
                _playfieldColumnSeparator3.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumnSeparator3.Fill = columnSeparatorsColor;

                _playfieldColumnSeparator4.Width = _jsonPlayfield7k.Playfield.ColumnSeparatorsWidth;
                _playfieldColumnSeparator4.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumnSeparator4.Fill = columnSeparatorsColor;

                _playfieldColumnSeparator5.Width = _jsonPlayfield7k.Playfield.ColumnSeparatorsWidth;
                _playfieldColumnSeparator5.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumnSeparator5.Fill = columnSeparatorsColor;

                _playfieldColumnSeparator6.Width = _jsonPlayfield7k.Playfield.ColumnSeparatorsWidth;
                _playfieldColumnSeparator6.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldColumnSeparator6.Fill = columnSeparatorsColor;

                // Playfield border

                SolidColorBrush playfieldBorderColor = new(ColorFromColor1(_jsonPlayfield7k.Playfield.BorderColor));

                _playfieldBorderLeft.Width = 1.0;
                _playfieldBorderLeft.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldBorderLeft.Fill = playfieldBorderColor;

                _playfieldBorderRight.Width = 1.0;
                _playfieldBorderRight.Height = _jsonPlayfield7k.Playfield.Height;
                _playfieldBorderRight.Fill = playfieldBorderColor;

                double playfieldWidth = Calculate7kPlayfieldWidth(true);
                _playfieldBorderBottom.Width = playfieldWidth + 2;
                _playfieldBorderBottom.Height = 1.0;
                _playfieldBorderBottom.Fill = playfieldBorderColor;

                // Verdict line

                _playfieldVerdictLine.Width = playfieldWidth;
                // IMPORTANT: _playfieldVerdictLine.Height = this is done in *Notes*
                _playfieldVerdictLine.Fill = new SolidColorBrush(ColorFromColor1(_jsonPlayfield7k.Playfield.VerdictLineColor));

                /* Bombs */

                textBox_bombName.Text = _jsonPlayfield7k.BombName;
                textBox_bombFrameQuantity.Text = _jsonPlayfield7k.BombFrameQuantity.ToString();
                textBox_bombFrameWidth.Text = _jsonPlayfield7k.BombFrameWidth.ToString();
                textBox_bombFrameHeight.Text = _jsonPlayfield7k.BombFrameHeight.ToString();
                textBox_bombDuration.Text = _jsonPlayfield7k.BombDuration.ToString();
                textBox_bombSW.Text = _jsonPlayfield7k.Bomb_W.ToString();
                textBox_bombSH.Text = _jsonPlayfield7k.Bomb_H.ToString();

                /* Lasers */

                // Load the laser texture

                if (IsValidSize(_jsonPlayfield7k.Laser_7k_1357.TW, _jsonPlayfield7k.Laser_7k_1357.TH))
                {
                    _texLaser_7k_1357.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Laser_7k_1357.U, _jsonPlayfield7k.Laser_7k_1357.V, _jsonPlayfield7k.Laser_7k_1357.TW, _jsonPlayfield7k.Laser_7k_1357.TH));
                    _texLaser_7k_1357.Width = _jsonPlayfield7k.Laser_7k_1357.TW;
                    _texLaser_7k_1357.Height = IsValidSize(_jsonPlayfield7k.LaserHeight) ? _jsonPlayfield7k.LaserHeight : _jsonPlayfield7k.Laser_7k_1357.TH;

                    textBox_laserDuration.Text = _jsonPlayfield7k.LaserDuration.ToString();
                    textBox_laserSH.Text = _texLaser_7k_1357.Height.ToString();
                }

                // Load the laser animation objects

                foreach (Skin.LaserAnimation animation in _jsonPlayfield7k.LaserAnimations)
                {
                    _laserAnimations.Add(new LaserAnime(animation));
                }

                /* Keys */

                // Keys background

                if (IsValidSize(_jsonPlayfield7k.KeysBackground.TW, _jsonPlayfield7k.KeysBackground.TH))
                {
                    _isKeysBackgroundUsed = true;

                    _texKeysBG = new Image();

                    _texKeysBG.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.KeysBackground.U, _jsonPlayfield7k.KeysBackground.V, _jsonPlayfield7k.KeysBackground.TW, _jsonPlayfield7k.KeysBackground.TH));
                    _texKeysBG.Width = IsValidSize(_jsonPlayfield7k.KeysBackground.W) ? _jsonPlayfield7k.KeysBackground.W : _jsonPlayfield7k.KeysBackground.TW;
                    _texKeysBG.Height = IsValidSize(_jsonPlayfield7k.KeysBackground.H) ? _jsonPlayfield7k.KeysBackground.H : _jsonPlayfield7k.KeysBackground.TH;

                    Canvas.SetLeft(_texKeysBG, _jsonPlayfield7k.KeysBackground.X);
                    Canvas.SetTop(_texKeysBG, _jsonPlayfield7k.KeysBackground.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.KeysBackground, Properties.Resources.KeysBackground, _texKeysBG));

                    textBox_keysBG_X.Text = _jsonPlayfield7k.KeysBackground.X.ToString();
                    textBox_keysBG_Y.Text = _jsonPlayfield7k.KeysBackground.Y.ToString();
                    textBox_keysBG_W.Text = _texKeysBG.Width.ToString();
                    textBox_keysBG_H.Text = _texKeysBG.Height.ToString();
                }

                // Down keys

                textBox_keys1357margin.Text = _jsonPlayfield7k.Keys_1357_Margin.ToString();
                textBox_keys1357SH.Text = _jsonPlayfield7k.Keys_1357_Height.ToString();
                textBox_keys246margin.Text = _jsonPlayfield7k.Keys_246_Margin.ToString();
                textBox_keys246SH.Text = _jsonPlayfield7k.Keys_246_Height.ToString();

                // Load the textures for the keys

                ThrowIfNotValidTexSize(_jsonPlayfield7k.Key_7k_1357.TW, _jsonPlayfield7k.Key_7k_1357.TH);
                ThrowIfNotValidTexSize(_jsonPlayfield7k.Key_7k_26.TW, _jsonPlayfield7k.Key_7k_26.TH);
                ThrowIfNotValidTexSize(_jsonPlayfield7k.Key_7k_4.TW, _jsonPlayfield7k.Key_7k_4.TH);

                //if (IsValidSize(_jsonPlayfield7k.Key_7k_1357.TW, _jsonPlayfield7k.Key_7k_1357.TH))
                {
                    CroppedBitmap texture = new(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Key_7k_1357.U, _jsonPlayfield7k.Key_7k_1357.V, _jsonPlayfield7k.Key_7k_1357.TW, _jsonPlayfield7k.Key_7k_1357.TH));
                    _texKey_7k_1357_Sprite1.Source = texture;
                    _texKey_7k_1357_Sprite2.Source = texture;
                    _texKey_7k_1357_Sprite3.Source = texture;
                    _texKey_7k_1357_Sprite4.Source = texture;

                    _texKey_7k_1357_Sprite1.Width = _jsonPlayfield7k.Playfield.Columns_1357_Width;
                    _texKey_7k_1357_Sprite2.Width = _jsonPlayfield7k.Playfield.Columns_1357_Width;
                    _texKey_7k_1357_Sprite3.Width = _jsonPlayfield7k.Playfield.Columns_1357_Width;
                    _texKey_7k_1357_Sprite4.Width = _jsonPlayfield7k.Playfield.Columns_1357_Width;

                    _texKey_7k_1357_Sprite1.Height = _jsonPlayfield7k.Keys_1357_Height;
                    _texKey_7k_1357_Sprite2.Height = _jsonPlayfield7k.Keys_1357_Height;
                    _texKey_7k_1357_Sprite3.Height = _jsonPlayfield7k.Keys_1357_Height;
                    _texKey_7k_1357_Sprite4.Height = _jsonPlayfield7k.Keys_1357_Height;
                }

                //if (IsValidSize(_jsonPlayfield7k.Key_7k_26.TW, _jsonPlayfield7k.Key_7k_26.TH))
                {
                    CroppedBitmap texture = new(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Key_7k_26.U, _jsonPlayfield7k.Key_7k_26.V, _jsonPlayfield7k.Key_7k_26.TW, _jsonPlayfield7k.Key_7k_26.TH));
                    _texKey_7k_26_Sprite1.Source = texture;
                    _texKey_7k_26_Sprite2.Source = texture;

                    _texKey_7k_26_Sprite1.Width = _jsonPlayfield7k.Playfield.Columns_246_Width;
                    _texKey_7k_26_Sprite2.Width = _jsonPlayfield7k.Playfield.Columns_246_Width;

                    _texKey_7k_26_Sprite1.Height = _jsonPlayfield7k.Keys_246_Height;
                    _texKey_7k_26_Sprite2.Height = _jsonPlayfield7k.Keys_246_Height;
                }

                //if (IsValidSize(_jsonPlayfield7k.Key_7k_4.TW, _jsonPlayfield7k.Key_7k_4.TH))
                {
                    _texKey_7k_4_Sprite1.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Key_7k_4.U, _jsonPlayfield7k.Key_7k_4.V, _jsonPlayfield7k.Key_7k_4.TW, _jsonPlayfield7k.Key_7k_4.TH));
                    _texKey_7k_4_Sprite1.Width = _jsonPlayfield7k.Playfield.Columns_246_Width;
                    _texKey_7k_4_Sprite1.Height = _jsonPlayfield7k.Keys_246_Height;
                }

                /* Notes */

                // Load textures for normal-notes and long-notes

                ThrowIfNotValidTexSize(_jsonPlayfield7k.Note_7k_N_1357.TW, _jsonPlayfield7k.Note_7k_N_1357.TH);
                ThrowIfNotValidTexSize(_jsonPlayfield7k.Note_7k_N_26.TW, _jsonPlayfield7k.Note_7k_N_26.TH);
                ThrowIfNotValidTexSize(_jsonPlayfield7k.Note_7k_N_4.TW, _jsonPlayfield7k.Note_7k_N_4.TH);
                ThrowIfNotValidTexSize(_jsonPlayfield7k.Note_7k_L_1357.TW, _jsonPlayfield7k.Note_7k_L_1357.TH);
                ThrowIfNotValidTexSize(_jsonPlayfield7k.Note_7k_L_26.TW, _jsonPlayfield7k.Note_7k_L_26.TH);
                ThrowIfNotValidTexSize(_jsonPlayfield7k.Note_7k_L_4.TW, _jsonPlayfield7k.Note_7k_L_4.TH);

                int notesHeight = 0;

                {
                    _texNote_7k_1357.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Note_7k_N_1357.U, _jsonPlayfield7k.Note_7k_N_1357.V, _jsonPlayfield7k.Note_7k_N_1357.TW, _jsonPlayfield7k.Note_7k_N_1357.TH));

                    notesHeight = IsValidSize(_jsonPlayfield7k.Notes_H) ? _jsonPlayfield7k.Notes_H : _jsonPlayfield7k.Note_7k_N_1357.TH;
                    _texNote_7k_1357.Height = notesHeight;

                    textBox_notesHeight.Text = notesHeight.ToString();

                    _playfieldVerdictLine.Height = notesHeight;
                }

                {
                    _texNote_7k_26.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Note_7k_N_26.U, _jsonPlayfield7k.Note_7k_N_26.V, _jsonPlayfield7k.Note_7k_N_26.TW, _jsonPlayfield7k.Note_7k_N_26.TH));
                    _texNote_7k_26.Height = notesHeight;
                }

                {
                    _texNote_7k_4.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Note_7k_N_4.U, _jsonPlayfield7k.Note_7k_N_4.V, _jsonPlayfield7k.Note_7k_N_4.TW, _jsonPlayfield7k.Note_7k_N_4.TH));
                    _texNote_7k_4.Height = notesHeight;
                }

                {
                    _texLN_7k_1357.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Note_7k_L_1357.U, _jsonPlayfield7k.Note_7k_L_1357.V, _jsonPlayfield7k.Note_7k_L_1357.TW, _jsonPlayfield7k.Note_7k_L_1357.TH));
                    _texLN_7k_1357.Height = 80.0;
                }

                {
                    _texLN_7k_26.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Note_7k_L_26.U, _jsonPlayfield7k.Note_7k_L_26.V, _jsonPlayfield7k.Note_7k_L_26.TW, _jsonPlayfield7k.Note_7k_L_26.TH));
                    _texLN_7k_26.Height = 80.0;
                }

                {
                    _texLN_7k_4.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Note_7k_L_4.U, _jsonPlayfield7k.Note_7k_L_4.V, _jsonPlayfield7k.Note_7k_L_4.TW, _jsonPlayfield7k.Note_7k_L_4.TH));
                    _texLN_7k_4.Height = 80.0;
                }

                /* Injections */

                ThrowIfNotValidTexSize(_jsonPlayfield7k.Injections_TW, _jsonPlayfield7k.Injections_TH);

                int injectionsWidth = IsValidSize(_jsonPlayfield7k.Injections_W) ? _jsonPlayfield7k.Injections_W : _jsonPlayfield7k.Injections_TW;
                int injectionsHeight = IsValidSize(_jsonPlayfield7k.Injections_H) ? _jsonPlayfield7k.Injections_H : _jsonPlayfield7k.Injections_TH;

                textBox_injectionsSW.Text = injectionsWidth.ToString();
                textBox_injectionsSH.Text = injectionsHeight.ToString();

                textBox_injectionsX.Text = _jsonPlayfield7k.Injection_Replay.X.ToString();
                textBox_injectionsY.Text = _jsonPlayfield7k.Injection_Replay.Y.ToString();

                // Load injections textures (only the ones needed)

                {
                    _texInjection_Replay.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Injection_Replay.U, _jsonPlayfield7k.Injection_Replay.V, _jsonPlayfield7k.Injections_TW, _jsonPlayfield7k.Injections_TH));
                    _texInjection_Replay.Width = injectionsWidth;
                    _texInjection_Replay.Height = injectionsHeight;

                    Canvas.SetLeft(_texInjection_Replay, _jsonPlayfield7k.Injection_Replay.X);
                    Canvas.SetTop(_texInjection_Replay, _jsonPlayfield7k.Injection_Replay.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Injection_AutoplayAndReplay, Properties.Resources.Injection_AutoplayAndReplay, _texInjection_Replay));
                }

                {
                    _texInjection_O2Hard.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Injection_O2Hard.U, _jsonPlayfield7k.Injection_O2Hard.V, _jsonPlayfield7k.Injections_TW, _jsonPlayfield7k.Injections_TH));
                    _texInjection_O2Hard.Width = injectionsWidth;
                    _texInjection_O2Hard.Height = injectionsHeight;

                    Canvas.SetLeft(_texInjection_O2Hard, _jsonPlayfield7k.Injection_O2Hard.X);
                    Canvas.SetTop(_texInjection_O2Hard, _jsonPlayfield7k.Injection_O2Hard.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Injection_Gauges, Properties.Resources.Injection_Gauges, _texInjection_O2Hard));
                }

                {
                    _texInjection_Mirror.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Injection_Mirror.U, _jsonPlayfield7k.Injection_Mirror.V, _jsonPlayfield7k.Injections_TW, _jsonPlayfield7k.Injections_TH));
                    _texInjection_Mirror.Width = injectionsWidth;
                    _texInjection_Mirror.Height = injectionsHeight;

                    Canvas.SetLeft(_texInjection_Mirror, _jsonPlayfield7k.Injection_Mirror.X);
                    Canvas.SetTop(_texInjection_Mirror, _jsonPlayfield7k.Injection_Mirror.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Injection_PlacementModifiers, Properties.Resources.Injection_PlacementModifiers, _texInjection_Mirror));
                }

                {
                    _texInjection_AllLN.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Injection_AllLN.U, _jsonPlayfield7k.Injection_AllLN.V, _jsonPlayfield7k.Injections_TW, _jsonPlayfield7k.Injections_TH));
                    _texInjection_AllLN.Width = injectionsWidth;
                    _texInjection_AllLN.Height = injectionsHeight;

                    Canvas.SetLeft(_texInjection_AllLN, _jsonPlayfield7k.Injection_AllLN.X);
                    Canvas.SetTop(_texInjection_AllLN, _jsonPlayfield7k.Injection_AllLN.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Injection_AllLN, Properties.Resources.Injection_AllLN, _texInjection_AllLN));
                }

                {
                    _texInjection_LR2NMJudge.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Injection_LR2NMJudge.U, _jsonPlayfield7k.Injection_LR2NMJudge.V, _jsonPlayfield7k.Injections_TW, _jsonPlayfield7k.Injections_TH));
                    _texInjection_LR2NMJudge.Width = injectionsWidth;
                    _texInjection_LR2NMJudge.Height = injectionsHeight;

                    Canvas.SetLeft(_texInjection_LR2NMJudge, _jsonPlayfield7k.Injection_LR2NMJudge.X);
                    Canvas.SetTop(_texInjection_LR2NMJudge, _jsonPlayfield7k.Injection_LR2NMJudge.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Injection_LR2NormalJudge, Properties.Resources.Injection_LR2NMJudge, _texInjection_LR2NMJudge));
                }

                {
                    _texInjection_NoSSC.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Injection_NoSSC.U, _jsonPlayfield7k.Injection_NoSSC.V, _jsonPlayfield7k.Injections_TW, _jsonPlayfield7k.Injections_TH));
                    _texInjection_NoSSC.Width = injectionsWidth;
                    _texInjection_NoSSC.Height = injectionsHeight;

                    Canvas.SetLeft(_texInjection_NoSSC, _jsonPlayfield7k.Injection_NoSSC.X);
                    Canvas.SetTop(_texInjection_NoSSC, _jsonPlayfield7k.Injection_NoSSC.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Injection_NoSSC, Properties.Resources.Injection_NoSSC, _texInjection_NoSSC));
                }

                /* HealthBar */

                checkBox_healthBarFillIsHorizontal.IsChecked = _jsonPlayfield7k.HealthBarFillIsHorizontal;

                // HealthBar background

                if (IsValidSize(_jsonPlayfield7k.HealthBarBackground.TW, _jsonPlayfield7k.HealthBarBackground.TH))
                {
                    _texHealthBarBG = new Image();
                    _texHealthBarBG.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.HealthBarBackground.U, _jsonPlayfield7k.HealthBarBackground.V, _jsonPlayfield7k.HealthBarBackground.TW, _jsonPlayfield7k.HealthBarBackground.TH));
                    _texHealthBarBG.Width = IsValidSize(_jsonPlayfield7k.HealthBarBackground.W) ? _jsonPlayfield7k.HealthBarBackground.W : _jsonPlayfield7k.HealthBarBackground.TW;
                    _texHealthBarBG.Height = IsValidSize(_jsonPlayfield7k.HealthBarBackground.H) ? _jsonPlayfield7k.HealthBarBackground.H : _jsonPlayfield7k.HealthBarBackground.TH;

                    Canvas.SetLeft(_texHealthBarBG, _jsonPlayfield7k.HealthBarBackground.X);
                    Canvas.SetTop(_texHealthBarBG, _jsonPlayfield7k.HealthBarBackground.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.HealthBarBackground, Properties.Resources.HealthBarBackground, _texHealthBarBG));

                    textBox_healthBarBackgroundX.Text = _jsonPlayfield7k.HealthBarBackground.X.ToString();
                    textBox_healthBarBackgroundY.Text = _jsonPlayfield7k.HealthBarBackground.Y.ToString();
                    textBox_healthBarBackgroundSW.Text = _texHealthBarBG.Width.ToString();
                    textBox_healthBarBackgroundSH.Text = _texHealthBarBG.Height.ToString();
                }

                // HealthBar body

                ThrowIfNotValidTexSize(_jsonPlayfield7k.HealthBar.TW, _jsonPlayfield7k.HealthBar.TH);

                {
                    _texHealthBar.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.HealthBar.U, _jsonPlayfield7k.HealthBar.V, _jsonPlayfield7k.HealthBar.TW, _jsonPlayfield7k.HealthBar.TH));
                    _texHealthBar.Width = IsValidSize(_jsonPlayfield7k.HealthBar.W) ? _jsonPlayfield7k.HealthBar.W : _jsonPlayfield7k.HealthBar.TW;
                    _texHealthBar.Height = IsValidSize(_jsonPlayfield7k.HealthBar.H) ? _jsonPlayfield7k.HealthBar.H : _jsonPlayfield7k.HealthBar.TH;

                    Canvas.SetLeft(_texHealthBar, _jsonPlayfield7k.HealthBar.X);
                    Canvas.SetTop(_texHealthBar, _jsonPlayfield7k.HealthBar.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.HealthBar, Properties.Resources.HealthBar, _texHealthBar));

                    textBox_healthBarX.Text = _jsonPlayfield7k.HealthBar.X.ToString();
                    textBox_healthBarY.Text = _jsonPlayfield7k.HealthBar.Y.ToString();
                    textBox_healthBarSW.Text = _texHealthBar.Width.ToString();
                    textBox_healthBarSH.Text = _texHealthBar.Height.ToString();
                }

                /* Ranks */

                ThrowIfNotValidTexSize(_jsonPlayfield7k.Ranks_TW, _jsonPlayfield7k.Ranks_TH);

                // Load the rank S texture (only that one is needed)

                {
                    _texRank_S.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Rank_S.U, _jsonPlayfield7k.Rank_S.V, _jsonPlayfield7k.Ranks_TW, _jsonPlayfield7k.Ranks_TH));
                    _texRank_S.Width = IsValidSize(_jsonPlayfield7k.Ranks_W) ? _jsonPlayfield7k.Ranks_W : _jsonPlayfield7k.Ranks_TW;
                    _texRank_S.Height = IsValidSize(_jsonPlayfield7k.Ranks_H) ? _jsonPlayfield7k.Ranks_H : _jsonPlayfield7k.Ranks_TH;

                    Canvas.SetLeft(_texRank_S, _jsonPlayfield7k.Ranks_X);
                    Canvas.SetTop(_texRank_S, _jsonPlayfield7k.Ranks_Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.RankS, Properties.Resources.Rank_S, _texRank_S));

                    textBox_rankX.Text = _jsonPlayfield7k.Ranks_X.ToString();
                    textBox_rankY.Text = _jsonPlayfield7k.Ranks_Y.ToString();
                    textBox_rankSW.Text = _texRank_S.Width.ToString();
                    textBox_rankSH.Text = _texRank_S.Height.ToString();
                }

                /* Verdicts */

                // Load the COOL texture (only that one is needed)

                {
                    _texVerdictCOOL.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(_jsonPlayfield7k.Verdict_COOL.U, _jsonPlayfield7k.Verdict_COOL.V, _jsonPlayfield7k.Verdict_COOL.TW, _jsonPlayfield7k.Verdict_COOL.TH));
                    _texVerdictCOOL.Width = _jsonPlayfield7k.Verdict_COOL.TW * _jsonPlayfield7k.Verdicts_ScaleW;
                    _texVerdictCOOL.Height = IsValidSize(_jsonPlayfield7k.Verdicts_H) ? _jsonPlayfield7k.Verdicts_H : _jsonPlayfield7k.Verdict_COOL.TH;

                    Canvas.SetLeft(_texVerdictCOOL, _jsonPlayfield7k.Verdicts_X);
                    Canvas.SetTop(_texVerdictCOOL, _jsonPlayfield7k.Verdicts_Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.VerdictCOOL, Properties.Resources.Verdict_COOL, _texVerdictCOOL));

                    // Fill the controls

                    checkBox_judgementsClassicPosition.IsChecked = _jsonPlayfield7k.Verdicts_ClassicPosition;

                    textBox_verdicts_X.Text = _jsonPlayfield7k.Verdicts_X.ToString();
                    textBox_verdicts_Y.Text = _jsonPlayfield7k.Verdicts_Y.ToString();
                    textBox_verdicts_ScaleW.Text = _jsonPlayfield7k.Verdicts_ScaleW.ToString();
                    textBox_verdicts_H.Text = _texVerdictCOOL.Height.ToString();
                }

                /* Placards */

                if (IsValidSize(_jsonPlayfield7k.Placard_Health.TW, _jsonPlayfield7k.Placard_Health.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_Health;
                    _texPlacard_Health = new Image();
                    Image image = _texPlacard_Health;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_Health, Properties.Resources.Placard_Health, image));

                    textBox_placard_X.Text = layout.X.ToString();
                    textBox_placard_Y.Text = layout.Y.ToString();
                    textBox_placard_W.Text = image.Width.ToString();
                    textBox_placard_H.Text = image.Height.ToString();
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_BPM.TW, _jsonPlayfield7k.Placard_BPM.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_BPM;
                    _texPlacard_BPM = new Image();
                    Image image = _texPlacard_BPM;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_BPM, Properties.Resources.Placard_BPM, image));
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_ScrollSpeed.TW, _jsonPlayfield7k.Placard_ScrollSpeed.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_ScrollSpeed;
                    _texPlacard_ScrollSpeed = new Image();
                    Image image = _texPlacard_ScrollSpeed;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_ScrollSpeed, Properties.Resources.Placard_ScrollSpeed, image));
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_Offset.TW, _jsonPlayfield7k.Placard_Offset.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_Offset;
                    _texPlacard_Offset = new Image();
                    Image image = _texPlacard_Offset;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_Offset, Properties.Resources.Placard_Offset, image));
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_MaxCombo.TW, _jsonPlayfield7k.Placard_MaxCombo.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_MaxCombo;
                    _texPlacard_MaxCombo = new Image();
                    Image image = _texPlacard_MaxCombo;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_MaxCombo, Properties.Resources.Placard_MaxCombo, image));
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_Accuracy.TW, _jsonPlayfield7k.Placard_Accuracy.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_Accuracy;
                    _texPlacard_Accuracy = new Image();
                    Image image = _texPlacard_Accuracy;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_Accuracy, Properties.Resources.Placard_Accuracy, image));
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_Time.TW, _jsonPlayfield7k.Placard_Time.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_Time;
                    _texPlacard_Time = new Image();
                    Image image = _texPlacard_Time;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_Time, Properties.Resources.Placard_Time, image));
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_COOL.TW, _jsonPlayfield7k.Placard_COOL.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_COOL;
                    _texPlacard_COOL = new Image();
                    Image image = _texPlacard_COOL;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_COOL, Properties.Resources.Placard_COOL, image));
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_GOOD.TW, _jsonPlayfield7k.Placard_GOOD.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_GOOD;
                    _texPlacard_GOOD = new Image();
                    Image image = _texPlacard_GOOD;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_GOOD, Properties.Resources.Placard_GOOD, image));
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_OKAY.TW, _jsonPlayfield7k.Placard_OKAY.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_OKAY;
                    _texPlacard_OKAY = new Image();
                    Image image = _texPlacard_OKAY;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_OKAY, Properties.Resources.Placard_OKAY, image));
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_BAD.TW, _jsonPlayfield7k.Placard_BAD.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_BAD;
                    _texPlacard_BAD = new Image();
                    Image image = _texPlacard_BAD;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_BAD, Properties.Resources.Placard_BAD, image));
                }

                if (IsValidSize(_jsonPlayfield7k.Placard_MISS.TW, _jsonPlayfield7k.Placard_MISS.TH))
                {
                    Skin.FullLayout layout = _jsonPlayfield7k.Placard_MISS;
                    _texPlacard_MISS = new Image();
                    Image image = _texPlacard_MISS;
                    image.Source = new CroppedBitmap(_textureAtlasPlayfield7k, new Int32Rect(layout.U, layout.V, layout.TW, layout.TH));
                    image.Width = IsValidSize(layout.W) ? layout.W : layout.TW;
                    image.Height = IsValidSize(layout.H) ? layout.H : layout.TH;

                    Canvas.SetLeft(image, layout.X);
                    Canvas.SetTop(image, layout.Y);

                    _skinComponents.Add(new SkinComponent(ComponentIDs.Placard_MISS, Properties.Resources.Placard_MISS, image));
                }

                /* Numbers */

                // Load the textures (only the number 0)

                InitializeNumbers(_texNumbers_Health, _jsonPlayfield7k.Numbers_Health, ComponentIDs.Numbers_Health, Properties.Resources.Numbers_Health);
                InitializeNumbers(_texNumbers_BPM, _jsonPlayfield7k.Numbers_BPM, ComponentIDs.Numbers_BPM, Properties.Resources.Numbers_BPM);
                InitializeNumbers(_texNumbers_ScrollSpeed, _jsonPlayfield7k.Numbers_ScrollSpeed, ComponentIDs.Numbers_ScrollSpeed, Properties.Resources.Numbers_ScrollSpeed);
                InitializeNumbers(_texNumbers_Offset, _jsonPlayfield7k.Numbers_Offset, ComponentIDs.Numbers_Offset, Properties.Resources.Numbers_Offset);
                InitializeNumbers(_texNumbers_MaxCombo, _jsonPlayfield7k.Numbers_MaxCombo, ComponentIDs.Numbers_MaxCombo, Properties.Resources.Numbers_MaxCombo);
                InitializeNumbers(_texNumbers_Accuracy, _jsonPlayfield7k.Numbers_Accuracy, ComponentIDs.Numbers_Accuracy, Properties.Resources.Numbers_Accuracy);
                InitializeNumbers(_texNumbers_Time, _jsonPlayfield7k.Numbers_Time, ComponentIDs.Numbers_Time, Properties.Resources.Numbers_Time);
                InitializeNumbers(_texNumbers_COOL, _jsonPlayfield7k.Numbers_COOL, ComponentIDs.Numbers_ComboCOOL, Properties.Resources.Numbers_COOL);

                textBox_number_X.Text = Canvas.GetLeft(_texNumbers_Health).ToString();
                textBox_number_Y.Text = Canvas.GetTop(_texNumbers_Health).ToString();
                textBox_number_W.Text = _texNumbers_Health.Width.ToString();
                textBox_number_H.Text = _texNumbers_Health.Height.ToString();

                // Verdict counters
                {
                    CroppedBitmap texture = CreateTextureFromAtlas(_jsonPlayfield7k.Numbers_VerdictCounter);

                    _texNumbers_CounterCOOL.Source = texture;
                    InitializeCounterNumbers(_texNumbers_CounterCOOL, _jsonPlayfield7k.CounterPosition_COOL, ComponentIDs.Numbers_CounterCOOL, Properties.Resources.Numbers_CounterCOOL);

                    _texNumbers_CounterGOOD.Source = texture;
                    InitializeCounterNumbers(_texNumbers_CounterGOOD, _jsonPlayfield7k.CounterPosition_GOOD, ComponentIDs.Numbers_CounterGOOD, Properties.Resources.Numbers_CounterGOOD);

                    _texNumbers_CounterOKAY.Source = texture;
                    InitializeCounterNumbers(_texNumbers_CounterOKAY, _jsonPlayfield7k.CounterPosition_OKAY, ComponentIDs.Numbers_CounterOKAY, Properties.Resources.Numbers_CounterOKAY);

                    _texNumbers_CounterBAD.Source = texture;
                    InitializeCounterNumbers(_texNumbers_CounterBAD, _jsonPlayfield7k.CounterPosition_BAD, ComponentIDs.Numbers_CounterBAD, Properties.Resources.Numbers_CounterBAD);

                    _texNumbers_CounterMISS.Source = texture;
                    InitializeCounterNumbers(_texNumbers_CounterMISS, _jsonPlayfield7k.CounterPosition_MISS, ComponentIDs.Numbers_CounterMISS, Properties.Resources.Numbers_CounterMISS);
                }

                textBox_verdictCounterNumbers_W.Text = _texNumbers_CounterCOOL.Width.ToString();
                textBox_verdictCounterNumbers_H.Text = _texNumbers_CounterCOOL.Height.ToString();
                textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(_texNumbers_CounterCOOL).ToString();
                textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(_texNumbers_CounterCOOL).ToString();

                /* Arbitrary texture */

                if (IsValidTexSize(_jsonPlayfield7k.ArbitraryTexture))
                {
                    _isArbitraryTextureUsed = true;

                    InitializeImage(_texArbitraryTexture, _jsonPlayfield7k.ArbitraryTexture, ComponentIDs.ArbitraryTexture, Properties.Resources.ArbitraryTextures);

                    textBox_arbitraryTexture_X.Text = Canvas.GetLeft(_texArbitraryTexture).ToString();
                    textBox_arbitraryTexture_Y.Text = Canvas.GetTop(_texArbitraryTexture).ToString();
                    textBox_arbitraryTexture_W.Text = _texArbitraryTexture.Width.ToString();
                    textBox_arbitraryTexture_H.Text = _texArbitraryTexture.Height.ToString();
                }

                UpdatePlayfieldComponentsPosition();
                UpdateKeysLayout();
                UpdateNotesPositionAndWidth();

                listBox_skinComponents.ItemsSource = _skinComponents;
                MessageBox.Show(this, Properties.Resources.Notification_Success_LoadSkinData, Properties.Resources.Notification, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                _skinDataIsLoaded = false;
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_ResizeCanvas(object sender, RoutedEventArgs args)
        {
            try
            {
                int width = int.Parse(textBox_canvasWidth.Text);
                int height = int.Parse(textBox_canvasHeight.Text);

                canvas.Width = width;
                canvas.Height = height;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /*
        private void MenuItem_Click_UnloadSkinData(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }
        }
        */

        private void MenuItem_Click_PreviewSkin(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            DrawingVisual dv = new();
            using (DrawingContext dc = dv.RenderOpen())
            {
                dc.DrawRectangle(new VisualBrush(canvas), null, new Rect(canvas.RenderSize));
            }

            RenderTargetBitmap rtb = new((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96.0, 96.0, PixelFormats.Pbgra32);
            rtb.Render(dv);

            Image image = new();
            image.Source = rtb;
            image.Stretch = Stretch.None;

            Preview window = new(image);
            window.Owner = this;
            window.Show();
        }

        private void MenuItem_Click_GeneratePreview(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            SaveFileDialog dlg = new();
            dlg.Filter = "PNG Files|*.png";
            dlg.DefaultExt = ".png";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (dlg.ShowDialog(this) == false) { return; }

            DrawingVisual dv = new();
            using (DrawingContext dc = dv.RenderOpen())
            {
                dc.DrawRectangle(new VisualBrush(canvas), null, new Rect(canvas.RenderSize));
            }

            RenderTargetBitmap rtb = new((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96.0, 96.0, PixelFormats.Pbgra32);
            rtb.Render(dv);

            PngBitmapEncoder encoder = new();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (FileStream fs = File.Create(dlg.FileName))
            {
                encoder.Save(fs);
            }

            MessageBox.Show(this, Properties.Resources.Notification_Success_TaskCompleted, Properties.Resources.Notification, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_ArtworkApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            try
            {
                Image? image = _skinComponents.Find(x => x.Id == ComponentIDs.AccompanyingArtwork).Texture;
                image.Width = int.Parse(textBox_accompanyingArtwork_W.Text);
                image.Height = int.Parse(textBox_accompanyingArtwork_H.Text);

                Canvas.SetLeft(image, int.Parse(textBox_accompanyingArtwork_X.Text));
                Canvas.SetTop(image, int.Parse(textBox_accompanyingArtwork_Y.Text));
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_CompanionApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            try
            {
                int x = int.Parse(textBox_companionX.Text);
                int y = int.Parse(textBox_companionY.Text);
                int w = int.Parse(textBox_companionSW.Text);
                int h = int.Parse(textBox_companionSH.Text);

                Image? image = _skinComponents.Find(x => x.Id == ComponentIDs.Companion).Texture;
                image.Width = w;
                image.Height = h;
                Canvas.SetLeft(image, x);
                Canvas.SetTop(image, y);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_PlayfieldApplyChanges(object sender, RoutedEventArgs args)
        {
            UpdatePlayfieldHeight(int.Parse(textBox_playfieldHeight.Text));

            int width1357;
            if (!int.TryParse(textBox_playfield1357ColumnsWidth.Text, out width1357)) { return; }

            int width246;
            if (!int.TryParse(textBox_playfield246ColumnsWidth.Text, out width246)) { return; }

            int separatorsWidth;
            if (!int.TryParse(textBox_playfieldColumnSeparatorsWidth.Text, out separatorsWidth)) { return; }

            _playfieldColumn1.Width = width1357;
            _playfieldColumn3.Width = width1357;
            _playfieldColumn5.Width = width1357;
            _playfieldColumn7.Width = width1357;

            _playfieldColumn2.Width = width246;
            _playfieldColumn4.Width = width246;
            _playfieldColumn6.Width = width246;

            _playfieldColumnSeparator1.Width = separatorsWidth;
            _playfieldColumnSeparator2.Width = separatorsWidth;
            _playfieldColumnSeparator3.Width = separatorsWidth;
            _playfieldColumnSeparator4.Width = separatorsWidth;
            _playfieldColumnSeparator5.Width = separatorsWidth;
            _playfieldColumnSeparator6.Width = separatorsWidth;

            double playfieldNewWidth = Calculate7kPlayfieldWidth(false);

            _playfieldVerdictLine.Width = playfieldNewWidth;
            _playfieldBorderBottom.Width = playfieldNewWidth + 2.0;

            UpdatePlayfieldComponentsPosition();

            UpdateKeysLayout();
            UpdateKeysWidth(width1357, width246);

            UpdateNotesPositionAndWidth();
        }

        private void Button_Click_ApplyColor(object sender, RoutedEventArgs args)
        {
            try
            {
                int red = int.Parse(textBox_red.Text);
                int green = int.Parse(textBox_green.Text);
                int blue = int.Parse(textBox_blue.Text);
                int opacity = int.Parse(textBox_opacity.Text);

                Color color = new();
                color.R = (byte)Math.Clamp(red, 0, 255);
                color.G = (byte)Math.Clamp(green, 0, 255);
                color.B = (byte)Math.Clamp(blue, 0, 255);
                color.A = (byte)Math.Clamp(opacity, 0, 255);

                SolidColorBrush colorBrush = new(color);

                rectangle_color.Fill = colorBrush;

                switch (comboBox_coloring.SelectedIndex)
                {
                    case 0:
                        {
                            _playfieldColumn1.Fill = colorBrush;
                            _playfieldColumn3.Fill = colorBrush;
                            _playfieldColumn5.Fill = colorBrush;
                            _playfieldColumn7.Fill = colorBrush;
                        }
                        break;
                    case 1:
                        {
                            _playfieldColumn2.Fill = colorBrush;
                            _playfieldColumn4.Fill = colorBrush;
                            _playfieldColumn6.Fill = colorBrush;
                        }
                        break;
                    case 2:
                        {
                            _playfieldColumnSeparator1.Fill = colorBrush;
                            _playfieldColumnSeparator2.Fill = colorBrush;
                            _playfieldColumnSeparator3.Fill = colorBrush;
                            _playfieldColumnSeparator4.Fill = colorBrush;
                            _playfieldColumnSeparator5.Fill = colorBrush;
                            _playfieldColumnSeparator6.Fill = colorBrush;
                        }
                        break;
                    case 3:
                        {
                            _playfieldVerdictLine.Fill = colorBrush;
                        }
                        break;
                    case 4:
                        {
                            _playfieldBorderLeft.Fill = colorBrush;
                            _playfieldBorderRight.Fill = colorBrush;
                            _playfieldBorderBottom.Fill = colorBrush;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_AddWallpaper(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(textBox_wallpaper.Text)) { return; }

            _wallpapers.Add(textBox_wallpaper.Text);
        }

        private void Button_Click_RemoveWallpaper(object sender, RoutedEventArgs args)
        {
            if (listBox_wallpapers.SelectedIndex == -1) { return; }

            _wallpapers.RemoveAt(listBox_wallpapers.SelectedIndex);
        }

        private void Button_Click_BombsVisualize(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            string fileLocation = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "\\LluviaAppSpace\\Bombs\\", textBox_bombName.Text);
            if (!File.Exists(fileLocation)) { return; }

            try
            {
                int frameQuantity = int.Parse(textBox_bombFrameQuantity.Text);
                int frameWidth = int.Parse(textBox_bombFrameWidth.Text);
                int frameHeight = int.Parse(textBox_bombFrameHeight.Text);
                int duration = int.Parse(textBox_bombDuration.Text);
                int width = int.Parse(textBox_bombSW.Text);
                int height = int.Parse(textBox_bombSH.Text);

                Bomb bombInfo = new();
                bombInfo.frameQuantity = frameQuantity;
                bombInfo.frameWidth = frameWidth;
                bombInfo.frameHeight = frameHeight;
                bombInfo.duration = duration;
                bombInfo.width = width;
                bombInfo.height = height;

                BitmapImage bombSheet = new(new Uri(fileLocation, UriKind.Absolute));

                BombPreview bombPreview_Window = new(bombSheet, bombInfo);
                bombPreview_Window.Owner = this;
                bombPreview_Window.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_LasersVisualize(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            if (_laserAnimations.Count == 0) { return; }

            int laserHeight = 0;
            if (!int.TryParse(textBox_laserSH.Text, out laserHeight)) { return; }

            int animeDuration = 0;
            if (!int.TryParse(textBox_laserDuration.Text, out animeDuration)) { return; }
            if (animeDuration == 0) { return; }

            LasersPreview window = new(_texLaser_7k_1357, _laserAnimations, laserHeight, animeDuration);
            window.Owner = this;
            window.Show();
        }

        private void Button_Click_AddLaserAnime(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            try
            {
                int time = int.Parse(textBox_laserTime.Text);
                float widthFactor = float.Parse(textBox_laserWidthFactor.Text);
                float heightFactor = float.Parse(textBox_laserHeightFactor.Text);
                float opacityFactor = float.Parse(textBox_laserOpacityFactor.Text);

                _laserAnimations.Add(new LaserAnime(time, widthFactor, heightFactor, opacityFactor));
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_ModifyLaserAnime(object sender, RoutedEventArgs args)
        {
            if (listBox_laserAnimations.SelectedIndex == -1) { return; }

            try
            {
                int time = int.Parse(textBox_laserTime.Text);
                float widthFactor = float.Parse(textBox_laserWidthFactor.Text);
                float heightFactor = float.Parse(textBox_laserHeightFactor.Text);
                float opacityFactor = float.Parse(textBox_laserOpacityFactor.Text);

                LaserAnime animation = (LaserAnime)listBox_laserAnimations.SelectedItem;
                animation.Time = time;
                animation.WidthFactor = widthFactor;
                animation.HeightFactor = heightFactor;
                animation.OpacityFactor = opacityFactor;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click_RemoveLaserAnime(object sender, RoutedEventArgs args)
        {
            if (listBox_laserAnimations.SelectedIndex == -1) { return; }

            _laserAnimations.RemoveAt(listBox_laserAnimations.SelectedIndex);
        }

        private void Button_Click_KeysApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            int margin1357;
            if (!int.TryParse(textBox_keys1357margin.Text, out margin1357)) { return; }
            int height1357;
            if (!int.TryParse(textBox_keys1357SH.Text, out height1357)) { return; }
            int margin246;
            if (!int.TryParse(textBox_keys246margin.Text, out margin246)) { return; }
            int height246;
            if (!int.TryParse(textBox_keys246SH.Text, out height246)) { return; }

            _texKey_7k_1357_Sprite1.Height = height1357;
            _texKey_7k_1357_Sprite2.Height = height1357;
            _texKey_7k_1357_Sprite3.Height = height1357;
            _texKey_7k_1357_Sprite4.Height = height1357;

            _texKey_7k_26_Sprite1.Height = height246;
            _texKey_7k_26_Sprite2.Height = height246;

            _texKey_7k_4_Sprite1.Height = height246;

            Canvas.SetTop(_texKey_7k_1357_Sprite1, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin1357);
            Canvas.SetTop(_texKey_7k_1357_Sprite2, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin1357);
            Canvas.SetTop(_texKey_7k_1357_Sprite3, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin1357);
            Canvas.SetTop(_texKey_7k_1357_Sprite4, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin1357);

            Canvas.SetTop(_texKey_7k_26_Sprite1, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin246);
            Canvas.SetTop(_texKey_7k_26_Sprite2, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin246);

            Canvas.SetTop(_texKey_7k_4_Sprite1, Canvas.GetTop(_playfieldBorderBottom) + 1.0 + margin246);
        }

        private void Button_Click_NotesApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            int height;
            if (!int.TryParse(textBox_notesHeight.Text, out height)) { return; }

            _texNote_7k_1357.Height = height;
            _texNote_7k_26.Height = height;
            _texNote_7k_4.Height = height;

            Canvas.SetTop(_texNote_7k_1357, Canvas.GetTop(_playfieldBorderBottom) - (height * 2.0));
            Canvas.SetTop(_texNote_7k_26, Canvas.GetTop(_playfieldBorderBottom) - (height * 2.0));
            Canvas.SetTop(_texNote_7k_4, Canvas.GetTop(_playfieldBorderBottom) - (height * 2.0));

            Canvas.SetTop(_texLN_7k_1357, Canvas.GetTop(_playfieldBorderBottom) - (height * 4.0 + 80.0));
            Canvas.SetTop(_texLN_7k_26, Canvas.GetTop(_playfieldBorderBottom) - (height * 4.0 + 80.0));
            Canvas.SetTop(_texLN_7k_4, Canvas.GetTop(_playfieldBorderBottom) - (height * 4.0 + 80.0));

            _playfieldVerdictLine.Height = height;
            Canvas.SetTop(_playfieldVerdictLine, _playfieldColumn1.Height - height); // playfieldColumn1.Height == playfield's height
        }

        private void Button_Click_InjectionsApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            int x;
            if (!int.TryParse(textBox_injectionsX.Text, out x)) { return; }

            int y;
            if (!int.TryParse(textBox_injectionsY.Text, out y)) { return; }

            Image image0 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_AutoplayAndReplay).Texture;
            Image image1 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_Gauges).Texture;
            Image image2 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_PlacementModifiers).Texture;
            Image image3 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_AllLN).Texture;
            Image image4 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_LR2NormalJudge).Texture;
            Image image5 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_NoSSC).Texture;

            switch (comboBox_injections.SelectedIndex)
            {
                case 0:
                    {
                        Canvas.SetLeft(image0, x);
                        Canvas.SetTop(image0, y);
                    }
                    break;

                case 1:
                    {
                        Canvas.SetLeft(image1, x);
                        Canvas.SetTop(image1, y);
                    }
                    break;

                case 2:
                    {
                        Canvas.SetLeft(image2, x);
                        Canvas.SetTop(image2, y);
                    }
                    break;

                case 3:
                    {
                        Canvas.SetLeft(image3, x);
                        Canvas.SetTop(image3, y);
                    }
                    break;

                case 4:
                    {
                        Canvas.SetLeft(image4, x);
                        Canvas.SetTop(image4, y);
                    }
                    break;

                case 5:
                    {
                        Canvas.SetLeft(image5, x);
                        Canvas.SetTop(image5, y);
                    }
                    break;
            }
        }

        private void Button_Click_HealthBarApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            // Health bar background

            int bgX;
            if (!int.TryParse(textBox_healthBarBackgroundX.Text, out bgX)) { return; }

            int bgY;
            if (!int.TryParse(textBox_healthBarBackgroundY.Text, out bgY)) { return; }

            int bgW;
            if (!int.TryParse(textBox_healthBarBackgroundSW.Text, out bgW)) { return; }

            int bgH;
            if (!int.TryParse(textBox_healthBarBackgroundSH.Text, out bgH)) { return; }

            // Health bar body

            int x;
            if (!int.TryParse(textBox_healthBarX.Text, out x)) { return; }

            int y;
            if (!int.TryParse(textBox_healthBarY.Text, out y)) { return; }

            int w;
            if (!int.TryParse(textBox_healthBarSW.Text, out w)) { return; }

            int h;
            if (!int.TryParse(textBox_healthBarSH.Text, out h)) { return; }

            Image image = _skinComponents.Find(x => x.Id == ComponentIDs.HealthBarBackground).Texture;
            image.Width = bgW;
            image.Height = bgH;
            Canvas.SetLeft(image, bgX);
            Canvas.SetTop(image, bgY);

            Image image2 = _skinComponents.Find(x => x.Id == ComponentIDs.HealthBar).Texture;
            image2.Width = w;
            image2.Height = h;
            Canvas.SetLeft(image2, x);
            Canvas.SetTop(image2, y);
        }

        private void Button_Click_RanksApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            int x;
            if (!int.TryParse(textBox_rankX.Text, out x)) { return; }

            int y;
            if (!int.TryParse(textBox_rankY.Text, out y)) { return; }

            int w;
            if (!int.TryParse(textBox_rankSW.Text, out w)) { return; }

            int h;
            if (!int.TryParse(textBox_rankSH.Text, out h)) { return; }

            Image image = _skinComponents.Find(x => x.Id == ComponentIDs.RankS).Texture;
            image.Width = w;
            image.Height = h;
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
        }

        private void Button_Click_VerdictsApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            int x;
            if (!int.TryParse(textBox_verdicts_X.Text, out x)) { return; }

            int y;
            if (!int.TryParse(textBox_verdicts_Y.Text, out y)) { return; }

            float scaleW;
            if (!float.TryParse(textBox_verdicts_ScaleW.Text, out scaleW)) { return; }

            int h;
            if (!int.TryParse(textBox_verdicts_H.Text, out h)) { return; }

            Image image = _skinComponents.Find(x => x.Id == ComponentIDs.VerdictCOOL).Texture;
            image.Width = _verdictCOOLTextureWidth * scaleW;
            image.Height = h;
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
        }

        private void Button_Click_PlacardsApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            int x;
            if (!int.TryParse(textBox_placard_X.Text, out x)) { return; }

            int y;
            if (!int.TryParse(textBox_placard_Y.Text, out y)) { return; }

            int w;
            if (!int.TryParse(textBox_placard_W.Text, out w)) { return; }

            int h;
            if (!int.TryParse(textBox_placard_H.Text, out h)) { return; }

            switch (comboBox_placards.SelectedIndex)
            {
                case 0:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_Health);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 1:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_BPM);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 2:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_ScrollSpeed);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 3:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_Offset);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 4:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_MaxCombo);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 5:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_Time);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 6:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_Accuracy);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 7:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_COOL);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 8:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_GOOD);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 9:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_OKAY);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 10:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_BAD);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;

                case 11:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_MISS);
                        if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
                    }
                    break;
            }
        }

        private void Button_Click_NumbersApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            int x;
            if (!int.TryParse(textBox_number_X.Text, out x)) { return; }

            int y;
            if (!int.TryParse(textBox_number_Y.Text, out y)) { return; }

            int w;
            if (!int.TryParse(textBox_number_W.Text, out w)) { return; }

            int h;
            if (!int.TryParse(textBox_number_H.Text, out h)) { return; }

            switch (comboBox_numbers.SelectedIndex)
            {
                case 0:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_Health).Texture;
                        SetImagePositionAndSize(image, x, y, w, h);
                    }
                    break;

                case 1:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_BPM).Texture;
                        SetImagePositionAndSize(image, x, y, w, h);
                    }
                    break;

                case 2:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_ScrollSpeed).Texture;
                        SetImagePositionAndSize(image, x, y, w, h);
                    }
                    break;

                case 3:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_Offset).Texture;
                        SetImagePositionAndSize(image, x, y, w, h);
                    }
                    break;

                case 4:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_MaxCombo).Texture;
                        SetImagePositionAndSize(image, x, y, w, h);
                    }
                    break;

                case 5:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_Time).Texture;
                        SetImagePositionAndSize(image, x, y, w, h);
                    }
                    break;

                case 6:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_Accuracy).Texture;
                        SetImagePositionAndSize(image, x, y, w, h);
                    }
                    break;

                case 7:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_ComboCOOL).Texture;
                        SetImagePositionAndSize(image, x, y, w, h);
                    }
                    break;
            }
        }

        private void Button_Click_Numbers2ApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            int x;
            if (!int.TryParse(textBox_verdictCounterNumber_X.Text, out x)) { return; }

            int y;
            if (!int.TryParse(textBox_verdictCounterNumber_Y.Text, out y)) { return; }

            Image image0 = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterCOOL).Texture;

            Image image1 = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterGOOD).Texture;

            Image image2 = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterOKAY).Texture;

            Image image3 = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterBAD).Texture;

            Image image4 = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterMISS).Texture;

            switch (comboBox_verdictCounterNumbers.SelectedIndex)
            {
                case 0:
                    {
                        Canvas.SetLeft(image0, x);
                        Canvas.SetTop(image0, y);
                    }
                    break;

                case 1:
                    {
                        Canvas.SetLeft(image1, x);
                        Canvas.SetTop(image1, y);
                    }
                    break;

                case 2:
                    {
                        Canvas.SetLeft(image2, x);
                        Canvas.SetTop(image2, y);
                    }
                    break;

                case 3:
                    {
                        Canvas.SetLeft(image3, x);
                        Canvas.SetTop(image3, y);
                    }
                    break;

                case 4:
                    {
                        Canvas.SetLeft(image4, x);
                        Canvas.SetTop(image4, y);
                    }
                    break;
            }
        }

        private void Button_Click_ArbitraryTexturesApplyChanges(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }
            if (!_isArbitraryTextureUsed) { return; }

            int x;
            if (!int.TryParse(textBox_arbitraryTexture_X.Text, out x)) { return; }

            int y;
            if (!int.TryParse(textBox_arbitraryTexture_Y.Text, out y)) { return; }

            int w;
            if (!int.TryParse(textBox_arbitraryTexture_W.Text, out w)) { return; }

            int h;
            if (!int.TryParse(textBox_arbitraryTexture_H.Text, out h)) { return; }

            SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.ArbitraryTexture);
            if (component != null) { SetImagePositionAndSize(component.Texture, x, y, w, h); }
        }

        private void Button_Click_InsertAllInCanvas(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            foreach (SkinComponent component in _skinComponents)
            {
                if (!canvas.Children.Contains(component.Texture))
                {
                    component.Texture.MouseLeftButtonDown += Image_MouseLeftButtonDown;
                    component.Texture.Stretch = Stretch.Fill;
                    canvas.Children.Add(component.Texture);
                    component.InCanvas = true;
                }
            }
        }

        private void Button_Click_ExtractAllFromCanvas(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            foreach (SkinComponent component in _skinComponents)
            {
                if (canvas.Children.Contains(component.Texture))
                {
                    canvas.Children.Remove(component.Texture);
                    component.Texture.MouseLeftButtonDown -= Image_MouseLeftButtonDown;
                    component.InCanvas = false;
                }
            }

            if (canvas.Children.Contains(_resizerBorder)) { canvas.Children.Remove(_resizerBorder); }
        }

        private void Button_Click_InsertInCanvas(object sender, RoutedEventArgs args)
        {
            try
            {
                if (listBox_skinComponents.SelectedIndex == -1) { return; }

                SkinComponent component = (SkinComponent)listBox_skinComponents.SelectedItem;
                if (!component.HasTexture || component.InCanvas)
                {
                    //TODO: Delete Properties.Resources.InvalidOp_AlreadyInCanvas
                    return;
                }

                component.Texture.MouseLeftButtonDown += Image_MouseLeftButtonDown;
                component.Texture.Stretch = Stretch.Fill;
                canvas.Children.Add(component.Texture);
                component.InCanvas = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_ExtractFromCanvas(object sender, RoutedEventArgs args)
        {
            try
            {
                if (listBox_skinComponents.SelectedIndex == -1) { return; }

                SkinComponent selectedComponent = (SkinComponent)listBox_skinComponents.SelectedItem;
                if (selectedComponent.InCanvas)
                {
                    foreach (UIElement elem in canvas.Children)
                    {
                        Image? image = elem as Image;
                        if (image != null && object.ReferenceEquals(selectedComponent.Texture, image))
                        {
                            canvas.Children.Remove(image);
                            selectedComponent.InCanvas = false;
                            image.MouseLeftButtonDown -= Image_MouseLeftButtonDown;

                            if (object.ReferenceEquals(image, _clickedImage))
                            {
                                _clickedImage = null;
                                if (canvas.Children.Contains(_resizerBorder))
                                {
                                    canvas.Children.Remove(_resizerBorder);
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RadioButton_toolResizer_Unchecked(object sender, RoutedEventArgs args)
        {
            if (canvas.Children.Contains(_resizerBorder))
            {
                canvas.Children.Remove(_resizerBorder);
                _clickedImage = null;
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            try
            {
                if (radioButton_toolMover.IsChecked == true)
                {
                    _clickedImage = (Image)sender;
                    _startPosition = args.GetPosition(canvas);

                    double left = Canvas.GetLeft(_clickedImage);
                    double top = Canvas.GetTop(_clickedImage);
                    _clickedImage_StartPos.X = double.IsNaN(left) ? 0.0 : left;
                    _clickedImage_StartPos.Y = double.IsNaN(top) ? 0.0 : top;

                    _onMoveOperation = true;
                    canvas.CaptureMouse();

                    UpdatePasTextBlocks(Canvas.GetLeft(_clickedImage), Canvas.GetTop(_clickedImage), _clickedImage.Width, _clickedImage.Height);
                }
                else if (radioButton_toolResizer.IsChecked == true)
                {
                    // if ReferenceEquals returns true then there is nothing to do, because _clickedImage is null when the program starts.
                    if (!object.ReferenceEquals(sender, _clickedImage))
                    {
                        _clickedImage = (Image)sender;

                        if (!canvas.Children.Contains(_resizerBorder))
                        {
                            canvas.Children.Add(_resizerBorder);
                        }
                        Canvas.SetLeft(_resizerBorder, Canvas.GetLeft(_clickedImage) - 1.0);
                        Canvas.SetTop(_resizerBorder, Canvas.GetTop(_clickedImage) - 1.0);
                        _resizerBorder.Width = _clickedImage.Width + 2.0;
                        _resizerBorder.Height = _clickedImage.Height + 2.0;

                        UpdatePasTextBlocks(Canvas.GetLeft(_clickedImage), Canvas.GetTop(_clickedImage), _clickedImage.Width, _clickedImage.Height);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Resizer_leftHandle_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            _startPosition = args.GetPosition(canvas);
            _resizerBorder.SelectedHandle = UC.ResizerHandles.Left;
            _onResizeOperation = true;
            canvas.CaptureMouse();
        }

        private void Resizer_topHandle_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            _startPosition = args.GetPosition(canvas);
            _resizerBorder.SelectedHandle = UC.ResizerHandles.Top;
            _onResizeOperation = true;
            canvas.CaptureMouse();
        }

        private void Resizer_rightHandle_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            _startPosition = args.GetPosition(canvas);
            _resizerBorder.SelectedHandle = UC.ResizerHandles.Right;
            _onResizeOperation = true;
            canvas.CaptureMouse();
        }

        private void Resizer_bottomHandle_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            _startPosition = args.GetPosition(canvas);
            _resizerBorder.SelectedHandle = UC.ResizerHandles.Bottom;
            _onResizeOperation = true;
            canvas.CaptureMouse();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs args)
        {
            try
            {
                if (_onMoveOperation)
                {
                    Point mousePosition = args.GetPosition(canvas);

                    Canvas.SetLeft(_clickedImage, _clickedImage_StartPos.X + (mousePosition.X - _startPosition.X));
                    Canvas.SetTop(_clickedImage, _clickedImage_StartPos.Y + (mousePosition.Y - _startPosition.Y));

                    textBlock_componentX.Text = "X:" + Canvas.GetLeft(_clickedImage).ToString();
                    textBlock_componentY.Text = "Y:" + Canvas.GetTop(_clickedImage).ToString();
                }
                else if (_onResizeOperation)
                {
                    if (IsNotAllowedToResizeInCanvas(_clickedImage)) { return; }

                    switch (_resizerBorder.SelectedHandle)
                    {
                        case UC.ResizerHandles.Left:
                            {
                                // I do not want to erase this, it shows an evolution of problem comprehension...

                                /*
                                double originalLeft = Canvas.GetLeft(_clickedImage); // to use later

                                // calculate the new Canvas.Left position for the Image2
                                Point mousePosition = args.GetPosition(canvas);
                                Canvas.SetLeft(_clickedImage, _clickedImage.SkinComponent._x + Convert.ToInt32(mousePosition.X - _startPosition.X));

                                // prevent the image to move to the right
                                if (Canvas.GetLeft(_clickedImage) > Canvas.GetLeft(_resizerBorder) + _resizerBorder.ActualWidth - 6)
                                {
                                    Canvas.SetLeft(_clickedImage, originalLeft);
                                }

                                // calculate the new width
                                double movedLeft = Canvas.GetLeft(_clickedImage);
                                double newWidth = _clickedImage.ActualWidth + (originalLeft - movedLeft);
                                if (newWidth < 4)
                                {
                                    newWidth = 4;
                                }

                                _clickedImage.BeginInit();
                                _clickedImage.Source = new TransformedBitmap(_clickedImage.SkinComponent.Texture, new ScaleTransform(newWidth / _clickedImage.SkinComponent.Texture.PixelWidth, 1));
                                _clickedImage.EndInit();

                                // reposition the ResizerBorder so it surrounds the image
                                Canvas.SetLeft(_resizerBorder, movedLeft - 1);
                                Canvas.SetTop(_resizerBorder, Canvas.GetTop(_clickedImage) - 1);
                                _resizerBorder.Width = _clickedImage.ActualWidth + 2;
                                _resizerBorder.Height = _clickedImage.ActualHeight + 2;
                                */

                                /*
                                Point mousePosition = args.GetPosition(canvas);
                                double positionDifference = mousePosition.X - _startPosition.X;

                                _startPosition = mousePosition;

                                double newWidth = _clickedImage.ActualWidth + (positionDifference * -1);
                                if (newWidth < 4)
                                {
                                    newWidth = 4;
                                }

                                _clickedImage.BeginInit();
                                _clickedImage.Source = new TransformedBitmap(_clickedImage.SkinComponent.Texture, new ScaleTransform(newWidth / _clickedImage.SkinComponent.Texture.PixelWidth, 1));
                                _clickedImage.EndInit();

                                Canvas.SetLeft(_clickedImage, Canvas.GetLeft(_clickedImage) + positionDifference);

                                _resizerBorder.Width = _clickedImage.ActualWidth + 2;
                                _resizerBorder.Height = _clickedImage.ActualHeight + 2;
                                Canvas.SetLeft(_resizerBorder, Canvas.GetLeft(_clickedImage) - 1);
                                Canvas.SetTop(_resizerBorder, Canvas.GetTop(_clickedImage) - 1);
                                */

                                Point mousePosition = args.GetPosition(canvas);
                                double positionDifference = mousePosition.X - _startPosition.X;

                                _startPosition = mousePosition;

                                _resizerBorder.Width = (_resizerBorder.Width + positionDifference * -1.0) < 6.0 ? 6.0 : _resizerBorder.Width + positionDifference * -1.0;
                                Canvas.SetLeft(_resizerBorder, Canvas.GetLeft(_resizerBorder) + positionDifference);

                                _clickedImage.Width = _resizerBorder.Width - 2.0;
                                _clickedImage.Height = _resizerBorder.Height - 2.0;

                                Canvas.SetLeft(_clickedImage, Canvas.GetLeft(_resizerBorder) + 1.0);
                                Canvas.SetTop(_clickedImage, Canvas.GetTop(_resizerBorder) + 1.0);

                                textBlock_componentX.Text = "X:" + Canvas.GetLeft(_clickedImage).ToString();
                                textBlock_componentSW.Text = _clickedImage.Width.ToString();
                            }
                            break;
                        case UC.ResizerHandles.Top:
                            {
                                Point mousePosition = args.GetPosition(canvas);
                                double positionDifference = mousePosition.Y - _startPosition.Y;

                                _startPosition = mousePosition;

                                _resizerBorder.Height = (_resizerBorder.Height + positionDifference * -1.0) < 6.0 ? 6.0 : _resizerBorder.Height + positionDifference * -1.0;
                                Canvas.SetTop(_resizerBorder, Canvas.GetTop(_resizerBorder) + positionDifference);

                                _clickedImage.Width = _resizerBorder.Width - 2.0;
                                _clickedImage.Height = _resizerBorder.Height - 2.0;

                                Canvas.SetLeft(_clickedImage, Canvas.GetLeft(_resizerBorder) + 1.0);
                                Canvas.SetTop(_clickedImage, Canvas.GetTop(_resizerBorder) + 1.0);

                                textBlock_componentY.Text = "Y:" + Canvas.GetTop(_clickedImage).ToString();
                                textBlock_componentSH.Text = _clickedImage.Height.ToString();
                            }
                            break;
                        case UC.ResizerHandles.Right:
                            {
                                Point mousePosition = args.GetPosition(canvas);
                                double positionDifference = mousePosition.X - _startPosition.X;

                                _startPosition = mousePosition;

                                _resizerBorder.Width = (_resizerBorder.Width + positionDifference) < 6.0 ? 6.0 : _resizerBorder.Width + positionDifference;

                                _clickedImage.Width = _resizerBorder.Width - 2.0;
                                _clickedImage.Height = _resizerBorder.Height - 2.0;

                                Canvas.SetLeft(_clickedImage, Canvas.GetLeft(_resizerBorder) + 1.0);
                                Canvas.SetTop(_clickedImage, Canvas.GetTop(_resizerBorder) + 1.0);

                                textBlock_componentSW.Text = _clickedImage.Width.ToString();
                            }
                            break;
                        case UC.ResizerHandles.Bottom:
                            {
                                Point mousePosition = args.GetPosition(canvas);
                                double positionDifference = mousePosition.Y - _startPosition.Y;

                                _startPosition = mousePosition;

                                _resizerBorder.Height = (_resizerBorder.Height + positionDifference) < 6.0 ? 6.0 : _resizerBorder.Height + positionDifference;

                                _clickedImage.Width = _resizerBorder.Width - 2.0;
                                _clickedImage.Height = _resizerBorder.Height - 2.0;

                                Canvas.SetLeft(_clickedImage, Canvas.GetLeft(_resizerBorder) + 1.0);
                                Canvas.SetTop(_clickedImage, Canvas.GetTop(_resizerBorder) + 1.0);

                                textBlock_componentSH.Text = _clickedImage.Height.ToString();
                            }
                            break;
                        case UC.ResizerHandles.None:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs args)
        {
            try
            {
                if (_onMoveOperation)
                {
                    UpdateUIBasedOnImage(_clickedImage);

                    _clickedImage = null;
                    _onMoveOperation = false;
                    canvas.ReleaseMouseCapture();
                }
                else if (_onResizeOperation)
                {
                    UpdateUIBasedOnImage(_clickedImage);

                    _onResizeOperation = false;
                    canvas.ReleaseMouseCapture();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckBox_DrawPlayfield_Checked(object sender, RoutedEventArgs args)
        {
            try
            {
                DrawPlayfield();
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckBox_DrawPlayfield_Unchecked(object sender, RoutedEventArgs args)
        {
            try
            {
                UndrawPlayfield();
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckBox_DrawKeys_Checked(object sender, RoutedEventArgs e)
        {
            if (!_skinDataIsLoaded) { return; }

            DrawKeys();
        }

        private void CheckBox_DrawKeys_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!_skinDataIsLoaded) { return; }

            UndrawKeys();
        }

        private void CheckBox_DrawNotes_Checked(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            canvas.Children.Add(_texNote_7k_1357);
            canvas.Children.Add(_texNote_7k_26);
            canvas.Children.Add(_texNote_7k_4);
            canvas.Children.Add(_texLN_7k_1357);
            canvas.Children.Add(_texLN_7k_26);
            canvas.Children.Add(_texLN_7k_4);
        }

        private void CheckBox_DrawNotes_Unchecked(object sender, RoutedEventArgs args)
        {
            if (!_skinDataIsLoaded) { return; }

            canvas.Children.Remove(_texNote_7k_1357);
            canvas.Children.Remove(_texNote_7k_26);
            canvas.Children.Remove(_texNote_7k_4);
            canvas.Children.Remove(_texLN_7k_1357);
            canvas.Children.Remove(_texLN_7k_26);
            canvas.Children.Remove(_texLN_7k_4);
        }


        private void ListBox_SelectionChanged_LaserAnimations(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_laserAnimations.SelectedIndex == -1) { return; }

            LaserAnime laserAnimation = (LaserAnime)listBox_laserAnimations.SelectedItem;

            textBox_laserTime.Text = laserAnimation.Time.ToString();
            textBox_laserWidthFactor.Text = laserAnimation.WidthFactor.ToString();
            textBox_laserHeightFactor.Text = laserAnimation.HeightFactor.ToString();
            textBox_laserOpacityFactor.Text = laserAnimation.OpacityFactor.ToString();
        }

        private void Button_Click_KeysBackgroundApplyChanges(object sender, RoutedEventArgs e)
        {
            if (!_isKeysBackgroundUsed) { return; }

            int x;
            if (!int.TryParse(textBox_keysBG_X.Text, out x)) { return; }
            int y;
            if (!int.TryParse(textBox_keysBG_Y.Text, out y)) { return; }
            int w;
            if (!int.TryParse(textBox_keysBG_W.Text, out w)) { return; }
            int h;
            if (!int.TryParse(textBox_keysBG_H.Text, out h)) { return; }

            Image image = _skinComponents.Find(x => x.Id == ComponentIDs.KeysBackground).Texture;
            image.Width = w;
            image.Height = h;

            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
        }

        private void ComboBox_SelectionChanged_Injections(object sender, SelectionChangedEventArgs e)
        {
            if (!_skinDataIsLoaded) { return; }

            switch (comboBox_injections.SelectedIndex)
            {
                case 0:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_AutoplayAndReplay).Texture;

                        textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                        textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                    }
                    break;

                case 1:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_Gauges).Texture;

                        textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                        textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                    }
                    break;

                case 2:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_PlacementModifiers).Texture;

                        textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                        textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                    }
                    break;

                case 3:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_AllLN).Texture;

                        textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                        textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                    }
                    break;

                case 4:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_LR2NormalJudge).Texture;

                        textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                        textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                    }
                    break;

                case 5:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_NoSSC).Texture;

                        textBox_injectionsX.Text = Canvas.GetLeft(image).ToString();
                        textBox_injectionsY.Text = Canvas.GetTop(image).ToString();
                    }
                    break;
            }
        }

        private void ComboBox_SelectionChanged_Placards(object sender, SelectionChangedEventArgs e)
        {
            if (!_skinDataIsLoaded) { return; }

            switch (comboBox_placards.SelectedIndex)
            {
                case 0:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_Health);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 1:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_BPM);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 2:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_ScrollSpeed);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 3:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_Offset);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 4:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_MaxCombo);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 5:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_Time);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 6:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_Accuracy);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 7:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_COOL);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 8:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_GOOD);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 9:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_OKAY);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 10:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_BAD);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;

                case 11:
                    {
                        SkinComponent? component = _skinComponents.Find(x => x.Id == ComponentIDs.Placard_MISS);
                        if (component != null)
                        {
                            textBox_placard_X.Text = Canvas.GetLeft(component.Texture).ToString();
                            textBox_placard_Y.Text = Canvas.GetTop(component.Texture).ToString();
                            textBox_placard_W.Text = component.Texture.Width.ToString();
                            textBox_placard_H.Text = component.Texture.Height.ToString();
                        }
                    }
                    break;
            }
        }

        private void ComboBox_SelectionChanged_Numbers(object sender, SelectionChangedEventArgs e)
        {
            if (!_skinDataIsLoaded) { return; }

            switch (comboBox_numbers.SelectedIndex)
            {
                case 0:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_Health).Texture;
                        textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_number_W.Text = image.Width.ToString();
                        textBox_number_H.Text = image.Height.ToString();
                    }
                    break;

                case 1:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_BPM).Texture;
                        textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_number_W.Text = image.Width.ToString();
                        textBox_number_H.Text = image.Height.ToString();
                    }
                    break;

                case 2:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_ScrollSpeed).Texture;
                        textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_number_W.Text = image.Width.ToString();
                        textBox_number_H.Text = image.Height.ToString();
                    }
                    break;

                case 3:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_Offset).Texture;
                        textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_number_W.Text = image.Width.ToString();
                        textBox_number_H.Text = image.Height.ToString();
                    }
                    break;

                case 4:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_MaxCombo).Texture;
                        textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_number_W.Text = image.Width.ToString();
                        textBox_number_H.Text = image.Height.ToString();
                    }
                    break;

                case 5:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_Time).Texture;
                        textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_number_W.Text = image.Width.ToString();
                        textBox_number_H.Text = image.Height.ToString();
                    }
                    break;

                case 6:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_Accuracy).Texture;
                        textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_number_W.Text = image.Width.ToString();
                        textBox_number_H.Text = image.Height.ToString();
                    }
                    break;

                case 7:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_ComboCOOL).Texture;
                        textBox_number_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_number_Y.Text = Canvas.GetTop(image).ToString();
                        textBox_number_W.Text = image.Width.ToString();
                        textBox_number_H.Text = image.Height.ToString();
                    }
                    break;
            }
        }

        private void ComboBox_SelectionChanged_CounterNumbers(object sender, SelectionChangedEventArgs e)
        {
            if (!_skinDataIsLoaded) { return; }

            switch (comboBox_verdictCounterNumbers.SelectedIndex)
            {
                case 0:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterCOOL).Texture;
                        textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(image).ToString();
                    }
                    break;

                case 1:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterGOOD).Texture;
                        textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(image).ToString();
                    }
                    break;

                case 2:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterOKAY).Texture;
                        textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(image).ToString();
                    }
                    break;

                case 3:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterBAD).Texture;
                        textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(image).ToString();
                    }
                    break;

                case 4:
                    {
                        Image image = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterMISS).Texture;
                        textBox_verdictCounterNumber_X.Text = Canvas.GetLeft(image).ToString();
                        textBox_verdictCounterNumber_Y.Text = Canvas.GetTop(image).ToString();
                    }
                    break;
            }
        }

        private void Button_Click_Injections0ApplyChanges(object sender, RoutedEventArgs e)
        {
            if (!_skinDataIsLoaded) { return; }

            int width;
            if (!int.TryParse(textBox_injectionsSW.Text, out width)) { return; }

            int height;
            if (!int.TryParse(textBox_injectionsSH.Text, out height)) { return; }

            Image image0 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_AutoplayAndReplay).Texture;
            Image image1 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_Gauges).Texture;
            Image image2 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_PlacementModifiers).Texture;
            Image image3 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_AllLN).Texture;
            Image image4 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_LR2NormalJudge).Texture;
            Image image5 = _skinComponents.Find(x => x.Id == ComponentIDs.Injection_NoSSC).Texture;

            image0.Width = width;
            image0.Height = height;

            image1.Width = width;
            image1.Height = height;

            image2.Width = width;
            image2.Height = height;

            image3.Width = width;
            image3.Height = height;

            image4.Width = width;
            image4.Height = height;

            image5.Width = width;
            image5.Height = height;
        }

        private void Button_Click_Numbers21ApplyChanges(object sender, RoutedEventArgs e)
        {
            if (!_skinDataIsLoaded) { return; }

            int w;
            if (!int.TryParse(textBox_verdictCounterNumbers_W.Text, out w)) { return; }

            int h;
            if (!int.TryParse(textBox_verdictCounterNumbers_H.Text, out h)) { return; }

            Image image0 = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterCOOL).Texture;
            image0.Width = w;
            image0.Height = h;

            Image image1 = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterGOOD).Texture;
            image1.Width = w;
            image1.Height = h;

            Image image2 = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterOKAY).Texture;
            image2.Width = w;
            image2.Height = h;

            Image image3 = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterBAD).Texture;
            image3.Width = w;
            image3.Height = h;

            Image image4 = _skinComponents.Find(x => x.Id == ComponentIDs.Numbers_CounterMISS).Texture;
            image4.Width = w;
            image4.Height = h;
        }
    }

    public enum ComponentIDs
    {
        None,
        AccompanyingArtwork,
        Companion,
        KeysBackground,
        Injection_AutoplayAndReplay,
        Injection_Gauges,
        Injection_PlacementModifiers,
        Injection_AllLN,
        Injection_LR2NormalJudge,
        Injection_NoSSC,
        HealthBarBackground,
        HealthBar,
        RankS,
        VerdictCOOL,
        Placard_Health,
        Placard_BPM,
        Placard_ScrollSpeed,
        Placard_Offset,
        Placard_MaxCombo,
        Placard_Time,
        Placard_Accuracy,
        Placard_COOL,
        Placard_GOOD,
        Placard_OKAY,
        Placard_BAD,
        Placard_MISS,
        Numbers_Health,
        Numbers_BPM,
        Numbers_ScrollSpeed,
        Numbers_Offset,
        Numbers_MaxCombo,
        Numbers_Accuracy,
        Numbers_Time,
        Numbers_ComboCOOL,
        Numbers_CounterCOOL,
        Numbers_CounterGOOD,
        Numbers_CounterOKAY,
        Numbers_CounterBAD,
        Numbers_CounterMISS,
        ArbitraryTexture
    }

    public struct PlayfieldState
    {
        public PlayfieldState()
        {
        }

        public int _startX = 0;
        public int _height = 0;
        public int _column1357Width = 0;
        public int _column246Width = 0;
        public Color _column1357Color = new();
        public Color _column246Color = new();
        public Color _columnSeparatorColor = new();
        public Color _verdictLineColor = new();
        public Color _playfieldBorderColor = new();
    }

    public class LaserAnime : INotifyPropertyChanged
    {
        private int _time = 0;
        private float _widthFactor = 0.0f;
        private float _heightFactor = 0.0f;
        private float _opacityFactor = 0.0f;

        public LaserAnime(int time, float widthFactor, float heightFactor, float opacityFactor)
        {
            _time = time;
            _widthFactor = widthFactor;
            _heightFactor = heightFactor;
            _opacityFactor = opacityFactor;
        }

        public LaserAnime(Skin.LaserAnimation animation)
        {
            _time = animation.Time;
            _widthFactor = animation.WidthFactor;
            _heightFactor = animation.HeightFactor;
            _opacityFactor = animation.OpacityFactor;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new(50);
            stringBuilder.Append(_time).Append(" : ");
            stringBuilder.Append(_widthFactor).Append(" : ");
            stringBuilder.Append(_heightFactor).Append(" : ");
            stringBuilder.Append(_opacityFactor);
            return stringBuilder.ToString();
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Time
        {
            get { return _time; }
            set
            {
                if (value != _time)
                {
                    _time = value;
                    NotifyPropertyChanged("Rep");
                }
            }
        }

        public float WidthFactor
        {
            get { return _widthFactor; }
            set
            {
                if (value != _widthFactor)
                {
                    _widthFactor = value;
                    NotifyPropertyChanged("Rep");
                }
            }
        }

        public float HeightFactor
        {
            get { return _heightFactor; }
            set
            {
                if (value != _heightFactor)
                {
                    _heightFactor = value;
                    NotifyPropertyChanged("Rep");
                }
            }
        }

        public float OpacityFactor
        {
            get { return _opacityFactor; }
            set
            {
                if (value != _opacityFactor)
                {
                    _opacityFactor = value;
                    NotifyPropertyChanged("Rep");
                }
            }
        }

        public string Rep
        {
            get { return ToString(); }
        }
    }

    public enum Keys
    {
        None, Up, Down
    }

    /*
    public struct InjectionsPositionState
    {
        public InjectionsPositionState()
        {
        }

        public int _autoplayAndReplay_X = 0;
        public int _autoplayAndReplay_Y = 0;

        public int _gauges_X = 0;
        public int _gauges_Y = 0;

        public int _placementModifiers_X = 0;
        public int _placementModifiers_Y = 0;

        public int _allLN_X = 0;
        public int _allLN_Y = 0;

        public int _lr2NormalJudge_X = 0;
        public int _lr2NormalJudge_Y = 0;

        public int _noSSC_X = 0;
        public int _noSSC_Y = 0;
    }
    */

    /*
    public struct NumbersState
    {
        public NumbersState()
        {
        }

        public int _health_X = 0;
        public int _health_Y = 0;
        public int _health_SW = 0;
        public int _health_SH = 0;

        public int _bpm_X = 0;
        public int _bpm_Y = 0;
        public int _bpm_SW = 0;
        public int _bpm_SH = 0;

        public int _speed_X = 0;
        public int _speed_Y = 0;
        public int _speed_SW = 0;
        public int _speed_SH = 0;

        public int _offset_X = 0;
        public int _offset_Y = 0;
        public int _offset_SW = 0;
        public int _offset_SH = 0;

        public int _maxCombo_X = 0;
        public int _maxCombo_Y = 0;
        public int _maxCombo_SW = 0;
        public int _maxCombo_SH = 0;

        public int _time_X = 0;
        public int _time_Y = 0;
        public int _time_SW = 0;
        public int _time_SH = 0;

        public int _rate_X = 0;
        public int _rate_Y = 0;
        public int _rate_SW = 0;
        public int _rate_SH = 0;

        public int _judgementComboNumbers_X = 0;
        public int _judgementComboNumbers_Y = 0;
        public int _judgementComboNumbers_SW = 0;
        public int _judgementComboNumbers_SH = 0;

        public int _coolCounter_X = 0;
        public int _coolCounter_Y = 0;

        public int _goodCounter_X = 0;
        public int _goodCounter_Y = 0;

        public int _okayCounter_X = 0;
        public int _okayCounter_Y = 0;

        public int _badCounter_X = 0;
        public int _badCounter_Y = 0;

        public int _missCounter_X = 0;
        public int _missCounter_Y = 0;

        public int _verdictCounterNumbers_SW = 0;
        public int _verdictCounterNumbers_SH = 0;
    }
    */
}