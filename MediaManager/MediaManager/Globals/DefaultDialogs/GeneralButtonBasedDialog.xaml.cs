using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MediaManager.Globals.DefaultDialogs
{
    /// <summary>
    /// A dialog with a header and text that can contain several buttons to select an action or result.
    /// <br/>
    /// The dialog is created via the given configuration methods and can either be configured to trigger an <see cref="Action"/> or the set a <see cref="Result"/> whenever a specific button is clicked.
    /// For the latter, use the <see cref="ShowForResult"/> method with the according building methods for the buttons.
    /// </summary>
    public partial class GeneralButtonBasedDialog : Window
    {
        /// <summary>
        /// Result value of the dialog.
        /// <br/>
        /// To set a result whenever a button is clicked, use the <see cref="ShowForResult"/> method with the according building methods for the buttons.
        /// </summary>
        public object Result { get; private set; }

        private Color NegativeColor;
        private Color NeutralColor;
        private Color PositiveColor;
        private Color CancelColor;
        private Thickness ButtonMargin;

        /// <summary>
        /// Create a new <see cref="GeneralButtonBasedDialog"/>.
        /// </summary>
        /// <param name="style"><see cref="GeneralButtonBasedDialogStyle"/> of the dialog</param>
        public GeneralButtonBasedDialog(GeneralButtonBasedDialogStyle style)
        {
            InitializeComponent();
            SizeToContent = SizeToContent.WidthAndHeight;

            Background = style.Background;
            FontSize = style.FontSize;
            FontFamily = style.FontFamily;

            headerContainer.Background = style.HeaderBackground;
            header.FontSize = style.HeaderFontSize;
            header.FontFamily = style.HeaderFontFamily;

            headerBorder.BorderBrush = style.HeaderBorderColor;
            headerBorder.BorderThickness = new Thickness(style.HeaderBorderThickness);

            NegativeColor = style.NegativeColor;
            NeutralColor = style.NeutralColor;
            PositiveColor = style.PositiveColor;
            CancelColor = style.CancelButtonBackground;
            ButtonMargin = style.ButtonMargin;

            BorderBrush = new SolidColorBrush(NeutralColor);
        }
        #region Building
        /// <summary>
        /// Set the header of the dialog.
        /// </summary>
        /// <param name="title">Header of the dialog</param>
        public GeneralButtonBasedDialog WithTitle(string title)
        {
            header.Text = title;
            return this;
        }
        /// <summary>
        /// Set the text of the dialog.
        /// </summary>
        /// <param name="text">Text of the dialog</param>
        public GeneralButtonBasedDialog WithText(string text)
        {
            mainText.Text = text;
            return this;
        }
        /// <summary>
        /// Configure a default button with an <see cref="Action"/> to perform when clicked.
        /// </summary>
        /// <param name="text">Button caption that can include mnemonics</param>
        /// <param name="action">Action to perform when the button is clicked</param>
        public GeneralButtonBasedDialog WithDefaultButton(string text, Action action)
        {
            AddButton(new Button()
            {
                IsDefault = true,
                Margin = ButtonMargin,
                Content = text,
            }, action);
            return this;
        }
        /// <summary>
        /// Configure a neutral button with an <see cref="Action"/> to perform when clicked.
        /// </summary>
        /// <param name="text">Button caption that can include mnemonics</param>
        /// <param name="action">Action to perform when the button is clicked</param>
        public GeneralButtonBasedDialog WithNeutralButton(string text, Action action, bool isDefault = false, bool isCancel = false)
        {
            AddButton(new Button()
            {
                IsDefault = isDefault,
                IsCancel = isCancel,
                Margin = ButtonMargin,
                Content = text,
            }, action);
            return this;
        }
        /// <summary>
        /// Configure a cancel button with an <see cref="Action"/> to perform when clicked.
        /// </summary>
        /// <param name="text">Button caption that can include mnemonics</param>
        /// <param name="action">Action to perform when the button is clicked</param>
        public GeneralButtonBasedDialog WithCancelButton(string text, Action action)
        {
            AddButton(new Button()
            {
                IsCancel = true,
                Margin = ButtonMargin,
                Content = text,
                Background = new SolidColorBrush(CancelColor),
            }, action);
            return this;
        }
        /// <summary>
        /// Configure a default button with a result to set when clicked.
        /// <br/>
        /// Use <see cref="ShowForResult"/> to show the dialog in the end or retrieve the <see cref="Result"/> via the variable.
        /// </summary>
        /// <param name="text">Button caption that can include mnemonics</param>
        /// <param name="result">Dialog result to set when the button is clicked</param>
        public GeneralButtonBasedDialog WithDefaultButton(string text, object result = null)
        {
            AddButton(new Button()
            {
                IsDefault = true,
                Margin = ButtonMargin,
                Content = text,
            }, () => Result = result);
            return this;
        }
        /// <summary>
        /// Configure a neutral button with a result to set when clicked.
        /// <br/>
        /// Use <see cref="ShowForResult"/> to show the dialog in the end or retrieve the <see cref="Result"/> via the variable.
        /// </summary>
        /// <param name="text">Button caption that can include mnemonics</param>
        /// <param name="result">Dialog result to set when the button is clicked</param>
        public GeneralButtonBasedDialog WithNeutralButton(string text, object result = null, bool isDefault = false, bool isCancel = false)
        {
            AddButton(new Button()
            {
                IsDefault = isDefault,
                IsCancel = isCancel,
                Margin = ButtonMargin,
                Content = text,
            }, () => Result = result);
            return this;
        }
        /// <summary>
        /// Configure a cancel button with a result to set when clicked.
        /// <br/>
        /// Use <see cref="ShowForResult"/> to show the dialog in the end or retrieve the <see cref="Result"/> via the variable.
        /// </summary>
        /// <param name="text">Button caption that can include mnemonics</param>
        /// <param name="result">Dialog result to set when the button is clicked</param>
        public GeneralButtonBasedDialog WithCancelButton(string text, object result = null)
        {
            AddButton(new Button()
            {
                IsCancel = true,
                Margin = ButtonMargin,
                Content = text,
                Background = new SolidColorBrush(CancelColor),
            }, () => Result = result);
            return this;
        }
        /// <summary>
        /// Set a <see cref="SuccessMode"/> to style the border as described in the <see cref="GeneralButtonBasedDialogStyle"/>.
        /// </summary>
        /// <param name="mode">SuccessMode to use</param>
        public GeneralButtonBasedDialog WithBorder(SuccessMode mode)
        {
            switch (mode)
            {
                case SuccessMode.Error: BorderBrush = new SolidColorBrush(NegativeColor); break;
                case SuccessMode.Neutral: BorderBrush = new SolidColorBrush(NeutralColor); break;
                case SuccessMode.Success: BorderBrush = new SolidColorBrush(PositiveColor); break;
            }
            return this;
        }
        /// <summary>
        /// Show the dialog and wait for a result to return.
        /// <br/>
        /// <br/>
        /// The <see cref="Result"/> can also be retrieved via the variable.
        /// <br/>
        /// Use the methods <see cref="WithDefaultButton(string, object)"/>, <see cref="WithNeutralButton(string, object, bool, bool)"/> and <see cref="WithCancelButton(string, object)"/> to configure the buttons.
        /// </summary>
        /// <returns>Result of the dialog</returns>
        public object ShowForResult()
        {
            ShowDialog();
            return Result;
        }
        #endregion
        private void AddButton(Button btn, Action action = null)
        {
            btn.Click += (object sender, RoutedEventArgs e) => { action?.Invoke(); Close(); };
            buttonContainer.Children.Add(btn);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
