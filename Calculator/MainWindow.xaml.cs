using CalcLibrary;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _defaultButtonZeroMargin = ButtonZero.Margin;
        }

        private const string _defaultText = "0";
        private const int _operationLength = 3, _numberLength = 1;
        private readonly Thickness _defaultButtonZeroMargin;
        private bool _buttonZeroMarginChanged = false;

        private enum Action
        {
            Number,
            Operation
        }
        private Action _latestAction;

        protected void Button_Click(object sender, EventArgs e)
        {
            Button current = (Button)sender;
            TextBox.Text += $" {current.Content} ";
            _latestAction = Action.Operation;
        }

        protected void Button_ClickNumber(object sender, RoutedEventArgs e)
        {
            Button current = (Button)sender;
            if (TextBox.Text == _defaultText)
                TextBox.Text = (string)current.Content;
            else
                TextBox.Text += (string)current.Content;

            _latestAction = Action.Number;
        }

        protected void Button_ClickEquals(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox.Text = Calc.DoOperation(TextBox.Text);
                StatusLabel.Content = "Выражение успешно вычислено.";
            }
            catch (FormatException exception) when (exception.Message == "Неверный формат числа. Все числа должны иметь одинаковый разделитель дробной и целой части и вид *...[Р*...*] где * - цифра (0-9), Р - разделитель, [] - необязательная часть. Пример: 23")
            {
                StatusLabel.Content = $"Ошибка: {exception.Message}";
            }
            catch (FormatException exception) when (exception.Message == "Ошибка: Неверный формат числа. Все числа должны иметь одинаковый разделитель  дробной и целой части в 1 символ длиной")
            {
                StatusLabel.Content = $"Ошибка: {exception.Message}";
            }
            catch (ArgumentException exception) when (exception.Message == "Неверный формат выражения - в нем должен быть один оператор.")
            {
                StatusLabel.Content = $"Ошибка: {exception.Message}";
            }
            catch (ParsingException exception) when (exception.Message == "Неверный формат числа.")
            {
                StatusLabel.Content = $"Ошибка: {exception.Message}";
            }
            catch (InvalidOperandsException exception) when (exception.Message == "В выражении недостаточно операндов/чисел (должно быть 2).")
            {
                StatusLabel.Content = $"Ошибка: {exception.Message}";
            }
            catch (Exception exception)
            {
                StatusLabel.Content = $"Произошла неизвестная ошибка: {exception.GetType()}";
            }
        }

        protected void Button_ClickClear(object sender, RoutedEventArgs e)
        {
            TextBox.Text = _defaultText;
            StatusLabel.Content = "Выражение стерто.";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox.Text = _defaultText;
            StatusLabel.Content = "Приложение загружено.";
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                Button_ClickEquals(sender, e);
            else if (TextBox.Text == _defaultText)
                TextBox.Text = "";
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StatusBar.Width = Width - BorderThickness.Right - BorderThickness.Left;
            TextBox.Width = Width - ButtonEquals.Width - ButtonEquals.Margin.Right * 2 - TextBox.Margin.Left;
            if (Height < 450)
            {
                Thickness newMargins = ButtonZero.Margin;
                newMargins.Left = 0;
                newMargins.Top = 232;
                newMargins.Right = ButtonDivision.Margin.Right;
                ButtonZero.Margin = newMargins;
                ButtonZero.HorizontalAlignment = HorizontalAlignment.Right;
                UpdateLayout();
                _buttonZeroMarginChanged = true;
            }
            else if (_buttonZeroMarginChanged)
            {
                ButtonZero.Margin = _defaultButtonZeroMargin;
                _buttonZeroMarginChanged = false;
            }
        }

        protected void Button_ClickRemoveLast(object sender, RoutedEventArgs e)
        {
            switch (_latestAction)
            {
                case Action.Number:
                    TextBox.Text = (TextBox.Text.Length >= _numberLength) ? TextBox.Text.Remove(TextBox.Text.Length - _numberLength) : TextBox.Text;
                    break;
                case Action.Operation:
                    TextBox.Text = (TextBox.Text.Length > _operationLength) ? TextBox.Text.Remove(TextBox.Text.Length - _operationLength - 1, 1) : TextBox.Text;
                    break;
                default:
                    break;
            }
            StatusLabel.Content = "Удаления последней цифры набранного числа успешно выполнено.";
        }
    }
}
