import { useEffect, useMemo, useState } from 'react';
import { useSelector } from 'react-redux';
import { Checkbox, FormControlLabel, IconButton, Link, Pagination, Popover, Stack, Switch, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from '@mui/material';

import AccountTreeIcon from '@mui/icons-material/AccountTree';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import MoreVertIcon from '@mui/icons-material/MoreVert';

import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { ListPartialViewProps } from 'src/shared/viewModels/ListPartialViewProps';
import { QueryOrderDirections } from 'src/shared/dataModels/QueryOrderDirections';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { EnhancedTableHead } from 'src/shared/views/EnhancedTableHead';
import { Item } from 'src/shared/views/Item';
import { getComparator, HeadCell, stableSort } from 'src/shared/views/TableFeatures';

import { RootState } from 'src/store/CombinedReducers';

import { IProductModelDataModel } from 'src/dataModels/IProductModelDataModel';
import { IProductModelIdentifier, getIProductModelIdentifier, getRouteParamsOfIProductModelIdentifier } from 'src/dataModels/IProductModelQueries';
import CompareHtmlTablePartial from './CompareHtmlTablePartial';

export default function CompareHtmlTablePage(): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();

    return (
        <>
            <CompareHtmlTablePartial />
        </>
    );
}
