import React from "react";
import { IconButton, TableCell, TableRow, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';

export default function CompareAvailabilityTableRow(props: { Availabilitykey: string; Availabilities: boolean[]; detailsTableRow: JSX.Element }): JSX.Element {
    const { t } = useTranslation();
    const [open, setOpen] = React.useState(false);
    return (
        <>
            <TableRow key={props.Availabilitykey}>
                <TableCell component="th" align='left'>
                    <Typography component='h6' variant="subtitle1">
                        <IconButton
                            aria-label="expand row"
                            size="small"
                            onClick={() => setOpen(!open)}
                        >
                            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                        </IconButton>
                        {props.Availabilitykey === "NULL" ? t("NotSpecifiec") : t(props.Availabilitykey)}
                    </Typography>
                </TableCell>
                {props.Availabilities.map((availability, availabilityIndex) => (
                    <TableCell key={availabilityIndex} align='center'>{availability ? <CheckCircleIcon sx={{ color: 'red' }} /> : ""}</TableCell>
                ))}
            </TableRow>
            {open && props.detailsTableRow}
        </>
    );
}
