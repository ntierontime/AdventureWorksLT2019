import React, { useEffect, useMemo, useState } from 'react';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material';


import { useNavigate } from 'react-router-dom';
import { TFunction, useTranslation, UseTranslationResponse } from 'react-i18next';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import CompareAvailabilityTableRow from 'src/shared/views/CompareAvailabilityTableRow';

import { IProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';

import { productModelApi } from "src/apiClients/ProductModelApi";
import { IProductModelCompareModel } from 'src/dataModels/IProductModelCompareModel';
import { ConvertObjectToList } from 'src/shared/utility';
import { getCRUDItemPartialViewPropsInline } from 'src/shared/viewModels/ItemPartialViewProps';
import { IProductDataModel } from 'src/dataModels/IProductDataModel';

import { default as ProductItemViewsPartial } from 'src/views/Product/ItemViewsPartial';

const __Master__Categories = ["name", "catalogDescription", "modifiedDate"];

const crudItemPartialViewPropsInline = getCRUDItemPartialViewPropsInline<IProductDataModel>(
    ViewItemTemplates.Details,
    null
);

export default function CompareHtmlTablePartial(): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();

    const [compareModel, setCompareModel] = useState<IProductModelCompareModel>();
    const [headCells, setHeadCells] = useState<any[]>([]);
    const [__Master__, set__Master__] = useState<any[][]>([]);

    useEffect(() => {
        // // if you want to change page title <html><head><title>...</title></head></html>
        // document.title = 

        productModelApi.Compare({ pageIndex: 1, pageSize: 4 } as unknown as IProductModelAdvancedQuery)
            .then((res) => {
                console.log(res);
                setCompareModel(res);

                if (!!res) {
                    const newHeadCells = res.productModelCompositeModelList.map((item, index) => {
                        return {
                            id: item.__Master__.productModelID.toString(),
                            label: item.__Master__.name,
                            width: (100 / res.productModelCompositeModelList.length).toString() + "%",
                        };
                    });

                    setHeadCells(newHeadCells);

                    // 1. __Master__
                    const comparedData___Master__ = __Master__Categories.map((fieldName, index) => {
                        return [fieldName, ...res.productModelCompositeModelList.map((item, itemIndex) => { return item.__Master__[fieldName] })];
                    });
                    set__Master__(comparedData___Master__);

                    // 4.1. List Products_Via_ProductModelID
                }
            })
            .finally(() => { });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <>
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
                                <TableCell key={cell.id} align="center" sx={{width: cell.width}}>
                                    <Typography sx={{ textTransform: 'capitalize', fontWeight: 'bold', m: 1 }}>{cell.label}</Typography>
                                </TableCell>
                            ))}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {!!__Master__ && __Master__.map((row, index) => {
                                return (
                                    <React.Fragment  key={index}>
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
                        <TableRow sx={{backgroundColor: 'lightgray'}}>
                            <TableCell colSpan={headCells.length + 1} align='center'>
                                <Typography component='h6' variant="subtitle1">{t("Product")}</Typography>
                            </TableCell>
                        </TableRow>
                        {!!compareModel && !!compareModel.compareResult_Products_Via_ProductModelID &&
                            ConvertObjectToList(compareModel.compareResult_Products_Via_ProductModelID).map((availabilityResult, index) => {
                                return (<CompareAvailabilityTableRow  key={availabilityResult.key}
                                    Availabilitykey={availabilityResult.key} Availabilities={availabilityResult.value}
                                    detailsTableRow={renderProductTableRow(availabilityResult.key)} />)

                            })}
                        {/* {renderProductTableRow()} */}
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    );

    function renderProductTableRow(availabilityKey: string) {
        return <TableRow>
            <TableCell></TableCell>
            {!!compareModel && compareModel.productModelCompositeModelList.map((row, index) => {
                const item = row.products_Via_ProductModelID.find(t => t.size === availabilityKey || !!!t.size && availabilityKey==="NULL");
                return (
                    <TableCell key={index} align="center">
                        <ProductItemViewsPartial {...crudItemPartialViewPropsInline} item={item} />
                    </TableCell>
                );
            })}
        </TableRow>;
    }
}

