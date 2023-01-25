import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { LinearProgress } from "@mui/material";

import { getCRUDItemPartialViewPropsStandalone } from "src/shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import { IAddressDataModel } from "src/dataModels/IAddressDataModel";
import ItemViewsPartial from "./ItemViewsPartial";
import { addressApi } from "src/apiClients/AddressApi";

export default function ItemPage(props: {viewItemTemplate: ViewItemTemplates}): JSX.Element {
    const navigate = useNavigate();

    const params = useParams()
    const addressID = parseInt(params.addressID, 10);
    const [item, setItem] = useState<IAddressDataModel>(null);

    useEffect(() => {
        addressApi.Get({addressID})
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

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<IAddressDataModel>(
        props.viewItemTemplate,
        () => { 
            navigate(-1); 
        } // go back to previous page
    );

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} item={item} />
    );
}


