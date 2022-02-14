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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SDA2035_WPF_HW17
{
    /// <summary>
    /// Логика взаимодействия для ColorUserControl.xaml
    /// </summary>
    public partial class ColorUserControl : UserControl
    {
        public ColorUserControl()
        {
            InitializeComponent();
        }


        public static DependencyProperty ColorProperty;
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;

        static ColorUserControl()
        {

            ColorProperty = DependencyProperty.Register(
                                                        nameof(Color),
                                                        typeof(Color),
                                                        typeof(ColorUserControl),
                                                        new FrameworkPropertyMetadata(
                                                            Colors.Black,
                                                            new PropertyChangedCallback(OnColorChanged)));
            RedProperty = DependencyProperty.Register(
                                                        nameof(Red),
                                                        typeof(byte),
                                                        typeof(ColorUserControl),
                                                        new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            GreenProperty = DependencyProperty.Register(
                                                        nameof(Green),
                                                        typeof(byte),
                                                        typeof(ColorUserControl),
                                                        new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            BlueProperty = DependencyProperty.Register(
                                                        nameof(Blue),
                                                        typeof(byte),
                                                        typeof(ColorUserControl),
                                                        new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
        }

        //Свойства
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        //Реакция на изменения
        private static void OnColorRGBChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            ColorUserControl colorSelector = (ColorUserControl)sender;
            Color color = colorSelector.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;

            colorSelector.Color = color;
        }
        private static void OnColorChanged(DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
        {
            Color newColorPick = (Color)e.NewValue;
            ColorUserControl colorselector = (ColorUserControl)sender;
            colorselector.Red = newColorPick.R;
            colorselector.Green = newColorPick.G;
            colorselector.Blue = newColorPick.B;
        }

        //Обработчики событий
        public static readonly RoutedEvent ColorChangedEvent = EventManager.RegisterRoutedEvent(
                                                                                                nameof(ColorChanged),
                                                                                                RoutingStrategy.Bubble,
                                                                                                typeof(RoutedPropertyChangedEventHandler<Color>),
                                                                                                typeof(ColorUserControl));
        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }
    }
}
