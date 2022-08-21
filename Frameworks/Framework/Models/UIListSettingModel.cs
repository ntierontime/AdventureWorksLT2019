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
            return (UIParams.PagedViewOption == PagedViewOptions.Table || UIParams.PagedViewOption == PagedViewOptions.Tiles) &&
                (withBulkDelete && UIListFeatures.CanBulkDelete || UIListFeatures.CanBulkActions);
            //return (UIParams.PagedViewOption == PagedViewOptions.Table || UIParams.PagedViewOption == PagedViewOptions.Tiles) &&
            //    (withBulkDelete && UIListFeatures.CanBulkDelete || UIListFeatures.CanBulkActions) &&
            //    UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Any(t=> t == PagedViewOptions.Table && t != PagedViewOptions.Tiles) &&
            //    (UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || (UIAvailableFeatures.HasBulkDelete || UIAvailableFeatures.HasBulkActions) && UIAvailableFeatures.AvailableListViewFeatures!.Any(t=>t.Key != PagedViewOptions.EditableTable && t.Key != PagedViewOptions.Card));
        }

        public bool HasEditableList()
        {
            return UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Contains(PagedViewOptions.EditableTable);
            //return UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Contains(PagedViewOptions.EditableTable) &&
            //    (UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || UIAvailableFeatures.AvailableListViewFeatures.ContainsKey(PagedViewOptions.EditableTable));
        }

        public bool ShowItemUIStatus()
        {
            return UIParams.PagedViewOption == PagedViewOptions.EditableTable && HasEditableList();
        }

        public bool ShowEditableListDeleteSelect()
        {
            return UIParams.PagedViewOption == PagedViewOptions.EditableTable && UIListFeatures.CanBulkDelete && HasEditableList();
        }

        public bool ShowItemButtons()
        {
            return UIParams.PagedViewOption != PagedViewOptions.EditableTable;
        }

        public List<PagedViewOptions> GetAvailablePagedViewOptions()
        {
            return UIListFeatures.AvailableListViews ?? Enumerable.Empty<PagedViewOptions>().ToList();
            //return UIListFeatures.AvailableListViews != null
            //    ? UIListFeatures.AvailableListViews.Where(t => UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || UIAvailableFeatures.AvailableListViewFeatures.ContainsKey(t)).ToList()
            //    : Enumerable.Empty<PagedViewOptions>().ToList();
        }

        public bool CanGotoCreate(CrudViewContainers crudViewContainers)
        {
            return (UIParams.PagedViewOption == PagedViewOptions.Table || UIParams.PagedViewOption == PagedViewOptions.Tiles) && UIListFeatures.PrimayEditViewContainer == crudViewContainers ||
                UIParams.PagedViewOption == PagedViewOptions.EditableTable && crudViewContainers == CrudViewContainers.Inline;
        }

        // 1.end List/Editable list related
    }
}

