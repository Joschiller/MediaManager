using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaManager.GUI.Atoms
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        #region Events
        public delegate void ValueChangeHandler(uint newVal);
        public event ValueChangeHandler ValueChanged;
        #endregion

        #region Properties
        public uint Value { get; private set; } = 0;
        public uint? Min { get; private set; } = null;
        public uint? Max { get; private set; } = null;
        /// <summary>
        /// Maximum character length of the numeric value. To set no limit set the <see cref="MaxLength"/> to <c>null</c>.<br/>
        /// Default: <c>null</c>
        /// </summary>
        public uint? MaxLength { get; set; } = null;
        /// <summary>
        /// Value that will be added or substracted from the current value per scoll-event when using the mouse wheel over the input field.
        /// </summary>
        public uint StepPerScroll { get; set; } = 1;
        #endregion

        #region Setup
        // the control only allows non-negative numbers
        private readonly Regex NON_NEGATIVE_NUMBER_REGEX = new Regex(@"^\d+$");
        public NumericUpDown()
        {
            InitializeComponent();
            DataContext = this;
            updateGUI(() => { });
        }
        private void updateGUI(Action callback)
        {
            number.Text = Value.ToString();
            if (Min.HasValue && Value < Min.Value) SetValue(Min.Value);
            if (Max.HasValue && Value > Max.Value) SetValue(Max.Value);
            down.IsEnabled = Value > uint.MinValue && (!Min.HasValue || Value > Min.Value);
            up.IsEnabled = Value < uint.MaxValue && (!Max.HasValue || Value < Max.Value) && (!MaxLength.HasValue || (Value + 1).ToString().Length <= MaxLength.Value);
            number.IsEnabled = down.IsEnabled || up.IsEnabled;
            callback?.Invoke();
        }
        #endregion

        #region Getter/Setter
        public void SetValue(uint val)
        {
            Value = val;
            updateGUI(() => { });
        }
        public void SetMin(uint? val)
        {
            Min = val;
            updateGUI(() => ValueChanged?.Invoke(Value));
        }
        public void SetMax(uint? val)
        {
            Max = val;
            updateGUI(() => ValueChanged?.Invoke(Value));
        }
        #endregion

        #region Handler
        private void up_Click(object sender, RoutedEventArgs e)
        {
            Value++;
            updateGUI(() => ValueChanged?.Invoke(Value));
        }
        private void down_Click(object sender, RoutedEventArgs e)
        {
            Value--;
            updateGUI(() => ValueChanged?.Invoke(Value));
        }
        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true; // prevent scolling outside the control
            var newValue = Value;
            if (e.Delta > 0) newValue += StepPerScroll;
            if (e.Delta < 0 && StepPerScroll >= newValue) newValue = 0;
            if (e.Delta < 0 && StepPerScroll < newValue) newValue -= StepPerScroll;
            if (MaxLength.HasValue && newValue.ToString().Length > MaxLength.Value)
            {
                uint.TryParse(new string('9', (int)MaxLength.Value), out newValue);
            }
            Value = newValue;
            updateGUI(() => ValueChanged?.Invoke(Value));
        }
        private void number_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NON_NEGATIVE_NUMBER_REGEX.IsMatch(number.Text))
            {
                var parsedValue = Value;
                if (uint.TryParse(number.Text, out parsedValue))
                {
                    Value = parsedValue;
                    updateGUI(() => ValueChanged?.Invoke(Value));
                }
                else
                {
                    // ignore new number, because it cannot be parsed => set max number
                    SetValue(uint.MaxValue);
                }
            }
            else
            {
                if (number.Text == "")
                {
                    Value = 0;
                    updateGUI(() => ValueChanged?.Invoke(Value));
                    number.CaretIndex = 1;
                }
                var carPos = number.CaretIndex;
                number.Text = Value.ToString();
                number.CaretIndex = Math.Min(carPos, number.Text.Length);
            }
        }
        #endregion
    }
}
