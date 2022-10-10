using System;
using Android.Views;
using Com.Tomergoldst.Tooltips;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Droid.Effects;
using XamFormsLanguageLearningApp.Effects;
using Xamarin.Forms.Platform.Android;
using static Com.Tomergoldst.Tooltips.ToolTipsManager;

[assembly: ResolutionGroupName("CrossGeeks")]
[assembly: ExportEffect(typeof(DroidTooltipEffect), nameof(TooltipEffect))]

namespace XamFormsLanguageLearningApp.Droid.Effects
{
    public class DroidTooltipEffect : PlatformEffect
    {
        #region Fields

        private ToolTipsManager _toolTipsManager;
        private ITipListener listener;
        private ToolTip toolTipView;

        #endregion Fields

        #region Constructors

        public DroidTooltipEffect()
        {
            listener = new TipListener();
            _toolTipsManager = new ToolTipsManager(listener);
        }

        #endregion Constructors



        #region Methods

        protected override void OnAttached()
        {
            var control = Control ?? Container;
            control.Click += OnTap;
        }

        protected override void OnDetached()
        {
            var control = Control ?? Container;
            control.Click -= OnTap;
            _toolTipsManager.FindAndDismiss(control);
        }

        private void OnTap(object sender, EventArgs e)
        {
            var control = Control ?? Container;

            var text = TooltipEffect.GetText(Element);

            if (!string.IsNullOrEmpty(text))
            {
                ToolTip.Builder builder;
                var parentContent = control.RootView;

                var position = TooltipEffect.GetPosition(Element);
                switch (position)
                {
                    case TooltipPosition.Top:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(0, ' '), ToolTip.PositionAbove);
                        break;

                    case TooltipPosition.Left:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionLeftTo);
                        break;

                    case TooltipPosition.Right:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionRightTo);
                        break;

                    default:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(0, ' '), ToolTip.PositionBelow);
                        break;
                }

                builder.SetAlign(ToolTip.PositionBelow);
                builder.SetBackgroundColor(TooltipEffect.GetBackgroundColor(Element).ToAndroid());
                builder.SetTextColor(TooltipEffect.GetTextColor(Element).ToAndroid());

                toolTipView = builder.Build();

                _toolTipsManager?.Show(toolTipView);
            }
        }

        #endregion Methods



        #region Classes

        private class TipListener : Java.Lang.Object, ITipListener
        {
            #region Methods

            public void OnTipDismissed(Android.Views.View p0, int p1, bool p2)
            {
            }

            #endregion Methods
        }

        #endregion Classes
    }
}