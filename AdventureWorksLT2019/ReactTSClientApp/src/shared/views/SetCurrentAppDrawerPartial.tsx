import { useEffect } from "react";
import { AppDrawerOptions, setCurrentAppDrawer } from "src/slices/userPreferenceDataSlice";
import { useDispatch } from "react-redux";
import { AppDispatch } from "src/store/Store";
import { AppDrawerSelection } from "src/slices/userPreferenceDataSlice";
import { useParams } from "react-router";
import { UIRouteLinkSetting } from "../dataModels/UIRouteLinkSetting";

export interface SetCurrentAppDrawerProps {
    selectedAppDrawerOption: AppDrawerOptions;
    currentAppDrawer: AppDrawerSelection;
    availableList:  UIRouteLinkSetting[]
}

// Table head with Client side Column Sort feature.
export function SetCurrentAppDrawerPartial(props: SetCurrentAppDrawerProps) {
    const { selectedAppDrawerOption, currentAppDrawer, availableList } = props;
    const dispatch = useDispatch<AppDispatch>();

    //const userPreference = useSelector((state: RootState) => state.userPreference);
    const params = useParams();
    const { _masterUniqueName_ } = params;
    useEffect(() => {
        if(!!availableList && currentAppDrawer?.option && currentAppDrawer?.option !== selectedAppDrawerOption) {
            const uiRouteLinkSetting = availableList.find(v => v.uniqueName === _masterUniqueName_);
            if(!!uiRouteLinkSetting) {
                dispatch(setCurrentAppDrawer({ option: selectedAppDrawerOption, uiRouteLinkSetting }));
            }
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return (
        <></>);
}