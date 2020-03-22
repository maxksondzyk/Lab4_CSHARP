using System;
using Lab4_CSHARP.Views;
using View = Lab4_CSHARP.Views.View;

namespace Lab4_CSHARP.Tools.Navigation
{
    internal class ListNavigationModel : BaseNavigationModel
    {
        public ListNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Add:
                    AddView(ViewType.Add, new AddView());
                    break;
                case ViewType.Edit:
                    AddView(ViewType.Edit, new AddView());
                    break;
                case ViewType.Main:
                    AddView(ViewType.Main, new View());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
