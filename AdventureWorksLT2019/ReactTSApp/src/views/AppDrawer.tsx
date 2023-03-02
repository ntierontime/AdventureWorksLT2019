import { Accordion, AccordionDetails, AccordionSummary, Divider, IconButton, List, ListItemButton, ListItemIcon, ListItemText, Typography } from '@mui/material';
import MuiDrawer from '@mui/material/Drawer';
import { styled, useTheme, Theme, CSSObject } from '@mui/material/styles';

import { useNavigate } from 'react-router-dom';

import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

import HelpOutlineIcon from '@mui/icons-material/HelpOutline';

import { drawerWidth } from 'src/shared/constants';
import { AppDrawerItem } from 'src/shared/viewModels/AppDrawerItem';
import { useTranslation } from 'react-i18next';

interface AppDrawerProps {
    open?: boolean;
    closeDrawerHandler?: () => void;
    appDrawerItems: AppDrawerItem[];
}

const openedMixin = (theme: Theme): CSSObject => ({
    width: drawerWidth,
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
    }),
    overflowX: 'hidden',
});

const closedMixin = (theme: Theme): CSSObject => ({
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: `calc(${theme.spacing(7)} + 1px)`,
    [theme.breakpoints.up('sm')]: {
        width: `calc(${theme.spacing(8)} + 1px)`,
    },
});

const DrawerHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
}));

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
    ({ theme, open }) => ({
        width: drawerWidth,
        flexShrink: 0,
        whiteSpace: 'nowrap',
        boxSizing: 'border-box',
        ...(open && {
            ...openedMixin(theme),
            '& .MuiDrawer-paper': openedMixin(theme),
        }),
        ...(!open && {
            ...closedMixin(theme),
            '& .MuiDrawer-paper': closedMixin(theme),
        }),
    }),
);

export default function AppDrawer(props: AppDrawerProps) {
    const theme = useTheme();
    const { t } = useTranslation();
    const navigate = useNavigate();

    // TODO: developer should customize AppDrawer Items
    // TODO: For testing purpose, developer can remove this list and related code
    const appDrawerItems_IndexPages = [
        {
            label: 'BuildVersion',
            icon: <HelpOutlineIcon />,
            url: '/BuildVersion',
        },
        {
            label: 'ErrorLog',
            icon: <HelpOutlineIcon />,
            url: '/ErrorLog',
        },
        {
            label: 'Address',
            icon: <HelpOutlineIcon />,
            url: '/Address',
        },
        {
            label: 'Customer',
            icon: <HelpOutlineIcon />,
            url: '/Customer',
        },
        {
            label: 'CustomerAddress',
            icon: <HelpOutlineIcon />,
            url: '/CustomerAddress',
        },
        {
            label: 'Product',
            icon: <HelpOutlineIcon />,
            url: '/Product',
        },
        {
            label: 'ProductCategory',
            icon: <HelpOutlineIcon />,
            url: '/ProductCategory',
        },
        {
            label: 'ProductDescription',
            icon: <HelpOutlineIcon />,
            url: '/ProductDescription',
        },
        {
            label: 'ProductModel',
            icon: <HelpOutlineIcon />,
            url: '/ProductModel',
        },
        {
            label: 'ProductModelProductDescription',
            icon: <HelpOutlineIcon />,
            url: '/ProductModelProductDescription',
        },
        {
            label: 'SalesOrderDetail',
            icon: <HelpOutlineIcon />,
            url: '/SalesOrderDetail',
        },
        {
            label: 'SalesOrderHeader',
            icon: <HelpOutlineIcon />,
            url: '/SalesOrderHeader',
        },
    ] as {label: string, icon: any, url: string}[];;


    // TODO: For testing purpose, developer can remove this list and related code
    const appDrawerItems_CreatePages = [
        {
            label: 'New BuildVersion',
            icon: <HelpOutlineIcon />,
            url: '/BuildVersion/Create',
        },
        {
            label: 'New ErrorLog',
            icon: <HelpOutlineIcon />,
            url: '/ErrorLog/Create',
        },
        {
            label: 'New Address',
            icon: <HelpOutlineIcon />,
            url: '/Address/Create',
        },
        {
            label: 'New Customer',
            icon: <HelpOutlineIcon />,
            url: '/Customer/Create',
        },
        {
            label: 'New CustomerAddress',
            icon: <HelpOutlineIcon />,
            url: '/CustomerAddress/Create',
        },
        {
            label: 'New Product',
            icon: <HelpOutlineIcon />,
            url: '/Product/Create',
        },
        {
            label: 'New ProductCategory',
            icon: <HelpOutlineIcon />,
            url: '/ProductCategory/Create',
        },
        {
            label: 'New ProductDescription',
            icon: <HelpOutlineIcon />,
            url: '/ProductDescription/Create',
        },
        {
            label: 'New ProductModel',
            icon: <HelpOutlineIcon />,
            url: '/ProductModel/Create',
        },
        {
            label: 'New ProductModelProductDescription',
            icon: <HelpOutlineIcon />,
            url: '/ProductModelProductDescription/Create',
        },
        {
            label: 'New SalesOrderDetail',
            icon: <HelpOutlineIcon />,
            url: '/SalesOrderDetail/Create',
        },
        {
            label: 'New SalesOrderHeader',
            icon: <HelpOutlineIcon />,
            url: '/SalesOrderHeader/Create',
        },
    ] as {label: string, icon: any, url: string}[];;
	
    // TODO: For testing purpose, developer can remove this list and related code
    const appDrawerItems_CreateWizardPages = [
        {
            label: 'BuildVersion Wizard',
            icon: <HelpOutlineIcon />,
            url: '/BuildVersion/CreateWizard',
        },
        {
            label: 'ErrorLog Wizard',
            icon: <HelpOutlineIcon />,
            url: '/ErrorLog/CreateWizard',
        },
        {
            label: 'Address Wizard',
            icon: <HelpOutlineIcon />,
            url: '/Address/CreateWizard',
        },
        {
            label: 'Customer Wizard',
            icon: <HelpOutlineIcon />,
            url: '/Customer/CreateWizard',
        },
        {
            label: 'CustomerAddress Wizard',
            icon: <HelpOutlineIcon />,
            url: '/CustomerAddress/CreateWizard',
        },
        {
            label: 'Product Wizard',
            icon: <HelpOutlineIcon />,
            url: '/Product/CreateWizard',
        },
        {
            label: 'ProductCategory Wizard',
            icon: <HelpOutlineIcon />,
            url: '/ProductCategory/CreateWizard',
        },
        {
            label: 'ProductDescription Wizard',
            icon: <HelpOutlineIcon />,
            url: '/ProductDescription/CreateWizard',
        },
        {
            label: 'ProductModel Wizard',
            icon: <HelpOutlineIcon />,
            url: '/ProductModel/CreateWizard',
        },
        {
            label: 'ProductModelProductDescription Wizard',
            icon: <HelpOutlineIcon />,
            url: '/ProductModelProductDescription/CreateWizard',
        },
        {
            label: 'SalesOrderDetail Wizard',
            icon: <HelpOutlineIcon />,
            url: '/SalesOrderDetail/CreateWizard',
        },
        {
            label: 'SalesOrderHeader Wizard',
            icon: <HelpOutlineIcon />,
            url: '/SalesOrderHeader/CreateWizard',
        },
    ] as {label: string, icon: any, url: string}[];;

    return (
        <Drawer variant="permanent" open={props.open}>
            <DrawerHeader>
                <IconButton onClick={props.closeDrawerHandler}>
                    {theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
                </IconButton>
            </DrawerHeader>
            <Divider />
            {!!appDrawerItems_CreateWizardPages && <Accordion>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel1a-content"
                    id="panel1a-header"
                >
					<HelpOutlineIcon />
                    <Typography>Create Wizard Pages</Typography>
                </AccordionSummary>
                <AccordionDetails>
                    <List>
                        {appDrawerItems_CreateWizardPages.map((item, index) => (
                            <ListItemButton
                                key={item.label}
                                sx={{
                                    minHeight: 48,
                                    justifyContent: props.open ? 'initial' : 'center',
                                    px: 2.5,
                                }}
                                onClick={() => { navigate(item.url) }}
                            >
                                <ListItemIcon
                                    sx={{
                                        minWidth: 0,
                                        mr: props.open ? 3 : 'auto',
                                        justifyContent: 'center',
                                    }}
                                >
                                    {item.icon}
                                </ListItemIcon>
                                <ListItemText primary={item.label} sx={{ opacity: props.open ? 1 : 0 }} />
                            </ListItemButton>
                        ))}
                    </List>
                </AccordionDetails>
            </Accordion>}
            <Divider />
            {!!appDrawerItems_IndexPages && <Accordion>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel1a-content"
                    id="panel1a-header"
                >
					<HelpOutlineIcon />
                    <Typography>Index Pages</Typography>
                </AccordionSummary>
                <AccordionDetails>
                    <List>
                        {appDrawerItems_IndexPages.map((item, index) => (
                            <ListItemButton
                                key={item.label}
                                sx={{
                                    minHeight: 48,
                                    justifyContent: props.open ? 'initial' : 'center',
                                    px: 2.5,
                                }}
                                onClick={() => { navigate(item.url) }}
                            >
                                <ListItemIcon
                                    sx={{
                                        minWidth: 0,
                                        mr: props.open ? 3 : 'auto',
                                        justifyContent: 'center',
                                    }}
                                >
                                    {item.icon}
                                </ListItemIcon>
                                <ListItemText primary={item.label} sx={{ opacity: props.open ? 1 : 0 }} />
                            </ListItemButton>
                        ))}
                    </List>
                </AccordionDetails>
            </Accordion>}
            <Divider />
            {!!appDrawerItems_CreatePages && <Accordion>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel2a-content"
                    id="panel2a-header"
                >
					<HelpOutlineIcon />
                    <Typography>Create New Pages</Typography>
                </AccordionSummary>
                <AccordionDetails>
                    <List>
                        {appDrawerItems_CreatePages.map((item, index) => (
                            <ListItemButton
                                key={item.label}
                                sx={{
                                    minHeight: 48,
                                    justifyContent: props.open ? 'initial' : 'center',
                                    px: 2.5,
                                }}
                                onClick={() => { navigate(item.url) }}
                            >
                                <ListItemIcon
                                    sx={{
                                        minWidth: 0,
                                        mr: props.open ? 3 : 'auto',
                                        justifyContent: 'center',
                                    }}
                                >
                                    {item.icon}
                                </ListItemIcon>
                                <ListItemText primary={item.label} sx={{ opacity: props.open ? 1 : 0 }} />
                            </ListItemButton>
                        ))}
                    </List>
                </AccordionDetails>
            </Accordion>}
        </Drawer>
    );
}

