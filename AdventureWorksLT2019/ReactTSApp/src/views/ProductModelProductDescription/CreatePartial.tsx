import { useEffect, useRef, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, MenuItem, TextField, Typography } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { yupResolver } from '@hookform/resolvers/yup';

import { Controller } from 'react-hook-form';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';
import { defaultIProductDescriptionAdvancedQuery } from 'src/dataModels/IProductDescriptionQueries';



// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';

import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { IProductModelProductDescriptionDataModel, productModelProductDescriptionFormValidationWhenCreate } from 'src/dataModels/IProductModelProductDescriptionDataModel';
import { post } from 'src/slices/ProductModelProductDescriptionSlice';

export default function CreatePartial(props: ItemPartialViewProps<IProductModelProductDescriptionDataModel>): JSX.Element {
    const { t } = useTranslation();
    // #region 1.start redux-hook-form related
    const { item } = props;
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer } = props;
    // 'control' is only used by boolean fields, you can remove it if this form doesn't have it
    // 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const methods = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
        resolver: yupResolver(productModelProductDescriptionFormValidationWhenCreate)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, formState: { isValid, errors, isDirty } } = methods;
    // #endregion 1. redux-hook-form related

    // #region 2. CodeLists if any
	

    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productModel_Name, value: item.productModelID, selected: false }]);

    const [productDescription_ProductDescriptionIDCodeList, setProductDescription_ProductDescriptionIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productDescription_Name, value: item.productDescriptionID, selected: false }]);
    const [modifiedDate, setModifiedDate] = useState<string>();






    // #endregion 2. CodeLists if any

    // #region 3. crudViewContainer !== CrudViewContainers.Wizard
    const { doneAction } = props; // dialog
    const dispatch = useDispatch<AppDispatch>();

    const [creating, setCreating] = useState(false);
    const [created, setCreated] = useState(false);

    const [createMessage, setCreateMessage] = useState<string>();
    const [createAnother, setCreateAnother] = useState(true);
    const handleChangeCreateAnother = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCreateAnother(event.target.checked);
    };

    const onSubmit = (data: IProductModelProductDescriptionDataModel) => {
        if(crudViewContainer === CrudViewContainers.Wizard) {
            onWizardStepSubmit(data);
            return;
        }

        setCreating(true);
        dispatch(post({ ...data }))
            .then((result: any) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    if (createAnother) {
                        setCreating(false);
                        setCreated(false);
                        setCreateMessage(null);
                        reset(item);
                    }
                    else {
                        setCreateMessage(t('SuccessfullySaved'));
                        setCreated(true);
                    }
                }
                else { // failed
                    setCreateMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error: any) => { setCreateMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setCreating(false); console.log('finally'); });
    }


    const renderButtonGroup_IconButtons = () => {
        return (
            <>
                <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t("CreateAnotherOne")} />
                <IconButton aria-label="create" color="primary" type='submit' disabled={!isValid || creating || created}>
                    <SaveIcon />
                </IconButton>
                <IconButton aria-label="close" disabled={creating || created}>
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    const renderButtonGroup_TextAndIconButtons = () => {
        return (
            <>
                <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t("CreateAnotherOne")} />
                <ButtonGroup sx={{ marginLeft: 'auto', }}
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    <Button
                        type='submit'
                        fullWidth
                        variant='contained'
                        disabled={!isValid || creating || created}
                        startIcon={<SaveIcon />}>
                        {t('Create')}
                    </Button>
                    <Button
                        autoFocus
                        disabled={creating || created}
                        fullWidth
                        variant='contained'
                        startIcon={<CloseIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>
                </ButtonGroup>
            </>
        );
    }
	// #endregion 3. crudViewContainer !== CrudViewContainers.Wizard
	
    // #region 4. crudViewContainer === CrudViewContainers.Wizard
    const { wizardOrientation, onWizardStepSubmit, renderWizardButtonGroup, isFirstStep, isLastStep, isStepOptional } = props;
	const submitRef = useRef(); // used for external trigger submit event.
    // #endregion 4. crudViewContainer === CrudViewContainers.Wizard

    useEffect(() => {


        codeListsApi.getProductModelCodeList({ ...defaultIProductModelAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductModel_ProductModelIDCodeList(res.responseBody);
                setValue('productModelID', res.responseBody[0].value);
				
            }
        });

        codeListsApi.getProductDescriptionCodeList({ ...defaultIProductDescriptionAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductDescription_ProductDescriptionIDCodeList(res.responseBody);
                setValue('productDescriptionID', res.responseBody[0].value);
				
            }
        });
        setCreating(false);
        setCreated(false);
        setCreateMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [item]);
	
	useEffect(() => {
        // console.log("trigger validation");
        trigger();
    }, [trigger]);

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)} sx={{height: '100%', display: "flex", flexDirection: "column",}}>
            {crudViewContainer !== CrudViewContainers.Wizard && <CardHeader
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={t("Create_New")}
                subheader={t("ProductModelProductDescription")}
            />}
            {crudViewContainer !== CrudViewContainers.Wizard && !!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={1}>
                        <Grid item {...gridColumns}>
                            <TextField
                            	sx={{marginTop: 2}}
                                label={t("ProductModelID")}
                                id="productModelIDSelect"
                                select
                                name='productModelID'
                                {...register("productModelID")}
                                autoComplete='productModelID'
                                variant="outlined"
                                fullWidth
                                defaultValue={item.productModelID}
                            >
                                {productModel_ProductModelIDCodeList && productModel_ProductModelIDCodeList.map((v, index) => {
                                    return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                                })}
                            </TextField>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                            	sx={{marginTop: 2}}
                                label={t("ProductDescriptionID")}
                                id="productDescriptionIDSelect"
                                select
                                name='productDescriptionID'
                                {...register("productDescriptionID")}
                                autoComplete='productDescriptionID'
                                variant="outlined"
                                fullWidth
                                defaultValue={item.productDescriptionID}
                            >
                                {productDescription_ProductDescriptionIDCodeList && productDescription_ProductDescriptionIDCodeList.map((v, index) => {
                                    return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                                })}
                            </TextField>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='culture'
                                label={t('Culture')}
                                defaultValue={item.culture}
                                variant='outlined'
                                margin='normal'
                                {...register("culture")}
                                autoComplete='culture'
                                error={!!errors.culture}
                                fullWidth
                                helperText={!!errors.culture ? t(errors.culture.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Controller
                                name="modifiedDate"
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={modifiedDate}
                            				label={t('ModifiedDate')}
                            				onChange={(event: string) => { setModifiedDate(event); setValue('modifiedDate', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.modifiedDate}
                            						helperText={!!errors.modifiedDate ? t(errors.modifiedDate.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                    </Grid>
                </Box>
            </CardContent>
            {crudViewContainer != CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing sx={{ mt: "auto" }}>
                {renderButtonGroup_TextAndIconButtons()}
            </CardActions>}
            {crudViewContainer === CrudViewContainers.Wizard && <CardActions disableSpacing sx={{ mt: "auto" }}>
                <button ref={submitRef} type="submit" style={{ display: 'none' }} />{renderWizardButtonGroup(isFirstStep, isLastStep, isStepOptional, ()=>!isValid || creating || created, submitRef)}
            </CardActions>}
        </Card >
    );
}

