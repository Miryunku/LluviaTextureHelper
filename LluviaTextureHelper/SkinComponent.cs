using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LluviaTextureHelper
{
    public class SkinComponent : INotifyPropertyChanged
    {
        #region FIELDS

        /* General */

        private ComponentIDs _id;
        private string _byname;
        private Image _texture;

        /* Other book-keeping */

        private bool _inCanvas = false;

        /* Position and size */

        private int _u = 0;
        private int _v = 0;
        private int _tw = 0;
        private int _th = 0;
        private int _x = 0;
        private int _y = 0;
        private int _w = 0;
        private int _h = 0;

        private float _scaleW = 0;

        #endregion

        #region CONSTRUCTORS

        public SkinComponent(ComponentIDs id, string byname, Image texture)
        {
            _id = id;
            _byname = byname;
            _texture = texture;
        }

        #endregion

        #region METHODS

        public override string ToString()
        {
            return _byname;
        }

        private void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        #endregion

        #region EVENTS

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region PROPERTIES

        public ComponentIDs Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Byname
        {
            get { return _byname; }
            set { _byname = value; }
        }

        public Image Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public bool HasTexture
        {
            get { return _texture != null; }
        }

        public bool InCanvas
        {
            get { return _inCanvas; }
            set
            {
                if (value != _inCanvas)
                {
                    _inCanvas = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int U
        {
            get { return _u; }
            set { _u = value; }
        }

        public int V
        {
            get { return _v; }
            set { _v = value; }
        }

        public int TW
        {
            get { return _tw; }
            set { _tw = value; }
        }

        public int TH
        {
            get { return _th; }
            set { _th = value; }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int W
        {
            get { return _w; }
            set { _w = value; }
        }

        public int H
        {
            get { return _h; }
            set { _h = value; }
        }

        public float ScaleW
        {
            get { return _scaleW; }
            set { _scaleW = value; }
        }

        #endregion
    }
}