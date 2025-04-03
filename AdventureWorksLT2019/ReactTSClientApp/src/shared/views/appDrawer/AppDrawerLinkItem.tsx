import { ListItemButton, ListItemIcon, ListItemText } from "@mui/material";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

export interface AppDrawerLinkItemProps {
    label: string;
    icon: any;
    url: string;
    urlRouteParams: string | null
    appDrawerOpen: boolean;
    selected?: boolean;
}

export default function AppDrawerLinkItem(props: AppDrawerLinkItemProps): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();
    
    const {label, icon, url, urlRouteParams, appDrawerOpen, selected = false } = props; 
    const urlWithRouteParams = !!!urlRouteParams ? url : url + "/" + urlRouteParams;
    return <ListItemButton
        key={label}
        sx={{
            minHeight: 36,
            maxHeight: 36,
            justifyContent: appDrawerOpen ? 'initial' : 'center',
            px: 0.5,
            "&.Mui-selected": {
                backgroundColor: "#2e8b57!important"
            },
        }}
        selected={selected}
        href={urlWithRouteParams}
        //onClick={() => { navigate(urlWithRouteParams); } }
    >
        <ListItemIcon
            sx={{
                minWidth: 0,
                mr: appDrawerOpen ? 3 : 'auto',
                justifyContent: 'center',
            }}
        >
            {icon}
        </ListItemIcon>
        {appDrawerOpen && <ListItemText primary={t(label)} sx={{ opacity: appDrawerOpen ? 1 : 0 }} />}
    </ListItemButton>;
}