import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';

import AppBar from 'src/shared/views/AppBar';
import AppDrawer from 'src/views/AppDrawer';
import { useDispatch, useSelector } from 'react-redux';
import { closeAppDrawer, openAppDrawer } from 'src/slices/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { Link } from 'react-router-dom';
import MasterRoutes from './MasterRoutes';
import { Backdrop, CircularProgress } from '@mui/material';

const DrawerHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
}));

export default function MasterLayout() {
    const app = useSelector((state: RootState) => state.app);
    const auth = useSelector((state: RootState) => state.auth);
    const dispatch = useDispatch();

    // const [open, setOpen] = React.useState(false);

    const handleDrawerOpen = () => {
        dispatch(openAppDrawer());
    };

    const handleDrawerClose = () => {
        dispatch(closeAppDrawer());
    };

    return (
        <Box sx={{ display: 'flex' }}>
            {(auth && auth.isAuthenticated) &&
                <>
                    <AppBar open={app.drawerOpen} title={'abcdefg'} openDrawerHandler={handleDrawerOpen} />
                    <AppDrawer open={app.drawerOpen} closeDrawerHandler={handleDrawerClose} appDrawerItems={[]} />
                </>
            }

            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                {(auth && auth.isAuthenticated) &&
                    <DrawerHeader />
                }
                <nav>
                    <Link to="/account/login">login</Link>
                    <Link to="/PrivateRouteTestPage">PrivateRouteTestPage</Link>
                    <Link to="/product">product</Link>
                </nav>
                <MasterRoutes />
            </Box>
            <Backdrop open={app.loading}>
                <CircularProgress color="inherit" />
            </Backdrop>
        </Box>
    );
}