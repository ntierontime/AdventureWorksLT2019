import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { defaultIAddressCompositeModel } from 'src/dataModels/IAddressCompositeModel';
import CreateWizardPartial from './CreateWizardPartial';

export default function CreateWizardPage(): JSX.Element {
    const { t } = useTranslation();
    // if you want to change page title <html><head><title>...</title></head></html>
    useEffect(() => {
        document.title = t("CreateAddressWizard") + ":" + t("_APPLICATION_TITLE_");
    }, []);

    return (
        <CreateWizardPartial data={defaultIAddressCompositeModel()} orientation="horizontal" />
    );
}

