import React from "react";
import { IconButton, TableCell, TableRow, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { ListDisplayOptions } from "../viewModels/CompareHtmlTableProps";

export default function CompareAvailabilityTableRow(props: { availabilitykey: string; availabilities: boolean[]; detailsTableRow: JSX.Element; listDisplayOption: ListDisplayOptions; selectedIndexes: number[]; }): JSX.Element {
    const { availabilitykey, availabilities, detailsTableRow, listDisplayOption, selectedIndexes } = props;
    const { t } = useTranslation();
    const [open, setOpen] = React.useState(false);
    return (
        <>
            <TableRow key={availabilitykey}>
                <TableCell component="th" align='left'>
                    <Typography component='h6' variant="subtitle1">
                        <IconButton
                            aria-label="expand row"
                            size="small"
                            onClick={() => setOpen(!open)}
                        >
                            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                        </IconButton>
                        {availabilitykey === "NULL" ? t("NotSpecifiec") : t(availabilitykey)}
                    </Typography>
                </TableCell>
                {availabilities?.filter((item, index) => listDisplayOption === ListDisplayOptions.All || selectedIndexes === null || selectedIndexes.some(tt => tt === index)).map((availability, availabilityIndex) => (
                    <TableCell key={availabilityIndex} align='center'>{availability ? <CheckCircleIcon sx={{ color: 'red' }} /> : ""}</TableCell>
                ))}
            </TableRow>
            {open && detailsTableRow}
        </>
    );
}
