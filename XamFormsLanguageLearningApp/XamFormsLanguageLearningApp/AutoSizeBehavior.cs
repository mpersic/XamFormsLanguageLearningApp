using System;
using System.Collections.Generic;
using System.Text;

namespace XamFormsLanguageLearningApp.ViewModels
{
    using System;
    using System.Linq;
    using Xamarin.Forms;

    public class AutoSizeBehavior : Behavior<ListView>
    {
        #region Fields

        private ListView _ListView;

        #endregion Fields



        #region Properties

        private ITemplatedItemsView<Cell> Cells => _ListView;

        #endregion Properties



        #region Methods

        protected override void OnAttachedTo(ListView bindable)
        {
            bindable.ItemAppearing += AppearanceChanged;
            bindable.ItemDisappearing += AppearanceChanged;
            _ListView = bindable;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            bindable.ItemAppearing -= AppearanceChanged;
            bindable.ItemDisappearing -= AppearanceChanged;
            _ListView = null;
        }

        private void AppearanceChanged(object sender, ItemVisibilityEventArgs e) =>
          UpdateHeight(e.Item);

        private int MeasureRowHeight(object item)
        {
            var template = _ListView.ItemTemplate;
            var cell = (Cell)template.CreateContent();
            cell.BindingContext = item;
            var height = cell.RenderHeight;
            var mod = height % 1;
            if (mod > 0)
                height = height - mod + 1;
            return (int)height;
        }

        private void SetHeight(double height)
        {
            //TODO if header or footer is string etc.
            if (_ListView.Header is VisualElement header)
                height += header.Height;
            if (_ListView.Footer is VisualElement footer)
                height += footer.Height;
            _ListView.HeightRequest = height;
        }

        private void UpdateHeight(object item)
        {
            if (_ListView.HasUnevenRows)
            {
                double height;
                if ((height = _ListView.HeightRequest) ==
                    (double)VisualElement.HeightRequestProperty.DefaultValue)
                    height = 0;

                height += MeasureRowHeight(item);
                SetHeight(height);
            }
            else if (_ListView.RowHeight == (int)ListView.RowHeightProperty.DefaultValue)
            {
                var height = MeasureRowHeight(item);
                _ListView.RowHeight = height;
                SetHeight(height);
            }
        }

        #endregion Methods
    }
}