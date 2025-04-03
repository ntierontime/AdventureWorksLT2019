import { List } from '@mui/material';
import { useTheme, } from '@mui/material/styles';

import { useNavigate } from 'react-router-dom';

import { useTranslation } from 'react-i18next';
import { AppDrawerProps } from 'src/shared/views/appDrawer/Drawer';
import AppDrawerLinkItem, { AppDrawerLinkItemProps } from 'src/shared/views/appDrawer/AppDrawerLinkItem';

// TODO: For testing purpose, developer can remove this list and related code
export const appDrawerItems_Home = [
    // {
    //     label: 'ServicesAndSpecializations',
    //     icon: <HelpOutlineIcon />,
    //     url: '/Consumer/CreateWizard',
    // },
] as AppDrawerLinkItemProps[];

export default function AppDrawerHome(props: AppDrawerProps) {
    // const theme = useTheme();
    // const { t } = useTranslation();
    // const navigate = useNavigate();

    return <>
        <List>
            {appDrawerItems_Home.map((item, index) => {
                return <AppDrawerLinkItem {...item} appDrawerOpen={props.open} />;
            }
            )}
        </List>
    </>;
}