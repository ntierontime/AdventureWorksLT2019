import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';

import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import { setLoading } from 'src/shared/slices/appSlice';
import { productModelApi } from 'src/apiClients/ProductModelApi';
import { IProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';
import { IProductModelCompareModel } from 'src/dataModels/IProductModelCompareModel';
import CompareHtmlTablePartial from 'src/views/ProductModel/CompareHtmlTablePartial';
import { localStorageKeys } from 'src/dataModels/localStorageKeys';
import { getLocalStorage, setLocalStorage } from 'src/shared/utility';

export default function CompareHtmlTablePage(): JSX.Element {
    const dispatch = useDispatch();
    const { t } = useTranslation();
    const navigate = useNavigate();
    const [compareModel, setCompareModel] = useState<IProductModelCompareModel>();

    useEffect(() => {
        dispatch(setLoading(true));
        const localStorageData = getLocalStorage<IProductModelCompareModel>(localStorageKeys.ProductModelCompareData);
        if(!!localStorageData) {
            setCompareModel(localStorageData);
            dispatch(setLoading(false));
            return;
        }
        // // if you want to change page title <html><head><title>...</title></head></html>
        // document.title = 
        productModelApi.Compare({ pageIndex: 1, pageSize: 4 } as unknown as IProductModelAdvancedQuery)
            .then((res) => {
                // console.log(res);
                setCompareModel(res);
                setLocalStorage(localStorageKeys.ProductModelCompareData, res);
            })
            .finally(() => {dispatch(setLoading(false)); });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <>
            <CompareHtmlTablePartial data={compareModel}/>
        </>
    );
}
