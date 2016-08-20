using System;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace Vnap.Views.Components
{
    public partial class SatelliteMenu : AbsoluteLayout
    {
        private CachedImage menubutton;
        private DateTime beginTime;
        private AbsoluteLayout absoluteLayout;

        private bool isMoving = false;
        private float each = 0.0f;
        private double seconds = 0;
        private double offset = 0;
        private double RatioScreen;
        private double delta = frac;
        private static double frac = 0.25;

        public SatelliteMenu()
        {
            InitializeComponent();
            
            menubutton = MenuButton;
            absoluteLayout = this;

            var gestureRecognizer = new TapGestureRecognizer();
            gestureRecognizer.Tapped += OnButtonClicked;

            menubutton.GestureRecognizers.Add(gestureRecognizer);
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            ExpandContractMenu();
            absoluteLayout.RaiseChild(MenuButton);
        }

        private void ExpandContractMenu()
        {
            if (!isMoving)
            {
                isMoving = true;
                delta = delta > 0 ? 0 : frac;
                seconds = 0;
                offset = 0;
                beginTime = DateTime.Now;
                Device.StartTimer(TimeSpan.FromSeconds(1.0 / 60), () =>
                {

                    if (isMoving && seconds < frac)
                    {
                        seconds = Math.Min((DateTime.Now - beginTime).TotalSeconds, frac);
                        offset = seconds + delta;
                        //* convert linear transition to sinusoid *//
                        double offsetB = (Math.Sin(4 * Math.PI * (offset - 0.125)) + 1) / 4;

                        each = 0;
                        AbsoluteLayout.SetLayoutBounds(CallButton,
                            new Rectangle(1 - (2 * offsetB), 1,
                                AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                        AbsoluteLayout.SetLayoutBounds(MessageButton,
                           new Rectangle(1, 1 - (2 * offsetB),
                               AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

                        return true;
                    }
                    isMoving = false;

                    return false;
                });
            }
        }
    }
}
