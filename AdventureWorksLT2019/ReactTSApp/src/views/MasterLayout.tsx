import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';

import AppBar from 'src/shared/views/AppBar';
import AppDrawer from 'src/views/AppDrawer';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from 'src/store/CombinedReducers';
import MasterRoutes from './MasterRoutes';
import { useState } from 'react';

const DrawerHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
}));

export default function MasterLayout() {
    const auth = useSelector((state: RootState) => state.auth);
    const dispatch = useDispatch();

    const [drawerOpen, setDrawerOpen] = useState(true);

    const handleDrawerOpen = () => {
        setDrawerOpen(true);
    };

    const handleDrawerClose = () => {
        setDrawerOpen(false);
    };

    return (
        <Box sx={{ display: 'flex' }}>
            {(auth && auth.isAuthenticated) &&
                <>
                    <AppBar open={drawerOpen} title={'AdventureWorksLT2019'} openDrawerHandler={handleDrawerOpen} />
                    <AppDrawer open={drawerOpen} closeDrawerHandler={handleDrawerClose} appDrawerItems={[]} />
                </>
            }

            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                {(auth && auth.isAuthenticated) &&
                    <DrawerHeader />
                }
                <MasterRoutes />
            </Box>
        </Box>
    );
}

