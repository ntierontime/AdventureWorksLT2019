import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { LinearProgress } from "@mui/material";

import { getCRUDItemPartialViewPropsStandalone } from "src/shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import { IErrorLogDataModel } from "src/dataModels/IErrorLogDataModel";
import ItemViewsPartial from "./ItemViewsPartial";
import { errorLogApi } from "src/apiClients/ErrorLogApi";

export default function ItemPage(props: {viewItemTemplate: ViewItemTemplates}): JSX.Element {
    const navigate = useNavigate();

    const params = useParams()
    const errorLogID = parseInt(params.errorLogID, 10);
    const [item, setItem] = useState<IErrorLogDataModel>(null);

    useEffect(() => {
        errorLogApi.Get({errorLogID})
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

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<IErrorLogDataModel>(
        props.viewItemTemplate,
        () => { 
            navigate(-1); 
        } // go back to previous page
    );

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} item={item} />
    );
}


