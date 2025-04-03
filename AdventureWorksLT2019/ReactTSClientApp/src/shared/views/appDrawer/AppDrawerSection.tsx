import { Accordion, AccordionDetails, AccordionSummary, List, Typography } from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

import { AppDrawerSelection } from "src/slices/userPreferenceDataSlice";
import AppDrawerLinkItem, { AppDrawerLinkItemProps } from "./AppDrawerLinkItem";

export interface AppDrawerSectionProps {
    section: AppDrawerLinkItemProps;
    items: AppDrawerLinkItemProps[];
    appDrawerSelection: AppDrawerSelection;
}

export default function AppDrawerSection(props: AppDrawerSectionProps): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();

    const { section, items, appDrawerSelection } = props;

    return <>
        {!!section && !!items && <Accordion>
            <AccordionSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel1a-content"
                id="panel1a-header"
            >
                {section.icon}
                {section.appDrawerOpen && <Typography paddingLeft={1}>{t(section.label)}</Typography>}
            </AccordionSummary>
            <AccordionDetails>
                <List>
                    {!!items && items.map((item, index) => {
                        return <AppDrawerLinkItem key={item.label} {...item } urlRouteParams={appDrawerSelection?.uiRouteLinkSetting?.uniqueName} appDrawerOpen={section?.appDrawerOpen} />;
                    }
                    )}
                </List>
            </AccordionDetails>
        </Accordion>}
    </>;
}