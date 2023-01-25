import { useNavigate } from "react-router-dom";

import { getCRUDItemPartialViewPropsStandalone } from "src/shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import ItemViewsPartial from "./ItemViewsPartial";
import { ISalesOrderHeaderDataModel } from "src/dataModels/ISalesOrderHeaderDataModel";

export default function CreatePage(): JSX.Element {
    const navigate = useNavigate();
    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<ISalesOrderHeaderDataModel>(
        ViewItemTemplates.Create,
        () => {
            navigate("-1");
        } // go back to previous page
    );

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} />
    );
}


