import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { defaultIProductModelProductDescriptionCompositeModel } from 'src/dataModels/IProductModelProductDescriptionCompositeModel';
import CreateWizardPartial from './CreateWizardPartial';

export default function CreateWizardPage(): JSX.Element {
    const { t } = useTranslation();
    // if you want to change page title <html><head><title>...</title></head></html>
    useEffect(() => {
        document.title = t("CreateProductModelProductDescriptionWizard") + ":" + t("_APPLICATION_TITLE_");
    }, []);

    return (
        <CreateWizardPartial data={defaultIProductModelProductDescriptionCompositeModel()} orientation="horizontal" />
    );
}

