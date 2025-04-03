import { Checkbox, ListItem, ListItemButton, ListItemIcon, ListItemText, Tooltip } from "@mui/material";
import DataItemBadgedChip, { DataItemBadgedChipProps } from "./DataItemBadgedChip";

export interface DataItemBadgedChipWithCheckBoxProps extends DataItemBadgedChipProps {
    id: string;
    sx?: any;
    checked: boolean;
    displayDataItemBadgedChip: boolean;
    handleToggle?: () => void;
}

export function DataItemBadgedChipWithCheckBox(props: DataItemBadgedChipWithCheckBoxProps): JSX.Element {
    const { 
        id, checked, 
        sx,
        displayDataItemBadgedChip,
        handleToggle,
        itemUIStatus, isDeleted, 
        label, tooltip, 
        handleItemClick, handleItemDeleted, 
        minWidth = "100%" } = props;
    return (
        <ListItem disablePadding sx={sx}>
            <ListItemButton role={undefined} onClick={handleToggle} sx={{paddingTop:0, paddingBottom:0}}>
                <ListItemIcon sx={{ minWidth: 25 }}>
                    <Checkbox
                        edge="start"
                        checked={checked}
                        tabIndex={-1}
                        disableRipple
                        inputProps={{ 'aria-labelledby': id }}
                    />
                </ListItemIcon>
                {displayDataItemBadgedChip && <DataItemBadgedChip
                    minWidth="90%"
                    label={label}
                    tooltip={tooltip}
                    isDeleted={isDeleted}
                    itemUIStatus={itemUIStatus}
                    handleItemClick={handleItemClick}
                    handleItemDeleted={handleItemDeleted}
                />}
                {!displayDataItemBadgedChip &&
                    <Tooltip title={tooltip}>
                        <ListItemText id={id} primary={label} />
                    </Tooltip>}
            </ListItemButton>
        </ListItem>
    );
}
