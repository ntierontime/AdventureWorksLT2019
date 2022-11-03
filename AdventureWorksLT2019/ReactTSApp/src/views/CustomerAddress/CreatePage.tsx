import { useNavigate } from "react-router-dom";

import { getCRUDItemPartialViewPropsStandalone } from "src/shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import ItemViewsPartial from "./ItemViewsPartial";
import { ICustomerAddressDataModel } from "src/dataModels/ICustomerAddressDataModel";

export default function CreatePage(): JSX.Element {
    const navigate = useNavigate();
    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<ICustomerAddressDataModel>(
        ViewItemTemplates.Create,
        () => {
            navigate("-1");
        } // go back to previous page
    );

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} />
    );
}


