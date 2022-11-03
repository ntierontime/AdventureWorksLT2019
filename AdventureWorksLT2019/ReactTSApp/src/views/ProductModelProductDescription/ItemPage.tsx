import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { LinearProgress } from "@mui/material";

import { getCRUDItemPartialViewPropsStandalone } from "src/shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import { IProductModelProductDescriptionDataModel } from "src/dataModels/IProductModelProductDescriptionDataModel";
import ItemViewsPartial from "./ItemViewsPartial";
import { productModelProductDescriptionApi } from "src/apiClients/ProductModelProductDescriptionApi";

export default function ItemPage(props: {viewItemTemplate: ViewItemTemplates}): JSX.Element {
    const navigate = useNavigate();

    const params = useParams()
    const productModelID = parseInt(params.productModelID, 10);
    const productDescriptionID = parseInt(params.productDescriptionID, 10);
    const culture = params.culture;
    const [item, setItem] = useState<IProductModelProductDescriptionDataModel>(null);

    useEffect(() => {
        productModelProductDescriptionApi.Get({productModelID, productDescriptionID, culture})
            .then((res)=>{
                if(res.status === "OK") {
                    setItem(res.responseBody)
                }
            })
            .finally(() => {});
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    if(item === null) {
        return (<LinearProgress />);
    }

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<IProductModelProductDescriptionDataModel>(
        props.viewItemTemplate,
        () => { 
            navigate(-1); 
        } // go back to previous page
    );

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} item={item} />
    );
}


