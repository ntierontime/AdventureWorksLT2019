import { useEffect, useState } from 'react';
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import { useDispatch, useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import { setIsAuthenticated } from 'src/shared/slices/authenticationSlice';
import AppBar from 'src/shared/views/AppBar';
import AppDrawer from 'src/views/AppDrawer';
import { RootState } from 'src/store/CombinedReducers';
import MasterRoutes from './MasterRoutes';
import AppFooter from 'src/shared/views/AppFooter';

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
    const auth = useSelector((state: RootState) => state.auth);
    const dispatch = useDispatch();

    const [drawerOpen, setDrawerOpen] = useState(true);

    const handleDrawerOpen = () => {
        setDrawerOpen(true);
    };

    const handleDrawerClose = () => {
        setDrawerOpen(false);
    };

    useEffect(() => {
        const loggedInUser = localStorage.getItem("user");
        if (loggedInUser) {
            JSON.parse(loggedInUser);
            dispatch(setIsAuthenticated());
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <Box sx={{ display: 'flex' }}>
            <AppBar open={drawerOpen && auth && auth.isAuthenticated} title={t('AdventureWorksLT2019')} openDrawerHandler={handleDrawerOpen} />
            {(auth && auth.isAuthenticated) &&
                <AppDrawer open={drawerOpen} closeDrawerHandler={handleDrawerClose} appDrawerItems={[]} />
            }
            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                <DrawerHeader />
                <MasterRoutes />
                <AppFooter />
            </Box>
        </Box>
    );
}

