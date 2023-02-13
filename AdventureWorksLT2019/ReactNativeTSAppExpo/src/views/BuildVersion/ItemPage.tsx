import { useEffect, useState } from "react";
import { ActivityIndicator } from "react-native-paper";
import { useNavigation, useRoute } from "@react-navigation/native";

import { getCRUDItemPartialViewPropsStandalone } from "../../shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "../../shared/viewModels/ViewItemTemplates";

import { IBuildVersionDataModel } from "../../dataModels/IBuildVersionDataModel";
import ItemViewsPartial from "./ItemViewsPartial";
import { buildVersionApi } from "../../apiClients/BuildVersionApi";
import { IBuildVersionIdentifier } from "../../dataModels/IBuildVersionQueries";

function ItemPage(props: {viewItemTemplate: ViewItemTemplates}): JSX.Element {
    const navigation = useNavigation();
    const route = useRoute();
    const [item, setItem] = useState<IBuildVersionDataModel>(null);

    const {systemInformationID, versionDate, modifiedDate} = route.params as IBuildVersionIdentifier;

    useEffect(() => {
        buildVersionApi.Get({systemInformationID, versionDate, modifiedDate})
            .then((res)=>{
                if(res.status === "OK") {
                    setItem(res.responseBody)
                }
            })
            .finally(() => {});
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    if(item === null) {
        return (<ActivityIndicator />);
    }

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<IBuildVersionDataModel>(
        props.viewItemTemplate,
        () => { 
            navigation.goBack(); 
        } // go back to previous page
    );

    return (
        <ItemViewsPartial {...crudItemPartialViewProps} item={item} />
    );
}

ItemPage.title = "BuildVersionItemPage";

export default ItemPage;