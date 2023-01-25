import { useNavigate } from "react-router-dom";

import { getCRUDItemPartialViewPropsStandalone } from "src/shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import ItemViewsPartial from "./ItemViewsPartial";
import { IProductCategoryDataModel } from "src/dataModels/IProductCategoryDataModel";

export default function CreatePage(): JSX.Element {
    const navigate = useNavigate();
    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<IProductCategoryDataModel>(
        ViewItemTemplates.Create,
        () => {
            navigate("-1");
        } // go back to previous page
    );

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} />
    );
}


