import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { LinearProgress } from "@mui/material";

import { getCRUDItemPartialViewPropsStandalone } from "src/shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import { IProductDescriptionDataModel } from "src/dataModels/IProductDescriptionDataModel";
import ItemViewsPartial from "./ItemViewsPartial";
import { productDescriptionApi } from "src/apiClients/ProductDescriptionApi";

export default function ItemPage(props: {viewItemTemplate: ViewItemTemplates}): JSX.Element {
    const navigate = useNavigate();

    const params = useParams()
    const productDescriptionID = parseInt(params.productDescriptionID, 10);
    const [item, setItem] = useState<IProductDescriptionDataModel>(null);

    useEffect(() => {
        productDescriptionApi.Get({productDescriptionID})
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

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<IProductDescriptionDataModel>(
        props.viewItemTemplate,
        () => { 
            navigate(-1); 
        } // go back to previous page
    );

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} item={item} />
    );
}


