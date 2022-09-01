namespace Framework.Models
{
    public class UIListSettingModel
    {
        public UIParams UIParams { get; set; } = null!;
        public UIListFeatures UIListFeatures { get; set; } = null!;

        // Use this if you want more control
        //public UIAvailableFeatures? UIAvailableFeatures { get; set; }

        public UIItemFeatures GetUIItemFeatures()
        {
            return new UIItemFeatures
            {
                BindingPath = UIListFeatures.BindingPath,
                CanGotoDashboard = UIListFeatures.CanGotoDashboard,
                IsArrayBinding = UIListFeatures.IsArrayBinding,
                PrimayCreateViewContainer = UIListFeatures.PrimayCreateViewContainer,
                PrimayDeleteViewContainer = UIListFeatures.PrimayDeleteViewContainer,
                PrimayDetailsViewContainer = UIListFeatures.PrimayDetailsViewContainer,
                PrimayEditViewContainer = UIListFeatures.PrimayEditViewContainer,
                ShowEditableListDeleteSelect = ShowEditableListDeleteSelect(),
                ShowItemButtons = ShowItemButtons(),
                ShowItemUIStatus = ShowItemUIStatus(),
                ShowListBulkSelectCheckbox = this.ShowListBulkActionRelated(true),
            };
        }

        // 1.Start List/Editable list related

        public bool ShowListBulkActionRelated(bool withBulkDelete)
        {
            return (UIParams.PagedViewOption == ListViewOptions.Table || UIParams.PagedViewOption == ListViewOptions.Tiles) &&
                (withBulkDelete && UIListFeatures.CanBulkDelete || UIListFeatures.CanBulkActions);
            //return (UIParams.PagedViewOption == ListViewOptions.Table || UIParams.PagedViewOption == ListViewOptions.Tiles) &&
            //    (withBulkDelete && UIListFeatures.CanBulkDelete || UIListFeatures.CanBulkActions) &&
            //    UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Any(t=> t == ListViewOptions.Table && t != ListViewOptions.Tiles) &&
            //    (UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || (UIAvailableFeatures.HasBulkDelete || UIAvailableFeatures.HasBulkActions) && UIAvailableFeatures.AvailableListViewFeatures!.Any(t=>t.Key != ListViewOptions.EditableTable && t.Key != ListViewOptions.Card));
        }

        public bool HasEditableList()
        {
            return UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Contains(ListViewOptions.EditableTable);
            //return UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Contains(ListViewOptions.EditableTable) &&
            //    (UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || UIAvailableFeatures.AvailableListViewFeatures.ContainsKey(ListViewOptions.EditableTable));
        }

        public bool ShowItemUIStatus()
        {
            return UIParams.PagedViewOption == ListViewOptions.EditableTable && HasEditableList();
        }

        public bool ShowEditableListDeleteSelect()
        {
            return UIParams.PagedViewOption == ListViewOptions.EditableTable && UIListFeatures.CanBulkDelete && HasEditableList();
        }

        public bool ShowItemButtons()
        {
            return UIParams.PagedViewOption != ListViewOptions.EditableTable;
        }

        public List<ListViewOptions> GetAvailablePagedViewOptions()
        {
            return UIListFeatures.AvailableListViews ?? Enumerable.Empty<ListViewOptions>().ToList();
            //return UIListFeatures.AvailableListViews != null
            //    ? UIListFeatures.AvailableListViews.Where(t => UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || UIAvailableFeatures.AvailableListViewFeatures.ContainsKey(t)).ToList()
            //    : Enumerable.Empty<ListViewOptions>().ToList();
        }

        public bool CanGotoCreate(CrudViewContainers crudViewContainers)
        {
            return (UIParams.PagedViewOption == ListViewOptions.Table || UIParams.PagedViewOption == ListViewOptions.Tiles) && UIListFeatures.PrimayEditViewContainer == crudViewContainers ||
                UIParams.PagedViewOption == ListViewOptions.EditableTable && crudViewContainers == CrudViewContainers.Inline;
        }

        // 1.end List/Editable list related
    }
}

