import { useState } from 'react';
import { useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { IndexPageProps } from 'src/shared/views/IndexPageProps';

import { RootState } from 'src/store/CombinedReducers';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';
import { xs1sm1md2lg2xl2GridItem } from "src/shared/views/ResponsiveGridItem";
import { scrollableCardContent } from "src/shared/views/ItemCardProps";

import { IProductCategoryDataModel } from 'src/dataModels/IProductCategoryDataModel';
import { productCategorySelectors } from 'src/generated/slices/ProductCategorySlice';
import { defaultIProductCategoryAdvancedQuery, IProductCategoryAdvancedQuery, } from 'src/dataModels/IProductCategoryQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage(props: IndexPageProps) {
    const { t } = useTranslation();
    const [advancedQuery, setAdvancedQuery] = useState<IProductCategoryAdvancedQuery>(defaultIProductCategoryAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => productCategorySelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        currentListViewOption: props.currentListViewOption ?? ListViewOptions.Table,
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIProductCategoryAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        hasListToolBar : props.hasListToolBar ?? true,
        hasAdvancedSearch : props.hasAdvancedSearch ?? true,
        addNewButtonContainer: ContainerOptions.Absolute, // at bottom-right using SnackBar
        itemPopupGridColumns: xs1sm1md2lg2xl2GridItem,
        itemPopupScrollableCardContent: scrollableCardContent,
        listToolBarSetting: {
            textSearchPlaceHolder: t("Search") + ' ' + t("ProductCategory"),
            hasListViewOptionsSelect: false,
	        availableListViewOptions: [ListViewOptions.Table],
            hasItemsSelect: false,
            hasBulkDelete: false,
            hasBulkUpdate: false,
            hasItemsPerRowSelect: true, // When "Tiles"
            hasPageSizeSelect: true,    // When "Table"
            hasOrderBySelect: true,
            hasSearch: true,			// Text Search
            hasAdvancedSearchAccordion: false,
            hasAdvancedSearchDialog: true,
        }
    } as ListsPartialViewProps<IProductCategoryAdvancedQuery, IProductCategoryDataModel>;
	
    // // if you want to change page title <html><head><title>...</title></head></html>
    // useEffect(() => {
    //     document.title = t("ProductCategory") + ":" + t("_APPLICATION_TITLE_");
    // }, []);

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

