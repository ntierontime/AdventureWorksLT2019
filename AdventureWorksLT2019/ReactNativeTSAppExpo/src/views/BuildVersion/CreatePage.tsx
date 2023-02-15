import { useNavigation } from "@react-navigation/native";

import { getCRUDItemPartialViewPropsStandalone } from "../../shared/viewModels/ItemPartialViewProps";
import { ViewItemTemplates } from "../../shared/viewModels/ViewItemTemplates";

import ItemViewsPartial from "./ItemViewsPartial";
import { defaultBuildVersion, IBuildVersionDataModel } from "../../dataModels/IBuildVersionDataModel";

export default function CreatePage(): JSX.Element {
    const navigation = useNavigation();
    const crudItemPartialViewProps = getCRUDItemPartialViewPropsStandalone<IBuildVersionDataModel>(
        ViewItemTemplates.Create,
        () => {
            navigation.goBack();
        } // go back to previous page
    );
    crudItemPartialViewProps.item=defaultBuildVersion();
    return (
        <ItemViewsPartial {...crudItemPartialViewProps} />
    );
}


