import React, { useEffect, useState } from 'react';
import { IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';

import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import CompareAvailabilityTableRow from 'src/shared/views/CompareAvailabilityTableRow';

import { IProductModelCompareModel } from 'src/dataModels/IProductModelCompareModel';
import { ConvertObjectToList } from 'src/shared/utility';
import { getCRUDItemPartialViewPropsInline } from 'src/shared/viewModels/ItemPartialViewProps';
import { IProductDataModel } from 'src/dataModels/IProductDataModel';

import { default as ProductItemViewsPartial } from 'src/views/Product/ItemViewsPartial';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from 'src/store/CombinedReducers';
import { CompareHtmlTableProps } from 'src/shared/viewModels/CompareHtmlTableProps';

const __Master__Categories = ["name", "catalogDescription", "modifiedDate"];

const crudItemPartialViewPropsInline = getCRUDItemPartialViewPropsInline<IProductDataModel>(
    ViewItemTemplates.Details,
    null
);

export default function CompareHtmlTablePartial(props: CompareHtmlTableProps<IProductModelCompareModel>): JSX.Element {
    const { data } = props;
    const app = useSelector((state: RootState) => state.app);
    const { t } = useTranslation();
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const [headCells, setHeadCells] = useState<any[]>([]);
    const [__Master__, set__Master__] = useState<any[][]>([]);
    useEffect(() => {
        if (!!data) {
            const newHeadCells = data.productModelCompositeModelList.map((item, index) => {
                return {
                    id: item.__Master__.productModelID.toString(),
                    label: item.__Master__.name,
                    width: (100 / data.productModelCompositeModelList.length).toString() + "%",
                };
            });

            setHeadCells(newHeadCells);

            // 1. __Master__
            const comparedData___Master__ = __Master__Categories.map((fieldName, index) => {
                return [fieldName, ...data.productModelCompositeModelList.map((item, itemIndex) => { return item.__Master__[fieldName] })];
            });
            set__Master__(comparedData___Master__);

            // 4.1. List Products_Via_ProductModelID
        }
    }, [data]);

    return (
        <TableContainer component={Paper}>
            <Table
                sx={{ minWidth: 750 }}
                aria-labelledby="tableTitle"
                size='small'
                stickyHeader
            >
                <TableHead>
                    <TableRow sx={{
                        "& th": {
                            color: "rgba(96, 96, 96)",
                            backgroundColor: "lightblue"
                        }
                    }}>
                        <TableCell></TableCell>
                        {!!headCells && headCells.map((cell, index) => (
                            <TableCell key={cell.id} align="center" sx={{ width: cell.width }}>
                                <Typography sx={{ textTransform: 'capitalize', fontWeight: 'bold', m: 1 }}>{cell.label}</Typography>
                            </TableCell>
                        ))}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {!!__Master__ && __Master__.map((row, index) => {
                        return (
                            <React.Fragment key={index}>
                                {row !== null && <TableRow hover>
                                    {!!row && row.map((column, columnIndex) => {
                                        return (
                                            <React.Fragment key={columnIndex}>
                                                {columnIndex === 0
                                                    ? <TableCell align='left'><Typography component='h6' variant="subtitle1">{column}</Typography></TableCell>
                                                    : <TableCell align='left'>{column}</TableCell>}
                                            </React.Fragment>
                                        )
                                    })}
                                </TableRow>}
                            </React.Fragment>
                        )
                    })}
                    {Products_Via_ProductModelIDTableRows(data, headCells.length + 1)}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

function Products_Via_ProductModelIDTableRows(data: IProductModelCompareModel, headCellsLength: number): JSX.Element {
    const { t } = useTranslation();
    const [open, setOpen] = React.useState(true);

    return (
        <>
            <TableRow sx={{ backgroundColor: 'lightgray' }}>
                <TableCell colSpan={headCellsLength + 1} align='center'>
                    <Typography component='h6' variant="subtitle1">{t("Product")}
                        <IconButton
                            aria-label="expand row"
                            size="small"
                            onClick={() => setOpen(!open)}
                        >
                            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                        </IconButton>
                    </Typography>
                </TableCell>
            </TableRow>
            {open && !!data && !!data.compareResult_Products_Via_ProductModelID &&
                ConvertObjectToList(data.compareResult_Products_Via_ProductModelID).map((availabilityResult, index) => {
                    return (<CompareAvailabilityTableRow key={availabilityResult.key}
                        Availabilitykey={availabilityResult.key} Availabilities={availabilityResult.value}
                        detailsTableRow={Products_Via_ProductModelIDTableRow(availabilityResult.key, data)} />)

                })}
        </>
    );
}

function Products_Via_ProductModelIDTableRow(availabilityKey: string, data: IProductModelCompareModel): JSX.Element {
    return <TableRow>
        <TableCell></TableCell>
        {!!data && data.productModelCompositeModelList.map((row, index) => {
            const item = row.products_Via_ProductModelID.find(t => t.size === availabilityKey || !!!t.size && availabilityKey === "NULL");
            return (
                <TableCell key={index} align="center">
                    <ProductItemViewsPartial {...crudItemPartialViewPropsInline} item={item} />
                </TableCell>
            );
        })}
    </TableRow>;
}
