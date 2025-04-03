import { Badge, Chip, Tooltip, Typography } from "@mui/material";
import UndoOutlinedIcon from '@mui/icons-material/UndoOutlined';
import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { useTranslation } from "react-i18next";

export interface DataItemBadgedChipProps {
    label: string;
    tooltip: string;
    isDeleted: boolean;
    itemUIStatus: ItemUIStatus;
    minWidth?: string;
    handleItemClick?: () => void;
    handleItemDeleted?: () => void;
    handleItemUndoDeleted?: () => void;
}

export default function DataItemBadgedChip(props: DataItemBadgedChipProps): JSX.Element {
    const { itemUIStatus, isDeleted } = props;
    if (isDeleted)
        return <DataItemBadgedChipDeleted {...props} />
    if (itemUIStatus == ItemUIStatus.New)
        return <DataItemBadgedChipNew {...props} />
    if (itemUIStatus == ItemUIStatus.Updated)
        return <DataItemBadgedChipUpdated {...props} />
    return <DataItemBadgedChipNoChange {...props} />
}

export function DataItemBadgedChipNoChange(props: DataItemBadgedChipProps): JSX.Element {
    const { label, tooltip, handleItemClick, handleItemDeleted, minWidth = "100%" } = props;
    return (
        <Tooltip title={tooltip} sx={{ maxWidth: minWidth }}>
            <Chip style={{ minWidth: minWidth }}
                label={label}
                variant="outlined"
                onClick={handleItemClick}
                onDelete={handleItemDeleted}
            />
        </Tooltip>
    );
}

export function DataItemBadgedChipNew(props: DataItemBadgedChipProps): JSX.Element {
    const { t } = useTranslation();
    const { label, tooltip, handleItemClick, handleItemDeleted, minWidth = "100%" } = props;
    return (
        <Tooltip title={tooltip} sx={{ maxWidth: minWidth }}>
            <Badge badgeContent={t("New")} color="warning" style={{ minWidth: minWidth }}>
                <Chip style={{ minWidth: minWidth }}
                    label={label}
                    variant="outlined"
                    color="warning"
                    onClick={handleItemClick}
                    onDelete={handleItemDeleted}
                />
            </Badge>
        </Tooltip>
    );
}

export function DataItemBadgedChipDeleted(props: DataItemBadgedChipProps): JSX.Element {

    const { label, tooltip, handleItemClick, handleItemUndoDeleted, minWidth = "100%" } = props;
    return (
        <Tooltip title={tooltip} sx={{ maxWidth: minWidth }}>
            <Chip style={{ minWidth: minWidth }}
                label={<Typography variant="subtitle2" style={{ textDecoration: "line-through", }}>{label}</Typography>}
                variant="outlined"
                onClick={handleItemClick}
                onDelete={handleItemUndoDeleted}
                color="error"
                deleteIcon={<UndoOutlinedIcon />}
            />
        </Tooltip>
    );
}

export function DataItemBadgedChipUpdated(props: DataItemBadgedChipProps): JSX.Element {

    const { label, tooltip, handleItemClick, handleItemDeleted, minWidth = "100%" } = props;
    return (
        <Tooltip title={tooltip} sx={{ maxWidth: minWidth }}>
            <Badge variant="dot" color="success" style={{ minWidth: minWidth }}>
                <Chip style={{ minWidth: minWidth }}
                    label={label}
                    variant="outlined"
                    onClick={handleItemClick}
                    onDelete={handleItemDeleted}
                    color="success"
                />
            </Badge>
        </Tooltip>
    );
}