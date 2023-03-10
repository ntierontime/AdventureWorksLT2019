import { Box, TableCell, TableHead, TableRow, TableSortLabel, Typography } from "@mui/material";
import { visuallyHidden } from '@mui/utils';
import { EnhancedTableProps } from "./TableFeatures";

// Table head with Client side Column Sort feature.
export function EnhancedTableHead(props: EnhancedTableProps) {
    const { hasItemsSelect, order, orderBy, onRequestSort, headCells, onMouseOver } = props;
    const createSortHandler =
        (property: string | number) => (event: React.MouseEvent<unknown>) => {
            if(onRequestSort)
                onRequestSort(event, property);
        };

    return (
        <TableHead>
            <TableRow onMouseOver={onMouseOver}>
                {hasItemsSelect && <TableCell padding="checkbox">
                </TableCell>}
                {headCells.map((headCell) => (
                    <TableCell
                        key={headCell.id}
                        align='center'
                        padding={headCell.disablePadding ? 'none' : 'normal'}
                        sortDirection={orderBy === headCell.id ? order : false}
                    >
                        {!!onRequestSort 
                            ? <TableSortLabel
                                active={orderBy === headCell.id}
                                direction={orderBy === headCell.id ? order : 'asc'}
                                onClick={createSortHandler(headCell.id)}
                            >
                                <Typography sx={{ textTransform: 'capitalize', fontWeight: 'bold', m: 1 }}>{headCell.label}</Typography>
                                {orderBy === headCell.id ? (
                                    <Box component="span" sx={visuallyHidden}>
                                        {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                    </Box>
                                ) : null}
                            </TableSortLabel>
                            :<Typography sx={{ textTransform: 'capitalize', fontWeight: 'bold', m: 1 }}>{headCell.label}</Typography>}
                    </TableCell>
                ))}
                <TableCell padding="checkbox">
                </TableCell>
            </TableRow>
        </TableHead>
    );
}