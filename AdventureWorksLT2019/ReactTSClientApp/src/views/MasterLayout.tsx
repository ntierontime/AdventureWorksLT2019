import { styled } from '@mui/material/styles';
import { Backdrop, Box, CircularProgress } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import { RootState } from 'src/store/CombinedReducers';
import { AppDrawerOptions, setAppDrawerOpen } from 'src/slices/userPreferenceDataSlice';
import AppBar from 'src/views/AppBar';
import AppDrawer from 'src/views/AppDrawer';
import AppFooter from 'src/views/AppFooter';
import MasterRoutes from 'src/views/MasterRoutes';
import SetCurrentAppDrawerGeneratedRoutes from 'src/generated/views/SetCurrentAppDrawerGeneratedRoutes';

const DrawerHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
}));

export default function MasterLayout() {
    const { t } = useTranslation();
    const userPreference = useSelector((state: RootState) => state.userPreference);
    const auth = useSelector((state: RootState) => state.auth);
    const { loading, gettingTempToken } = useSelector((state: RootState) => state.app);
    const dispatch = useDispatch();

    const handleDrawerOpen = () => {
        dispatch(setAppDrawerOpen(true));
    };

    const handleDrawerClose = () => {
        dispatch(setAppDrawerOpen(false));
    };

    return (
        <>
            <SetCurrentAppDrawerGeneratedRoutes 

                currentAppDrawer={userPreference.currentAppDrawer} />      
            <Box sx={{ display: 'flex' }}>
                <AppBar open={userPreference.appDrawerOpen && auth && auth.isAuthenticated} title={t('AdventureWorksLT2019')} openDrawerHandler={handleDrawerOpen} />
                {(auth && auth.isAuthenticated && (!!userPreference?.currentAppDrawer && userPreference?.currentAppDrawer.option !== AppDrawerOptions.None)) &&
                    <AppDrawer open={userPreference.appDrawerOpen} closeDrawerHandler={handleDrawerClose} appDrawerSelection={userPreference.currentAppDrawer} />
                }
                <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                    <DrawerHeader />
                    {!gettingTempToken && <MasterRoutes />}
                    {(!!!auth || !auth.isAuthenticated) && !!!loading && <AppFooter />}
                </Box>
            </Box>
            <Backdrop
                sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                open={loading}>
                <CircularProgress color="inherit" />
            </Backdrop>
      </>
    );
}

