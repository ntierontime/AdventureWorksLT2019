import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { defaultICustomerCompositeModel } from 'src/dataModels/ICustomerCompositeModel';
import CreateWizardPartial from './CreateWizardPartial';

export default function CreateWizardPage(): JSX.Element {
    const { t } = useTranslation();
    // if you want to change page title <html><head><title>...</title></head></html>
    useEffect(() => {
        document.title = t("CreateCustomerWizard") + ":" + t("_APPLICATION_TITLE_");
    }, []);

    return (
        <CreateWizardPartial data={defaultICustomerCompositeModel()} orientation="horizontal" />
    );
}

