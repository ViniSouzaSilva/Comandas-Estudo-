using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AmbiPDV.Controls
{
    /// <summary>
    /// Interaction logic for MarqueeText.xaml
    /// </summary>
    public partial class MarqueeText : UserControl
    {
        MarqueeType _marqueeType;

        public MarqueeType MarqueeType
        {
            get { return _marqueeType; }
            set { _marqueeType = value; }
        }

        //public string MarqueeContent
        //{
        //    set { tbmarquee.Text = value; }
        //    get { return tbmarquee.Text; }
        //}


        public string MarqueeContent
        {
            get
            {
                return (string)GetValue(MarqueeContentProperty);
            }
            set
            {
                if (value is null)
                {
                    this.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    tbmarquee.Text = value;
                }
                SetValue(MarqueeContentProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MyCustomProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarqueeContentProperty =
            DependencyProperty.Register("MarqueeContent", typeof(string), typeof(MarqueeText), new UIPropertyMetadata(MyPropertyChangedHandler));

        public static void MyPropertyChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //// Get instance of current control from sender
            //// and property value from e.NewValue

            // Set public property on TaregtCatalogControl, e.g.

            ((MarqueeText)sender).MarqueeContent = (string)e.NewValue;
        }



        //public string MarqueeContent
        //{
        //    get
        //    {
        //        return (string)GetValue(MarqueeContentProperty);
        //    }
        //    set
        //    {
        //        tbmarquee.Text = value;
        //        SetValue(MarqueeContentProperty, value);
        //    }
        //}

        //// Using a DependencyProperty as the backing store for MarqueeContent.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MarqueeContentProperty =
        //    DependencyProperty.Register("MarqueeContent", typeof(string), typeof(MarqueeText), new PropertyMetadata(string.Empty));





        private double _marqueeTimeInSeconds;

        public double MarqueeTimeInSeconds
        {
            get { return _marqueeTimeInSeconds; }
            set { _marqueeTimeInSeconds = value; }
        }


        public MarqueeText()
        {
            InitializeComponent();
            canMain.Height = this.Height;
            canMain.Width = this.Width;
            this.Loaded += new RoutedEventHandler(MarqueeText_Loaded);
        }

        void MarqueeText_Loaded(object sender, RoutedEventArgs e)
        {
            StartMarqueeing(_marqueeType);
        }



        public void StartMarqueeing(MarqueeType marqueeType)
        {
            if (marqueeType == MarqueeType.LeftToRight)
            {
                LeftToRightMarquee();
            }
            else if (marqueeType == MarqueeType.RightToLeft)
            {
                RightToLeftMarquee();
            }
            else if (marqueeType == MarqueeType.TopToBottom)
            {
                TopToBottomMarquee();
            }
            else if (marqueeType == MarqueeType.BottomToTop)
            {
                BottomToTopMarquee();
            }
        }

        private void LeftToRightMarquee()
        {
            double height = canMain.ActualHeight - tbmarquee.ActualHeight;
            tbmarquee.Margin = new Thickness(0, height / 2, 0, 0);
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = -tbmarquee.ActualWidth,
                To = canMain.ActualWidth,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(_marqueeTimeInSeconds))
            };
            tbmarquee.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        }
        private void RightToLeftMarquee()
        {
            double height = canMain.ActualHeight - tbmarquee.ActualHeight;
            tbmarquee.Margin = new Thickness(0, height / 2, 0, 0);
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = -tbmarquee.ActualWidth,
                To = canMain.ActualWidth,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(_marqueeTimeInSeconds))
            };
            tbmarquee.BeginAnimation(Canvas.RightProperty, doubleAnimation);
        }
        private void TopToBottomMarquee()
        {
            double width = canMain.ActualWidth - tbmarquee.ActualWidth;
            tbmarquee.Margin = new Thickness(width / 2, 0, 0, 0);
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = -tbmarquee.ActualHeight,
                To = canMain.ActualHeight,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(_marqueeTimeInSeconds))
            };
            tbmarquee.BeginAnimation(Canvas.TopProperty, doubleAnimation);
        }
        private void BottomToTopMarquee()
        {
            double width = canMain.ActualWidth - tbmarquee.ActualWidth;
            tbmarquee.Margin = new Thickness(width / 2, 0, 0, 0);
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = -tbmarquee.ActualHeight,
                To = canMain.ActualHeight,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(_marqueeTimeInSeconds))
            };
            tbmarquee.BeginAnimation(Canvas.BottomProperty, doubleAnimation);
        }
    }
    public enum MarqueeType
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }
}
