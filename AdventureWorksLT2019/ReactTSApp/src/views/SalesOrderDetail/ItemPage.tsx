import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { LinearProgress } from "@mui/material";

import { getCRUDItemPartialViewPropsStandalone } from "src/shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import { ISalesOrderDetailDataModel } from "src/dataModels/ISalesOrderDetailDataModel";
import ItemViewsPartial from "./ItemViewsPartial";
import { salesOrderDetailApi } from "src/apiClients/SalesOrderDetailApi";

export default function ItemPage(props: {viewItemTemplate: ViewItemTemplates}): JSX.Element {
    const navigate = useNavigate();

    const params = useParams()
    const salesOrderID = parseInt(params.salesOrderID, 10);
    const salesOrderDetailID = parseInt(params.salesOrderDetailID, 10);
    const [item, setItem] = useState<ISalesOrderDetailDataModel>(null);

    useEffect(() => {
        salesOrderDetailApi.Get({salesOrderID, salesOrderDetailID})
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

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<ISalesOrderDetailDataModel>(
        props.viewItemTemplate,
        () => { 
            navigate(-1); 
        } // go back to previous page
    );

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} item={item} />
    );
}


