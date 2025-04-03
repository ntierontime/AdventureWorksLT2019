import { Divider, IconButton, } from '@mui/material';
import { useTheme } from '@mui/material/styles';

import { Route, Routes, useNavigate } from 'react-router-dom';

import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';

import { useTranslation } from 'react-i18next';
import AppDrawerGeneratedLinks from '../generated/views/AppDrawerGeneratedLinks';
import { AppDrawerProps, Drawer, DrawerHeader } from 'src/shared/views/appDrawer/Drawer';

import AppDrawerHome from './AppDrawerHome';
import { useSelector } from 'react-redux';
import { RootState } from 'src/store/CombinedReducers';
import { AppDrawerOptions } from 'src/slices/userPreferenceDataSlice';




function AppDrawerMasterRoutes(props: AppDrawerProps): JSX.Element {
    return (
        <Routes>
            
            <Route path="*" element={<></>} />
        </Routes>);
}

export default function AppDrawer(props: AppDrawerProps) {
    const userPreference = useSelector((state: RootState) => state.userPreference);
    const theme = useTheme();
    const { t } = useTranslation();
    const navigate = useNavigate();

    return (
        <Drawer variant="permanent" open={props.open}>
            <DrawerHeader>
                <IconButton onClick={props.closeDrawerHandler}>
                    {theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
                </IconButton>
            </DrawerHeader>
            <AppDrawerMasterRoutes {...props}/>
            <Divider />
            {(!!!userPreference?.currentAppDrawer || userPreference?.currentAppDrawer.option === AppDrawerOptions.None) && 
                <AppDrawerHome {...props}/>
            }
            {/* {!!userPreference?.currentAppDrawer?.uiRouteLinkSetting && userPreference?.currentAppDrawer?.option === AppDrawerOptions.BusinessCenter &&
                <AppDrawerBusinessCenter  {...props}/>
            }
            {!!userPreference?.currentAppDrawer?.uiRouteLinkSetting && userPreference?.currentAppDrawer?.option === AppDrawerOptions.EmployeeCenter &&
                <AppDrawerEmployeeCenter  {...props}/>
            }
            {(!!!userPreference?.currentAppDrawer || userPreference?.currentAppDrawer.option === AppDrawerOptions.ConsumerCenter) && 
                <AppDrawerConsumerCenter  {...props}/>
            } */}
            <Divider />
            <AppDrawerGeneratedLinks {...props}/>
        </Drawer>
    );
}

