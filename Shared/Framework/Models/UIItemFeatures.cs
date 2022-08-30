using Framework.Common;
namespace Framework.Models
{
    public class UIItemFeatures
    {
        public bool ShowListBulkSelectCheckbox { get; set; } = false;
        public bool ShowItemButtons { get; set; } = false;
        public bool ShowItemUIStatus { get; set; } = false;
        public bool ShowEditableListDeleteSelect { get; set; } = false;

        /// <summary>
        /// PrimayCreateViewContainer must be Inline when EditableList
        /// </summary>
        public CrudViewContainers PrimayCreateViewContainer { get; set; } = CrudViewContainers.Dialog;
        public CrudViewContainers PrimayDeleteViewContainer { get; set; } = CrudViewContainers.Dialog;
        public CrudViewContainers PrimayDetailsViewContainer { get; set; } = CrudViewContainers.None;
        public CrudViewContainers PrimayEditViewContainer { get; set; } = CrudViewContainers.Dialog;

        public bool CanGotoDashboard { get; set; } = false;

        /// <summary>
        /// For Mvc Core
        /// For <form></form> postback
        /// 1. empty when current PagedViewOptions=List/Tiles/Slideshow, id/name={property name}
        /// 2. not empty when EditableList, for array binding, e.g. "Data" when EditableList in Index.cshtml, then id/name="Data[i].{property name}"
        /// </summary>
        public bool IsArrayBinding { get; set; } = false;
        public string? BindingPath { get; set; }
    }
}

