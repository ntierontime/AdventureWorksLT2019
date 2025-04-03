import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { Routes, Route } from "react-router-dom";
import { UIRouteLinkSetting } from "src/shared/dataModels/UIRouteLinkSetting";
import { AppDrawerOptions, AppDrawerSelection, setCurrentAppDrawer } from "src/slices/userPreferenceDataSlice";
import { AppDispatch } from "src/store/Store";



export interface SetCurrentAppDrawerGeneratedRoutesProps {
    currentAppDrawer: AppDrawerSelection;

}

function SetNoAppDrawerRoutes(props: {currentAppDrawer: AppDrawerSelection}): JSX.Element {
    const { currentAppDrawer } = props;
    const dispatch = useDispatch<AppDispatch>();
    useEffect(() => {
        if(currentAppDrawer == null || currentAppDrawer.option !== AppDrawerOptions.None) {
            dispatch(setCurrentAppDrawer({ option: AppDrawerOptions.None, uiRouteLinkSetting: null }));
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);  
    return (<></>);
}

export default function SetCurrentAppDrawerGeneratedRoutes(props: SetCurrentAppDrawerGeneratedRoutesProps): JSX.Element {
    const {
        currentAppDrawer,

    } = props;
    
    return (
        <Routes>

            <Route path="*" element={ <SetNoAppDrawerRoutes currentAppDrawer={currentAppDrawer} /> } />
        </Routes>);
}

