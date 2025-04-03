import { Box, TableCell, TableHead, TableRow, TableSortLabel, Typography } from "@mui/material";
import { visuallyHidden } from '@mui/utils';
import { RegularTableProps } from "./TableFeatures";

// Table head with Client side Column Sort feature.
export function RegularTableHead(props: RegularTableProps) {
    const { hasItemsSelect, headCells, onMouseOver } = props;

    return (
        <TableHead>
            <TableRow onMouseOver={onMouseOver}>
                {hasItemsSelect && <TableCell padding="checkbox">
                </TableCell>}
                {headCells.map((headCell) => (
                    <TableCell
                        key={headCell.id}
                        align='center' width={1/4}
                        padding={headCell.disablePadding ? 'none' : 'normal'}
                    >
                        <Typography sx={{ textTransform: 'capitalize', fontWeight: 'bold', m: 1 }}>{headCell.label}</Typography>
                    </TableCell>
                ))}
            </TableRow>
        </TableHead>
    );
}