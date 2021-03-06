using Prism.Common;
using Prism.Navigation;
using System;
using Xamarin.Forms;
using Prism.Behaviors;


namespace GpsNotebook.Behavior
{
    public class TabbedPageNavigationBehavior : BehaviorBase<TabbedPage>
    {
        #region -- Private Helpers --

        private Page CurrentPage;
        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            var newPage = this.AssociatedObject.CurrentPage;

            //if (this.CurrentPage != null)
            {
                var parameters = new NavigationParameters();
                PageUtilities.OnNavigatedFrom(this.CurrentPage, parameters);
                PageUtilities.OnNavigatedTo(newPage, parameters);
            }

            this.CurrentPage = newPage;
        }

        #endregion

        #region -- Ovverides --

        protected override void OnAttachedTo(TabbedPage bindable)
        {
            bindable.CurrentPageChanged += this.OnCurrentPageChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(TabbedPage bindable)
        {
            bindable.CurrentPageChanged -= this.OnCurrentPageChanged;
            base.OnDetachingFrom(bindable);
        }

        #endregion
    }
}
