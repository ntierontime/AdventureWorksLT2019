import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { LinearProgress } from "@mui/material";
import { useTranslation } from "react-i18next";

import { getCRUDItemPartialViewPropsStandalone } from "src/shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import { IProductCategoryDataModel } from "src/dataModels/IProductCategoryDataModel";
import ItemViewsPartial from "./ItemViewsPartial";
import { productCategoryApi } from "src/apiClients/ProductCategoryApi";

export default function ItemPage(props: {viewItemTemplate: ViewItemTemplates}): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();

    const params = useParams()
    const productCategoryID = parseInt(params.productCategoryID, 10);
    const [item, setItem] = useState<IProductCategoryDataModel>(null);

    useEffect(() => {
        productCategoryApi.Get({productCategoryID})
            .then((res)=>{
                if(res.status === "OK") {
                    setItem(res.responseBody);

					// // if you want to change page title <html><head><title>...</title></head></html>
                    // document.title = props.viewItemTemplate === ViewItemTemplates.Edit 
                    //     ? t("Edit") + res.responseBody.
                    //     : res.responseBody.;
                }
            })
            .finally(() => {});
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    if(item === null) {
        return (<LinearProgress />);
    }

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<IProductCategoryDataModel>(
        props.viewItemTemplate,
        () => { 
            navigate(-1); 
        } // go back to previous page
    );

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} item={item} />
    );
}


