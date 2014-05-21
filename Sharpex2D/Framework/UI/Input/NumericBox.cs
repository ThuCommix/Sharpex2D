using System;
using System.Globalization;

namespace Sharpex2D.Framework.UI.Input
{
    public abstract class NumericBox : UIControl
    {

        private string _buffer = "";
        private int _intValue;
        private decimal _decimalValue;

        /// <summary>
        /// Initializes a new NumericBox.
        /// </summary>
        /// <param name="mode">The NumericMode.</param>
        /// <param name="assignedUIManager">The assigned UIManager.</param>
        protected NumericBox(NumericMode mode, UIManager assignedUIManager) : base(assignedUIManager)
        {
            Mode = mode;
        }
        /// <summary>
        /// Processes a Tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public override void OnTick(float elapsed)
        {
            //back
            if (IsKeyPressed(Framework.Input.Keys.Back))
            {
                if (_buffer.Length > 1)
                {
                    _buffer = _buffer.Substring(0, _buffer.Length - 1);
                }
            }

            //handle -
            if (IsKeyPressed(Framework.Input.Keys.OemMinus))
            {
                if (_buffer == "")
                {
                    _buffer = "-";
                }
            }

            //numbers
            if (IsKeyPressed(Framework.Input.Keys.D0))
            {
                _buffer += "0";
            }
            if (IsKeyPressed(Framework.Input.Keys.D1))
            {
                _buffer += "1";
            }
            if (IsKeyPressed(Framework.Input.Keys.D2))
            {
                _buffer += "2";
            }
            if (IsKeyPressed(Framework.Input.Keys.D3))
            {
                _buffer += "3";
            }
            if (IsKeyPressed(Framework.Input.Keys.D4))
            {
                _buffer += "4";
            }
            if (IsKeyPressed(Framework.Input.Keys.D5))
            {
                _buffer += "5";
            }
            if (IsKeyPressed(Framework.Input.Keys.D6))
            {
                _buffer += "6";
            }
            if (IsKeyPressed(Framework.Input.Keys.D7))
            {
                _buffer += "7";
            }
            if (IsKeyPressed(Framework.Input.Keys.D8))
            {
                _buffer += "8";
            }
            if (IsKeyPressed(Framework.Input.Keys.D9))
            {
                _buffer += "9";
            }

            //handle point
            if (Mode == NumericMode.Decimal)
            {
                if (IsKeyPressed(Framework.Input.Keys.Oemcomma))
                {
                    if (!_buffer.Contains("."))
                    {
                        _buffer += ".";
                    }
                }
            }

            ConvertBuffer();
            base.OnTick(elapsed);
        }

        /// <summary>
        /// Converts the Buffer.
        /// </summary>
        private void ConvertBuffer()
        {
            if (Mode == NumericMode.Int)
            {
                IntValue = Convert.ToInt32(_buffer);
            }
            else
            {
                DecimalValue = Convert.ToDecimal(_buffer);
            }
        }

        /// <summary>
        /// Sets or gets the IntValue.
        /// </summary>
        public int IntValue
        {
            set { _intValue = value;
                _buffer = value.ToString(CultureInfo.InvariantCulture);
            }
            get { return _intValue; }
        }

        /// <summary>
        /// Sets or gets the DecimalValue.
        /// </summary>
        public decimal DecimalValue
        {
            set
            {
                _decimalValue = value;
                _buffer = value.ToString(CultureInfo.InvariantCulture);
            }
            get { return _decimalValue; }
        }

        /// <summary>
        /// Gets the DisplayValue.
        /// </summary>
        public string DisplayValue { get { return _buffer; } }

        /// <summary>
        /// Sets or gets the mode.
        /// </summary>
        public NumericMode Mode { set; get; }
    }
}
