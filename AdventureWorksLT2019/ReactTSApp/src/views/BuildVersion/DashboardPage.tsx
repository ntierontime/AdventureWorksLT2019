import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import { IProductDataModel } from "src/dataModels/IProductDataModel";
import { productApi } from "src/apiClients/ProductApi";
import { LinearProgress } from "@mui/material";

export default function DashboardPage(): JSX.Element {
    const navigate = useNavigate();

    const params = useParams()
    const productID = parseInt(params.productID, 10);

    useEffect(() => {
        productApi.GetCompositeModel({ productID })
            .then((res) => {
                console.log(res);
            })
            .finally(() => { });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (<LinearProgress />);
}


