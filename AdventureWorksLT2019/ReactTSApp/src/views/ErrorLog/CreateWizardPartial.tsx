import * as React from 'react';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useDispatch, } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, Chip, Paper, Step, StepContent, StepLabel, Stepper, Typography } from '@mui/material';

import { Map } from '@mui/icons-material';

import { useTranslation } from 'react-i18next';

import { AppDispatch } from 'src/store/Store';
import { WizardPartialProps } from 'src/shared/viewModels/WizardPartialProps';
import { WizardStepOptions, WizardStepProps } from 'src/shared/viewModels/WizardStepProps';
import { getCRUDItemPartialViewPropsWizard } from 'src/shared/viewModels/ItemPartialViewProps';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';

import { IErrorLogCompositeModel } from 'src/dataModels/IErrorLogCompositeModel';
import { createComposite } from 'src/slices/ErrorLogSlice';

import { IErrorLogDataModel } from "src/dataModels/IErrorLogDataModel";

import { default as ErrorLogCreatePartial } from '../ErrorLog/CreatePartial';

const wizardSteps = [
    {
        id: 'Welcome',
        wizardStepOptions: WizardStepOptions.Welcome,
        label: 'Welcome',
        description: 'Welcome',
        icon: <Map fontSize='small' />,
        optional: false,
    },
    {
        id: '__Master__',
        wizardStepOptions: WizardStepOptions.Editor,
        label: 'ErrorLog',
        description: 'ErrorLog',
        icon: <Map fontSize='small' />,
        optional: true,
    },
    {
        id: 'ReviewAndSubmit',
        wizardStepOptions: WizardStepOptions.ReviewAndSubmit,
        label: 'ReviewAndSubmit',
        description: 'ReviewAndSubmit',
        icon: <Map fontSize='small' />,
        optional: false,
    },
    {
        id: 'Done',
        wizardStepOptions: WizardStepOptions.Done,
        label: 'Done',
        description: 'Done',
        icon: <Map fontSize='small' />,
        optional: false,
    },
];

export default function CreateWizardPartial(props: WizardPartialProps<IErrorLogCompositeModel>): JSX.Element {
    const { orientation, data } = props;
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();
    const navigate = useNavigate();
    
    // #region 1. Wizard related
    const [activeStep, setActiveStep] = useState(0);
    const [skipped, setSkipped] = useState(new Set<number>());
    const isStepOptional = (step: number) => {
        return wizardSteps[step].optional;
    };

    const isStepSkipped = (step: number) => {
        return skipped.has(step);
    };

    const handleNext = () => {
        if (wizardSteps[activeStep].wizardStepOptions === WizardStepOptions.ReviewAndSubmit) {
            if (!created) {
                onSubmit();
                return;
            }
        }
        if (wizardSteps[activeStep].wizardStepOptions === WizardStepOptions.Done || activeStep === wizardSteps.length - 1) {
            navigate("/");
        }
        setActiveStep((prevActiveStep) => prevActiveStep + 1);
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };

    const handleReset = () => {
        setActiveStep(0);
        setCreating(false);
        setCreated(false);
        setCreateMessage(null);
        skipped.clear();
        setSkipped(skipped);

		set__Master__(props.data.__Master__);

    };
    const handleSkip = () => {
        if (!isStepOptional(activeStep)) {
            // You probably want to guard against something like this,
            // it should never occur unless someone's actively trying to break something.
            throw new Error("You can't skip a step that isn't optional.");
        }
        setActiveStep((prevActiveStep) => prevActiveStep + 1);
        setSkipped((prevSkipped) => {
            const newSkipped = new Set(prevSkipped.values());
            newSkipped.add(activeStep);
            return newSkipped;
        });
    }

    const isFirstStep = (index: number): boolean => index === 0;
    const isLastStep = (index: number): boolean => index === wizardSteps.length - 1;

    const [creating, setCreating] = useState(false);
    const [created, setCreated] = useState(false);

    const [createMessage, setCreateMessage] = useState<string>();

    const onSubmit = () => {
        const toSubmitData = {
            __Master__,

        } as IErrorLogCompositeModel;

        dispatch(createComposite(toSubmitData))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setCreateMessage(t('SuccessfullySaved'));
                    setCreated(true);
                }
                else { // failed
                    setCreateMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error) => { setCreateMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setCreating(false); console.log('finally'); });
    }

    // #endregion 1. Wizard related

    // #region 2. Composite Data for Wizard Steps
    const [__Master__, set__Master__] = React.useState(props.data.__Master__);

    // #endregion 2. Composite Data Wizard Steps

    // #region 3. Common steps 

    const renderWelcomeStep = (step: WizardStepProps, index: number) => {
        return (
            <Card sx={{height: '100%', display: "flex", flexDirection: "column",}}>
                <CardContent>
                    <Typography>Welcome</Typography>
                </CardContent>
                <CardActions disableSpacing sx={{ mt: "auto" }}>{renderWizardButtonGroupHere(step, index)}</CardActions>
            </Card>
        )
    }

    const renderReviewStep = (step: WizardStepProps, index: number) => {
        return (
            <Card sx={{height: '100%', display: "flex", flexDirection: "column",}}>
                {!created && <CardContent>
                    <Typography>Review and Submit</Typography>
                </CardContent>}
                {created && !!!createMessage && <CardContent>
                    <Typography>{createMessage}</Typography>
                </CardContent>}
                <CardActions disableSpacing sx={{ mt: "auto" }}>{renderWizardButtonGroupHere(step, index)}</CardActions>
            </Card>
        )
    }

    const renderDoneStep = (step: WizardStepProps, index: number) => {
        return (
            <Card sx={{height: '100%', display: "flex", flexDirection: "column",}}>
                <CardContent>
                    <Typography>Done</Typography>
                </CardContent>
                <CardActions disableSpacing sx={{ mt: "auto" }}>{renderWizardButtonGroupHere(step, index)}</CardActions>
            </Card>
        )
    }

    // #endregion 3. Common steps

    // 4. renderWizardStep, used by Horizontal and Vertical
    const renderWizardStep = (step: WizardStepProps, index: number, renderWizardButtonGroup: (isFirstStep: boolean, isLastStep: boolean, isStepOptional: boolean, disableNextButton: () => boolean, submitRef: React.MutableRefObject<any>) => JSX.Element,) => {
        return (
            <Box sx={{height: '70vh'}}>
                {step.id === "Welcome" && renderWelcomeStep(step, index)}
                {step.id === "__Master__" && (render__Master___Single(step, index, renderWizardButtonGroup))}
                {step.id === "ReviewAndSubmit" && renderReviewStep(step, index)}
                {step.id === "Done" && renderDoneStep(step, index)}
            </Box>
        )
    }



    const render__Master___Single = (step: WizardStepProps, index: number, renderWizardButtonGroup: (isFirstStep: boolean, isLastStep: boolean, isStepOptional: boolean, disableNextButton: () => boolean, submitRef: React.MutableRefObject<any>) => JSX.Element) => {
        return (
            <ErrorLogCreatePartial
                {...getCRUDItemPartialViewPropsWizard<IErrorLogDataModel>(
                    ViewItemTemplates.Create, orientation, (data: IErrorLogDataModel) => { set__Master__(data); })} item={__Master__}
                isFirstStep={isFirstStep(index)}
                isLastStep={isLastStep(index)}
                isStepOptional={step.optional}
                renderWizardButtonGroup={renderWizardButtonGroup} />
        )
    }

    const renderWizardButtonGroupHere = (step: WizardStepProps, index: number) => {
        return (
            <>
                {(orientation as string) === 'horizontal'
                    ? renderHorizontalWizardButtonGroup(isFirstStep(index), isLastStep(index), step.optional, () => false, null) // the last parameter submitRef is set to null here, you may need it 
                    : renderVerticalWizardButtonGroup(isFirstStep(index), isLastStep(index), step.optional, () => false, null) // the last parameter submitRef is set to null here, you may need it 
                }
            </>)
    }

    // #region 4.1. Render Horizontal Wizard
    const renderHorizontalWizardButtonGroup = (isFirstStep: boolean, isLastStep: boolean, isStepOptional: boolean, disableNextButton: () => boolean, submitRef: React.MutableRefObject<any>) => {
        return (
            <>
                <Button
                    color="inherit"
                    disabled={isFirstStep || created || creating}
                    onClick={() => { handleBack();  if(!!submitRef && !!submitRef.current) {submitRef.current?.click()} }}
                    sx={{ mr: 1 }}
                >
                    {t('Back')}
                </Button>
                <Box sx={{ flex: '1 1 auto' }} />
                {isStepOptional && !isLastStep && (
                    <Button color="inherit" onClick={handleSkip} sx={{ mr: 1 }}>
                        {t('Skip')}
                    </Button>
                )}
                {activeStep === wizardSteps.length - 1 && (
                    <Button onClick={() => { handleReset(); }}>
                        {t('Reset')}
                    </Button>
                )}
                <Button onClick={() => { handleNext(); if(!!submitRef && !!submitRef.current) {submitRef.current?.click()} }} disabled={disableNextButton() || creating}>
                    {wizardSteps[activeStep].wizardStepOptions === WizardStepOptions.ReviewAndSubmit
                        ? t('Comfirm')
                        : isLastStep ? t('Finish') : t('Next')}
                </Button>
            </>
        );
    }
	
    const renderHorizontalWizard = () => {
        return (
            <Box sx={{ width: '100%' }}>
                <Stepper activeStep={activeStep} sx={{height: 100}} alternativeLabel>
                    {wizardSteps.map((step, index) => {
                        const stepProps: { completed?: boolean } = {};
                        const labelProps: {
                            optional?: React.ReactNode;
                        } = {};
                        if (isStepOptional(index)) {
                            labelProps.optional = (
                                <Typography variant="caption">{t('Optional')}</Typography>
                            );
                        }
                        if (isStepSkipped(index)) {
                            stepProps.completed = false;
                        }
                        return (
                            <Step key={step.label} {...stepProps}>
                                <StepLabel {...labelProps}>
                                    <Typography variant={index === activeStep ? "h6" : "subtitle1"}>
                                        {t(step.label)}
                                    </Typography>
                                </StepLabel>
                            </Step>
                        );
                    })}
                </Stepper>
                {renderWizardStep(wizardSteps[activeStep], activeStep, renderHorizontalWizardButtonGroup)}
            </Box>
        );
    }

    // #endregion 4.1. Render Horizontal Wizard

    // #region 4.2. Render Vertical Wizard
    const renderVerticalWizardButtonGroup = (isFirstStep: boolean, isLastStep: boolean, isStepOptional: boolean, disableNextButton: () => boolean, submitRef: React.MutableRefObject<any>) => {
        return (
            <ButtonGroup sx={{ marginLeft: 'auto', }}
                disableElevation
                variant="contained"
                aria-label="navigation buttons"
            >
                <Button
                    variant="contained"
                    disabled={disableNextButton() || creating}
                    onClick={() => { handleNext(); if(!!submitRef && !!submitRef.current) {submitRef.current?.click()} }} 
                    sx={{ mt: 1, mr: 1 }}
                >
                    {wizardSteps[activeStep].wizardStepOptions === WizardStepOptions.ReviewAndSubmit
                        ? t('Comfirm')
                        : isLastStep ? t('Finish') : t('Next')}
                </Button>
                {activeStep === wizardSteps.length - 1 && (
                    <Button onClick={() => { handleReset(); }}>
                        {t('Reset')}
                    </Button>
                )}
                <Button
                    disabled={isFirstStep || created || creating}
                    onClick={() => { handleBack(); if(!!submitRef && !!submitRef.current) {submitRef.current?.click()} }}
                    sx={{ mt: 1, mr: 1 }}
                >
                    {t('Back')}
                </Button>
            </ButtonGroup>
        );
    }

    const renderVerticalWizard = () => {
        return (
            <Box>
                {/* <Box sx={{ p: 1, my: 1, border: '1px solid' }} ref={buttonsContainer_Entity} /> */}
                <Stepper activeStep={activeStep} orientation='vertical'>
                    {wizardSteps.map((step, index) => (
                        <Step key={step.label + index}>
                            <StepLabel>
                                <Chip avatar={<Avatar>{step.icon}</Avatar>} label={t(step.label)} />
                            </StepLabel>
                            <StepContent>
                                {renderWizardStep(step, index, renderVerticalWizardButtonGroup)}
                            </StepContent>
                        </Step>
                    ))}
                </Stepper>
            </Box>
        );
    }

    // #endregion 4.2. Render Vertical Wizard

    return (
        <>
            {orientation === 'vertical' ? renderVerticalWizard() : renderHorizontalWizard()}
        </>
    );
}

