import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";

import { getCRUDItemPartialViewPropsStandalone } from "src/shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import ItemViewsPartial from "./ItemViewsPartial";
import { IErrorLogDataModel } from "src/dataModels/IErrorLogDataModel";

export default function CreatePage(): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<IErrorLogDataModel>(
        ViewItemTemplates.Create,
        () => {
            navigate("-1");
        } // go back to previous page
    );

    // // if you want to change page title <html><head><title>...</title></head></html>
    // useEffect(() => {
    //     document.title = t("Create") + " " + t("ErrorLog");
    // }, []);

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} />
    );
}


