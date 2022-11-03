import { Divider, MenuItem, Stack, TextField } from "@mui/material";
import { DatePicker } from '@mui/x-date-pickers';
import { useTranslation } from "react-i18next";
import { Controller } from 'react-hook-form';
import { PreDefinedDateTimeRanges } from "../dataModels/PreDefinedDateTimeRanges";

export interface SearchDateTimeRangeFieldProps {
    label: string,
    rangeFieldName: string,
    rangeDefaultValue: PreDefinedDateTimeRanges,
    register: any,
    control: any,
    handleRangeChanged: (event: any) => void,
}
export function SearchDateTimeRangeField(props: SearchDateTimeRangeFieldProps) {
    const { register, control, label, rangeFieldName, rangeDefaultValue, handleRangeChanged } = props;
    const { t } = useTranslation();
    const preDefinedDateTimeRangesValues = Object.values(PreDefinedDateTimeRanges);

    const rangeLowerFieldName = rangeFieldName + 'Lower';
    const rangeUpperFieldName = rangeFieldName + 'Upper';

    return (
        <Stack direction="row" spacing={0} divider={<Divider orientation="vertical" flexItem />}>
            <TextField
                label={label}
                id={rangeFieldName + "Select"}
                select
                sx={{ minWidth: 150 }}
                name={rangeFieldName}
                {...register(rangeFieldName)}
                autoComplete={rangeFieldName}
                variant="filled"
                size="small"
                onChange={handleRangeChanged}
                defaultValue={rangeDefaultValue}
            >
                {preDefinedDateTimeRangesValues.map((v, index) => {
                    return (<MenuItem key={v} value={v}>{t(v)}</MenuItem>)
                })}
            </TextField>
            <Controller
                name={rangeLowerFieldName}
                control={control}
                {...register(rangeLowerFieldName)}
                ref={null}
                render={
                    ({ field: { onChange, ...restField } }) =>
                        <DatePicker
                            label={t('From')}
                            autoFocus
                            onChange={(event) => { onChange(event); }}
                            renderInput={(params) =>
                                <TextField
                                    variant="filled"
                                    size="small"
                                    fullWidth
                                    autoComplete={rangeLowerFieldName}
                                    {...params}
                                />}
                            {...restField}
                        />
                }
            />
            <Controller
                name={rangeUpperFieldName}
                control={control}
                {...register(rangeUpperFieldName)}
                ref={null}
                render={
                    ({ field: { onChange, ...restField } }) =>
                        <DatePicker
                            label={t('To')}
                            autoFocus
                            onChange={(event) => { onChange(event); }}
                            renderInput={(params) =>
                                <TextField
                                    variant="filled"
                                    size="small"
                                    fullWidth
                                    autoComplete={rangeUpperFieldName}
                                    {...params}
                                />}
                            {...restField}
                        />
                }
            />
        </Stack>
    );
}