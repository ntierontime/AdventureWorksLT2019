import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { defaultIProductModelCompositeModel } from 'src/dataModels/IProductModelCompositeModel';
import CreateWizardPartial from './CreateWizardPartial';

export default function CreateWizardPage(): JSX.Element {
    const { t } = useTranslation();
    // if you want to change page title <html><head><title>...</title></head></html>
    useEffect(() => {
        document.title = t("CreateProductModelWizard") + ":" + t("_APPLICATION_TITLE_");
    }, []);

    return (
        <CreateWizardPartial data={defaultIProductModelCompositeModel()} orientation="horizontal" />
    );
}

