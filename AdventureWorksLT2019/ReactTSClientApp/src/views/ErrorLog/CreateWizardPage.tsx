import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { defaultIErrorLogCompositeModel } from 'src/dataModels/IErrorLogCompositeModel';
import CreateWizardPartial from './CreateWizardPartial';

export default function CreateWizardPage(): JSX.Element {
    const { t } = useTranslation();
    // if you want to change page title <html><head><title>...</title></head></html>
    useEffect(() => {
        document.title = t("CreateErrorLogWizard") + ":" + t("_APPLICATION_TITLE_");
    }, []);

    return (
        <CreateWizardPartial data={defaultIErrorLogCompositeModel()} orientation="horizontal" />
    );
}

