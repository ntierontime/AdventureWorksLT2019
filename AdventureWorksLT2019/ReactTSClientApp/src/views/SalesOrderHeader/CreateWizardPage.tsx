import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { defaultISalesOrderHeaderCompositeModel } from 'src/dataModels/ISalesOrderHeaderCompositeModel';
import CreateWizardPartial from './CreateWizardPartial';

export default function CreateWizardPage(): JSX.Element {
    const { t } = useTranslation();
    // if you want to change page title <html><head><title>...</title></head></html>
    useEffect(() => {
        document.title = t("CreateSalesOrderHeaderWizard") + ":" + t("_APPLICATION_TITLE_");
    }, []);

    return (
        <CreateWizardPartial data={defaultISalesOrderHeaderCompositeModel()} orientation="horizontal" />
    );
}

