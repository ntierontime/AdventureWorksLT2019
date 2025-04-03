import { Accordion, AccordionDetails, AccordionSummary, Divider, List, ListItemButton, ListItemIcon, ListItemText, Typography } from '@mui/material';
import { useTheme, } from '@mui/material/styles';

import { useNavigate } from 'react-router-dom';

import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

import HelpOutlineIcon from '@mui/icons-material/HelpOutline';

import { useTranslation } from 'react-i18next';
import { AppDrawerProps } from 'src/shared/views/appDrawer/Drawer';

// TODO: For testing purpose, developer can remove this list and related code
const appDrawerItems_InfoCompositePages = [

] as {label: string, icon: any, url: string}[];
	
// TODO: For testing purpose, developer can remove this list and related code
const appDrawerItems_CreateWizardPages = [

] as {label: string, icon: any, url: string}[];

// TODO: For testing purpose, developer can remove this list and related code
const appDrawerItems_CreatePages = [

] as {label: string, icon: any, url: string}[];

// TODO: developer should customize AppDrawer Items
// TODO: For testing purpose, developer can remove this list and related code
const appDrawerItems_IndexPages = [
        {
            label: 'ErrorLogs',
            icon: <HelpOutlineIcon />,
            url: '/ErrorLogs',
        },
        {
            label: 'Addresses',
            icon: <HelpOutlineIcon />,
            url: '/Addresses',
        },
        {
            label: 'Customers',
            icon: <HelpOutlineIcon />,
            url: '/Customers',
        },
        {
            label: 'Products',
            icon: <HelpOutlineIcon />,
            url: '/Products',
        },
        {
            label: 'ProductCategories',
            icon: <HelpOutlineIcon />,
            url: '/ProductCategories',
        },
        {
            label: 'ProductDescriptions',
            icon: <HelpOutlineIcon />,
            url: '/ProductDescriptions',
        },
        {
            label: 'ProductModels',
            icon: <HelpOutlineIcon />,
            url: '/ProductModels',
        },
        {
            label: 'SalesOrderHeaders',
            icon: <HelpOutlineIcon />,
            url: '/SalesOrderHeaders',
        },
] as {label: string, icon: any, url: string}[];

export default function AppDrawerGeneratedLinks(props: AppDrawerProps) {
    const navigate = useNavigate();

    return <>
        {!!appDrawerItems_InfoCompositePages && <Accordion>
            <AccordionSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel1a-content"
                id="panel1a-header"
            >
                <HelpOutlineIcon />
                {props.open && <Typography>InfoComposite List Pages</Typography>}
            </AccordionSummary>
            <AccordionDetails>
                <List>
                    {appDrawerItems_InfoCompositePages.map((item, index) => (
                        <ListItemButton
                            key={item.label}
                            sx={{
                                minHeight: 48,
                                justifyContent: props.open ? 'initial' : 'center',
                                px: 2.5,
                            }}
                            onClick={() => { navigate(item.url); } }
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
                            {props.open && <ListItemText primary={item.label} sx={{ opacity: props.open ? 1 : 0 }} />}
                        </ListItemButton>
                    ))}
                </List>
            </AccordionDetails>
        </Accordion>}
        <Divider />
        {!!appDrawerItems_CreateWizardPages && <Accordion>
            <AccordionSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel1a-content"
                id="panel1a-header"
            >
                <HelpOutlineIcon />
                {props.open && <Typography>Create Wizard Pages</Typography>}
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
                            onClick={() => { navigate(item.url); } }
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
                            {props.open && <ListItemText primary={item.label} sx={{ opacity: props.open ? 1 : 0 }} />}
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
                            onClick={() => { navigate(item.url); } }
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
                            onClick={() => { navigate(item.url); } }
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
    </>;
}



